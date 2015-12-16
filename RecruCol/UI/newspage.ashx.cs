using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// newspage 的摘要说明
    /// </summary>
    public class newspage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int id = 1;
            string Id =  context.Request["id"].ToString();
            if(!string.IsNullOrEmpty(Id))
            {
                id = Convert.ToInt32(Id);
            }
            DataTable st = golobHelp.CareerGuid.Article(id);
            var data = new
            {
                ArticleInfo = st.Rows[0],
                Maintitle = "职业指导文章"
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("newspage.html", data);
            context.Response.Write(html);
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