using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Librarian_aspx_LibraDamagedBook : System.Web.UI.Page
{
	public string htmlstr;
	String url = HttpContext.Current.Request.RawUrl;
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text =  Session["LibraName"].ToString();
		if (!IsPostBack)
		{
			Bind();
			barcodeTxt.Value= "";
		}
	}
	protected void Bind()
	{
		string sqlStr = "select barcode,lm_barcode.ISBN as myISBN,book_name,lm_barcode.status_date,stakeholder "
		   + "from lm_barcode,lm_books where book_status=4 and lm_barcode.ISBN=lm_books.ISBN;";
		DataSet ds = DB.getDataSet(sqlStr);
		foreach (DataRow col in ds.Tables[0].Rows)
		{
			htmlstr += "{\"barcode\":\"" + col["barcode"].ToString() + "\","
					   + "\"bookname\":\"" + col["book_name"].ToString() + "\","
					   + "\"isbn\":\"" + col["myISBN"].ToString() + "\","
					   + "\"time\":\"" + col["status_date"].ToString() + "\","
					   + "\"userid\":\"" + col["stakeholder"].ToString() + "\","
					   + "},";
		}
	}



	protected void Button1_Click(object sender, EventArgs e)
	{
		if (barcodeTxt.Value.Trim() == "")
		{
			HttpContext.Current.Response.Write("<script>alert('Please input the barcode!');location.href='" + url + "'</script>");
		}
        else if(barcodeTxt.Value.ToString().Length>=10)
        {
            Response.Write("<script>alert('Your typed string is too long!')</script>");
        }
        else if (!LibraCheckInput.IsNum(barcodeTxt.Value.Trim()))
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string about bracode.');</script>");
        else
		{
			string barcodeStr = barcodeTxt.Value.Trim();
			DataSet ds = DB.getDataSet("select * from lm_barcode where barcode='" + barcodeStr + "';");
			if (ds.Tables[0].Rows.Count < 1)
			{
				HttpContext.Current.Response.Write("<script>alert('The barcode dose not exit!');location.href='" + url + "'</script>");
			}
			else if (ds.Tables[0].Rows[0][2].ToString().Trim() == "1")
			{
				HttpContext.Current.Response.Write("<script>alert('The book has been lent out.You need to wait for the reader to return book!');location.href='" + url + "'</script>");
			}
			else if (ds.Tables[0].Rows[0][2].ToString().Trim() == "2")
			{
				HttpContext.Current.Response.Write("<script>alert('The book has been reseved.You need to wait for the reader to cancel the reserve!');location.href='" + url + "'</script>");
			}
			else if (ds.Tables[0].Rows[0][2].ToString().Trim() == "3")
			{
				HttpContext.Current.Response.Write("<script>alert('The book is lost.You need to do nothing!');location.href='" + url + "'</script>");
			}
			else if (ds.Tables[0].Rows[0][2].ToString().Trim() == "4")
			{
				HttpContext.Current.Response.Write("<script>alert('This book has been processed. You  need to do nothing!');location.href='" + url + "'</script>");
			}
			else
			{
                string ISBN = DB.getDataSet("select ISBN from lm_barcode where barcode=" + barcodeStr).Tables[0].Rows[0][0].ToString();
                string sqlStr = "update lm_books set inventory=inventory-1,amount=amount-1 where ISBN='" + ISBN + "';";
                int rowCount = DB.executeNonQuery(sqlStr);
                if (rowCount < 1)
                    HttpContext.Current.Response.Write("<script>alert('Adding damaged book is failed');location.href='" + url + "'</script>");
                else
                {
                    sqlStr = "update lm_barcode set book_status=4,status_date=getdate(),stakeholder=" + Session["LibraId"].ToString() + " where barcode='" + barcodeStr + "';";
                    DB.executeNonQuery(sqlStr);
                    HttpContext.Current.Response.Write("<script>alert('Add damaged book successfully!');location.href='" + url + "'</script>");
                    Bind();
                }
            }

		}
	}

	
}