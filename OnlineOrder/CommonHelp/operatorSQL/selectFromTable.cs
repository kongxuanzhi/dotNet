using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineOrder.CommonHelp.operatorSQL
{
    public class selectFromTable
    {
        public static DataTable select(string tabName) 
        {
            //   假删除
            string procName = "usp_select";
            if (string.IsNullOrEmpty(tabName)) //1 是真删除，0是假删除
            {
                return  null;
            }
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@tableName", tabName));
            return DbHelperSQL.RunProcedure(procName, pms.ToArray(),"dt").Tables["dt"];
        }
    }
}