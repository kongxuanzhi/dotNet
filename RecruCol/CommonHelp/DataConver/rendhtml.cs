using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Commonhelp.DataConver
{
    public class rendhtml
    {
        public static string RendHtml(string templateName, object data)
        {
            //通用渲染语句
            VelocityEngine vltEngine = new VelocityEngine();
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, System.Web.Hosting.HostingEnvironment.MapPath("~/templates"));//模板文件所在的文件夹
            vltEngine.Init();

            VelocityContext vltContext = new VelocityContext();
            vltContext.Put("data", data);//s是在html中的变量名，S是当前文档中变量名
            Template vltTemplate = vltEngine.GetTemplate(templateName);
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);
            return  vltWriter.GetStringBuilder().ToString();
        }

    }
}