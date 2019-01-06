using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Librarian_aspx_LibranewBookBarcode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            BindData();
          
        }
    }

    protected void BindData()
    {

        string Num = Request["Num"];
        string ISBN = Request["ISBN"];
       
        string sqlStr = "select top " + Num + " book_name,lm_barcode.ISBN,barcode,barcode_image from lm_books,lm_barcode"
            + " where lm_books.ISBN=lm_barcode.ISBN and lm_barcode.ISBN='" + ISBN + "'"
            + "order by barcode desc";
        DataSet ds = DB.getDataSet(sqlStr);
        GridView1.DataSource = ds;
        GridView1.DataBind();


    }

    protected void ComebackBt_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraAddNewBook.aspx");
    }
}