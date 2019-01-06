using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraMoreInfoBarcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            gridviewBindata();
        }
    }

    private void gridviewBindata()
    {
        string ISBN = Request["ISBN"].ToString();
        string sqlStr = "select barcode,lm_barcode.ISBN,book_name,barcode_image,"
            + "status=(case when book_status=0 then 'in' when book_status=1 then 'lend out' when book_status=2 then 'reserved' end) from lm_barcode,lm_books "
            + "where lm_barcode.ISBN=lm_books.ISBN and (book_status=0 or book_status=1 or book_status=2) and lm_barcode.ISBN="+ISBN;
        GridView1.DataSource = DB.getDataSet(sqlStr);
        GridView1.DataBind();
    }
}