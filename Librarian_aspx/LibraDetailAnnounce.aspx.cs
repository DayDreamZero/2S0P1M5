using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraDetailAnnounce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {
        string notice_id = Request["notice_id"];
        string sqlStr = "select title,publicist,release_time,modifier,last_modify_time,content from lm_notice,lm_librarian" +
            " where notice_id=" + notice_id;
        DataSet ds = DB.getDataSet(sqlStr);
        FormView1.DataSource = ds;
        FormView1.DataBind();
    }
    protected void ComebackBt_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraAnnouncement.aspx");
    }
}