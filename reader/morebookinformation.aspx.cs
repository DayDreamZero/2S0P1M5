using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class morebookinformation : System.Web.UI.Page
{
    public String htmlStr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Session["username"] == null && this.Session["userid"] == null)
            {
                htmlStr = "<ul class='nav navbar-nav navbar-right'><li><a href = '/Login.aspx'> Login </a></li></ul>";
            }
            else
            {
                htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = '/reader/myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
            }

        }
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        string ISBN = Request["ISBN"];
        string sqlStr = "select ISBN,book_name,type_id,author,publishing_house,CONVERT(varchar(7),publication_date, 23) as date,"
            + "borrow_frequency,price, lm_books.bookshelf_id,amount,amount-inventory as amount_inven, inventory,description,book_picture,"
            + "room_id,floor from lm_books,lm_bookshelf"
            + " where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and ISBN='" + ISBN + "'";
        DataSet ds = DB.getDataSet(sqlStr);
        FormView1.DataSource = ds;
        FormView1.DataBind();
    }
    
    /*protected void Cb_Click(object sender, EventArgs e)
    {
        string resource = HttpContext.Current.Session["infoResource"].ToString();
        Session["infoResource"] = null;
        Server.Transfer(resource+".aspx");
    }*/
}