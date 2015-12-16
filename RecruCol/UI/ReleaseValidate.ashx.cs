using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.UI
{
    /// <summary>
    /// ReleaseValidate 的摘要说明
    /// </summary>
    public class ReleaseValidate : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            string pos = context.Request["pos"];
            string arttype = context.Request["arttype"];
            string ManCount = context.Request["ManCount"];
            string gender = context.Request["gender"];
            string EduRequir = context.Request["EduRequir"];
            string Context = context.Request["Context"];
            #region 后台验证
            
            
            if (string.IsNullOrEmpty(pos))
            {
                context.Response.Write("<script>alert('请填写招聘或者实习岗位');window.history.go(-1);</script>");
                return;
            }
            if (arttype=="-1")
            {
                context.Response.Write("<script>alert('请填写文章类型');window.history.go(-1);</script>");
                return;
            }
            if (string.IsNullOrEmpty(ManCount))
            {
                context.Response.Write("<script>alert('请填写招聘或者实习人数');window.history.go(-1);</script>");
                return;
            }
            if (gender == "-1")
            {
                context.Response.Write("<script>alert('请选择性别要求');window.history.go(-1);</script>");
                return;
            }
            if (EduRequir == "-1")
            {
                context.Response.Write("<script>alert('请选择学历要求');window.history.go(-1);</script>");
                return;
            }
            if (string.IsNullOrEmpty(Context))
            {
                context.Response.Write("<script>alert('请填写岗位详细要求等信息');window.history.go(-1);</script>");
                return;
            }
            #endregion
            DataTable dt = null;
            string org = "";
            if(context.Session["Login"]!=null)
            {
                dt = (DataTable)context.Session["Login"];
                org = dt.Rows[0][1].ToString();
            }
            else
            {
                context.Response.Write("<script>window.location.href='etplogin.ashx';</script>");
                return;
            }

            golobHelp.ReleaseInfo.InsertInto(pos, org, arttype, ManCount, gender,EduRequir, Context);
            context.Response.Write("<script>alert('发布成功，我们将在一个工作日内完成对您发布的内容的人工检测，谢谢配合！');window.location.href='recruitInfo.ashx ';</script>");
            //context.Response.Write(org+pos + arttype + ManCount + gender +EduRequir+ Context);
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