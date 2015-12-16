using Cocu;
using Maticsoft.DBUtility;
using RecruCol.Admin.HelpClass;
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
    /// AdminLogin 的摘要说明
    /// </summary>
    public class AdminLogin : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string usr = context.Request["usr"];
            string pwd = context.Request["passWord"];
            string sql  = "select * from AdminManage where name= @usr";
            SqlParameter  p = new SqlParameter("@usr",usr);
            DataTable dt =  DbHelperSQL.Query(sql,p).Tables[0];
            if(dt.Rows.Count <= 0)
            {
                context.Response.Write("<script>alert('不存在该用户！');window.history.go(-1);</script>");
                return;
            }
            else if(dt.Rows.Count > 1)
            {
                context.Response.Write("<script>alert('出错，存在同名用户');window.history.go(-1);</script>");
                return;
            }
            else
            {
                if (dt.Rows[0][1].ToString() == usr && dt.Rows[0][2].ToString() == CocuMD5.StrToMD5(pwd))
                 {
                     AdminManage admin = new AdminManage();
                     admin.id = (int)dt.Rows[0][0]; admin.name = dt.Rows[0][1].ToString();
                     admin.passWord = dt.Rows[0][2].ToString();
                     context.Session["admin"] = admin;
                     context.Response.Write("<script>alert('登陆成功');window.location.href='recuirtManage.ashx'</script>");
                 }
                 else
                 {
                     context.Response.Write("<script>alert('用户名或者密码错误');window.history.go(-1);</script>");
                     return;
                 }
            }
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