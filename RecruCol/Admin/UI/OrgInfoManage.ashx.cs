using RecruCol.Admin.HelpClass;
using RecruCol.Admin.Model;
using RecruCol.CommonHelp;
using RecruCol.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// OrgInfoManage 的摘要说明
    /// </summary>
    public class OrgInfoManage :dividePage, IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            commonhelp.HasLogin(context);
            context.Response.ContentType = "text/html";

            string Search = context.Request["Search"];
            Reflection<T_EnumOrg> Ref = new Reflection<T_EnumOrg>(new T_EnumOrg());
            Ref.SeachSetCooike(context, Search, "/");

            #region 分页参数
            pageSize = 15;
            procName = "usp_orgInfoManage";  
            string Index = context.Request["index"];
            if (!string.IsNullOrEmpty(Index)) index = Convert.ToInt32(Index);

            List<SqlParameter> parametes = Ref.AddPms();
            AddParams(parametes.ToArray());

            //执行存储过程，之后得到返回的totalPageCount值
            DataTable dt = proc(procName, pms.ToArray(), "dt").Tables["dt"];
            totalPageCount = (int)returnValue.Value; //执行完存储过程才能得到返回值

            linkHref = "OrgInfoManage.ashx";
            List<object> pages = PageHref();
            #endregion

            var data = new
            {
                Maintitle = "公司信息管理",
                OrgInfo = dt.Rows,
                SeachPara = Ref.map,
                Index = index,
                Pages = pages,
                Totalnum = totalPageCount,
            };
            string html = HelpClass.commonhelp.RendHtml("OrgInfoManage.html", data);
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