using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 兽兽1;

public partial class _Default : System.Web.UI.Page
{
    public string name = "df";
    public List<string> catalogs = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = SqlHelper.ExecuteDataTable("select * from T_catalog");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
               catalogs.Add(dr["catalog"].ToString());
            }
        }

    }
}