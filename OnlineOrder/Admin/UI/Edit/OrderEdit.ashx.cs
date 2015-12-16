using CommonHelp.CommonHelp.operatorSQL;
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
    /// OrderEdit 的摘要说明
    /// </summary>
    public class OrderEdit : IHttpHandler
    {

        #region edit
        ProcRequest<Maticsoft.Model.T_Order> PR;
        public OrderEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_Order>(new Maticsoft.Model.T_Order());
            PR.getModel = new Maticsoft.BLL.T_Order().GetModel;
            PR.update = new Maticsoft.BLL.T_Order().Update;
            PR.add = new Maticsoft.BLL.T_Order().Add;
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
            DataTable ItemsTypes = new Maticsoft.BLL.T_Order().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                ItemsTypes = ItemsTypes.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/OrderEdit.html", data);
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