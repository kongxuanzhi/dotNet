using RecruCol.golobHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.UI
{
    /// <summary>
    /// ValidateCode 的摘要说明
    /// </summary>
    public class validatecode : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string ValidCode = ValidateCode.GenerateCheckCode();
            context.Session["validatecode"] = ValidCode;
            System.IO.MemoryStream Img = ValidateCode.CreateCheckCodeImage(ValidCode);
            context.Response.ClearContent();
            context.Response.ContentType = "image/Gif";
            context.Response.BinaryWrite(Img.ToArray());
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