using RecruCol.Admin.HelpClass;
using RecruCol.Admin.paraClass;
using RecruCol.CommonHelp.DataConver;
using RecruCol.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.Admin.UI
{
    /// <summary>
    /// ArticleEdit 的摘要说明
    /// </summary>
    public class ArticleEdit : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            commonhelp.HasLogin(context);
            context.Response.ContentType = "text/html";
         
            Reflection<paraCheckArt> Ref = new Reflection<paraCheckArt>(new paraCheckArt()); // 变化
            string Action = context.Request["Action"];
            if(!string.IsNullOrEmpty(Action))
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
                if(Action == "Edit")
                {
                    string Save = context.Request["Save"];
                    if(string.IsNullOrEmpty(Save)) //没有点击提交按钮，第一次进入显示内容
                    {
                        selectById<paraCheckArt> content = new selectById<paraCheckArt>("usp_SelectArtById", Ref, Convert.ToInt32(Ref.map["id"]));
                    }
                    else //将数据更改， 读到所有数据，插入到数据库
                    {
                        Ref.HttpRecieve(context);
                        Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd"); //默认时间，以防出错
                        Update<paraCheckArt> update = new Update<paraCheckArt>(Ref,"usp_UpdateArtById");
                        context.Response.Write("<script>alert('更新成功');window.location.href='CheckArt.ashx';</script>");
                    }
                }
                else if(Action =="AddNew")
                {
                    string Save = context.Request["Save"];
                    if (string.IsNullOrEmpty(Save)) //没点
                    {
                         Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        Ref.HttpRecieve(context);
                        Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                        Update<paraCheckArt> update = new Update<paraCheckArt>(Ref, "usp_AddArticle");
                        context.Response.Write("<script>alert('添加成功');window.location.href='CheckArt.ashx';</script>");
                    }
                }
                else if(Action == "Delete")
                {
                    deleteById.Delete(Convert.ToInt32(Ref.map["id"]), "T_Info", false);
                    context.Response.Write("<script>alert('删除成功');window.location.href='CheckArt.ashx';</script>");
                }
            }
            DataTable Alltype  = select.listAllType();
            var data = new
            {
               Action = Action,
               artEdit = Ref.map,
               Alltype = Alltype.Rows
            };

            string html = Admin.HelpClass.commonhelp.RendHtml("ArticleEdit.html",data);
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