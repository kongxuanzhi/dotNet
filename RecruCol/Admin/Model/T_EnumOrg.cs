using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruCol.Admin.Model
{
    /// <summary>
    /// T_EnumOrg:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_EnumOrg
    {
        public T_EnumOrg()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _password;
        private string _adress;
        private string _briefintrodus;
        private bool _isrecom;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string adress
        {
            set { _adress = value; }
            get { return _adress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string briefIntrodus
        {
            set { _briefintrodus = value; }
            get { return _briefintrodus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isRecom
        {
            set { _isrecom = value; }
            get { return _isrecom; }
        }
        #endregion Model

    }
}