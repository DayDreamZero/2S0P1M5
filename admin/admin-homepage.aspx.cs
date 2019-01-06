using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public partial class aspx_admin_homepage : System.Web.UI.Page
{
    public String htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
        if (!IsPostBack)
        {
            
            this.library.Text = "Welcome to BiblioSoft Management.";
        }
    }
    
}