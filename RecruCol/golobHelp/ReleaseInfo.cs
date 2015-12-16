using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.golobHelp
{
    public class ReleaseInfo
    {
        internal static void InsertInto(string pos, string org, string arttype, string ManCount, string gender, string EduRequir, string Context)
        {
            string Orgid = "select id from T_EnumOrg where name = @content";
            int id = (int)DbHelperSQL.ExecuteSqlGet(Orgid, org);

            string insert = "insert into T_recuritInfo values(@pos,@org,@publicTime,@Context,@EduRequir,@arttype,@gender,@ManCount,@isLegal)";
            SqlParameter[] pms = new SqlParameter[] 
            {
                new SqlParameter("@pos",pos),
                new SqlParameter("@org",id),
                new SqlParameter("@publicTime",DateTime.Now.ToString("yyyy-MM-dd")),
                new SqlParameter("@Context",Context),
                new SqlParameter("@EduRequir",EduRequir),
                new SqlParameter("@arttype",arttype),
                new SqlParameter("@gender",gender),
                new SqlParameter("@ManCount",ManCount),
                new SqlParameter("@isLegal",false)   //发布的文章默认是不合法的，需要人工审核后在将该值置位1代表合格
            };
            DbHelperSQL.ExecuteSql(insert,pms);
        }
    }
}