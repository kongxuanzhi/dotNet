using Maticsoft.DBUtility;
using RecruCol.Admin.Model;
using RecruCol.CommonHelp.DataConver;
using RecruCol.CommonHelp.operatorSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// baDetail 的摘要说明
    /// </summary>
    public class baDetail : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            
            string strid = context.Request["id"];
            int id = 1;
            try
            {
                id = Convert.ToInt32(strid);
            }
            catch{}
            Reflection<T_EnumOrg> Ref = new Reflection<T_EnumOrg>(new T_EnumOrg());
            selectById<T_EnumOrg> select = new selectById<T_EnumOrg>("usp_T_EnumOrg_GetModel", Ref, id); //这里需要制定存储过程，要去用东软生成
            var data = new 
            {
                Maintitle =Ref.map["name"],
                BriefIntrodus = Ref.map
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("baDetail.html", data);
            context.Response.Write(html);
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