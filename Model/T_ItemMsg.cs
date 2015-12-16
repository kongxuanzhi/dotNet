/**  版本信息模板在安装目录下，可自行修改。
* T_ItemMsg.cs
*
* 功 能： N/A
* 类 名： T_ItemMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/21 星期二 上午 11:11:42   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model
{
	/// <summary>
	/// T_ItemMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_ItemMsg
	{
		public T_ItemMsg()
		{}
		#region Model
		private long _id;
		private string _itemsname;
		private string _username;
		private string _commment;
		private string _userip;
        private string  _date;
		/// <summary>
		/// 
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string itemsName
		{
			set{ _itemsname=value;}
			get{return _itemsname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string commment
		{
			set{ _commment=value;}
			get{return _commment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userIp
		{
			set{ _userip=value;}
			get{return _userip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string date
		{
			set{ _date=value;}
			get{return _date;}
		}
		#endregion Model

	}
}

