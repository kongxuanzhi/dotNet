using RecruCol.Admin.HelpClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// adminSession 的摘要说明
    /// </summary>
    public class adminSession : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            commonhelp.HasLogin(context);
            context.Session["admin"]=null;
            context.Response.Redirect("../Atemplates/AdminLogin.html");
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