using RecruCol.CommonHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace RecruCol.UI
{
    /// <summary>
    /// CareerGuid 的摘要说明
    /// </summary>
    public class CareerGuid :dividePage, IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string[] type = { "专题报道", "热门信息", "简历指导", "面试技巧", "求职实录", "职场眺望" };
            List<object> pms = new List<object>();
            for (int i=0;i<type.Length;i++)
            {
                List<object> row = new List<object>();
                
                CareerGuid pages = new CareerGuid();
                pages.procName = "usp_info";
                pages.pageSize = 10;
                pages.index = 1;
                List<SqlParameter> parameters =  pages.Addpms(type[i],"Guid");
                pages.AddParams(parameters.ToArray());
                
                DataTable dt = proc(pages.procName,pages.pms.ToArray(),"dt").Tables["dt"];

                if(dt.Rows.Count>0)
                {
                    for (int j=0;j<dt.Rows.Count;j++)
                    {
                        long id = Convert.ToInt64(dt.Rows[j][0]);
                        string title = dt.Rows[j][1].ToString();
                        row.Add(new { href = "newspage.ashx?id=" + id, title = title });
                    }
                }
                else
                {
                    row.Add(new { href = "", title = "" });
                }
                pms.Add(row);
            }
            var data = new {
                
                Maintitle = "职业指导",
                Report =pms[0],
                HotInfo = pms[1],
                ResumeGuid=pms[2],
                FaceTest = pms[3],
                LookJob = pms[4],
                LoofForward = pms[5]
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("CareerGuid.html", data);
            context.Response.Write(html);
        }
        public List<SqlParameter> Addpms(string type, string bigType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(type)) parameters.Add(new SqlParameter("@type",type));
            if (!string.IsNullOrEmpty(bigType)) parameters.Add(new SqlParameter("@bigType",bigType));
            return parameters;
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