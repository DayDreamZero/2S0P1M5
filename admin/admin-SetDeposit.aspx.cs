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

public partial class aspx_admin_SetDeposit : System.Web.UI.Page
{
    //string pattern = @"^(([1-9]\d*|0){0,4})(\.\d{1,2})$";
    string pattern = @"^(([1-9]\d{0,2})(\.\d{1,2}|\.)?|0\.([1-9]|\d[1-9]))$";
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
        string querysql = "Select set_deposit from lm_adminSet where Id = 10000000";
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
        string s = textbox1.Text.ToString();
        
        if (textbox1.Text.Trim() == "")
        {
            confirm1.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        /*else if (s[0].Equals('0'))
        {
            if (s.Length == 1)
            {
                confirm1.Text = "Input error, you can input between 1 and 10000, and no more than two decimal places.";
            }
            else if (s[1].Equals('.'))
            {
                if (s.Length == 2)
                {
                    confirm1.Text = "Input error, you can input between 1 and 10000, and no more than two decimal places.";
                }
                else if ((s[2].Equals('0')&&s.Length==3)||s[2].Equals('0') && s[3].Equals('0'))
                {
                    confirm1.Text = "Input error, you can input between 1 and 10000, and no more than two decimal places.";
                }
            }
        }*/
        else if (!Regex.IsMatch(textbox1.Text.Trim(), pattern))
        {
            confirm1.Text = "Input error, you can input between 0 and 1000(except 0 and 1000), and no more than two decimal places.";
           // Response.Write("<script>alert('Your input cannot be negative and no more than two decimal places.')</script>");
        }
        
        else
        {
            decimal num = Convert.ToDecimal(textbox1.Text);
            string updatesql = "update lm_adminSet set set_deposit = " + num + " where Id = 10000000";
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

   
