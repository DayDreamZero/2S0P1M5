using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class AllReader : System.Web.UI.Page
{
	public string htmlstr;
	private DataSet ds;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text = Session["LibraName"].ToString();

		if (!IsPostBack)
		{
			
		}
		string sql = "select * from lm_users where reader_status=0;";
		ds = DB.getDataSet(sql);
		foreach (DataRow col in ds.Tables[0].Rows)
		{
			htmlstr += "{\"id\":\"" + col["user_id"].ToString() + "\","
					  + "\"username\":\"" + col["username"].ToString() + "\","
					  + "\"email\":\"" + col["email"].ToString() + "\","
					  + "\"telephone\":\"" + col["telephone"].ToString() + "\","
					  //+ "\"balance\":\"" + col["balance"].ToString() + "\","
					  + "},";
		}
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		String input1 = Request["input1"].Trim().ToString();
		String input2 = Request["input2"].Trim().ToString();
		// int u_id = int.Parse(input1);
		if (!LibraCheckInput.IsNum(input1))
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
		else
		{
			htmlstr = null;
			input2 = LibraCheckInput.transApostrophe(input2.Trim());
			string sql = "select * from lm_users where reader_status=0 and (username like N'%" + input2 + "%' and  user_id like '%" + input1 + "%');";
			ds = DB.getDataSet(sql);
			foreach (DataRow col in ds.Tables[0].Rows)
			{
				htmlstr += "{\"id\":\"" + col["user_id"].ToString() + "\","
						  + "\"username\":\"" + col["username"].ToString() + "\","
						  + "\"email\":\"" + col["email"].ToString() + "\","
						  + "\"telephone\":\"" + col["telephone"].ToString() + "\","
						  //+ "\"balance\":\"" + col["balance"].ToString() + "\","
						  + "},";

			}
		}

	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		String input1 = Request["input1"].ToString();
		String input2 = Request["input2"].ToString();
		input1 = null;
		input2 = null;
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		Response.Redirect("EReaderRegister.aspx");
	}




	protected void Button4_Click(object sender, EventArgs e)
	{
		Response.Redirect("EReaderBarcode.aspx?Num=all");
	}
}