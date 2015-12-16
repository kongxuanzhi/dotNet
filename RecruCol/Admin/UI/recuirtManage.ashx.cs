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
    /// recuirtManage 的摘要说明
    /// </summary>
    public class recuirtManage : dividePage,  IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
           // commonhelp.HasLogin(context);
            context.Response.ContentType = "text/html";
         
            string Search = context.Request["Search"];
            Reflection<T_recuritInfo> Ref = new Reflection<T_recuritInfo>(new T_recuritInfo());
            Ref.SeachSetCooike(context, Search, "/");

            #region 分页参数
            pageSize = 10;  //覆盖父类中定义的变量，改变当前页显示的行数
            procName = "usp_RecuirtInfo";
            string strIndex = context.Request["index"];
            if (!string.IsNullOrEmpty(strIndex))
            {
                index = Convert.ToInt32(strIndex);
            }
            //添加额外参数和全部参数，注意，这个类被请求时，就会调用父类的构造函数，将totalPageCount放到pms中
            List<SqlParameter> parametes = Ref.AddPms();
            AddParams(parametes.ToArray());
            //执行存储过程，之后得到返回的totalPageCount值
            DataTable artInfo = proc(procName, pms.ToArray(), "dt").Tables["dt"];
            totalPageCount = (int)returnValue.Value; //执行完存储过程才能得到返回值
            //分页需求，给定请求页面，然后
            linkHref = "recuirtManage.ashx";
            List<object> pages = PageHref();
            #endregion

            DataTable eduType = HelpClass.select.SelecteduType();
            var data = new
            {
                SeachPara = Ref.map,
                RecuitInfo = artInfo.Rows,
                EduType = eduType.Rows,
                Index = index,
                Pages = pages,
                Totalnum = totalPageCount,
                Maintitle = "招聘信息管理"
            };
            string html = Admin.HelpClass.commonhelp.RendHtml("recuirtManage.html", data);
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