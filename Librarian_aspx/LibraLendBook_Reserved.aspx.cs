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

public partial class Reserve: System.Web.UI.Page
{
	public string htmlstr;
	private DataSet ds;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text = Session["LibraName"].ToString();

		Bind();

	}
    public void UpdateReserveFailBook()
    {
        //< asp:ScriptManager ID = "ScriptManager1" runat = "server" EnablePartialRendering = "true" ></ asp:ScriptManager >
        // < asp:UpdatePanel ID = "UpdatePanel1" runat = "server" ></ asp:UpdatePanel >
        // < asp:Timer ID = "Timer1" runat = "server" Interval = "10000" ontick = "Timer1_Tick" Enabled = "true" ></ asp:Timer >
        string sqlStr = "select set_reserve_period from lm_adminSet where id=10000000";
        DataSet ds = DB.getDataSet(sqlStr);
        int reserve_period = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        sqlStr = "select reserve_id from lm_reserve_record" + " where DATEDIFF(MINUTE,starttime,GETDATE())>" + reserve_period + " and reserve_status=0 ";
        ds = DB.getDataSet(sqlStr);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_reserve_record set reserve_status=1"
            + " where reserve_id=" + ds.Tables[0].Rows[i]["reserve_id"].ToString();
            DB.executeNonQuery(sqlStr);
        }
    }
    protected void Bind()
	{
		string sql = "select *,lm_reserve_record.barcode as newbarcode,lm_books.ISBN as newISBN from lm_reserve_record,lm_barcode,lm_books where reserve_status=0 and lm_reserve_record.barcode=lm_barcode.barcode and lm_barcode.ISBN=lm_books.ISBN;";
		ds = DB.getDataSet(sql);
		foreach (DataRow col in ds.Tables[0].Rows)
		{
			htmlstr += "{\"isbn\":\"" + col["ISBN"].ToString() + "\","
					   + "\"barcode\":\"" + col["newbarcode"].ToString() + "\","
					   + "\"bookname\":\"" + col["book_name"].ToString() + "\","
					   + "\"userid\":\"" + col["user_id"].ToString() + "\","
					   + "\"starttime\":\"" + col["starttime"].ToString() + "\","
					   + "\"endtime\":\"" + col["endtime"].ToString() + "\","
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
			if (!LibraCheckInput.IsNum(input2))
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
			else
			{
				htmlstr = null;

				string sql = "select *,lm_reserve_record.barcode as newbarcode,lm_books.ISBN as newISBN from lm_reserve_record,lm_barcode,lm_books where reserve_status=0 and lm_reserve_record.barcode=lm_barcode.barcode and lm_barcode.ISBN=lm_books.ISBN and user_id like '%" + input1 + "%' and lm_reserve_record.barcode = " + input2 + ";";
				if (input2 == "")
					sql = "select *,lm_reserve_record.barcode as newbarcode,lm_books.ISBN as newISBN from lm_reserve_record,lm_barcode,lm_books where reserve_status=0 and lm_reserve_record.barcode=lm_barcode.barcode and lm_barcode.ISBN=lm_books.ISBN and user_id like '%" + input1 + "%'";
				ds = DB.getDataSet(sql);
				foreach (DataRow col in ds.Tables[0].Rows)
				{
					htmlstr += "{\"isbn\":\"" + col["ISBN"].ToString() + "\","
							   + "\"barcode\":\"" + col["newbarcode"].ToString() + "\","
							   + "\"bookname\":\"" + col["book_name"].ToString() + "\","
							   + "\"userid\":\"" + col["user_id"].ToString() + "\","
							   + "\"starttime\":\"" + col["starttime"].ToString() + "\","
							   + "\"endtime\":\"" + col["endtime"].ToString() + "\","
							   + "},";
				}
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
		Response.Redirect("../Librarian_aspx/LibraLendBook_Reserved.aspx");
	}

    protected void UnReservedBt_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraLendBook_Unreserved.aspx");
    }

    protected void ReservedBt_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraLendBook_Reserved.aspx");
    }


}