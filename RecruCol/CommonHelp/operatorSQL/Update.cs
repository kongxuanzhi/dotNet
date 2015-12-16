    using Maticsoft.DBUtility;
    using RecruCol.Admin.paraClass;
    using RecruCol.CommonHelp.DataConver;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;

    namespace RecruCol.CommonHelp.operatorSQL
    {
        /// <summary>
        /// 更改和新增都是这个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Update<T>
        {
            public RunProcRunProcedure proc { get; set; } //在分页类中声明过了
            public List<SqlParameter> pms { get; set; } //更新的参数列表
            public  Update(Reflection<T> Ref,string procName)
            {
                pms = new List<SqlParameter>();
                foreach(KeyValuePair<string, object> item in Ref.map)
                {
                   //item.Key !=null 限制是表中字段，item.Value限制更新的字段，addnew时要设定map["id"]=null
                    if (item.Key !=null && item.Value != null )  
                    {
                        pms.Add(new SqlParameter("@" + item.Key,item.Value));
                    }
                }
                proc = new RunProcRunProcedure(DbHelperSQL.RunProcedure);
                proc(procName, pms.ToArray(), "dt");
            }
        }
    }