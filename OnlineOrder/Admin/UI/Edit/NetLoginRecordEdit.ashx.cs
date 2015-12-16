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
    /// NetLoginRecordEdit 的摘要说明
    /// </summary>
    public class NetLoginRecordEdit : IHttpHandler
    {
        
        #region edit
        ProcRequest<Maticsoft.Model.T_Login> PR;
        public NetLoginRecordEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_Login>(new Maticsoft.Model.T_Login());
            PR.getModel = new Maticsoft.BLL.T_Login().GetModel;
            PR.update = new Maticsoft.BLL.T_Login().Update;
            PR.add = new Maticsoft.BLL.T_Login().Add;
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
            DataTable ItemsTypes = new Maticsoft.BLL.T_Login().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                ItemsTypes = ItemsTypes.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/NetLoginRecordEdit.html", data);
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