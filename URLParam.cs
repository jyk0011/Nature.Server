using System.Globalization;
using System.Text;
using System.Web;
using Nature.Common;

namespace Nature.Service
{
    /// <summary>
    /// URL的常用参数
    /// </summary>
    /// user:jyk
    /// time:2013/2/6 15:01
    public class URLParam
    {
        #region 属性
        #region 网站应用ID
        /// <summary>
        /// 网站应用ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string WebAppID { get; set; }
        #endregion

        #region 请求访问的网站应用的IP
        /// <summary>
        /// 请求访问的网站应用的IP
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string WebAppIP { get; set; }
        #endregion

        #region 要访问的数据库ID
        /// <summary>
        /// 要访问的数据库ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string DataBaseID { get; set; }
        #endregion

        #region 用户在SSO里的ID
        /// <summary>
        /// 用户在SSO里的ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserSsoID { get; set; }
        #endregion

        #region 用户在网站应用里的ID
        /// <summary>
        /// 用户在网站应用里的ID
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserAppID { get; set; }
        #endregion

        #region 请求访问的用户的IP
        /// <summary>
        /// 请求访问的用户的IP
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string UserIP { get; set; }
        #endregion

        #region 请求的动作
        /// <summary>
        /// 请求的动作
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string Action { get; set; }
        #endregion

        #region 模块ID
        /// <summary>
        /// ModuleID，模块ID
        /// </summary>
        /// time:2012/10/8 14:14
        public string ModuleID { get; set; }
        #endregion

        #region 主视图ID
        /// <summary>
        /// OpenPageViewID，主视图ID：列表视图ID，或者表单视图ID
        /// </summary>
        public string MasterPageViewID { get; set; }
        #endregion

        #region 查询视图ID
        /// <summary>
        /// OpenPageViewID，查询视图ID
        /// </summary>
        public string FindPageViewID { get; set; }
        #endregion

        #region 记录ID
        /// <summary>
        /// 接收URL传递过来的记录ID，用于显示、修改、删除、查询数据
        /// </summary>
        public string DataID { get; set; }
        #endregion

        #region 按钮ID
        /// <summary>
        ///  按钮ID，用于是否可用按钮的验证
        /// </summary>
        public string ButtonID { get; set; }
        #endregion

        #region 部门ID
        /// <summary>
        /// 部门ID。string.Empty 表示没有传递参数
        /// </summary>
        public int DepartmentID{ get; set; }
        #endregion

        #region CallBack 回调函数的名称
        /// <summary>
        /// 回调函数的名称
        /// </summary>
        /// user:jyk
        /// time:2013/1/23 9:44
        public string CallBack { get; set; }
        #endregion
        #endregion

        /// <summary>
        /// 初始化，根据url里的参数，自动给属性赋值。
        /// </summary>
        /// <param name="context">The context.如果为null，则不给属性赋值</param>
        /// user:jyk
        /// time:2013/2/27 14:27
        public URLParam(HttpContext context)
        {
            if (context == null)
                return;

            //验证视图ID参数是否是数字。
            MasterPageViewID = URLParamVerification.PageViewID(context, "mpvid").ToString(CultureInfo.InvariantCulture);

            //验证查询视图ID参数是否是数字。
            FindPageViewID = URLParamVerification.PageViewID(context, "fpvid").ToString(CultureInfo.InvariantCulture);

            //DataList.aspx、DataForm.aspx 页面通过URL里的参数设置。
            DataID = URLParamVerification.FormDataID(context, "id");

            //验证按钮ID是否是数字
            ButtonID = URLParamVerification.PageViewID(context, "bid").ToString(CultureInfo.InvariantCulture);

            //验证部门ID参数是否是数字。
            DepartmentID = URLParamVerification.PageViewID(context, "did");

            UserSsoID = URLParamVerification.StringID(context, "userSSOID");
            UserAppID = URLParamVerification.StringID(context, "userAppID");
            Action = URLParamVerification.StringID(context, "action");
            CallBack = URLParamVerification.StringID(context, "callback");
            DataBaseID = URLParamVerification.StringID(context, "dbid");
            ModuleID = URLParamVerification.StringID(context, "mdid");
            WebAppID = URLParamVerification.StringID(context, "webappID");
            UserIP = context.Request.UserHostAddress;
            WebAppIP = UserIP;

        }

        #region 属性生成参数形式
        /// <summary>
        /// 生成参数形式
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2013/2/6 15:28
        public string ToURLParam(bool isFirst)
        {

            var sb = new StringBuilder(500);
            sb.Append(isFirst ? "?" : "&");

            sb.Append("webappID="); sb.Append(WebAppID);
            sb.Append("&action="); sb.Append(Action);
            sb.Append("&id=");sb.Append(DataID );
            sb.Append("&fpvid=");sb.Append(FindPageViewID );
            sb.Append("&mpvid=");sb.Append(MasterPageViewID );
            sb.Append("&mdid=");sb.Append(ModuleID );
            sb.Append("&userSSOID="); sb.Append(UserSsoID);
            sb.Append("&userAppID="); sb.Append(UserAppID);
            sb.Append("&bid="); sb.Append(ButtonID);
            sb.Append("&dbid="); sb.Append(DataBaseID);
           
            return sb.ToString();

        }

        #endregion

    
    }
}
