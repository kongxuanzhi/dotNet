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
    /// UserInfoEdit 的摘要说明
    /// </summary>
    public class UserInfoEdit : IHttpHandler
    {
        #region edit
        ProcRequest<Maticsoft.Model.T_custum> PR;
        public UserInfoEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_custum>(new Maticsoft.Model.T_custum());
            PR.getModel = new Maticsoft.BLL.T_custum().GetModel;
            PR.update = new Maticsoft.BLL.T_custum().Update;
            PR.add = new Maticsoft.BLL.T_custum().Add;
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
            DataTable ItemsTypes = new Maticsoft.BLL.T_custum().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                ItemsTypes = ItemsTypes.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/UserInfoEdit.html", data);
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