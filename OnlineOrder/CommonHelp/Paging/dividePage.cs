using CommonHelp.DataConver;
using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CommonHelp
{
    public delegate DataSet RunProcRunProcedure(string storedProcName, IDataParameter[] parameters, string tableName);
    public  class dividePage
    {
        public int totalPageCount { get; set; }
        public int index { get; set; }
        public int pageSize { get; set; }
        public string  linkHref { get; set; }
        public string procName { get; set; }
        public  List<SqlParameter> pms{ get; set; }
        public SqlParameter returnValue { get; set; }
        public RunProcRunProcedure proc{get; set;}
        //Maticsoft.Model.T_Login

        public List<object> pages;

        public dividePage()
        {
            
            totalPageCount = 0;
            index = 1;
            pageSize = 10;
            linkHref = "";
            this.procName = ""; 
            pms = new List<SqlParameter>();
            returnValue = new SqlParameter("@totalPageCount", DbType.Int32);
            returnValue.Direction = ParameterDirection.Output;
            pms.Add(returnValue);
            proc = new RunProcRunProcedure(DbHelperSQL.RunProcedure);
        }
        #region 用于分页的全局类
        public  static void pageStartToEnd(int totalnum, int index, ref int start, ref int end)
        {
            if (totalnum < 10)
            {
                start = 1; end = totalnum;
            }
            else
            {
                if (index <= 6)
                {
                    start = 1;
                    end = 10;
                }
                else if (index > 6)
                {
                    if (index + 5 < totalnum)
                    {
                        start = index - 5;
                        end = index + 4;
                    }
                    else
                    {
                        start = index - (9 - (totalnum - index));
                        end = totalnum;
                    }
                }
            }
        }
        /// <summary>
        /// 可被重载 增加传递参数
        /// </summary>
        /// <param name="totalnum">内容总的条数</param>
        /// <param name="index">当前页面的页数</param>
        /// <param name="desAshx">链接到的网页ashx页面</param>
        /// <returns></returns>
        public  virtual  List<object> PageHref()  
        {
            List<object> page = new List<object>();
            int start = 1, end = 1;
            pageStartToEnd(totalPageCount, index, ref start, ref end);
            //将链接写到object中
            for (int i = start; i <= end; i++)
            {
                object temp = new { href = linkHref + "?index=" + i, Id = i };
                page.Add(temp);
            }
            return page;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void AddParams(params SqlParameter[] parameters)
        {
            if(parameters != null && parameters.Length > 0)
            {
                pms.AddRange(parameters);
            }
            pms.Add(new SqlParameter("@index", index));
            pms.Add(new SqlParameter("@pageSize", pageSize));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="tableName"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        #endregion

        
    }
}