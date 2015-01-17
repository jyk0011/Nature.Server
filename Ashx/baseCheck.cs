using System;
using System.Web;
using Nature.Common;

namespace Nature.Service.Ashx
{

    /// <summary>
    /// 验证函数的基类
    /// </summary>
    public class BaseCheck
    {
        /// <summary>
        /// 验证url的参数值
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public virtual string Check(string key, string value)
        {
            return "";
        }
    }


    #region 验证int

    /// <summary>
    /// 验证int
    /// </summary>
    public class CheckInt : BaseCheck
    {
        /// <summary>
        /// 验证url的参数值，int类型的参数
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public override string Check(string key, string value)
        {
            if (value != null)
            {
                value = value.Trim('"');
                //验证 参数是否是int。
                if (!Functions.IsInt(value))
                {
                    if (!Functions.IsGuid(value))
                    {
                        HttpContext.Current.Response.Write(key + "参数不正确！不是int也不是guid" + value);
                        HttpContext.Current.Response.End();
                    }
                   
                }
            }
            else
                value = "0";

            return value;
        }

    }

    #endregion

    #region 验证ints

    /// <summary>
    /// 验证ints
    /// </summary>
    public class CheckInts : BaseCheck
    {
        /// <summary>
        /// 验证url的参数值，int类型的参数
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public override string Check(string key, string value)
        {
            if (value != null)
            {
                value = value.Trim('"');
                //验证 参数是否是int。
                if (!Functions.IsIDString(value))
                {
                    if (!Functions.IsGuid(value))
                    {
                        HttpContext.Current.Response.Write(key + "参数不正确！不是ints也不是guid" + value);
                        HttpContext.Current.Response.End();
                    }

                    
                }
            }
            else
                value = "0";

            return value;
        }

    }

    #endregion

    #region 验证Guid

    /// <summary>
    /// 验证Guid
    /// </summary>
    public class CheckGuid : BaseCheck
    {
        /// <summary>
        /// 验证url的参数值，int类型的参数
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public override string Check(string key, string value)
        {
            if (value != null)
            {
                value = value.Trim('"');
                //验证 参数是否是int。
                if (!Functions.IsGuid(value))
                {
                    HttpContext.Current.Response.Write(key + "参数不正确！" + value);
                    HttpContext.Current.Response.End();
                }
            }
            else
                value = "0";

            return value;
        }

    }

    #endregion

    #region 验证int、Guid

    /// <summary>
    /// 验证int、Guid
    /// </summary>
    public class CheckIntGuid : BaseCheck
    {
        /// <summary>
        /// 验证url的参数值，int类型的参数
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public override string Check(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return "-2";
            else
                value = value.Trim('"');

            //验证ID参数是否是数字。

            if (!Functions.IsInt(value))
            {
                if (!Functions.IsGuid(value))
                {
                    //没有传递，设置默认值
                    value = string.Empty;
                }

            }

            return value;
        }

    }

    #endregion

    #region 验证string

    /// <summary>
    /// 验证string
    /// </summary>
    public class CheckString : BaseCheck
    {
        /// <summary>
        /// 验证url的参数值，string类型的参数
        /// </summary>
        /// <returns></returns>
        /// <param name="key">url参数名</param>
        /// <param name="value">要验证的值</param>
        public override string Check(string key, string value)
        {
            if (value != null)
            {
                value = value.Trim('"');
            }
            else
                value = "";

            return value;
        }

    }

    #endregion

     
}