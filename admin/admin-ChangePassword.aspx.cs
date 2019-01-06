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

public partial class admin_admin_ChangePassword : System.Web.UI.Page
{
    string pattern = @"^[A-Za-z_0-9]{8,16}$";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        comfirm1.Visible = false;
        comfirm2.Visible = false;
        comfirm3.Visible = false;
        comfirm4.Visible = false;
        if (!IsPostBack)
        {
            
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
    }
    protected string GetDefaultPwd()
    {
       string querysql = "Select password from lm_admin where Id = "+ HttpContext.Current.Session["admin_id"].ToString();
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(querysql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        return obj.ToString();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-information-edit.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (textbox1.Text.Trim() == "" || textbox2.Text.Trim() == "" || textbox3.Text.Trim() == "")
        {
            comfirm4.Visible = true;
            comfirm4.Text = "Please input your modification！";
            //Response.Write("<script>alert('Please input your modification！')</script>");
        }
        else if(!textbox1.Text.Trim().Equals(GetDefaultPwd()))
        {
            comfirm1.Visible = true;
            comfirm1.Text = "Old password error!";
            //Response.Write("<script>alert('Old password error!')</script>");
        }
        else
        {
            if(textbox2.Text.Trim().Equals(GetDefaultPwd()))
            {
                comfirm2.Visible = true;
                comfirm2.Text = "Your new password cannot be same as the old!";
                //Response.Write("<script>alert('Your new password cannot be same as the old!')</script>");
            }
            else if (!Regex.IsMatch(textbox2.Text.Trim(), pattern))
            {
                comfirm2.Visible = true;
                comfirm2.Text = "You must enter characters of 8-16 digits, numbers and underscores!";
                //Response.Write("<script>alert('Password format error!')</script>");
            }
            else if(!textbox2.Text.Trim().Equals(textbox3.Text.Trim()))
            {
                comfirm3.Visible = true;
                comfirm3.Text = "You must input new password twice at same!";
                //Response.Write("<script>alert('You must input new password twice at same!')</script>");
            }
            else
            {
                string psd = textbox2.Text.ToString();
                string updatesql = "update lm_admin set password = '" + psd + "' where Id = " + HttpContext.Current.Session["admin_id"];
                string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(updatesql, conn);
                cmd.ExecuteScalar();
                conn.Close();
                string url = "admin-information-show.aspx";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Modify Successfully！');window.location='" + url + "';</script>");
            }
        }
    }
    
}