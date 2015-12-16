using CommonHelp.DataConver;
using CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.UI.Edit
{
    /// <summary>
    /// ItemtypeEdit 的摘要说明
    /// </summary>
    public class ItemtypeEdit : IHttpHandler
    {
        #region edit
        ProcRequest<Maticsoft.Model.T_ItemType> PR;
        public ItemtypeEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_ItemType>(new Maticsoft.Model.T_ItemType());
            PR.getModel = new Maticsoft.BLL.T_ItemType().GetModel;
            PR.update = new Maticsoft.BLL.T_ItemType().Update;
            PR.add = new Maticsoft.BLL.T_ItemType().Add;
        }
        public void ProcessRequest(HttpContext context) 
        {
            PR.ProcessRequest(context);
            if (PR.isProcResult)
            {
                DisplayResult(context);
            }
        }
        private void DisplayResult(HttpContext context)
        {
            DataTable ItemsTypes = new Maticsoft.BLL.T_ItemType().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                ItemsTypes = ItemsTypes.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/ItemtypeEdit.html", data);
            context.Response.Write(html);
        }
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