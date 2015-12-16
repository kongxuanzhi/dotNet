﻿/**  版本信息模板在安装目录下，可自行修改。
* T_priceType.cs
*
* 功 能： N/A
* 类 名： T_priceType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/21 星期二 上午 11:11:48   N/A    初版
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
	/// T_priceType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class T_priceType
	{
		public T_priceType()
		{}
		#region Model
		private int _id;
		private string _pricetypename;
		private string _pricetypeval;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string priceTypeName
		{
			set{ _pricetypename=value;}
			get{return _pricetypename;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string priceTypeVal
		{
			set{ _pricetypeval=value;}
			get{return _pricetypeval;}
		}
		#endregion Model

	}
}

