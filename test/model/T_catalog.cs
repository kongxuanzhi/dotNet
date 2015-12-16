using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// T_catalog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_catalog
    {
        public T_catalog()
        { }
        #region Model
        private long _id;
        private string _catalog;
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
        public string catalog
        {
            set { _catalog = value; }
            get { return _catalog; }
        }
        #endregion Model

    }
}

