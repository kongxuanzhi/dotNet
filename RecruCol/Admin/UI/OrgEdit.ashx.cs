using Cocu;
using Maticsoft.DBUtility;
using RecruCol.Admin.HelpClass;
using RecruCol.Admin.Model;
using RecruCol.Admin.paraClass;
using RecruCol.CommonHelp.DataConver;
using RecruCol.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// OrgEdit 的摘要说明
    /// </summary>
    public class OrgEdit : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            commonhelp.HasLogin(context);
            context.Response.ContentType = "text/html";
            #region
            Reflection<T_EnumOrg> Ref = new Reflection<T_EnumOrg>(new T_EnumOrg()); // 变化
            string Action = context.Request["Action"];
            if (!string.IsNullOrEmpty(Action))
            {
                try
                {
                    if (Action != "AddNew")
                        Ref.map["id"] = Convert.ToInt32(context.Request["id"]);
                }
                catch
                {
                    context.Response.Write("<script>document.write('您访问的内容不存在或者已被删除!');window.location.go(-1);</script>");
                }
                if (Action == "Edit")
                {
                    string Save = context.Request["Save"];
                    if (string.IsNullOrEmpty(Save)) //没有点击提交按钮，第一次进入显示内容
                    {
                        selectById<T_EnumOrg> content = new selectById<T_EnumOrg>("usp_T_EnumOrg_GetModel", Ref, Convert.ToInt32(Ref.map["id"]));
                    }
                    else //将数据更改， 读到所有数据，插入到数据库
                    {
                        Ref.HttpRecieve(context);//从前台得到更新后的数据
                        Ref.map["password"] = CocuMD5.StrToMD5(Convert.ToString(Ref.map["password"]));
                        Update<T_EnumOrg> update = new Update<T_EnumOrg>(Ref, "usp_T_EnumOrg_Update"); //【改】更新的存储过程
                        context.Response.Write("<script>alert('更新成功');window.location.href='OrgInfoManage.ashx';</script>");
                    }
                }
                else if (Action == "AddNew")
                {
                    string Save = context.Request["Save"];
                    if (!string.IsNullOrEmpty(Save)) //没点
                    {
                        Ref.HttpRecieve(context);
                        Ref.map["id"] = null;
                        string sql = "select count(*) from T_EnumOrg where name = @name";
                        SqlParameter p = new SqlParameter("@name",Ref.map["name"]);
                        int rowsEffect =(int)DbHelperSQL.GetSingle(sql, p);
                        if(rowsEffect>0)
                        {
                            context.Response.Write("<script> alert('用户名已注册，请更换一个用户名!');window.history.go(-1)</script>");
                            return;
                        }
                        Ref.map["password"] = CocuMD5.StrToMD5(Convert.ToString(Ref.map["password"]));
                        Update<T_EnumOrg> update = new Update<T_EnumOrg>(Ref, "usp_T_EnumOrg_ADD");//【改】存储过程，
                        context.Response.Write("<script>alert('添加成功');window.location.href='OrgInfoManage.ashx';</script>");

                    }
                    else
                    {
                        foreach (var property in Ref.target.GetType().GetProperties())  //把所有属性字段加在map
                        {
                            Ref.map[property.Name] = null;
                        }
                    }
                }
                else if (Action == "Delete")
                {
                    deleteById.Delete(Convert.ToInt32(Ref.map["id"]), "T_EnumOrg", true); //【改】存储过程，是否真删除
                    context.Response.Write("<script>alert('删除成功');window.location.href='OrgInfoManage.ashx';</script>");
                }
            }
            var data = new
            {
                Maintitle = "公司信息修改",
                Action = Action,
                orgEdit = Ref.map
            };
            string html = Admin.HelpClass.commonhelp.RendHtml("OrgEdit.html", data);
            context.Response.Write(html);
            #endregion          
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