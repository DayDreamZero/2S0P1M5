using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reader_DetailAnnounce : System.Web.UI.Page
{
    public String htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindData();
        }
        login_statue();
    }
    protected void login_statue()
    {
        if (this.Session["username"] == null && this.Session["userid"] == null)
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li><a href = '/Login.aspx'> Login </a></li></ul>";
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = '/reader/myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
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
        Response.Redirect("homepage.aspx");
    }
}