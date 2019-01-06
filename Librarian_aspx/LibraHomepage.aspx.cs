using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class homepage : System.Web.UI.Page
{
    public String htmlStr, myself, rank;
    protected void Page_Load(object sender, EventArgs e)
    {
        login_statue();
        
        if (!IsPostBack)
        {
            login_statue();
            Repeater_Databind();
        }
        
    }

    void Repeater_Databind()
    {
        //string sqlStr = "select ISBN,book_name as title,book_name,borrow_frequency from lm_books where borrow_frequency in (select top 5 borrow_frequency from lm_books) and borrow_frequency>0";
        string sqlStr = "select top 5 ISBN,book_name as title,book_name,borrow_frequency from lm_books where borrow_frequency>0 order by borrow_frequency desc;";
        string noticeSql = "select top 5 title,notice_id,release_time from lm_notice order by release_time desc";
        DataSet ds = DB.getDataSet(sqlStr);
        if (ds.Tables[0].Rows.Count > 0)
        {
            rank = "<td style='text-align: center'>title</td><td style ='text-align: center'> frequency </td> ";
        }
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        DataSet ds1 = DB.getDataSet(noticeSql);
        Repeater2.DataSource = ds1;
        Repeater2.DataBind();
    }
    protected void login_statue()
    {
        if (this.Session["LibraId"] == null && this.Session["LibraName"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["LibraName"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = 'LibraperCenterLibra.aspx'>personal Information</a></li><li><a href = 'LibraUpdatePassword.aspx'>update password</a></li><li><a href='../Login.aspx'>exit</a></li></ul></li></ul>";
        }
    }
    
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ISBN")
        {
            Session["infoResource"] = "homepage";
            String ISBN = e.CommandArgument.ToString();
            this.Session["ISBN"] = ISBN;
            Response.Redirect("LibraMoreBookInformation.aspx?ISBN=" + ISBN);
        }
    }
    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "detailNotice")
        {
            String id = e.CommandArgument.ToString();
            Response.Redirect("LibraDetailAnnounce.aspx?notice_id=" + id);
        }
    }
}