using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.Other
{
    /// <summary>
    /// Uploadimg 的摘要说明
    /// </summary>
    public class Uploadimg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/";
            string img = context.Request["path"];
           // FileStream  fle =  File.OpenRead(img);
            Image simg = Image.FromFile(img);
            simg.Save(@"E:\编程\暑期@公司任务\孔龙飞后勤\在线订餐系统\OnlineOrder\Admin\UploadImage\");
            string filepath = context.Server.MapPath(img);
            context.Response.Write(filepath);
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