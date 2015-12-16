using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruCol.Admin.Model
{
    /// <summary>
    /// T_recuritInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_recuritInfo
    {
        public T_recuritInfo()
        { }
        #region Model
        private long _id;
        private string _title;
        private long _orgid;
        private string _pubtime;
        private string _posdetail;
        private string _jobrequir;
        private string _type;
        private string _gender;
        private string _peoplenum;
        private bool _islegal;
        /// <summary>
        /// 
        /// </summary>
        public long id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long orgId
        {
            set { _orgid = value; }
            get { return _orgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pubTime
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string posDetail
        {
            set { _posdetail = value; }
            get { return _posdetail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jobRequir
        {
            set { _jobrequir = value; }
            get { return _jobrequir; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string gender
        {
            set { _gender = value; }
            get { return _gender; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Peoplenum
        {
            set { _peoplenum = value; }
            get { return _peoplenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isLegal
        {
            set { _islegal = value; }
            get { return _islegal; }
        }
        #endregion Model

    }
}