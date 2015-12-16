using Maticsoft.DBUtility;
using RecruCol.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.CommonHelp.operatorSQL
{
    public class selectById<T>
    {
        /// <summary>
        /// 利用反射将一行datatable转化为对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public selectById(string procName, Reflection<T> Ref, int id)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            if (id <= 0) return;
            pms.Add(new SqlParameter("@id", id));
            DataTable dt = DbHelperSQL.RunProcedure(procName, pms.ToArray(), "dt").Tables["dt"];
            if (dt.Rows.Count <= 0) return;
            Ref.DataRowToModel(dt.Rows[0]);
        }
    }
}