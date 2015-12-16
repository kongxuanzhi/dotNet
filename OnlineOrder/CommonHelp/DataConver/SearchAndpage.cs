using CommonHelp;
using CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineOrder.CommonHelp.DataConver
{
    public class SearchAndpage<T>
    {
        public Reflection<T> Ref;
        public DataTable DT;
        public dividePage DP;
        public SearchAndpage(T model)
        {
            DP = new dividePage();
            Ref = new Reflection<T>(model);
        }
        public  void ProcessRequest(HttpContext context)
        {
            string Search = context.Request["Search"];
            Ref.SeachSetCooike(context, Search, "/");

            #region 分页参数
            #region 获得参数 
            string Index = context.Request["pageNum"];
            string pageSize = context.Request["numPerPage"];
            if(!string.IsNullOrEmpty(pageSize))
            {
                DP.pageSize = Convert.ToInt32(pageSize);
            }
            if (!string.IsNullOrEmpty(Index)) DP.index = Convert.ToInt32(Index);
            #endregion
            #region 将参数加入list集合
            DP.AddParams(Ref.AddPms().ToArray());
            #endregion

            #region 执行存储过程，之后得到返回的totalPageCount值
            DT = DP.proc(DP.procName, DP.pms.ToArray(), "dt").Tables["dt"];
            DP.totalPageCount = (int)DP.returnValue.Value; //执行完存储过程才能得到返回值
            DP.pages = DP.PageHref();  //可选择
            #endregion

            #endregion
        }
    }
}