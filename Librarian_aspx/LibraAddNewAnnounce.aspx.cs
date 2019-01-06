using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraAddNewAnnounce : System.Web.UI.Page
{
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
    }
    protected void release_btn_Click(object sender, EventArgs e)
    {
        String title = LibraCheckInput.transApostrophe(title_content.Text);
        String content = LibraCheckInput.transApostrophe(contentbox.Text);
        String sql = "insert into lm_notice(release_time,publicist,title,content) values(getdate(),N'" + HttpContext.Current.Session["LibraId"].ToString() + "',N'" + title + "',N'" + content + "')";
        DB.executeNonQuery(sql);
        Response.Redirect("LibraHomepage.aspx");
    }
}