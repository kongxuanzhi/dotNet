using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// Paging:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Paging
    {
        public Paging()
        { }
        #region Model
        private int _id;
        private int? _pagesizes;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? pageSizes
        {
            set { _pagesizes = value; }
            get { return _pagesizes; }
        }
        #endregion Model

    }
}

