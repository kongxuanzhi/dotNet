using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.UI
{
    /// <summary>
    /// Getorg 的摘要说明
    /// </summary>
    public class Getorg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string logout = context.Request["logout"];
            if(string.IsNullOrEmpty(logout))
            {
                if (context.Session["Login"] == null)
                {
                    context.Response.Write("企业登录");
                }
                else
                {
                    DataTable dt = (DataTable)context.Session["Login"];
                    string org = dt.Rows[0][1].ToString();
                    context.Response.Write(org + "已登录");
                }
            }
            else
            {
                if (context.Session["Login"] == null)
                {
                    context.Response.Write("<script>window.location.href= 'recruitInfo.ashx';</script>");
                }
                else
                {
                    context.Session["Login"] = null;
                    context.Response.Write("<script>alert('退出成功！');window.location.href= 'recruitInfo.ashx';</script>");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}