using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraEditAnnounce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        
        if (!IsPostBack)
        {
            FormViewBindData();
        }
    }

    private void FormViewBindData()
    {
        String id = Request["notice_id"];
        String sql = "select title,publicist,content,release_time from lm_notice where notice_id=" + id;
        DataSet ds = DB.getDataSet(sql);
        FormView1.DataSource = ds;
        FormView1.DataBind();
    }

    protected void edit_btn_Click(object sender, EventArgs e)
    {
        String id = Request["notice_id"];
        String title = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("title_content")).Text.Trim());
        String content = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("contentbox")).Text.Trim());
        String sql = "update lm_notice set modifier=" + HttpContext.Current.Session["LibraId"].ToString() + 
            ",last_modify_time=getdate(),title=N'" + title + "',content=N'" + content + "' where notice_id="+id;
        DB.executeNonQuery(sql);
        Response.Redirect("LibraAnnouncement.aspx");
    }
}