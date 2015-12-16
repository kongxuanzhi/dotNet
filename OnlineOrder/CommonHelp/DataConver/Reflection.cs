using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CommonHelp.DataConver
{
    public class Reflection<T> 
    {
        public T target { get; set; }  //model 
        public Dictionary<string, dynamic> map { get; set; } //字段名称（列名）列的值
        public List<string> modelNames;
        public Reflection(T target)
        {
            this.target = target;
            map = new Dictionary<string, dynamic>();
            modelNames = new List<string>();
            foreach (var property in target.GetType().GetProperties())  //把所有属性字段加在map
            {
                map.Add(property.Name, null);
                modelNames.Add(property.Name);
            }
        }

        public  void  DataRowToModel(DataRow dr)
        {
            int i = 0;
            foreach (string name in modelNames)
            {
                if (i>=dr.ItemArray.Length) break;
                
                foreach(DataColumn dc in dr.Table.Columns)
                {
                    if (dc.ColumnName == name)
                    {
                        map[name] = dr[dc.ColumnName] == DBNull.Value ? "" : dr[dc.ColumnName].ToString();
                        i++;
                        break;
                    }
                }
            }
        }
        /// <summary>
        ///用于对得到的数据，进行update或者addnew
        /// </summary>
        /// <param name="context"></param>
        public  void HttpRecieve(HttpContext context)
        {
            foreach (string name in modelNames)
            {
                map[name] = context.Request[name] == "" ? null : context.Request[name];
            }
        }
        public void SeachSetCooike(HttpContext context, string Search,string path)
        {
            if (!string.IsNullOrEmpty(Search))
            {

                foreach (string name in modelNames)
                {
                    map[name] = context.Request[name] == "" ? null : context.Request[name];
                    if (map[name] != null)
                    {
                        HttpCookie cookie = new HttpCookie(name.ToUpper(), Convert.ToString(map[name]));
                        cookie.Expires = DateTime.Now.AddSeconds(10);
                        cookie.Path = path;
                        context.Response.SetCookie(cookie);
                    }
                }
            }
            else
            {
                foreach (string name in modelNames)
                {
                    if (context.Request.Cookies[name.ToUpper()] != null)
                        map[name] = context.Request.Cookies[name.ToUpper()].Value;
                }
            }
        }
        public List<SqlParameter> AddPms()
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            foreach (string name in modelNames)
            {
                if (map.ContainsKey(name) && map[name] != null)
                    pms.Add(new SqlParameter("@" + name, map[name]));
            }
            return pms;
        }
        #region 没必要将map值赋值到实例对象
        //  Ref.map.ContainsKey(property.Name) && Ref.map[property.Name] != null
        //object propertyVal = property.GetValue(target, null);
       
        /// <summary>
        /// 循环的通过Dictionary 给一个类的属性赋值 用于update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="map"></param>
        /// <param name="target"></param>
        public void CopyTo()
        {
            if (map == null)
                return;
            Type t = target.GetType();
            foreach (var property in target.GetType().GetProperties())  //把所有属性字段加在map
            {
                if (map.ContainsKey(property.Name) && map[property.Name] != null)
                {
                    property.SetValue(target, Convert.ChangeType(map[property.Name], property.PropertyType), null);
                }
            }
        }
        public void CopyToMap()
        {
            Type t = target.GetType();
            foreach (var property in target.GetType().GetProperties())  //把所有属性字段加在map
            {
                map[property.Name] =  property.GetValue(target, null);
            }
        }
        #endregion
    }
}

//string temp = property.Name;
//int loc1 = temp.IndexOf("<");
//if (loc1 > -1)
//{
//    int loc2 = temp.IndexOf(">");
//    temp = temp.Substring(loc1 + 1, loc2 - 1);
//}
//给那些传进来有参数的属性赋值

// FieldInfo property in t.GetFields(BindingFlags.NonP  ublic | BindingFlags.Instance
////| BindingFlags.Public)