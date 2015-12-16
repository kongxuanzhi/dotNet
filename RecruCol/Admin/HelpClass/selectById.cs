using Maticsoft.DBUtility;
using RecruCol.Admin.Model;
using RecruCol.Admin.paraClass;
using RecruCol.CommonHelp.DataConver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace RecruCol.Admin.HelpClass
{
    public class select
    {
        /// <summary>
        /// 利用反射将一行datatable转化为对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
       
        #region 旧
        //p.title = artEdit.Rows[0].ItemArray[1].ToString();
        //p.pubTime = artEdit.Rows[0].ItemArray[2].ToString();
        //p.author = artEdit.Rows[0].ItemArray[3].ToString();
        //p.src = artEdit.Rows[0].ItemArray[4].ToString();
        //p.article = artEdit.Rows[0].ItemArray[5].ToString();
        //p.type = artEdit.Rows[0].ItemArray[6].ToString();
        //p.isLegal = Convert.ToInt32(artEdit.Rows[0].ItemArray[7]).ToString();
        //p.bigType = artEdit.Rows[0].ItemArray[9].ToString();
        #endregion
        #region 旧
        public static T_recuritInfo selectById(int Id)
        {
            string procName = "usp_SelectArtById";
            List<SqlParameter> pms = new List<SqlParameter>();
            if (Id > 0)
            {
                pms.Add(new SqlParameter("@Id", Id));
            }
            else
            {
                return null;
            }
            DataTable dt = DbHelperSQL.RunProcedure(procName, pms.ToArray(), "dt").Tables["dt"];
            Reflection<T_recuritInfo> Ref = new Reflection<T_recuritInfo>(new T_recuritInfo());
            Ref.DataRowToModel(dt.Rows[0]);
            return (T_recuritInfo)Ref.target;
        }
        
        //public static void updateById(int Id, string title, string pubTime, string author, string src, string article, string type,int isLegal, string bigType)
        //{
        //    string procName = "usp_UpdateArtById";
        //    List<SqlParameter> pms = new List<SqlParameter>();
        //    if (Id > 0)  pms.Add(new SqlParameter("@id", Id));
        //    pms.Add(new SqlParameter("@title", title));
        //    pms.Add(new SqlParameter("@pubTime", pubTime));
        //    pms.Add(new SqlParameter("@author", author));
        //    pms.Add(new SqlParameter("@src", src));
        //    pms.Add(new SqlParameter("@article", article));
        //    pms.Add(new SqlParameter("@type", type));
        //    pms.Add(new SqlParameter("@isLegal", isLegal));
        //    pms.Add(new SqlParameter("@bigType", bigType));
        //    DbHelperSQL.RunProcedure(procName,pms.ToArray());
        //}
        //public static void AddNew(string title, string pubTime, string author, string src, string article, string type, int isLegal, string bigType)
        //{
        //    string procName = "usp_AddArticle";
        //    List<SqlParameter> pms = new List<SqlParameter>();
        //    pms.Add(new SqlParameter("@title", title));
        //    pms.Add(new SqlParameter("@pubTime", pubTime));
        //    pms.Add(new SqlParameter("@author", author));
        //    pms.Add(new SqlParameter("@src", src));
        //    pms.Add(new SqlParameter("@article", article));
        //    pms.Add(new SqlParameter("@type", type));
        //    pms.Add(new SqlParameter("@isLegal", isLegal));
        //    pms.Add(new SqlParameter("@bigType", bigType));
        //    DbHelperSQL.RunProcedure(procName, pms.ToArray());
        //}
        #endregion
        //public static void Delete(string procName,int id,string tabName,int isFDelete)
        //{
        // //   假删除
        //    //string procName = "usp_DeleteById";
        //    List<SqlParameter> pms = new List<SqlParameter>();
        //    if(id<=0)return;
        //    if (isFDelete==1) //1 是真删除，0是假删除
        //    {

        //    }
        //    pms.Add(new SqlParameter("@fDelete", isFDelete));
        //    pms.Add(new SqlParameter("@id", id));
        //    DbHelperSQL.RunProcedure(procName, pms.ToArray());
        //}
        internal static void DeleteRec(int id)
        {
            //真删除
            string procName = "usp_DeleteRecById";
            List<SqlParameter> pms = new List<SqlParameter>();
            if (id <= 0) return;
            pms.Add(new SqlParameter("@id", id));
            DbHelperSQL.RunProcedure(procName, pms.ToArray());
        }
        internal static DataTable listAllType()
        {
            string sql = "select * from T_ArticleType";
            return DbHelperSQL.Query(sql).Tables[0];
        }

        internal static DataTable selectRecById(int Id)
        {
            string procName = "usp_SelectRecById";
            List<SqlParameter> pms = new List<SqlParameter>();
            if (Id > 0)
            {
                pms.Add(new SqlParameter("@Id", Id));
            }
            else
            {
                return null;
            }
            return DbHelperSQL.RunProcedure(procName, pms.ToArray(), "dt").Tables["dt"];
        }
        internal static DataTable SelecteduType()
        {
             string sql = "select * from eduType";
             return  DbHelperSQL.Query(sql).Tables[0];
        }
    }
}