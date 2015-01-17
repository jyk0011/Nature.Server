using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Nature.Data;

namespace Nature.Service.Ashx
{
    /// <summary>
    /// 验证url的参数
    /// </summary>
    public static class UrlPara
    {
        /// <summary>
        /// 表单用的控件类型
        /// </summary>
        private static readonly Dictionary<string, Func<BaseCheck>> CheckFun;

        static UrlPara ()
        {
            CheckFun = new Dictionary<string, Func<BaseCheck>>
                {
                    {"mdid", () => new CheckInt()},                  //模块ID
                    {"mpvid", () => new CheckInt()},                 //列表页面视图ID
                    {"fpvid", () => new CheckInt()},                 //查询页面视图ID
                    {"id", () => new CheckIntGuid()},                //记录ID
                    {"ids", () => new CheckInts()},                  //记录ID集合
                    {"bid", () => new CheckInt()},                   //按钮ID
                    {"did", () => new CheckInt()},                   //部门ID
                    {"frid", () => new CheckInt()},                  //外键
                    {"frids", () => new CheckInts()},                //外键集合
                    {"webappid", () => new CheckInt()},              //网站应用ID
                    {"userssoid", () => new CheckInt()},             //用户在sso端的id
                    {"userappid", () => new CheckInt()},             //用户在app端的id
                    {"action", () => new CheckString()}              //ajax请求的标志
                };
        }

        /// <summary>
        /// 遍历参数，逐一验证
        /// </summary>
        public static  void CheckUrlPara(BaseAshx baseAshx)
        {
            //遍历参数，逐一验证
            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                string key = HttpContext.Current.Request.QueryString.Keys[i].ToLower();
                string value = HttpContext.Current.Request.QueryString[i];

                if (CheckFun.ContainsKey(key))
                {
                    //按照说明做验证，取验证方式
                    BaseCheck baseCheck = CheckFun[key]();
                    //验证参数，获取通过验证并且处理过的参数值
                    value = baseCheck.Check(key, value); // <int>(key,value);

                    //验证后，赋值
                    SetValue(key, value, baseAshx);
                }
                else
                {
                    //没有说明该参数如何验证

                }
            }

        }

        private static void SetValue(string key, string value, BaseAshx baseAshx)
        {
            switch (key)
            {
                //模块ID
                case "mdid":        baseAshx.ModuleID           = int.Parse(value);break;
                //列表页面视图ID
                case "mpvid":       baseAshx.MasterPageViewID   = int.Parse(value);break;
                //记录ID
                case "fpvid":       baseAshx.FindPageViewID     = int.Parse(value);break;
                //记录ID
                case "id":          baseAshx.DataID             = value;break;
                //记录ID集合
                case "ids":         baseAshx.DataIDs            = value;break;
                //按钮ID
                case "bid":         baseAshx.ButtonID           = int.Parse(value);break;
                //部门ID
                case "did":         baseAshx.DepartmentID       = int.Parse(value);break;
                //外键
                case "frid":        baseAshx.ForeignID          = value;break;
                //外键集合
                case "frids":       baseAshx.ForeignIDs         = value;break;
                //网站应用ID
                case "webappid":    baseAshx.WebAppID           = value;break;
                //用户在sso端的id
                case "userssoid":   baseAshx.UserSsoID          = value;break;
                //用户在app端的id
                case "userappid":   baseAshx.UserSsoID          = value;break;
                //ajax请求的标志
                case "action":      baseAshx.Action             = value;break;
              
            }
        }
    }
}
