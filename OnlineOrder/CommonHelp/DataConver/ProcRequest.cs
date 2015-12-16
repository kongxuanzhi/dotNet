using CommonHelp.CommonHelp.operatorSQL;
using CommonHelp.operatorSQL;
using OnlineOrder.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelp.DataConver
{

    public class ProcRequest<T> : IHttpHandler
    {
        public Reflection<T> Ref;

        public string Action;
        public bool isProcResult;  //是否要展现在页面上

        public delegate T GetModel(long id);
        public delegate bool Update(T model);
        public delegate int Add(T model);

        #region 必须的
        #region 指明增删改的所对应的方法 当然可以随便指向  泛型的
        public GetModel getModel;
        public Update update;
        public Add add;
        #endregion

        #region 处理结果完成后的效果，跳转或者提示等，可以重载
        public ResultProcess.AddResultProc addResult;
        public ResultProcess.DeleteResultProc deleteResult;
        public ResultProcess.UpdateResultProc updateResult;
        #endregion

        #endregion

        #region 可选的
        public ResultProcess.DisplayBeforeAddNew DBA;   //新增内容时，显示在页面上的，如当前时间
        public ResultProcess.ProcOtherRequest POR;  /*处理其他非字符串类型的输入*/
        #endregion
        private Type type; 
        public ProcRequest(T model)
        {
            Ref = new Reflection<T>(model); // 变化
            type = model.GetType();
            DBA = this.dba;
            POR = this.por;
            isProcResult = true;  

            addResult = ResultProcess.addResult;
            deleteResult = ResultProcess.deleteResult;
            updateResult = ResultProcess.updateResult;

        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            Action = context.Request["Action"];

            if (!string.IsNullOrEmpty(Action))
            {
                try
                {
                    if (Action != "AddNew")
                        Ref.map["id"] = Convert.ToInt32(context.Request["id"]);
                }
                catch
                {
                    context.Response.Write("<script>document.write('您访问的内容不存在或者已被删除!');window.location.go(-1);</script>");
                }
                if (Action == "Edit")
                {
                    string Save = context.Request["Save"];
                    if (string.IsNullOrEmpty(Save)) //没有点击提交按钮，第一次进入显示内容
                    {
                        /*【改】*/
                        Ref.target = getModel(Convert.ToInt64(Ref.map["id"]));
                        Ref.CopyToMap();
                        //CommonHelp.operatorSQL.selectById<Maticsoft.Model.T_custum> content = new CommonHelp.operatorSQL.selectById<Maticsoft.Model.T_custum>("usp_SelectArtById", Ref, Convert.ToInt32(Ref.map["id"]));
                    }
                    else //将数据更改， 读到所有数据，插入到数据库
                    {
                        
                        Ref.HttpRecieve(context);//从前台得到更新后的数据
                        /*【改】*/
                        // Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd"); //不要用户改的，有需要插入数据库，为防出错
                        /*【改】*/
                        POR(context);//处理其他的请求，比如图片之类的文件
                        Ref.CopyTo();
                        bool isSuccess = update(Ref.target); //【改】更新的存储过程
                        updateResult(isSuccess, context);
                        isProcResult = false;
                    }
                }
                else if (Action == "AddNew")
                {
                    string Save = context.Request["Save"];
                    if (string.IsNullOrEmpty(Save)) //没点
                    {
                        DBA();
                        /*【改】*/
                        //Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd"); //一般是空，不需要传值过去
                    }
                    else
                    {
                        Ref.HttpRecieve(context);
                        POR(context);  //处理其他的请求，比如图片之类的文件
                        #region 增加尝试

                        //数据可以在这里进行过滤，可以放到BLL里进行处理
                        /*【改】*/
                        //Ref.map["pubTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                        //Ref.map["id"] = null;  //这里插入不需要id，插入操作不需要id  //由于上面默认id=1，而新增不需要id，所以设为null
                        /*【改】*/
                        #endregion
                        Ref.CopyTo();
                        int rowEffect = add(Ref.target);
                        //Update<Maticsoft.Model.T_custum> update = new Update<Maticsoft.Model.T_custum>(Ref.target);//【改】存储过程，
                         addResult(rowEffect, context);
                         isProcResult = false;
                    }
                }
                else if (Action == "Delete")
                {
                    /*【改】*/
                    deleteById.Delete(Convert.ToInt32(Ref.map["id"]), type.Name, true); //【改】存储过程，是否真删除
                    deleteResult(context);
                    isProcResult = false;
                }
            }
        }
        public void dba()
        { }
        public void por(HttpContext context)
        { }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}