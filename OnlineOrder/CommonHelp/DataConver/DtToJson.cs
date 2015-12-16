using Maticsoft.DBUtility;
using CommonHelp.CommonHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace CommonHelpDb.Dataconver
{
    //已测试
    public static class DtToJson
    {
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
        public static string DataTableToJson(DataTable reInfo)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            foreach (DataRow item in reInfo.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in reInfo.Columns)
                {
                    row.Add(col.ColumnName, item[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        //对应的结果如下
        /*
            [
            {"Id":1,"Name":"xiao","Age":12,"Email":"34sdf","Class":3},
            {"Id":9,"Name":"234","Age":13,"Email":"234","Class":2},
            {"Id":10,"Name":"3453","Age":45643,"Email":"5676","Class":4},
            {"Id":11,"Name":"孔1龙飞","Age":210,"Email":"8175248646@qq.com","Class":3},
            {"Id":14,"Name":"33","Age":2,"Email":"3455","Class":2},
            {"Id":18,"Name":"23","Age":234,"Email":"123","Class":5}
            ]
         */
    }
}