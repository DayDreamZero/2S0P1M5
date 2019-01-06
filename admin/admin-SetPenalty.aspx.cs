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

public partial class aspx_admin_SetPenalty : System.Web.UI.Page
{
    string para1 = @"^[1-9]$|^[1-9][0-9]{0,1}$";
    string para2 = @"^(([1-9])(\.\d{1,2}|\.)?|0\.([1-9]|\d[1-9]))$";
    //string para2 = @"^[1 - 9]\d*\.\d*|0\.\d*[1 - 9]\d*$";

    //string querysql = "Select set_penalty_lost,set_penalty_overdue from lm_adminSet where Id = 10000000";
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
            this.literal2.Text = "You can set and modify fine value for lost books.";
            this.literal3.Text = "This is the multiple of price of lost books.(unit: times)";
            this.textbox1.Text = GetPenaltyForLost();
            this.literal5.Text = "You can set book fine value for overdue books.";
            this.literal6.Text = "This is the amount to be paid for each days of exceed.(unit: RMB)";
            this.textbox2.Text = GetPenaltyForOverdue();
        }
    }
    public string GetPenaltyForLost()
    {
        string querysql = "Select set_penalty_lost from lm_adminSet where Id = 10000000";
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
        string querysql = "Select set_penalty_overdue from lm_adminSet where Id = 10000000";
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
        if(textbox1.Text.Trim() == "")
        {
            confirm1.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        else if(!Regex.IsMatch(textbox1.Text.Trim(),para1))
        {
            confirm1.Text = "You can only set an integer from 1 to 100 (except 100) !";
            //Response.Write("<script>alert('You can only set from 0 to 100!')</script>");
        }
        else
        {
            int num = Convert.ToInt32(textbox1.Text);
            string updatesql = "update lm_adminSet set set_penalty_lost = " + num + " where Id = 10000000";
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
        string s = textbox2.Text.ToString();
        if (textbox2.Text.Trim() == "")
        {
            confirm2.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
       /* else if (s[0].Equals('0'))
        {
            if (s.Length == 1)
            {
                confirm2.Text = "Input error, you can input between 1 and 1000, and no more than two decimal places.";
            }
            else if (s[1].Equals('.'))
            {
                if (s.Length == 2)
                {
                    confirm2.Text = "Input error, you can input between 1 and 1000, and no more than two decimal places.";
                }
                else if ((s[2].Equals('0') && s.Length == 3) || s[2].Equals('0') && s[3].Equals('0'))
                {
                    confirm2.Text = "Input error, you can input between 1 and 1000, and no more than two decimal places.";
                }
            }
        }*/
        else if (!Regex.IsMatch(textbox2.Text.Trim(), para2))
        {
            confirm2.Text = "Input error, you can input between 0 and 10 (except 0 and 10) , and no more than two decimal places.";
            //Response.Write("<script>alert('Your input cannot be negative and no more than two decimal places.')</script>");
        }
        else
        {
            decimal num = Convert.ToDecimal(textbox2.Text);
            string updatesql = "update lm_adminSet set set_penalty_overdue = " + num + " where Id = 10000000";
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(updatesql, conn);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Write("<script>alert('Modify Successfully！')</script>");
            this.textbox2.Text = GetPenaltyForOverdue();
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