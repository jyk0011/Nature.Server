/**
 * 自然框架之服务器通讯
 * http://www.natureFW.com/
 *
 * @author
 * 金洋（金色海洋jyk）
 * 
 * @copyright
 * Copyright (C) 2005-2013 金洋.
 *
 * Licensed under a GNU Lesser General Public License.
 * http://creativecommons.org/licenses/LGPL/2.1/
 *
 * 自然框架之服务器通讯 is free software. You are allowed to download, modify and distribute 
 * the source code in accordance with LGPL 2.1 license, however if you want to use 
 * 自然框架之服务器通讯 on your site or include it in your commercial software, you must  be registered.
 * http://www.natureFW.com/registered
 */

/* ***********************************************
 * author :  金洋（金色海洋jyk）
 * email  :  jyk0011@live.cn  
 * function: 服务器之间之间通讯的封装，基于WebClient的封装，实现get、post的方法。
 * history:  created by 金洋 
 *           
 * **********************************************
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Web;
using Nature.Attributes;
using Nature.Common;


namespace Nature.Service
{
    /// <summary>
    /// 对WebClient的封装
    /// 1、post的方式提交数据
    /// 2、接收信息，放在dictionary里面。
    /// 3、填充实体类
    /// 4、json转换成实体类
    /// 
    /// </summary>
    /// user:jyk
    /// time:2013/2/2 9:06
    public static class MyWebClient
    {
        #region 1、利用WebClient，实现post的方式，向指定的网址发送提交数据
        /// <summary>
        /// 利用WebClient，实现post的方式，向指定的网址发送提交数据
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="postVar">post提交的参数</param>
        /// <param name="errorMsg">如果发生异常，返回异常信息</param>
        /// <returns>url地址返回的信息</returns>
        /// user:jyk
        /// time:2013/2/2 9:32
        public static string Post(string url,Dictionary<string,string > postVar,out string errorMsg )
        {
            var webClientObj = new WebClient();

            // 以下为解决方案
            //webClientObj.Credentials = CredentialCache.DefaultCredentials; // 添加授权证书
           // webClientObj.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.35 (KHTML, like Gecko) Chrome/27.0.1448.0 Safari/537.35");
           // webClientObj.Headers.Add("Host", "517.cn");

            var postVars = new NameValueCollection();
            
            //添加值域
            if (postVar != null)
            {
                foreach (KeyValuePair<string, string> a in postVar)
                {
                    postVars.Add(a.Key, a.Value);
                }
            }

            try
            {
                byte[] byRemoteInfo = webClientObj.UploadValues(url, "POST", postVars);
                
                //获取返回值        
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo); //这是获取返回信息   
                Debug.WriteLine(sRemoteInfo);
                errorMsg = "";
                return sRemoteInfo;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null ;
            }
        }
        #endregion

        #region 1.2、利用WebClient，转发post申请，实现间接跨域post提交
        /// <summary>
        /// 利用WebClient，实现post的方式，向指定的网址发送提交数据
        /// </summary>
        /// <param name="url">要访问的URL，包括参数</param>
        /// <param name="errorMsg">如果发生异常，返回异常信息</param>
        /// <returns>url地址返回的信息</returns>
        /// user:jyk
        /// time:2013/2/2 9:32
        public static string PostAjax(string url, out string errorMsg)
        {
            var webClientObj = new WebClient();
            var postVars = new NameValueCollection();
             
            //传递 form值
            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                string tmpKey = HttpContext.Current.Request.Form.Keys[i];
                postVars.Add(tmpKey, HttpContext.Current.Request.Form[i]);
            }
            
            try
            {
                byte[] byRemoteInfo = webClientObj.UploadValues(url, "POST", postVars);

                //获取返回值        
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo); //这是获取返回信息   
                Debug.WriteLine(sRemoteInfo);
                errorMsg = "";
                return sRemoteInfo;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 2.1、接收QueryString信息，放在dictionary里面。
        /// <summary>
        /// 接收当前页面接收到的信息form和QueryString，存放在字典里Dictionary。
        /// key：name （表单里的控件的name属性值）
        /// value：控件的值，string类型
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2013/2/5 11:08
        public static Dictionary<string, string> RequestQueryString()
        {
            var requestInfo = new Dictionary<string, string>();

            int i = 0;

            string tmpKey = "";
            for (i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                tmpKey = HttpContext.Current.Request.QueryString.Keys[i];
                requestInfo.Add(tmpKey,HttpContext.Current.Request.QueryString[i]);
            }
             

            return requestInfo;

        }
        #endregion

        #region 2.2、接收form信息，放在dictionary里面。
        /// <summary>
        /// 接收当前页面接收到的信息form和QueryString，存放在字典里Dictionary。
        /// key：name （表单里的控件的name属性值）
        /// value：控件的值，string类型
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2013/2/5 11:08
        public static Dictionary<string, string> RequestForm()
        {
            var requestInfo = new Dictionary<string, string>();

            int i = 0;

            for (i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                string tmpKey = HttpContext.Current.Request.Form.Keys[i];
                requestInfo.Add(tmpKey, HttpContext.Current.Request.Form[i]);
            }

            return requestInfo;

        }
        #endregion

        #region 3、从表单（form）里获取信息，填充实体类
        /// <summary>
        /// 自动给实体类的属性赋值
        /// </summary>
        /// <typeparam name="T">实体类的类型</typeparam>
        /// <param name="t">实例</param>
        public static void FormToEntity<T>(T t)
        {
            //获取用户输入的数据
            Dictionary<string, string> requestInfo = RequestForm();

            //获取类里面的属性
            PropertyInfo[] properties = t.GetType().GetProperties();   // typeof(obj).GetProperties();

            #region 遍历属性
            foreach (PropertyInfo p in properties)
            {
                //获取属性里的ColumnIDAttribute的值
                var columnID = (ColumnIDAttribute)p.GetCustomAttributes(typeof(ColumnIDAttribute), false)[0];

                if (columnID.ColumnID == 0)
                    continue;

                string tmpKey = columnID.ColumnID.ToString(CultureInfo.InvariantCulture);
                if (requestInfo.ContainsKey(tmpKey))
                {
                    //tmpValue = dic_BaseCols[columnID.ColumnID].ColValue;
                    string tmpValue = requestInfo[tmpKey];
                    p.SetValue(t, Convert.ChangeType(tmpValue, p.PropertyType), null);  //赋值
                }
            }
            #endregion

        }
        #endregion

        #region 4、json转换成实体类
        /// <summary>
        /// 自动给实体类的属性赋值
        /// </summary>
        /// <typeparam name="T">实体类的类型</typeparam>
        /// <param name="t">实例</param>
        /// <param name="json">json格式的信息，转换成实体类</param>
        public static void JsonToEntity<T>(T t, string json)
        {
            //获取用户输入的数据
            Dictionary<string, string> requestInfo = Json.JsonToDictionary(json);

            //获取类里面的属性
            PropertyInfo[] properties = t.GetType().GetProperties();   // typeof(obj).GetProperties();

            #region 遍历属性
            foreach (PropertyInfo p in properties)
            {
                //获取属性里的ColumnIDAttribute的值
                var columnID = (ColumnIDAttribute)p.GetCustomAttributes(typeof(ColumnIDAttribute), false)[0];

                if (columnID.ColumnID == 0)
                    continue;

                string key = columnID.ColumnID.ToString(CultureInfo.InvariantCulture);

                if (requestInfo.ContainsKey(key))
                {
                    //tmpValue = dic_BaseCols[columnID.ColumnID].ColValue;
                    string tmpValue = requestInfo[key];
                    p.SetValue(t, Convert.ChangeType(tmpValue, p.PropertyType), null);  //赋值
                }
            }
            #endregion

        }
        #endregion
    
        #region 5、实体类转成dictionary里面。
        /// <summary>
        /// 接收当前页面接收到的信息form和QueryString，存放在字典里Dictionary。
        /// key：name （表单里的控件的name属性值）
        /// value：控件的值，string类型
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2013/2/5 11:08
        public static Dictionary<string, string> EntityToDictionary<T>(T t, string key)
        {
            var tmp = new Dictionary<string, string>();

            PropertyInfo[] properties = t.GetType().GetProperties();

            #region 遍历属性

            foreach (PropertyInfo p in properties)
            {
                //获取属性里的ColumnIDAttribute的值
                var columnID = (ColumnIDAttribute)p.GetCustomAttributes(typeof(ColumnIDAttribute), false)[0];

                if (columnID.ColumnID == 0)
                    continue;

                string tmpKey = key + columnID.ColumnID;
                if (!tmp.ContainsKey(tmpKey))
                {
                    string tmpValue = p.GetValue(t, null) == null ? "" : p.GetValue(t, null).ToString();
                    tmp.Add(tmpKey, tmpValue);
                    
                }
            }
            #endregion

            return tmp;

        }
        #endregion

    }
}
