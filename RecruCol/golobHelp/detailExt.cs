using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.golobHelp
{
    public class detailExt
    {
        internal static DataTable Getdetail(long id)
        {
            string procName = "usp_detail"; //增加字段只需要在存储过程里增加就行了
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@id",id));
            return DbHelperSQL.RunProcedure(procName,pms.ToArray(),"dt").Tables["dt"];
        }
    }
}