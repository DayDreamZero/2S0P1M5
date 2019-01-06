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

public partial class aspx_admin_Setlendnum : System.Web.UI.Page
{
    string pattern = @"^[1-9]$|^[1-9][0-9]{0,1}$";
    string querysql = "Select max_borrow from lm_adminSet where Id = 10000000";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
        if (!IsPostBack)
        {
            
            this.literal1.Text = "In order to ensure the rational allocation of book resources,";
            this.literal2.Text = "We need to set the maximum number of books that each reader can borrow as below:";
            this.textbox1.Text = GetLendnum();
        }
       
    }

    public string GetLendnum()
    {
        string querysql = "Select max_borrow from lm_adminSet where Id = 10000000";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        int value=int.Parse(obj.ToString());
        return value.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (textbox1.Text.Trim() == "")
        {
            this.confirm1.Text = "Please input your modification！";
            textbox1.Text = null;
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        else if (!Regex.IsMatch(textbox1.Text.Trim(), pattern))
        {
            this.confirm1.Text = "You should enter an integer within 100 and the highest digit cannot be 0.";
            textbox1.Text = null;
            //Response.Write("<script>alert('You should enter an integer within 100 and the highest digit cannot be 0.')</script>");
        }
        else
        {
            int num = Convert.ToInt32(textbox1.Text);
            string updatesql = "update lm_adminSet set max_borrow = " + num + " where Id = 10000000";
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
   
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-ShowAllSettings.aspx");
    }
}