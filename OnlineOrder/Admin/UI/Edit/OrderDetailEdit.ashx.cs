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
    /// OrderDetailEdit 的摘要说明
    /// </summary>
    public class OrderDetailEdit : IHttpHandler
    {
         #region edit
        ProcRequest<Maticsoft.Model.T_orderItems> PR;
        public OrderDetailEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_orderItems>(new Maticsoft.Model.T_orderItems());
            PR.getModel = new Maticsoft.BLL.T_orderItems().GetModel;
            PR.update = new Maticsoft.BLL.T_orderItems().Update;
            PR.add = new Maticsoft.BLL.T_orderItems().Add;
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
            DataTable items = new Maticsoft.BLL.T_Items().GetAllList().Tables[0];

            var data = new
            {
                Items = items.Rows,
                Model = PR.Ref.map,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/OrderDetailEdit.html", data);
            context.Response.Write(html);
        }
        #endregion

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }
    }
}