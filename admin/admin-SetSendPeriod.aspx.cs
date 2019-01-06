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

public partial class admin_admin_SetSendPeriod : System.Web.UI.Page
{
    string pattern = @"^[1-9]$|^[1-9][0-9]{0,1}$";
    // string querysql = "Select set_deposit from lm_adminSet where Id = 10000000";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        if (!IsPostBack)
        {
            //this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
            this.textbox1.Text = GetDeposit();
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
    }
    public string GetDeposit()
    {
        string querysql = "Select sendmail_interval from lm_adminSet where Id = 10000000";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        decimal value = Convert.ToDecimal(obj);
        value = Math.Round(value, 2);
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
            decimal num = Convert.ToDecimal(textbox1.Text);
            string updatesql = "update lm_adminSet set sendmail_interval = " + num + " where Id = 10000000";
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatesql, conn);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Write("<script>alert('Modify Successfully！')</script>");
            this.textbox1.Text = GetDeposit();
            Response.Redirect("admin-ShowAllSettings.aspx");
        }
    }
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-ShowAllSettings.aspx");
    }
}
