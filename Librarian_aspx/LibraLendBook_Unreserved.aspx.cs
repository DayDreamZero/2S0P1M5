using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Unreserved : System.Web.UI.Page
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text =  Session["LibraName"].ToString();
		if (!IsPostBack)
		{
			ViewState["rowcount"] = "0";
			DataSet ds2 = syjDB.reDs("select top 0  DATEADD(DAY,(select set_return_period from lm_adminSet where Id=10000000), borrowed_date ) as return_date,lm_borrowed_record.barcode as mybarcode,* from lm_borrowed_record,lm_barcode,lm_books where lm_borrowed_record.barcode=lm_barcode.barcode and  lm_books.ISBN=lm_barcode.ISBN and borrowed_status=0  order by borrowed_date desc");
			dluserBorrow.DataSource = ds2;
			dluserBorrow.DataBind();
		}
		
			
	}
	//绑定DataList控件
	public void Bind(int top)
	{
		DataSet ds2 = syjDB.reDs("select top "+top+" DATEADD(DAY,(select set_return_period from lm_adminSet where Id=10000000), borrowed_date ) as return_date,lm_borrowed_record.barcode as mybarcode,* from lm_borrowed_record,lm_barcode,lm_books where lm_borrowed_record.barcode=lm_barcode.barcode and  lm_books.ISBN=lm_barcode.ISBN and borrowed_status=0  and user_id="+Readerid.Text+" order by borrowed_date desc");
		dluserBorrow.DataSource = ds2;
		dluserBorrow.DataBind();
	}


	protected int Check_barcode()
	{
		DataSet ds = syjDB.reDs("select barcode from lm_barcode where book_status=0 and barcode="+Barcode.Text.Trim());
		/*int i;
		for (i = 0; i < ds.Tables[0].Rows.Count; i++)
		{
			if (Barcode.Text == ds.Tables[0].Rows[i][0].ToString())
			{

				break;
			}
		}
		if (i >= ds.Tables[0].Rows.Count)
		{
			Barcode.Text = "";
			return 0;
		}
		else return 1;*/
		if(ds.Tables[0].Rows.Count==0) Barcode.Text = "";
		return ds.Tables[0].Rows.Count;
	}

	protected int Check_max()
	{
		DataSet ds1 = syjDB.reDs("select current_borrowed from lm_users where user_id=" + Readerid.Text);
		DataSet ds2 = syjDB.reDs("select max_borrow from lm_adminSet where Id = (select Max(id) from lm_adminSet)");
		int cur = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
		int max = int.Parse(ds2.Tables[0].Rows[0][0].ToString());
		if (cur < max) return 1;
		else return 0;
	}



	protected int Check_ISBN()
	{
		DataSet ds1 = syjDB.reDs("select  ISBN  from lm_barcode,lm_borrowed_record where user_id=" + Readerid.Text + " and lm_barcode.barcode=lm_borrowed_record.barcode and borrowed_status=0");
		DataSet ds2 = syjDB.reDs("select  ISBN  from lm_barcode where barcode =" + Barcode.Text.ToString());
		int i, j, k = 1;
		for (i = 0; i < ds1.Tables[0].Rows.Count; i++)
		{
			for (j = 0; j < ds2.Tables[0].Rows.Count; j++)
			{
				if (ds2.Tables[0].Rows[j][0].ToString() == ds1.Tables[0].Rows[i][0].ToString())
				{
					k = 0;
					break;
				}
			}
		}
		return k;

	}

	protected int Check_fine()
	{
		DataSet ds1 = syjDB.reDs("select fine_status from lm_borrowed_record where fine_status='no'and fine>0 and user_id=" + Readerid.Text);
		return ds1.Tables[0].Rows.Count;
	}

	protected int checkid()
	{
		DataSet ds = syjDB.reDs("select user_id from lm_users where reader_status=0 and user_id="+ Readerid.Text.Trim());
		return ds.Tables[0].Rows.Count;
	}

	protected void Borrow_Click(object sender, EventArgs e)
	{
		String url = HttpContext.Current.Request.RawUrl;
		if (Readerid.Text != "")
		{
			if (!LibraCheckInput.IsNum(Readerid.Text))
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
			else
			{
				if (checkid() != 0)
				{
					if (Check_fine() == 0)
					{
						if (Barcode.Text != "")
						{
							if (!LibraCheckInput.IsNum(Barcode.Text))
								Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
							else
							{
								if (Check_barcode() == 0)
								{
									{
										//Response.Write("<script>alert('The barcode is wrong!');location='javascript:history.go(-1);'</script>");
										//Barcode.Text = "";
										Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The barcode is wrong!');</script>");

									}
								}
								else
								{
									if (Check_max() == 0)
									{
										Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('you have reached the max capacity!');</script>");
										//Response.Write("<script>alert('you have reached the max capacity!');location='javascript:history.go(-1);'</script>");
									}
									else
									{
										if (Check_ISBN() == 0)
											Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('you have borrowed the book!');</script>");
										else
										{

											bool P_bool_reVal = syjDB.ExSql("insert into lm_borrowed_record (user_id,barcode,borrowed_status) values (" + Readerid.Text + "," + Barcode.Text.ToString() + ",0)");
											if (P_bool_reVal)
											{
												Barcode.Text = "";
												ViewState["rowcount"] = int.Parse(ViewState["rowcount"].ToString()) + 1;
												Bind(int.Parse(ViewState["rowcount"].ToString()));
											}
											else
												Response.Write("<script>alert('Fail!');location='javascript:history.go(-1);'</script>");
										}
									}
								}
							}
						}
						else
							Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please input Book Barcode!');</script>");
					}
					else
						HttpContext.Current.Response.Write("<script>alert('Fine has not been paid!');location.href='" + url + "'</script>");
				}
				else
					HttpContext.Current.Response.Write("<script>alert('The ReaderID is wrong!');location.href='" + url + "'</script>");

			}
		}
		else
			HttpContext.Current.Response.Write("<script>alert('Please input ReaderID!');location.href='" + url + "'</script>");
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