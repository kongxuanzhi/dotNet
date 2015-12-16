using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RecruCol.CommonHelp.DataConver
{
    public class Reflection<T> 
    {
        public T target { get; set; }  //model 
        public Dictionary<string, object> map { get; set; } //字段名称（列名）列的值
        public Reflection(T target)
        {
            this.target = target;
            map = new Dictionary<string, object>();
            foreach (var property in target.GetType().GetProperties())  //把所有属性字段加在map
            {
                map.Add(property.Name, null);
            }
        }

        public  void  DataRowToModel(DataRow dr)
        {
            Type t = target.GetType();
            int i = 0;
            foreach (var property in target.GetType().GetProperties())
            {
                if (i>=dr.ItemArray.Length) break;
                
                foreach(DataColumn dc in dr.Table.Columns)
                {
                    if (dc.ColumnName == property.Name)
                    {
                        map[property.Name] = dr[dc.ColumnName] == DBNull.Value ? "" : dr[dc.ColumnName].ToString();
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
            Type t = target.GetType();
            foreach (var property in target.GetType().GetProperties())
            {
                map[property.Name] = context.Request[property.Name];
            }
        }
        public void SeachSetCooike(HttpContext context, string Search,string path)
        {
            Type t = target.GetType();
            if (!string.IsNullOrEmpty(Search))
            {
                foreach (var property in t.GetProperties())
                {
                    map[property.Name] = context.Request[property.Name] == "" ? null : context.Request[property.Name];
                    if (map[property.Name] != null)
                    {
                        HttpCookie cookie =   new HttpCookie(property.Name.ToUpper(), Convert.ToString(map[property.Name]));
                        cookie.Expires = DateTime.Now.AddSeconds(10);
                        cookie.Path = path;
                        context.Response.SetCookie(cookie);
                    }
                }
            }
            else
            {
                foreach (var property in t.GetProperties())
                {
                    if(context.Request.Cookies[property.Name.ToUpper()] != null)
                        map[property.Name] = context.Request.Cookies[property.Name.ToUpper()].Value;
                }
            }
        }
        public List<SqlParameter> AddPms()
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            Type t = target.GetType();
            foreach (var property in t.GetProperties())
            {    
                if (map.ContainsKey(property.Name) &&  map[property.Name]!=null)
                    pms.Add(new SqlParameter("@" + property.Name, map[property.Name]));
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
        private void CopyTo()
        {
            if (map == null)
                return;
            Type t = target.GetType();
            foreach (var property in target.GetType().GetProperties())
            {
               
                if (map.ContainsKey(property.Name))
                {
                    object propertyVal = map[property.Name];
                    if (propertyVal != null)
                    {
                        //把map里的键值对赋值到对象类中
                        property.SetValue(property.Name, Convert.ChangeType(Convert.ToString(propertyVal), property.PropertyType), null);
                        //t.InvokeMember(property.Name, BindingFlags.SetProperty, null, target, new object[] { propertyVal });
                    }
                }
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

// FieldInfo property in t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
////| BindingFlags.Public)