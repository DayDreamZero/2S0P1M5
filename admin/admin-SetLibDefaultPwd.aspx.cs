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
public partial class admin_admin_SetLibDefaultPwd : System.Web.UI.Page
{
    string pattern = @"^[A-Za-z_0-9.]{8,16}$";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        if (!IsPostBack)
        {
            // this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
            this.literal1.Text = "You can set default password for registerring librarians: ";
            this.textbox1.Text = GetDefaultPwd();
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
    }
    protected string GetDefaultPwd()
    {
        string querysql = "Select lib_password from lm_adminSet where Id = 10000000";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        
       // char value = Convert.ToChar(obj);
        return obj.ToString();
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
            confirm1.Text = "You must enter 8-16 characters of letters, digits, underline and points!";
            //Response.Write("<script>alert('Password format error!')</script>");
        }
        else
        {
            string num = Convert.ToString(textbox1.Text);
            string updatesql = "update lm_adminSet set lib_password = '" + num + "' where Id = 10000000";
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