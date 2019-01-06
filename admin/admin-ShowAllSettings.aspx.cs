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

public partial class admin_admin_ShowAllSettings : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            
        }
        string sql = "select * from lm_adminSet where Id = 10000000;";
        ds = DB.getDataSet(sql);
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            htmlstr = "{\"setting\": \"max_borrow\" ," + " \"value\":\"" + col["max_borrow"] + "\"},"
                     + "{\"setting\": \"reader_deposit\"," + "\"value\":\"" + col["set_deposit"] + "\"},"
                     + "{\"setting\": \"penalty of lost\"," + "\"value\":\"" + col["set_penalty_lost"] + "\"},"
                     + "{\"setting\": \"penalty of overdue\"," + "\"value\":\"" + col["set_penalty_overdue"] + "\"},"
                     + "{\"setting\": \"period of return\"," + "\"value\":\"" + col["set_return_period"] + "\"},"
                     + "{\"setting\": \"period of reserve\"," + "\"value\":\"" + col["set_reserve_period"] + "\"},"
                     + "{\"setting\": \"default psd for librarian\"," + "\"value\":\"" + col["lib_password"] + "\"},"
                     + "{\"setting\": \"interval for send emails\"," + "\"value\":\"" + col["sendmail_interval"] + "\"},";

        }
    }

}
