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

public partial class aspx_admin_LibrarianGroup : System.Web.UI.Page
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
        string sql = "select * from lm_librarian where librarian_status = 0;";
        ds = DB.getDataSet(sql);
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            htmlstr += "{\"id\":\"" + col["librarian_id"].ToString() + "\","
                      + "\"username\":\"" + col["librarian_name"].ToString() + "\","
                      + "\"email\":\"" + col["email"].ToString() + "\","
                      + "\"telephone\":\"" + col["telephone"].ToString() + "\","
                      //+ "\"balance\":\"" + col["balance"].ToString() + "\","
                      + "},";

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String input1 = Request["input1"].Trim().ToString(); 
        String input2 = Request["input2"].Trim().ToString();
       // int u_id = int.Parse(input1);
        htmlstr = null;
        if(input1.Contains("'")||input2.Contains("'"))
        {
            Response.Write("<script>alert('Your input cannot contain apostrophe!')</script>");
        }
        else
        {
            string sql = "select * from lm_librarian where librarian_status = 0 and (librarian_name like '%" + input2 + "%' and  CONVERT(VARCHAR(50),librarian_id) like '%" + input1 + "%');";
            ds = DB.getDataSet(sql);
            foreach (DataRow col in ds.Tables[0].Rows)
            {
                htmlstr += "{\"id\":\"" + col["librarian_id"].ToString() + "\","
                            + "\"username\":\"" + col["librarian_name"].ToString() + "\","
                            + "\"email\":\"" + col["email"].ToString() + "\","
                            + "\"telephone\":\"" + col["telephone"].ToString() + "\","
                            // + "\"balance\":\"" + col["balance"].ToString() + "\","
                            + "},";
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String input1 =  Request["input1"].ToString();
        String input2 = Request["input2"].ToString();
        input1 = null;
        input2 = null;
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("../admin/admin-AddLibrarian.aspx");
    }

    protected void Add_Click(object sender, EventArgs e)
    {

    }
    
}