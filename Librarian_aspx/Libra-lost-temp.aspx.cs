using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Librarian_aspx_Libra_lost_temp : System.Web.UI.Page
{
    string user_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();

        decimal temp;
        temp = decimal.Parse(Request.Params["price"]);
        decimal temp2;
        temp2 = decimal.Parse(Request.Params["fine"]);
        decimal fine;
        fine = GetLostPenalty() * temp + temp2;

        UpdateSql();
        InsertSql();
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Need to pay a fine: " + fine+ ".');window.location.href='EReaderHistory.aspx';</script>");
        //Response.Redirect("EReaderHistory.aspx");
    }
    protected string GetUserID()
    {
        string querysql = "select user_id from lm_borrowed_record where barcode = " + Request.Params["barcode"] + ";";                      
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        return obj.ToString();
    }

    protected void UpdateSql()
    {
        decimal temp;
        temp = decimal.Parse(Request.Params["price"]);
        decimal temp2;
        temp2 = decimal.Parse(Request.Params["fine"]);
        decimal price;
        price = GetLostPenalty() * temp + temp2;

        string userID = GetUserID();

        string libraID = HttpContext.Current.Session["LibraId"].ToString();
        string sql = "update lm_borrowed_record set borrowed_status = 3,fine = "+ price +",fine_status = 'yes' where barcode = " + Request.Params["barcode"].ToString() + " and borrowed_status = 0;"
                    + "update lm_barcode set stakeholder = " + libraID + " where barcode = " + Request.Params["barcode"].ToString() + ";"
                    + "update lm_users set current_borrowed=current_borrowed-1 where user_id="+ userID + ";"
                    + "update lm_barcode set status_date = getdate() where barcode = " + Request.Params["barcode"];
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    protected int GetLostPenalty()
    {
        string querysql = "select set_penalty_lost from lm_adminSet where Id = 10000000;";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        int value;
        value = int.Parse(obj.ToString());
        return value;
    }
    protected void InsertSql()
    {
        decimal temp;
        temp = decimal.Parse(Request.Params["price"]);
        decimal temp2;
        temp2 = decimal.Parse(Request.Params["fine"]);
        decimal price;
        price = GetLostPenalty() * temp + temp2;
        string sql = "Insert into lm_income([stakeholder],[transaction_types],[money]) values( " + GetUserID() + ",1," + price.ToString() + ");";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        DB.executeNonQuery(sql);
    }
}