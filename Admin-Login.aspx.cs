using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class Admin_Login : System.Web.UI.Page
{
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    

    protected void log_Click(object sender, EventArgs e)
    {
        if(lN.Text.Trim()!= string.Empty && Pw.Text.Trim()!=string.Empty)
        {
            int b;
            if (!int.TryParse(lN.Text.Trim(),out b))
            {
                HttpContext.Current.Response.Write("<script>alert('Valid Admin_ID! ');location.href='" + url + "'</script>");
                //Response.Write("<script>alert('Valid Admin_ID! ')</script>");
            }
            else
            {
                string sqlStr = "select * from lm_admin where Id =" + lN.Text.Trim() + " and password ='" + Pw.Text.Trim() + "';";
                DataSet ds = DB.getDataSet(sqlStr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.Session["admin_id"] = ds.Tables[0].Rows[0][0].ToString().Trim();
                    this.Session["admin_name"] = ds.Tables[0].Rows[0][2].ToString().Trim();
                    Response.Redirect("admin/admin-homepage.aspx");
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>alert('Incorrect acoountID or password!');location.href='" + url + "'</script>");
                   // Response.Write("<script>alert('Incorrect acoountID or password!')</script>");
                    
                }
            }
        }
        else
        {
            HttpContext.Current.Response.Write("<script>alert('Please input completely! ');location.href='" + url + "'</script>");
            //Response.Write("<script>alert('Please input completely! ')</script>");
            
        }
    }
}