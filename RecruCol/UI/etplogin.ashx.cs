using Cocu;
using RecruCol.golobHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RecruCol.UI
{
    /// <summary>
    /// etplogin 的摘要说明
    /// </summary>
    public class etplogin : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            HttpCookie cookUsrName = context.Request.Cookies["UsrName"];
            HttpCookie cookPassWord = context.Request.Cookies["PassWord"];
            string UsrName = "", PassWord = "";
            if(cookUsrName!=null&&cookPassWord!=null)
            {
                UsrName = cookUsrName.Value;
                PassWord = cookPassWord.Value;
            }
            string save = context.Request["save"];
            string WrongInfo = "";
            if (!string.IsNullOrEmpty(save))
            {
                UsrName = context.Request["UsrName"];
                PassWord = context.Request["PassWord"];
                string check = context.Request["check"];
                if (!string.IsNullOrEmpty(UsrName) && !string.IsNullOrEmpty(PassWord))
                {
                    DataTable dt = loginExt.Exsit(UsrName);
                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0][2].ToString() == CocuMD5.StrToMD5(PassWord))
                        {
                            string Validatecode = context.Request["Vcode"];
                            if (context.Session["validatecode"] != null && context.Session["validatecode"].ToString() == Validatecode)
                            {
                                if (check == "on")
                                {
                                    context.Response.SetCookie(new HttpCookie("UsrName", UsrName));
                                    context.Response.SetCookie(new HttpCookie("PassWord", PassWord));
                                    context.Response.Cookies["UsrName"].Expires = DateTime.Now.AddDays(1);
                                    context.Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(1);
                                }
                                context.Session["Login"] = dt;
                                context.Response.Redirect("ReleaseInfo.ashx");
                            }
                            else
                            {
                                WrongInfo = "验证码不正确";
                            }
                        }
                        else
                        {
                            WrongInfo = "密码不正确";
                        }
                    }
                    else if (dt.Rows.Count > 1)
                    {
                        WrongInfo = "存在多个用户，错误！！";

                    }
                    else
                    {
                        WrongInfo = "用户不存在";
                    }
                }
                else
                {
                    WrongInfo = "请输入用户名或者密码";
                }
            }

            var data = new
            {
                Maintitle = "企业登录",
                WrongInfo = WrongInfo, UsrName = UsrName, PassWord = PassWord
            };
            string html = Commonhelp.DataConver.rendhtml.RendHtml("etplogin.html", data);
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