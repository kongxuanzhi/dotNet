using CommonHelp;
using CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.UI
{
    /// <summary>
    /// OrderDetailManage 的摘要说明
    /// </summary>
    public class OrderDetailManage :  IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        #region copy
        
        public OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_orderItems> SAP;
        public OrderDetailManage()
        {
            SAP = new OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_orderItems>(new Maticsoft.Model.T_orderItems());
            SAP.DP.pageSize = 20;
            SAP.DP.procName = "usp_orderItems";
            SAP.DP.linkHref = this.GetType().FullName.ToString()+".ashx";
        }
        
        public void ProcessRequest(HttpContext context)
        {
            SAP.ProcessRequest(context);
            ProceResult(context);
        }
        public void ProceResult(HttpContext context)
        {
            DataTable pageType = new Maticsoft.BLL.Paging().GetAllList().Tables[0];

            int total = new Maticsoft.BLL.T_orderItems().GetAllList().Tables[0].Rows.Count;

            var data = new
            {
                PageType = pageType.Rows,

                Dt = SAP.DT.Rows,
                Search = SAP.Ref.map,
                Index = SAP.DP.index,
                Pages = SAP.DP.pages,
                pageSize = SAP.DP.pageSize,

                Totalnum = SAP.DP.totalPageCount,
                Total = total
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/OrderDetailManage.html", data);
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