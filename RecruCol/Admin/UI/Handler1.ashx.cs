using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string name = context.Request["name"];
            string passWord = context.Request["passWord"];
            string gender = context.Request["gender"];
            string tel = context.Request["tel"];
            string orderTimes = context.Request["orderTimes"];
            
            //对数据筛选一下 ......

            string sql = "update T_userInfo set name=@name, passWord=@passWord...";
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@name",name));
            pms.Add(new SqlParameter("@passWord", passWord));
            pms.Add(new SqlParameter("@gender", gender));
            
            DbHelperSQL.ExecuteSql(sql,pms.ToArray()); 
            
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