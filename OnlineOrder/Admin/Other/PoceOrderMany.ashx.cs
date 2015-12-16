using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.Other
{
    /// <summary>
    /// PoceOrderMany 的摘要说明
    /// </summary>
    public class PoceOrderMany : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string ids = context.Request["delId"];

       
            if (string.IsNullOrEmpty(ids))
            {
                string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/procSuccess.html", null);
                context.Response.Write(html);
                return;
            }
            string[] id = ids.Split(new char[] { ',' });
            for (int i = 0; i < id.Length; i++)
            {
                Maticsoft.Model.T_Order model= new Maticsoft.BLL.T_Order().GetModel(Convert.ToInt64(id[i]));
                if (model.statu == "已处理")
                {
                    string html4= Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/procfailure.html", null);
                    context.Response.Write(html4);
                    return;
                }
                new Maticsoft.BLL.T_Order().Update(Convert.ToInt64(id[i]), "已处理");

            }
            string html2 = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/procSuccess.html", null);
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