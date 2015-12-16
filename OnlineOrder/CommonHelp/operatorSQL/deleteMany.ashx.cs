using CommonHelp.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineOrder.CommonHelp.operatorSQL
{
    /// <summary>
    /// deleteMany 的摘要说明
    /// </summary>
    public class deleteMany : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string TableName = context.Request["TabName"];
            string ids = context.Request["delId"];
            
            if (string.IsNullOrEmpty(TableName) || string.IsNullOrEmpty(ids))
            {
                string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/deleteFail.html", null);
                context.Response.Write(html);
                return;
            }
            string[] id = ids.Split(new char[] { ',' });
            for (int i = 0; i < id.Length; i++)
                deleteById.Delete(Convert.ToInt32(id[i]), TableName, true);
            string html2 = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/DeleteSuccess.html", null);
            context.Response.Write(html2);
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