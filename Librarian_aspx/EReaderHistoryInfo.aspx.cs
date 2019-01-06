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

public partial class HistoryInfo : System.Web.UI.Page
{
	public string htmlstr;
	private DataSet ds;
	String userid;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text =  Session["LibraName"].ToString();
		userid=Request["userid"];
		DataSet ds1 = DB.getDataSet("select sum(fine) as needpay from lm_borrowed_record where fine_status = 'no' and user_id =" + userid);
		Label7.Text = Request["userid"] + "'s Fine: " + ds1.Tables[0].Rows[0][0].ToString()+ "¥";
		String sql= "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and user_id=" + userid;
		DataSet ds2 = DB.getDataSet("select user_barcode_image from lm_users where  user_id =" + userid);
		Image1.ImageUrl = ds2.Tables[0].Rows[0][0].ToString();
		Bind( sql);
		
	}

	protected void Bind( String sql)
	{
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
		String input1 = name.Text.Trim().ToString();
		// int u_id = int.Parse(input1);
		String bookstatus = DropDownList1.SelectedValue.ToString();
		String finestatus = DropDownList2.SelectedValue.ToString();
		htmlstr = null;
		String s="";
		input1 = LibraCheckInput.transApostrophe(input1.Trim());
		//string sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id ="+ userid + " and book_name like N'%" + input1 + "%');";
		if (bookstatus != "5" && finestatus != "all")
			s = " and r.borrowed_status = " + bookstatus + " and fine_status = '" + finestatus + "'";
			//sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id =" + userid + " and book_name like N'%" + input1 + "%' and r.borrowed_status=" + bookstatus + " and fine_status='" + finestatus + "')";
		if (bookstatus == "5" && finestatus != "all")
			//sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like =" + userid + "and book_name like N'%" + input1 + "%' and  fine_status='" + finestatus + "')";
			s= " and  fine_status = '" + finestatus + "'";
		if (bookstatus != "5" && finestatus == "all")
			//sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and(user_id like =" + userid + " and book_name like N'%" + input1 + "%' and r.borrowed_status=" + bookstatus + " )";
			s= " and r.borrowed_status = " + bookstatus;
		if (bookstatus == "5" && finestatus == "all")
			s = "";
		string sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and user_id =" + userid + " and book_name like N'%" + input1 + "%'" + s;
		Bind(sql);

	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		htmlstr = null;
		String sql = "select  (case r.borrowed_status when 0 then 'out'when 1 then 'return' when 2 then 'overdue' when 3 then 'lost' when 4 then 'damaged' end)status,r.barcode as mybarcode,* from lm_borrowed_record as r,lm_barcode as ba,lm_books as bo where r.barcode=ba.barcode and ba.ISBN=bo.ISBN and user_id=" + userid;
		Bind(sql);
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		Response.Redirect("EReaderManage.aspx");
	}



}