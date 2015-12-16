using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.golobHelp
{
    public  class CareerGuid
    {
        public static DataTable Article(int id)
        {
            string SelectSql = "select info.id,info.title,info.pubTime,info.author,info.src,info.article, art.name as Arttype from T_Info as info left join  T_ArticleType as art on info.type = art.id where info.id=@id";
            using (DataSet ds= DbHelperSQL.Query(SelectSql,new SqlParameter("@id",id)))
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null; 
                }
            }
        }
    }
}