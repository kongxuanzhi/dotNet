using Cocu;
using Maticsoft.DBUtility;
using RecruCol.Admin.HelpClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// AddNewAdmim 的摘要说明
    /// </summary>
    public class AddNewAdmim : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
           // commonhelp.HasLogin(context);
            context.Response.ContentType = "text/html";
            string Action = context.Request["Action"];
            if(Action=="AddNew") 
            {
                string AdminName = context.Request["AdminName"];
                string AdminPwd = context.Request["AdminPwd"];
                string sql = "select count(*) from AdminManage where name=@name";
                SqlParameter p = new SqlParameter("@name", AdminName);
                int rowsEffect = (int)DbHelperSQL.GetSingle(sql, p);
                if (rowsEffect > 0)
                {
                    context.Response.Write("<script> alert('用户名已存在，请更换一个用户名!');window.history.go(-1)</script>");
                    return;
                }
                string InsertSql = "insert into AdminManage values(@name,@password,'super')";
                List<SqlParameter> pms = new List<SqlParameter>();
                pms.Add(new SqlParameter("@name", AdminName));
                pms.Add(new SqlParameter("@password", CocuMD5.StrToMD5(AdminPwd)));
                int roweffct = DbHelperSQL.ExecuteSql(InsertSql, pms.ToArray());
                if (roweffct > 0)
                {
                    context.Response.Write("<script> alert(' 添加成功！');window.location.href='recuirtManage.ashx';</script>");
                    return;
                }
                else
                {
                    context.Response.Write("<script> alert('意外故障， 添加失败！');window.location.href='recuirtManage.ashx';</script>");
                    return;
                }
            }
            else if(Action == "Edit")
            {
                string id =  context.Request["AdminId"];
                string passWord1 = context.Request["AdminPwd1"];
                string passWord2 = context.Request["AdminPwd2"];
                if(!passWord1.Equals(passWord2))
                {
                    context.Response.Write("<script> alert('两次输入不同！');window.histoy.go(-1);</script>");
                    return;
                }
                string sqlUpdate = "update AdminManage set password=@pwd where id=@id";
                SqlParameter[] pms = new SqlParameter[2]{new SqlParameter("@pwd",CocuMD5.StrToMD5(passWord1)),
                new SqlParameter("@id",id)};
                int rowEffect = DbHelperSQL.ExecuteSql(sqlUpdate, pms.ToArray());
                   context.Response.Write("<script> alert('更新成功！');window.histoy.go(-1);</script>");
                return;
            }
            else
            {
                context.Response.Write("<script> alert('意外故障， 失败！');window.location.href='recuirtManage.ashx';</script>");
                return;
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