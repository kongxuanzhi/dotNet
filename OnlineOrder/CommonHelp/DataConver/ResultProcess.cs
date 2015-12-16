using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineOrder.CommonHelp.DataConver
{
    public  static class  ResultProcess
    {

        public delegate void ProcOtherRequest(HttpContext context);//处理那些非文字上传的东西，针对文件上传

        public  delegate void DisplayBeforeAddNew();

        public  delegate  void DeleteResultProc(HttpContext context);
        public  delegate void UpdateResultProc(bool isSuccess, HttpContext context);
        public  delegate void AddResultProc(int rowEffect, HttpContext context);

        public static void updateResult(bool isSuccess, HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/UpdateSuccess.html", "");
            context.Response.Write(html);
        }

        public static void deleteResult(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/DeleteSuccess.html", "");
            context.Response.Write(html);
        }

        public static void addResult(int rowEffect, HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/Other/jsback/AddSuccess.html", "");
            context.Response.Write(html);
        }
    }
}
