using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.CommonHelp.operatorSQL
{
    public class deleteById
    {
        public static void Delete(int id, string tabName, bool isFDelete) //假删除true
        {
            //   假删除
            string procName = "usp_DeleteById";
            if (id <= 0) return;
            int jiashanchu = 0;
            if (isFDelete ) //1 是真删除，0是假删除
            {
                jiashanchu = 1;
            }
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@fDelete", jiashanchu));
            pms.Add(new SqlParameter("@id", id));
            pms.Add(new SqlParameter("@tableName", tabName));
            DbHelperSQL.RunProcedure(procName, pms.ToArray());
        }
    }
}