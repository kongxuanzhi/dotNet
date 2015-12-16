using RecruCol.Admin.HelpClass;
using RecruCol.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// deleteOrg 的摘要说明
    /// </summary>
    public class deleteOrg : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            commonhelp.HasLogin(context);
            context.Response.ContentType = "text/plain";
            string ids = context.Request["Ids"];
            string[] id = ids.Split(new char[] { '|' });
            for (int i = 0; i < id.Length - 1; i++)
                deleteById.Delete(Convert.ToInt32(id[i]), "T_EnumOrg", true);
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