using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.UI
{
    /// <summary>
    /// ReleaseInfo 的摘要说明
    /// </summary>
    public class ReleaseInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            //if(没有登录)就转到登录界面，否则就给发布条件
            if (context.Session["Login"] == null)
            {
                context.Response.Write("<script>alert('请先登录！');window.location.href='etplogin.ashx';</script>");
                return;
            }
            DataTable eduType = RecruCol.Admin.HelpClass.select.SelecteduType();
            var data = new
            {
                eduType =eduType.Rows,
                Maintitle = "发布信息"
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("ReleaseInfo.html", data);
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