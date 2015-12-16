using CommonHelpDb.Dataconver;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using 兽兽1;

namespace test
{
    /// <summary>
    /// getMove 的摘要说明
    /// </summary>
    public class getMovie : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string catalog = context.Request["catalog"].ToString();
            if(!string.IsNullOrEmpty(catalog))
            {
                 List<T_videos> videos = new List<T_videos>(); 
                 string sql = "select * from T_videos where Mcatalog in (select id from T_catalog where catalog = @MCatalog)";
                 SqlParameter  p = new SqlParameter("@MCatalog",catalog);
                 DataTable dt = SqlHelper.ExecuteDataTable(sql, p);
                 if (dt != null && dt.Rows.Count > 0)
                 {
                     context.Response.Write(DtToJson.DataTableToJson(dt));
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
}