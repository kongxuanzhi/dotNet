
using Maticsoft.DBUtility;
using RecruCol.CommonHelp;
using RecruCol.golobHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// RecommendPos 的摘要说明
    /// </summary>
    public class RecommendPos:dividePage,IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/HTML";
            #region 通用分页
            string strindex = context.Request["index"];
            pageSize = 13;
            procName = "usp_BaoAnPage";  //写一下这个存储过程
            if (!string.IsNullOrEmpty(strindex))
            {
                index = Convert.ToInt32(strindex);
            }
            AddParams();
            DataTable resuitInfo = proc(procName, pms.ToArray(), "dt").Tables["dt"];
            totalPageCount = (int)returnValue.Value;

            linkHref = "RecommendPos.ashx";
            List<object> pages = PageHref();
            #endregion
            var data = new
            {
                Maintitle = "宝安推介",
                BRecuritInfo = resuitInfo.Rows,
                
                Index = index,
                TotalCount = totalPageCount,
                Pages = pages
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("RecommendPos.html", data);

            context.Response.Write(html);
        }   

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}