using RecruCol.golobHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// recuritDetail 的摘要说明
    /// </summary>
    public class recuritDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string Id = context.Request["Id"];
            long id = 0;
            if(!long.TryParse(Id,out id))
            {
                context.Response.Write("您查看的信息不存在或者已经被删除！");
            }
           
            DataTable dt = detailExt.Getdetail(id);
            var data = new
            {
                Maintitle = dt.Rows[0][1].ToString(),
                OrgInfo = dt.Rows[0]

            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("recuritDetail.html", data);
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