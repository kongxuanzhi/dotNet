using OnlineOrder.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace OnlineOrder.CommonHelp.DataConver
{
    /// <summary>
    /// DataTableToExcel 的摘要说明
    /// </summary>
    public class DataTableToExcel : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/ms-excel";
            string tableName =  context.Request["TabName"];
            
            DataTable dt = selectFromTable.select(tableName);
            if(dt!=null)
                dataTableToexcel.CreateExcel(context, dt, "application/ms-excel", Change(tableName));
        }

        private string Change(string tableName)
        {
            switch(tableName)
            {
                case "T_Items":
                    return "菜肴列表";
                case "T_ItemType":
                    return "菜肴种类";
                case "T_ItemMsg":
                    return "菜肴评论";
                case "T_Order":
                    return "订餐列表";
                case "T_Login":
                    return "网站登录";
                case "T_orderItems":
                    return "订餐记录";
                case "T_priceType":
                    return "价格种类";
                case "T_custum":
                    return "顾客信息";
                default:
                    return "错误";
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}