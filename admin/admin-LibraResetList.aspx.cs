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

public partial class admin_LibraResetList : System.Web.UI.Page
{
    public string htmlstr;
    private DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        this.label1.Text = HttpContext.Current.Session["admin_name"].ToString();
       
        string sql = "select * from lm_libraResetPwd order by email_status asc;";
        ds = DB.getDataSet(sql);
        
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            string status_tmp = "Unhandled";
            if (int.Parse(col["email_status"].ToString()) == 1)
                status_tmp = "Handled";

            htmlstr += "{\"id\":\"" + col["libra_id"].ToString() + "\","
                      + "\"email\":\"" + col["libra_email"].ToString() + "\","
                      + "\"status\":\"" + status_tmp + "\","
                      + "\"time\":\"" + col["email_sendTime"].ToString() + "\","
                      + "},";
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-LibraResetList.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        htmlstr = null;
        string sql = "select * from lm_libraResetPwd where email_status = 0;";
        ds = DB.getDataSet(sql);
        
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            string status_tmp = "Unhandled";
            if (int.Parse(col["email_status"].ToString()) == 1)
                status_tmp = "Handled";
            htmlstr += "{\"id\":\"" + col["libra_id"].ToString() + "\","
                      + "\"email\":\"" + col["libra_email"].ToString() + "\","
                      + "\"status\":\"" + status_tmp + "\","
                      + "\"time\":\"" + col["email_sendTime"].ToString() + "\","
                      + "},";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        htmlstr = null;
        string sql = "select * from lm_libraResetPwd where email_status = 1;";
        ds = DB.getDataSet(sql);
        
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            string status_tmp = "Unhandled";
            if (int.Parse(col["email_status"].ToString()) == 1)
                status_tmp = "Handled";
            htmlstr += "{\"id\":\"" + col["libra_id"].ToString() + "\","
                      + "\"email\":\"" + col["libra_email"].ToString() + "\","
                      + "\"status\":\"" + status_tmp + "\","
                      + "\"time\":\"" + col["email_sendTime"].ToString() + "\","
                      + "},";
        }
    }
}