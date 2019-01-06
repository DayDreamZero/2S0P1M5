using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reader_bookrecordBorrow : System.Web.UI.Page
{
    public string htmlStr;
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
        UpdateOverdueBook();
        Repeater_Databind();
    }
    private void UpdateOverdueBook()
    {
        string sqlStr = "select set_penalty_overdue,set_return_period from lm_adminSet where Id=10000000;";
        DataSet ds = DB.getDataSet(sqlStr);
        Decimal fineDay = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
        int returnDays = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

        //检查是否没超期的是否超期并更新
        sqlStr = "select borrowed_id from lm_borrowed_record" + " where DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=0";
        ds = DB.getDataSet(sqlStr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set borrowed_status=2,fine_status='no',fine=(DATEDIFF(DAY,borrowed_date,GETDATE())-" + returnDays + ")*" + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }


        //更新已超期的
        sqlStr = "select borrowed_id from lm_borrowed_record " + " where fine_status='no'and DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=2";
        ds = DB.getDataSet(sqlStr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set fine = (DATEDIFF(DAY, borrowed_date, GETDATE()) - " + returnDays + ") * " + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }

    }
    void Repeater_Databind()
    {
        string sqlStr = "select book_picture,book_name,lm_borrowed_record.barcode,borrowed_date,(borrowed_date+(select set_return_period from lm_adminSet where id=10000000)) as endtime from lm_books,lm_borrowed_record,lm_barcode where lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and borrowed_status=0 and user_id=" + HttpContext.Current.Session["userid"];
        string sqlStr1 = "select book_picture,book_name,lm_borrowed_record.barcode,borrowed_date,(borrowed_date+(select set_return_period from lm_adminSet where id=10000000)) as endtime,fine from lm_books,lm_borrowed_record,lm_barcode where lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and borrowed_status=2 and user_id=" + HttpContext.Current.Session["userid"];
        DataSet ds = DB.getDataSet(sqlStr);
        DataSet ds1 = DB.getDataSet(sqlStr1);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        Repeater2.DataSource = ds1;
        Repeater2.DataBind();
    }
    //where user_id = "+HttpContext.Current.Session["userid"].ToString()+" +"and lm_reserve_record.barcode = "+HttpContext.Current.Session["barcode"].ToString()+" and user_id = "+HttpContext.Current.Session["userid"].ToString()+" and ISBN = "+HttpContext.Current.Session["ISBN"].ToString();
}