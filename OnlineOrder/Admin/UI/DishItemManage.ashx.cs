using CommonHelp;
using CommonHelp.DataConver;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Maticsoft.DBUtility;
using OnlineOrder.CommonHelp.DataConver;

namespace OnlineOrder.Admin.UI
{
    /// <summary>
    /// DishItemManage 的摘要说明
    /// 主要是对菜肴的展示，查找和分页，对表T_Items的操作
    /// </summary>
    public class DishItemManage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region copy
        public SearchAndpage<T_Items> SAP;
        public DishItemManage()
        {
            SAP = new SearchAndpage<T_Items>(new T_Items());
            SAP.DP.pageSize = 20;
            SAP.DP.procName = "usp_Items";
            SAP.DP.linkHref = this.GetType().FullName.ToString()+".ashx";
        }
        
        public void ProcessRequest(HttpContext context)
        {
            SAP.ProcessRequest(context);
            ProceResult(context);
        }
        public void ProceResult(HttpContext context)
        {
            DataTable priceType = new Maticsoft.BLL.T_priceType().GetAllList().Tables[0];
            DataTable type = new Maticsoft.BLL.T_ItemType().GetAllList().Tables[0];
          
            DataTable pageType = new Maticsoft.BLL.Paging().GetAllList().Tables[0];
            int total = new Maticsoft.BLL.T_Items().GetAllList().Tables[0].Rows.Count;
            
            var data = new
            {
                PageType =pageType.Rows,
                type = type.Rows,
                priceType =priceType.Rows,

                Dt = SAP.DT.Rows,
                Search = SAP.Ref.map,
                Index = SAP.DP.index,
                Totalnum = SAP.DP.totalPageCount,
                pageSize = SAP.DP.pageSize,
                Total = total
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("Admin/templates/DishItemManage.html", data);
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