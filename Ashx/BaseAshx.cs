using System.Collections.Generic;
using Nature.Common;
using Nature.DebugWatch;

namespace Nature.Service.Ashx
{
    /// <summary>
    /// ashx的基类，设置、判断url参数
    /// </summary>
    /// user:jyk
    /// time:2012/10/18 17:37
    public class BaseAshx : HandlerBase
    {

        #region 定义属性，记录URL参数值

        /// <summary>
        /// 字符串常量，返回alert的js脚本
        /// </summary>
        /// user:jyk
        public string ScriptAlert = "<script type=\"text/javascript\" language=\"javascript\">alert('{0}');{1};</script>";

        /// time:2012/10/8 14:14
        /// <summary>
        /// ModuleID，功能节点ID
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// OpenPageViewID，主视图ID：列表视图ID，或者表单视图ID。
        /// </summary>
        public int MasterPageViewID { get; set; }

        /// <summary>
        /// OpenPageViewID，主视图ID：列表视图ID，或者表单视图ID。
        /// </summary>
        public int FindPageViewID { get; set; }

        /// <summary>
        /// 接收URL传递过来的记录ID，用于显示、修改、删除、查询数据。
        /// </summary>
        public string DataID { get; set; }
       
        /// <summary>
        /// 接收URL传递过来的记录ID集合，用于批量操作，比如批量删除、批量选择。
        /// 1,2,3的字符串形式
        /// </summary>
        public string DataIDs { get; set; }

        /// <summary>
        ///  按钮ID，用于是否可用按钮的验证
        /// </summary>
        public int ButtonID { get; set; }


        /// <summary>
        /// 部门ID。string.Empty 表示没有传递参数
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        ///  外键ID。string.Empty 表示没有传递参数。
        /// 改来改去，这个属性和DataID一致了，保留此属性是为了向下兼容
        /// </summary>
        public string ForeignID { get; set; } //= string.Empty;

        /// <summary>
        /// 外键ID集合。string.Empty 表示没有传递参数。
        /// 1,2,3的字符串形式。第一个是最开始的DataID，最后一个是最近的一个外键
        /// </summary>
        public string ForeignIDs { get; set; } //= string.Empty;

        /// <summary>
        /// 动作名称
        /// </summary>
        public string Action { get; set; } //= string.Empty;
       
        #endregion

        #region 单点、用户、权限用的属性

         
        /// <summary>
        /// 网站应用ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string WebAppID { get; set; }

        /// <summary>
        /// 请求访问的网站应用的IP
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string WebAppIP { get; set; }

        /// <summary>
        /// 用户在SSO里的ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserSsoID { get; set; }

        /// <summary>
        /// 用户在网站应用里的ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserAppID { get; set; }

        /// <summary>
        /// 请求访问的用户的IP
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserIP { get; set; }

       
        #endregion

        /// <summary>
        /// 判断url参数
        /// </summary>
        /// user:jyk
        /// time:2012/10/18 17:41
        public override void Process()
        {
            //base.Process();   父类里的Process是空的，不折腾了
         
            //定义一个子步骤
            var debugInfo = new NatureDebugInfo { Title = "[Nature.Service.Ashx.BaseAshx]判断Url参数" };

            //cookies可以跨iframe
            //Context.Response.AddHeader("P3P", "CP=CAO PSA OUR");
            Context.Response.AddHeader("P3P", "CP=CAO DSP COR CUR ADM DEV TAI PSA PSD IVAi IVDi CONi TELo OTPi OUR DELi SAMi OTRi UNRi PUBi INDPHY ONL UNI PUR FIN COM NAV INT DEM CNT STA POL HEA PRE GOV");

            #region 验证参数，设置IP
            UrlPara.CheckUrlPara(this);
              
            UserIP = Request.UserHostAddress;           //网站应用服务器的IP
            WebAppIP = UserIP;
              
            #endregion

            debugInfo.Stop();
            BaseDebug.DetailList.Add(debugInfo);

            //验证用户是否登录
            debugInfo = new NatureDebugInfo { Title = "[Nature.Service.Ashx.BaseAshx]验证用户是否登录，由子类实现" };

            CheckUser(debugInfo.DetailList);  

            debugInfo.Stop();
            base.BaseDebug.DetailList.Add(debugInfo);
                
        }

        //可重写函数

        #region 验证用户是否登录
        /// <summary>
        /// 坚持当前访问者是否登录。
        /// 两种情况：
        /// 1、登录页面：这个不能检查，所以做个钩子，登录页面重新函数搞定
        /// 2、其他页面：已登录页面，需要检查了，放在基类里，子类省事了。
        /// </summary>
        /// <param name="debugInfoList">子步骤的列表</param>
        protected virtual void CheckUser(IList<NatureDebugInfo> debugInfoList)
        {
            
        }
        #endregion
         
        #region 共用函数

        #region 没通过验证的返回信息
        /// <summary>
        /// 没通过验证的返回信息。根据callback参数，返回适合的json
        /// </summary>
        /// user:jyk
        /// time:2013/3/2 9:31
        public void ResponseWriteError(string err)
        {
            Response.Write(string.Format("\"msg\":\"{0}\"", err));
        }

        #endregion

        #endregion


    }
}
 
