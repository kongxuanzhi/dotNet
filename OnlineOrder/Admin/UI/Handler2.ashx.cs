
using System;
using System.Web;

public class cutImage : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (!string.IsNullOrEmpty(context.Request.QueryString["fileUrl"]) && !string.IsNullOrEmpty(context.Request.QueryString["left"]) && !string.IsNullOrEmpty(context.Request.QueryString["top"])
            && !string.IsNullOrEmpty(context.Request.QueryString["width"]) && !string.IsNullOrEmpty(context.Request.QueryString["height"]))
        {
            string fileUrl = context.Request.QueryString["fileUrl"];
            int left = Convert.ToInt32(context.Request.QueryString["left"]);
            int top = Convert.ToInt32(context.Request.QueryString["top"]);
            int width = Convert.ToInt32(context.Request.QueryString["width"]);
            string path = "../images/";
            if (!System.IO.Directory.Exists(context.Server.MapPath(path)))
                System.IO.Directory.CreateDirectory(context.Server.MapPath(path));
            path = path + "Avatar" + Guid.NewGuid();
            string exten = System.IO.Path.GetExtension(fileUrl);
            int height = Convert.ToInt32(context.Request.QueryString["height"]);
            System.Drawing.Image image = null; ;
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Graphics g = null;
            try
            {
                image = System.Drawing.Image.FromFile(context.Server.MapPath("../temp/" + fileUrl));
                bitmap = new System.Drawing.Bitmap(width, height);
                g = System.Drawing.Graphics.FromImage(bitmap);
                g.Clear(System.Drawing.Color.White);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(image, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(left, top, width, height), System.Drawing.GraphicsUnit.Pixel);
                bitmap.Save(context.Server.MapPath(path + exten));
                context.Response.Write(path + exten);
                context.Response.End();
            }
            catch { }
            finally
            {
                image.Dispose();
                bitmap.Dispose();
                bitmap.Dispose();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}