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
    /// OrderManage 的摘要说明
    /// </summary>
    public class OrderManage :  IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region copy
        public OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_Order> SAP;
        public OrderManage()
        {
            SAP = new OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_Order>(new Maticsoft.Model.T_Order());
            SAP.DP.pageSize = 20;
            SAP.DP.procName = "usp_Order";
            SAP.DP.linkHref = this.GetType().FullName.ToString() + ".ashx";
        }

        public void ProcessRequest(HttpContext context)
        {
            SAP.ProcessRequest(context);
            ProceResult(context);
        }
        public void ProceResult(HttpContext context)
        {
            DataTable pageType = new Maticsoft.BLL.Paging().GetAllList().Tables[0];
             int total = 0;
            if(SAP.Ref.map["statu"]==null)
                total = new Maticsoft.BLL.T_Order().GetList("statu='待处理'").Tables[0].Rows.Count;
            else
                total = new Maticsoft.BLL.T_Order().GetList("statu='"+SAP.Ref.map["statu"]+"'").Tables[0].Rows.Count;

            
           var data = new
           {
                PageType = pageType.Rows,

                Dt = SAP.DT.Rows,
                Search = SAP.Ref.map,
                Index = SAP.DP.index,
                Pages = SAP.DP.pages,
                Totalnum = SAP.DP.totalPageCount,
                pageSize = SAP.DP.pageSize,
                Total = total

            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/OrderManage.html", data);
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