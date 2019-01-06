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

public partial class History : System.Web.UI.Page
{
	public string htmlstr;
	private DataSet ds;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text =Session["LibraName"].ToString();
		Bind();

	}

	protected void Bind()
	{
		string sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN";
		ds = DB.getDataSet(sql);
		foreach (DataRow col in ds.Tables[0].Rows)
		{
			htmlstr += "{\"userid\":\"" + col["user_id"].ToString() + "\","
					   + "\"bookname\":\"" + col["book_name"].ToString() + "\","
					   + "\"barcode\":\"" + col["mybarcode"].ToString() + "\","
					   + "\"book_status\":\"" + col["status"].ToString() + "\","
					   + "\"fine\":\"" + col["fine"].ToString() + "\","
					   + "\"fine_status\":\"" + col["fine_status"].ToString() + "\","
					   + "\"borrowtime\":\"" + col["borrowed_date"].ToString() + "\","
					   + "\"returntime\":\"" + col["return_date"].ToString() + "\","
					   + "},";
		}
	}



	protected void Button1_Click(object sender, EventArgs e)
	{
		String input1 = id.Text.Trim().ToString();
		String input2 = name.Text.Trim().ToString();
		// int u_id = int.Parse(input1);
		String bookstatus = DropDownList1.SelectedValue.ToString();
		String finestatus = DropDownList2.SelectedValue.ToString();		
		if (!LibraCheckInput.IsNum(input1))
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string about Reader ID.');</script>");
		else
		{
			input2 = LibraCheckInput.transApostrophe(input2.Trim());
			string sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like '%" + input1 + "%' and book_name like N'%" + input2 + "%');";
			if (bookstatus != "5" && finestatus != "all")
				sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like '%" + input1 + "%' and book_name like N'%" + input2 + "%' and r.borrowed_status=" + bookstatus + " and fine_status='" + finestatus + "')";
			if (bookstatus == "5" && finestatus != "all")
				sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like '%" + input1 + "%' and book_name like N'%" + input2 + "%' and  fine_status='" + finestatus + "')";
			if (bookstatus != "5" && finestatus == "all")
				sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like '%" + input1 + "%' and book_name like N'%" + input2 + "%' and r.borrowed_status=" + bookstatus + " )";
			ds = DB.getDataSet(sql);
			htmlstr = null;
			foreach (DataRow col in ds.Tables[0].Rows)
			{
				htmlstr += "{\"userid\":\"" + col["user_id"].ToString() + "\","
						   + "\"bookname\":\"" + col["book_name"].ToString() + "\","
						   + "\"barcode\":\"" + col["mybarcode"].ToString() + "\","
						   + "\"book_status\":\"" + col["status"].ToString() + "\","
						   + "\"fine\":\"" + col["fine"].ToString() + "\","
						   + "\"fine_status\":\"" + col["fine_status"].ToString() + "\","
						   + "\"borrowtime\":\"" + col["borrowed_date"].ToString() + "\","
						   + "\"returntime\":\"" + col["return_date"].ToString() + "\","
						   + "},";
			}
		}

	}


	protected void Button3_Click(object sender, EventArgs e)
	{
		Response.Redirect("../Librarian_aspx/EReaderHistory.aspx");
	}



}