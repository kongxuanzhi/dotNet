using CommonHelpDb.Dataconver;
using RecruCol.Admin.HelpClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// GetAdmin 的摘要说明
    /// </summary>
    public class GetAdmin : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            commonhelp.HasLogin(context);
            AdminManage Admin = (AdminManage)(context.Session["admin"]);
            string json = DtToJson.ObjectToJson(Admin);
            context.Response.Write(json);

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