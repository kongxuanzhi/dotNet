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
    /// DishjudgeEdit 的摘要说明
    /// </summary>
    public class DishjudgeEdit : IHttpHandler
    {
         #region edit
        //===========调用方法 及一些参数的初始化环节======================
        ProcRequest<Maticsoft.Model.T_ItemMsg> PR;
        public DishjudgeEdit()
        {
            PR = new ProcRequest<Maticsoft.Model.T_ItemMsg>(new Maticsoft.Model.T_ItemMsg());
            PR.getModel = new Maticsoft.BLL.T_ItemMsg().GetModel;
            PR.update = new Maticsoft.BLL.T_ItemMsg().Update;
            PR.add = new Maticsoft.BLL.T_ItemMsg().Add;
        }
        //=============数据处理环节   基本不需要变，如果有其他请求类型，重写方法===========================
        public void ProcessRequest(HttpContext context) 
        {
            PR.ProcessRequest(context);
            if (PR.isProcResult)
            {
                DisplayResult(context);
            }
        }
        //===============与前台的接口-- 数据展示环节===========
        private void DisplayResult(HttpContext context)
        {
            DataTable Items = new Maticsoft.BLL.T_Items().GetAllList().Tables[0];
            var data = new
            {
                Model = PR.Ref.map,
                Items = Items.Rows,
                Action = PR.Action
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Edit/DishjudgeEdit.html", data);
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