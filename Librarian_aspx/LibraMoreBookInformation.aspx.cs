using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Librarian_aspx_MoreBookInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        string ISBN = Request["ISBN"];
        string sqlStr = "select ISBN,book_name,type_name,author,publishing_house,CONVERT(varchar(7),publication_date, 23) as date,"
            + "borrow_frequency,price, lm_books.bookshelf_id,amount,amount-inventory as amount_inven, inventory,lm_books.description,book_picture,"
            + "room_id,floor from lm_books,lm_bookshelf,lm_booktype"
            + " where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and lm_books.type_id=lm_booktype.type_id and ISBN='" + ISBN+"'";
        DataSet ds = DB.getDataSet(sqlStr);
        FormView1.DataSource = ds;
        FormView1.DataBind();
    }

    protected void moreBarcode_Click(object sender, EventArgs e)
    {
        string ISBN = Request["ISBN"];
        Response.Redirect("LibraMoreInfoBarcode.aspx?ISBN="+ISBN);
    }

   
}