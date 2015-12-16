using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// T_videos:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_videos
    {
        public T_videos()
        { }
        #region Model
        private long _id;
        private string _mname;
        private string _murl;
        private string _mcaption;
        private long? _mcatalog;
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
        public string Mname
        {
            set { _mname = value; }
            get { return _mname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Murl
        {
            set { _murl = value; }
            get { return _murl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mcaption
        {
            set { _mcaption = value; }
            get { return _mcaption; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? Mcatalog
        {
            set { _mcatalog = value; }
            get { return _mcatalog; }
        }
        #endregion Model

    }
}

