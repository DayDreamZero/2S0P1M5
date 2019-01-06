using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraHistoryIncome : System.Web.UI.Page
{
	public string htmlstr;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text =  Session["LibraName"].ToString();
		if (!IsPostBack)
		{
			string strYMD = DateTime.Now.ToString("yyyy-MM-dd");
			time.Value = strYMD;
			String sql = "select * ,(case transaction_types when 0 then 'Deposit' when 1 then 'Fine' end)type from lm_income order by transaction_time desc";
			Bind(sql);
			DataSet ds1 = DB.getDataSet("select sum(money) from lm_income");
			Label3.Text = "Total Income: " + ds1.Tables[0].Rows[0][0].ToString()+ "¥";
		}
	}
	protected void Bind(String sql)
	{
		DataSet ds = DB.getDataSet(sql);
		foreach (DataRow col in ds.Tables[0].Rows)
		{
			htmlstr += "{\"income\":\"" + col["money"].ToString() + "\","
						   + "\"type\":\"" + col["type"].ToString() + "\","
						   + "\"time\":\"" + col["transaction_time"].ToString() + "\","
						   + "\"holder\":\"" + col["stakeholder"].ToString() + "\","
						   + "},";
		}
		
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		DateTime day = Convert.ToDateTime(time.Value);
		String sql = "select * ,(case transaction_types when 0 then 'Deposit' when 1 then 'Fine' end)type from lm_income  where DATEDIFF(WEEK, transaction_time,'" + day + "')= 0 order by transaction_time desc";
		Bind(sql);
		DataSet ds1 = DB.getDataSet("select sum(money) from lm_income  where DATEDIFF(WEEK, transaction_time,'" + day + "')= 0");
		Label3.Text = "Total Income: " + ds1.Tables[0].Rows[0][0].ToString() + "¥";
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		DateTime day = Convert.ToDateTime(time.Value);
		String sql = "select * ,(case transaction_types when 0 then 'Deposit' when 1 then 'Fine' end)type from lm_income  where DATEDIFF(MONTH, transaction_time,'" + day + "')= 0 order by transaction_time desc";
		Bind(sql);
		DataSet ds1 = DB.getDataSet("select sum(money) from lm_income  where DATEDIFF(MONTH, transaction_time,'" + day + "')= 0");
		Label3.Text = "Total Income: " + ds1.Tables[0].Rows[0][0].ToString() + "¥";
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		string strYMD = DateTime.Now.ToString("yyyy-MM-dd");
		time.Value = strYMD;
		String sql = "select * ,(case transaction_types when 0 then 'Deposit' when 1 then 'Fine' end)type from lm_income order by transaction_time desc";
		Bind(sql);
		DataSet ds1 = DB.getDataSet("select sum(money) from lm_income");
		Label3.Text = "Total Income: " + ds1.Tables[0].Rows[0][0].ToString() + "¥";
	}

	protected void Button0_Click(object sender, EventArgs e)
	{
		DateTime day = Convert.ToDateTime(time.Value);
		String sql = "select * ,(case transaction_types when 0 then 'Deposit' when 1 then 'Fine' end)type from lm_income  where DATEDIFF(Day, transaction_time,'" + day + "')= 0 order by transaction_time desc";
        Bind(sql);
		DataSet ds1 = DB.getDataSet("select sum(money) from lm_income  where DATEDIFF(Day, transaction_time,'" + day + "')= 0");
		Label3.Text = "Total Income: " + ds1.Tables[0].Rows[0][0].ToString() + "¥";
	}



}