using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reader_bookrecordReturn : System.Web.UI.Page
{
    public string htmlStr,Str;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Session["username"] == null && this.Session["userid"] == null)
            {
                Response.Redirect("../login.aspx");
            }
            else
            {
                htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = 'myprofileshow.aspx'>my profile</a></li><li><a href = 'bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
            }

        }
        Repeater_Databind();
    }
    void Repeater_Databind()
    {
        string sqlStr = "select book_picture,book_name,lm_borrowed_record.barcode,borrowed_date,return_date,fine,fine_status from lm_books,lm_borrowed_record,lm_barcode where lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and borrowed_status=1 and fine_status='no' and user_id=" + HttpContext.Current.Session["userid"];
        DataSet ds = DB.getDataSet(sqlStr);
        sqlStr = "select book_picture,book_name,lm_borrowed_record.barcode,borrowed_date,return_date,fine,fine_status from lm_books,lm_borrowed_record,lm_barcode where lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and borrowed_status=1 and fine_status='yes' and user_id=" + HttpContext.Current.Session["userid"];
        DataSet ds1 = DB.getDataSet(sqlStr);
        sqlStr = "select book_picture,book_name,lm_borrowed_record.barcode,borrowed_date,return_date,fine,fine_status from lm_books,lm_borrowed_record,lm_barcode where lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and borrowed_status=1 and fine_status is null and user_id=" + HttpContext.Current.Session["userid"];
        DataSet ds2 = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        Repeater2.DataSource = ds1;
        Repeater2.DataBind();
        Repeater3.DataSource = ds2;
        Repeater3.DataBind();
    }
}