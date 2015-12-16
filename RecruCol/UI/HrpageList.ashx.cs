using RecruCol.CommonHelp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// HrpageList 的摘要说明
    /// </summary>
    public class HrpageList :dividePage, IHttpHandler
    {
        public Dictionary<string,string>  types{ get; set; }
        public HrpageList()
        {
            types = new Dictionary<string, string>();
            types.Add("PersonManage","人力管理"); types.Add("Review","招聘面试");types.Add("Managestir","管理激励");
            types.Add("TaskCheck","业绩考核");types.Add("MoneyExt","薪酬福利");types.Add("NormalLaw","法律常识");
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string ENArttype = context.Request["Arttype"];
            string ZHArttype = types[ENArttype];
            pageSize = 10;  //覆盖父类中定义的变量，改变当前页显示的行数
            procName = "usp_info";  
            string strIndex = context.Request["index"];
            if (!string.IsNullOrEmpty(strIndex))
            {
                index = Convert.ToInt32(strIndex);
            }
            List<SqlParameter> parametes = AddPms(ZHArttype, -1, "Hr");
            AddParams(parametes.ToArray());
            DataTable dt = proc(procName, pms.ToArray(), "dt").Tables["dt"];
            totalPageCount = (int)returnValue.Value; //执行完存储过程才能得到返回值

            linkHref = "HrpageList.ashx";
            List<object> pages = PageHref(ENArttype, linkHref, totalPageCount, index);

            var data = new
            {
                Maintitle = "Hr分享文章列表",
                Arttype = ZHArttype,
                EnArttype = ENArttype,
                Index = index,
                totalCount = totalPageCount,
                Acticle = dt.Rows,
                Pages = pages
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("HrpageList.html", data);
            context.Response.Write(html);
        }
        private List<SqlParameter> AddPms(string ArtType, int top, string bigType)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            #region 参数限制

            if (!string.IsNullOrEmpty(ArtType))
            {
                pms.Add(new SqlParameter("@type", ArtType));
            }
            pms.Add(new SqlParameter("@top", top));
            pms.Add(new SqlParameter("@bigType", bigType));
            #endregion
            return pms;
        }
        public List<object> PageHref(string type, string linkHref, int totalnum, int index)
        {
            List<object> page = new List<object>();
            int start = 1, end = 1;
            CommonHelp.dividePage.pageStartToEnd(totalnum, index, ref start, ref end);
            //将链接写到object中
            for (int i = start; i <= end; i++)
            {
                object temp = new { href = linkHref + "?Arttype=" + type + "&index=" + i, index = i };
                page.Add(temp);
            }
            return page;
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













//public static string EnumType(string type)
//        {
//            switch(type)
//            {
//                case "Report":
//                    return "专题报道";
//                case "HotInfo":
//                    return "热门信息";
//                case "ResumeGuid":
//                    return "简历指导";
//                case "FaceTest":
//                    return "面试技巧";
//                case "LookJob":
//                    return "求职实录";
//                case "LoofForward":
//                    return "职场眺望";

//                case "PersonManage":
//                    return "人力管理";
//                case "Review":
//                    return "招聘面试";
//                case "Managestir":
//                    return "管理激励";
//                case "TaskCheck":
//                    return "业绩考核";
//                case "MoneyExt":
//                    return "薪酬福利";
//                case "NormalLaw":
//                    return "法律常识";     
//                default:
//                    return "专题报道";
//            }
//        }