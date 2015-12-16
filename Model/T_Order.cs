using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// T_Order:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_Order
    {
        public T_Order()
        { }
        #region Model
        private long _id;
        private string _ordernum;
        private int _desknum;
        private string _username;
        private string _ordertime;
        private string _mark;
        private string _totalpay;
        private string _statu;
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
        public int deskNum
        {
            set { _desknum = value; }
            get { return _desknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string orderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mark
        {
            set { _mark = value; }
            get { return _mark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string totalPay
        {
            set { _totalpay = value; }
            get { return _totalpay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string statu
        {
            set { _statu = value; }
            get { return _statu; }
        }
        #endregion Model

    }
}

