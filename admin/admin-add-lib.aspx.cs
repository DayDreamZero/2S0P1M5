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

public partial class admin_admin_add_lib : System.Web.UI.Page
{
    int num;
    int maxid;
    protected void Page_Load(object sender, EventArgs e)
    {
        num = int.Parse(Request.Params["number"]);
        maxid = getLibID();
        for (int i=1;i <= num;i++)
        {
            string sql = "Insert into lm_librarian([librarian_name],[librarian_pw],[picture]) values('librarian_id" + (++maxid) + "','" + GetDefaultPwd() + "','user_head/default_lib_head.jpg');";
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Response.Redirect("admin-LibrarianGroup.aspx");
    }

    protected int getLibID()
    {
        string sql = "select max(librarian_id) from lm_librarian;";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        int value = int.Parse(obj.ToString());
        return value;
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

}