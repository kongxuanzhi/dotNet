using CommonHelp.DataConver;
using CommonHelp.operatorSQL;
using OnlineOrder.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.UI.Edit
{
    /// <summary>
    /// DishItemEdit 的摘要说明
    /// </summary>
    public class DishItemEdit: IHttpHandler
    {
        #region edit

        #region 调用方法 及一些参数的初始化环节
        //===========调用方法 及一些参数的初始化环节======================
        ProcRequest<Maticsoft.Model.T_Items> PR;
        public  DishItemEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_Items>(new Maticsoft.Model.T_Items());
            PR.getModel = new Maticsoft.BLL.T_Items().GetModel;
            PR.update = new Maticsoft.BLL.T_Items().Update;
            PR.add = new Maticsoft.BLL.T_Items().Add;
            PR.POR = this.por;
         }
        #endregion
        #region 数据处理环节   基本不需要变，如果有其他请求类型，重写方法
        private void por(HttpContext context)
        {
            string conte = context.Request["itemsName"];
            HttpPostedFile pic = context.Request.Files["pic"];
             if (pic != null)
             {
                 string path = @"../../UploadImage/菜肴图片/";
                 string newFileName = pic.FileName;
                 pic.SaveAs(context.Server.MapPath(path + newFileName));
                 PR.Ref.map["pic"] = newFileName;
             }
        }
        public void ProcessRequest(HttpContext context) 
        {
            PR.ProcessRequest(context);
            if (PR.isProcResult)
            {
                DisplayResult(context);
            }
        }
        #endregion
        #region 与前台的接口-- 数据展示环节
        private void DisplayResult(HttpContext context)
        {
            DataTable ItemsTypes = new Maticsoft.BLL.T_ItemType().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                ItemsTypes = ItemsTypes.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/DishItemEdit.html", data);
            context.Response.Write(html);
        }
        #endregion
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}






