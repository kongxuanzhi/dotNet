using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.golobHelp
{
    public class loginExt
    {
        internal static DataTable Exsit(string UsrName)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            pms.Add(new SqlParameter("@Usrname",UsrName));
            string procName = "usp_Login";
            return DbHelperSQL.RunProcedure(procName, pms.ToArray(), "dt").Tables["dt"];
        }
    }
}