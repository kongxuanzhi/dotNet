using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.UI
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class uploadAvatar : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            HttpPostedFile postfile = context.Request.Files["File"];
            if (postfile != null)
            {
                int limitLength = 200;//单位KB
                if (postfile.ContentLength > limitLength * 1024)
                    ResponseText(100, "", 0, 0);
                if (!IsImage(postfile))
                    ResponseText(101, "", 0, 0);
                string path = "../temp/";
                string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(postfile.FileName);
                if (!System.IO.Directory.Exists(context.Server.MapPath(path)))
                    System.IO.Directory.CreateDirectory(context.Server.MapPath(path));
                postfile.SaveAs(context.Server.MapPath(path + newFileName));
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(context.Server.MapPath(path + newFileName));
                int width = bitmap.Width;
                int height = bitmap.Height;
                bitmap.Dispose();
                ResponseText(102, System.IO.Path.GetFileName(newFileName), width, height);
            }
        }
        public static bool IsImage(HttpPostedFile file)
        {
            string[] exten = { "255216", "7173", "13780" };//255216：jpg格式 7173：gif格式 6677：bmp格式 13780：png格式
            int fileLen = file.ContentLength;
            byte[] imgArray = new byte[fileLen];
            file.InputStream.Read(imgArray, 0, fileLen);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            br.Close();
            ms.Close();
            foreach (string s in exten)
            {
                if (fileclass == s)
                    return true;
            }
            return false;

        }
        public void ResponseText(int status, string fileUrl, int width, int height)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Write("<script>window.parent.upload(" + status + ",'" + fileUrl + "'," + width + "," + height + ")</script>");
            context.Response.End();
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
