using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_Order
	/// </summary>
	public partial class T_Order
	{
		public T_Order()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_Order");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.T_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Order(");
			strSql.Append("orderNum,deskNum,userName,orderTime,mark,totalPay,statu)");
			strSql.Append(" values (");
			strSql.Append("@orderNum,@deskNum,@userName,@orderTime,@mark,@totalPay,@statu)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@orderNum", SqlDbType.VarChar,200),
					new SqlParameter("@deskNum", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.VarChar,200),
					new SqlParameter("@orderTime", SqlDbType.VarChar,200),
					new SqlParameter("@mark", SqlDbType.VarChar,-1),
					new SqlParameter("@totalPay", SqlDbType.VarChar,30),
					new SqlParameter("@statu", SqlDbType.VarChar,20)};
			parameters[0].Value = model.orderNum;
			parameters[1].Value = model.deskNum;
			parameters[2].Value = model.userName;
			parameters[3].Value = model.orderTime;
			parameters[4].Value = model.mark;
			parameters[5].Value = model.totalPay;
			parameters[6].Value = model.statu;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.T_Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Order set ");
			strSql.Append("orderNum=@orderNum,");
			strSql.Append("deskNum=@deskNum,");
			strSql.Append("userName=@userName,");
			strSql.Append("orderTime=@orderTime,");
			strSql.Append("mark=@mark,");
			strSql.Append("totalPay=@totalPay,");
			strSql.Append("statu=@statu");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@orderNum", SqlDbType.VarChar,200),
					new SqlParameter("@deskNum", SqlDbType.Int,4),
					new SqlParameter("@userName", SqlDbType.VarChar,200),
					new SqlParameter("@orderTime", SqlDbType.VarChar,200),
					new SqlParameter("@mark", SqlDbType.VarChar,-1),
					new SqlParameter("@totalPay", SqlDbType.VarChar,30),
					new SqlParameter("@statu", SqlDbType.VarChar,20),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.orderNum;
			parameters[1].Value = model.deskNum;
			parameters[2].Value = model.userName;
			parameters[3].Value = model.orderTime;
			parameters[4].Value = model.mark;
			parameters[5].Value = model.totalPay;
			parameters[6].Value = model.statu;
			parameters[7].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Order ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Order ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_Order GetModel(long id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,orderNum,deskNum,userName,orderTime,mark,totalPay,statu from T_Order ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt)
			};
			parameters[0].Value = id;

			Maticsoft.Model.T_Order model=new Maticsoft.Model.T_Order();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.T_Order DataRowToModel(DataRow row)
		{
			Maticsoft.Model.T_Order model=new Maticsoft.Model.T_Order();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=long.Parse(row["id"].ToString());
				}
				if(row["orderNum"]!=null)
				{
					model.orderNum=row["orderNum"].ToString();
				}
				if(row["deskNum"]!=null && row["deskNum"].ToString()!="")
				{
					model.deskNum=int.Parse(row["deskNum"].ToString());
				}
				if(row["userName"]!=null)
				{
					model.userName=row["userName"].ToString();
				}
				if(row["orderTime"]!=null)
				{
					model.orderTime=row["orderTime"].ToString();
				}
				if(row["mark"]!=null)
				{
					model.mark=row["mark"].ToString();
				}
				if(row["totalPay"]!=null)
				{
					model.totalPay=row["totalPay"].ToString();
				}
				if(row["statu"]!=null)
				{
					model.statu=row["statu"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,orderNum,deskNum,userName,orderTime,mark,totalPay,statu ");
			strSql.Append(" FROM T_Order ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,orderNum,deskNum,userName,orderTime,mark,totalPay,statu ");
			strSql.Append(" FROM T_Order ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_Order ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from T_Order T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "T_Order";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/
		#endregion  BasicMethod
		#region  ExtensionMethod
        public bool Update(long id,string statu)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Order set ");
            strSql.Append("statu=@statu");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@statu", SqlDbType.VarChar,100),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
            parameters[0].Value = statu;
            parameters[1].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		#endregion  ExtensionMethod
	}
}

