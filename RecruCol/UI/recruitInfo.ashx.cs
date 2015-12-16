using Maticsoft.DBUtility;
using RecruCol.CommonHelp;
using RecruCol.golobHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecruCol.UI
{
    /// <summary>
    /// recruitInfo 的摘要说明
    /// </summary>
    public class recruitInfo : dividePage, IHttpHandler
    {
        public List<object> PageHref(string type, int totalnum, int index, string linkHref)
        {
            List<object> page = new List<object>();
            int start = 1, end = 1;
            CommonHelp.dividePage.pageStartToEnd(totalnum, index, ref start, ref end);
            //将链接写到object中
            for (int i = start; i <= end; i++)
            {
                object temp = new { href = linkHref+"?Action=" + type + "&index=" + i, index = i };
                page.Add(temp);
            }
            return page;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            #region 类别判断 --招聘或者实习参数
            string Action = context.Request["Action"];
            string type = "";
            if (Action == "RecruitInfo" || Action == "招聘") //工作岗位
            {
                type = "招聘";
            }
            else if (Action == "Trainee" || Action == "实习") // 实习岗位
            {
                type = "实习";

            }
            else if (Action == "Recruiting" || Action == "招聘会")
            {
                type = "招聘会";
            }
            else
                type = "招聘";
            #endregion
            #region 查询参数 使用了cookie 当提交表单时加入cookie，当在当前页操作时，就读取cookie
            string posName = "", orgName = "", workAddress = "";
            if(!(Action == "RecruitInfo"||Action == "Trainee"||Action == "Recruiting"))
            {
                string save =  context.Request["Save"]; //点了查找按钮
                if (!string.IsNullOrEmpty(save))
                {
                    posName = context.Request["posName"] == null ? "" : context.Request["posName"];
                    orgName = context.Request["orgName"] == null ? "" : context.Request["orgName"];
                    workAddress = context.Request["workAddress"] == null ? "" : context.Request["workAddress"];
                    context.Response.SetCookie(new HttpCookie("posName", posName));
                    context.Response.SetCookie(new HttpCookie("orgName", orgName));
                    context.Response.SetCookie(new HttpCookie("workAddress", workAddress));
                }
                else
                {
                    posName = context.Request.Cookies["posName"] == null ? "" : context.Request.Cookies["posName"].Value;
                    orgName = context.Request.Cookies["orgName"] == null ? "" : context.Request.Cookies["orgName"].Value;
                    workAddress = context.Request.Cookies["workAddress"] == null ? "" : context.Request.Cookies["workAddress"].Value;     
                }
            }
            else //不在当前页操作
            {
                posName = ""; orgName = ""; workAddress = "";
            }
            #endregion
            #region 分页参数
            //dividePage page = new recruitInfo(15, "numbypage");
            pageSize = 12;
            procName = "numbypage";
            //dividePage page2 = page;
            string Index = context.Request["index"];
            if(!string.IsNullOrEmpty(Index))
            {
                index = Convert.ToInt32(Index);
            }
            #endregion
          
            #region 实现不知道有多少个查询用到的参数，所以写在外面 加参数
            List<SqlParameter> parameters = AddPms(posName, orgName, workAddress, type);
            AddParams(parameters.ToArray());// 把查询关键字放进去
            #endregion

            DataTable reInfo  =  proc(procName,pms.ToArray(), "dt").Tables[0];//读表
            totalPageCount = (int)(returnValue.Value);//读返回值

            #region 设置页数展示  百度搜索的方式
            linkHref = "recruitInfo.ashx";
            List<object> pagediv = PageHref(type, totalPageCount, index, linkHref);
            #endregion
            #region 将需要的数据读到前台
            var pubInfo = new 
            {
                Type = type, RecuritInfo = reInfo.Rows, PosName = posName,
                OrgName = orgName,
                WorkAddress = workAddress,
                Pages = pagediv,
                Index = index,
                Totalnum =totalPageCount,
                Maintitle = type + "信息"               
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("recruitInfo.html", pubInfo);
            context.Response.Write(html);
            #endregion
        }

        private List<SqlParameter> AddPms(string posName, string orgName, string workAddress, string type)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(posName)) parameters.Add(new SqlParameter("@title", posName));
            if (!string.IsNullOrEmpty(orgName)) parameters.Add(new SqlParameter("@name", orgName));
            if (!string.IsNullOrEmpty(workAddress)) parameters.Add(new SqlParameter("@adress", workAddress));
            if (!string.IsNullOrEmpty(type)) parameters.Add(new SqlParameter("@type", type));
            return parameters;
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








//json   datatable
//[{"id":14,"title":"1234","name":"江西@科技有限责任公司","pubTime":"2345","adress":"345","type":"招聘","num":1},
//{"id":1,"title":"江西理工招聘会","name":"江西@科技有限责任公司","pubTime":"2015-05-17","adress":"345","type":"招聘","num":2},
//{"id":6,"title":"找程序猿","name":"江西@科技有限责任公司","pubTime":"05 17 2015 11:22AM","adress":"345","type":"招聘","num":3},
//{"id":7,"title":"找程序猿","name":"江西@科技有限责任公司","pubTime":"05 17 2015 11:22AM","adress":"345","type":"招聘","num":4}]