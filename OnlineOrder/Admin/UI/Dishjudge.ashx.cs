using CommonHelp;
using CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineOrder.Admin.UI
{
    /// <summary>
    /// Dishjudge 的摘要说明
    /// </summary>
    public class Dishjudge : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region copy
       
        public OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_ItemMsg> SAP;
        public Dishjudge()
        {
            SAP = new OnlineOrder.CommonHelp.DataConver.SearchAndpage<Maticsoft.Model.T_ItemMsg>(new Maticsoft.Model.T_ItemMsg());
            SAP.DP.pageSize = 20;
            SAP.DP.procName = "usp_ItemMsg";
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
              
            int total = new Maticsoft.BLL.T_ItemMsg().GetAllList().Tables[0].Rows.Count;

            var data = new
            {
                PageType = pageType.Rows,
                
                Dt = SAP.DT.Rows,
                Search = SAP.Ref.map,
                
                Index = SAP.DP.index,
                Totalnum = SAP.DP.totalPageCount,
                pageSize = SAP.DP.pageSize,
                
                Total = total
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/Dishjudge.html", data);
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