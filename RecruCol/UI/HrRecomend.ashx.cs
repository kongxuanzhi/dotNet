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
    /// HrRecomend 的摘要说明
    /// </summary>
    public class HrRecomend :dividePage, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string[] type = { "人力管理", "招聘面试", "管理激励", "业绩考核", "薪酬福利", "法律常识" };
            List<object> pms = new List<object>();
            for (int i = 0; i < type.Length; i++)
            {
                List<object> row = new List<object>();

                HrRecomend pages = new HrRecomend();
                pages.procName = "usp_info";
                pages.pageSize = 10;
                pages.index = 1;

                List<SqlParameter> parameters = pages.Addpms(type[i], "Hr");
                pages.AddParams(parameters.ToArray());

                DataTable dt = proc(pages.procName, pages.pms.ToArray(), "dt").Tables["dt"];
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        long id = Convert.ToInt64(dt.Rows[j][0]);
                        string title = dt.Rows[j][1].ToString();
                        row.Add(new { href = "Hrpage.ashx?id=" + id, title = title });
                    }
                }
                else
                {
                    row.Add(new { href = "", title = "" });
                }
                pms.Add(row);
            }

            var data = new
            {
                Maintitle = "Hr分享",
                PersonManage = pms[0],
                Review = pms[1],
                Managestir = pms[2],
                TaskCheck = pms[3],
                MoneyExt = pms[4],
                NormalLaw = pms[5]
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("HrRecomend.html", data);
            context.Response.Write(html);
        }
        public List<SqlParameter> Addpms(string type, string bigType)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(type)) parameters.Add(new SqlParameter("@type", type));
            if (!string.IsNullOrEmpty(bigType)) parameters.Add(new SqlParameter("@bigType", bigType));
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