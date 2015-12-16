using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// T_orderItems:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_orderItems
    {
        public T_orderItems()
        { }
        #region Model
        private long _id;
        private string _ordernum;
        private string _itemname;
        private int _itemnum;
        private string _totalpay;
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
        public string orderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int itemNum
        {
            set { _itemnum = value; }
            get { return _itemnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string totalPay
        {
            set { _totalpay = value; }
            get { return _totalpay; }
        }
        #endregion Model

    }
}

