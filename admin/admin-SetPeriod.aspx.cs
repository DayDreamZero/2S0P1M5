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

public partial class admin_admin_SetPeriod : System.Web.UI.Page
{
    string pattern = @"^[1-9]$|^[1-9][0-9]{0,1}$";
    string pattern2 = @"^[1-9]$|^[1-9][0-9]{0,2}$";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
        if (!IsPostBack)
        {
            //this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
            this.literal2.Text = "You can set and modify book return period.";
            this.literal3.Text = "This is the amount of days of books in lend at most.(unit: days)";
            this.textbox1.Text = GetPenaltyForLost();
            this.literal5.Text = "You can set and modify book reserve period in valid.";
            this.literal6.Text = "This is the amount of minutes of valid booking time.(unit: minutes)";
            this.textbox2.Text = GetPenaltyForOverdue();
        }
    }

    public string GetPenaltyForLost()
    {
        string querysql = "Select set_return_period from lm_adminSet where Id = 10000000";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        int value = int.Parse(obj.ToString());
        return value.ToString();
    }
    public string GetPenaltyForOverdue()
    {
        string querysql = "Select set_reserve_period from lm_adminSet where Id = 10000000";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        int value = int.Parse(obj.ToString());
        return value.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (textbox1.Text.Trim() == "")
        {
            confirm1.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        else if (!Regex.IsMatch(textbox1.Text.Trim(), pattern))
        {
            confirm1.Text = "You should enter an integer more than 0 and less than 100 and the highest digit cannot be 0.";
            //Response.Write("<script>alert('You should enter an integer within 100 and the highest digit cannot be 0.')</script>");
        }
        else
        {
            int num = Convert.ToInt32(textbox1.Text);
            string updatesql = "update lm_adminSet set set_return_period = " + num + " where Id = 10000000";
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatesql, conn);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Write("<script>alert('Modify Successfully！')</script>");
            Response.Redirect("admin-ShowAllSettings.aspx");
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (textbox2.Text.Trim() == "")
        {
            confirm2.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        else if (!Regex.IsMatch(textbox2.Text.Trim(), pattern2))
        {
            confirm2.Text = "You should enter an integer more than 0 and less than 1000 and the highest digit cannot be 0.";
            //Response.Write("<script>alert('You should enter an integer within 1000 and the highest digit cannot be 0.')</script>");
        }
        else
        {
            int num = Convert.ToInt32(textbox2.Text);
            string updatesql = "update lm_adminSet set set_reserve_period = " + num + " where Id = 10000000";
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatesql, conn);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Write("<script>alert('Modify Successfully！')</script>");
            Response.Redirect("admin-ShowAllSettings.aspx");
        }

    }
   
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-ShowAllSettings.aspx");
    }
    
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-ShowAllSettings.aspx");
    }

    protected void hyperlink1_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Session["admin_name"] = null;
    }
}