using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Security;

public partial class admin_admin_information_show : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
        if (!IsPostBack)
        {
            
            BindDataToRepeater();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-information-edit.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-homepage.aspx");
    }

    protected void allFileSize_ValueChanged(object sender, EventArgs e)
    {

    }
    private void BindDataToRepeater()
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from lm_admin where Id='" + HttpContext.Current.Session["admin_id"].ToString() + "'", conn);
        DataSet ds = new DataSet();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            this.username.Text = dr.GetString(dr.GetOrdinal("name"));
            this.admin_id.Text = HttpContext.Current.Session["admin_id"].ToString();
            if (!dr.IsDBNull(dr.GetOrdinal("telephone")))
            {
                this.tel.Text = dr.GetString(dr.GetOrdinal("telephone"));
            }
            else
            {
                this.tel.Text = "";
            }
            if (!dr.IsDBNull(dr.GetOrdinal("email")))
            {
                this.email.Text = dr.GetString(dr.GetOrdinal("email"));
            }
            else
            {
                this.tel.Text = "";
            }
            if (!dr.IsDBNull(dr.GetOrdinal("picture")))
            {
                this.tmpPic.ImageUrl = dr.GetString(dr.GetOrdinal("picture"));
            }
            else
            {
                this.tel.Text = "";
            }
        }
        while (dr.Read())
        {
            Response.Write(dr["name"]);
            Response.Write(dr["password"]);
            Response.Write(dr["telephone"]);
            Response.Write(dr["email"]);
            Response.Write(dr["picture"]);
        }
        dr.Close();
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }
}
