using RecruCol.Admin.HelpClass;
using RecruCol.Admin.Model;
using RecruCol.CommonHelp.DataConver;
using RecruCol.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// recuirtEdit 的摘要说明
    /// </summary>
    public class recuirtEdit : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            commonhelp.HasLogin(context);

            context.Response.ContentType = "text/html";

            Reflection<T_recuritInfo> Ref = new Reflection<T_recuritInfo>(new T_recuritInfo());
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
                    context.Response.Write("<script>document.write('您访问的内容不存在或者已被删除!');window.location.href='recuirtManage.ashx';</script>");
                }
                if (Action == "Edit")
                {
                    string Save = context.Request["Save"];
                    if (string.IsNullOrEmpty(Save)) //没有点击提交按钮，第一次进入显示内容
                    {
                        selectById<T_recuritInfo> content = new selectById<T_recuritInfo>("usp_SelectRecById", Ref, Convert.ToInt32(Ref.map["id"]));
                    }
                    else //将数据更改， 读到所有数据，插入到数据库
                    {
                        Ref.HttpRecieve(context);
                        Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                        Update<T_recuritInfo> update = new Update<T_recuritInfo>(Ref, "usp_T_recuritInfo_Update");  //写一个存储过程
                        context.Response.Write("<script>alert('更新成功');window.location.href='recuirtManage.ashx';</script>");
                    }
                }
                else if (Action == "Delete")
                {
                    deleteById.Delete(Convert.ToInt32(Ref.map["id"]), "T_Info", false);
                    context.Response.Write("<script>alert('删除成功');window.location.href='recuirtManage.ashx';</script>");
                }
            }
            DataTable eduType = HelpClass.select.SelecteduType();
            var data = new
            {
                Action = Action,
                retEdit= Ref.map,
                EduType = eduType.Rows,
            };
            string html = Admin.HelpClass.commonhelp.RendHtml("recuirtEdit.html", data);
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