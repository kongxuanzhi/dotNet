using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineOrder.CommonHelp.DataConver
{
    public class dataTableToexcel
    {
        public delegate DataSet GetAllList();
        public GetAllList getAllList;
       
        //DataTable ddt = GetExcelData("D:\\Data.xls", "sheet1");
        public  static void CreateExcel(HttpContext context, DataTable dt, string FileType, string FileName)
        {
            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.Buffer = true;
            context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls\"");
            context.Response.ContentType = FileType;
            string colHeaders = string.Empty;
            string ls_item = string.Empty;
            DataRow[] myRow = dt.Select();
            int i = 0;
            int cl = dt.Columns.Count;
            foreach (DataRow row in myRow)
            {
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))
                    {
                        ls_item += row[i].ToString() + "\n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "\t";
                    }
                }
                context.Response.Output.Write(ls_item);
                ls_item = string.Empty;
            }
            context.Response.Output.Flush();
            context.Response.End();
        }
        /// <summary>
        /// 获取指定路径、指定工作簿名称的Excel数据
        /// </summary>
        /// <param name="FilePath">文件存储路径</param>
        /// <param name="WorkSheetName">工作簿名称</param>
        /// <returns>如果争取找到了数据会返回一个完整的Table，否则返回异常</returns>
        public static DataTable GetExcelData(string FilePath, string WorkSheetName)
        {
            DataTable dtExcel = new DataTable();
            OleDbConnection con = new OleDbConnection(GetExcelConnection(FilePath));
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from [" + WorkSheetName + "$]", con);
            //读取
            con.Open();
            adapter.FillSchema(dtExcel, SchemaType.Mapped);
            adapter.Fill(dtExcel);
            con.Close();
            dtExcel.TableName = WorkSheetName;
            //返回
            return dtExcel;
        }

        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private static string GetExcelConnection(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                throw new Exception("指定的Excel文件不存在！");
            }
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended properties=\"Excel 8.0;Imex=1;HDR=Yes;\"";
            //@"Provider=Microsoft.Jet.OLEDB.4.0;" +
            //@"Data Source=" + strFilePath + ";" +
            //@"Extended Properties=" + Convert.ToChar(34).ToString() +
            //@"Excel 8.0;" + "Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
        }
       
        //通过流，伪xls，行不通
        public static void ExportExcel(System.Data.DataTable dt, StreamWriter w)
        {
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    w.Write(dt.Columns[i]);
                    w.Write("\t");
                }
                w.Write("\n");

                object[] values = new object[dt.Columns.Count];
                foreach (DataRow dr in dt.Rows)
                {
                    values = dr.ItemArray;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        w.Write(values[i]);
                        w.Write("\t");
                    }
                    w.Write("\n");

                }
                w.Flush();
                w.Close();
            }
            catch
            {
                w.Close();
            }
        }
    }
}




  //context.Response.ContentType = "application/octet-stream";

  //          DataTable dt = new Maticsoft.BLL.T_ItemType().GetAllList().Tables[0];
  //          string path = @"/downloadFiles/"+ System.Guid.NewGuid().ToString()+".doc";
  //          string xcfpath = HttpContext.Current.Server.MapPath(path);
  //          StreamWriter tw = new StreamWriter(xcfpath, false, Encoding.Default);
            
  //          System.IO.FileInfo lFileInfo = new System.IO.FileInfo(xcfpath);
  //          context.Response.WriteFile(lFileInfo.FullName);
  //          context.Response.End();