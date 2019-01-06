using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public String htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        login_statue();
        if (!IsPostBack)
        {
            

        }
        Repeater_Databind();

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
    void Repeater_Databind()
    {
        string sqlStr = "select book_picture,book_name,author,publishing_house,price,inventory, amount - inventory as amount_inven ,amount,ISBN from lm_books";
        DataSet ds = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;
        Repeater1.DataSource = ds;

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = ds.Tables[0].DefaultView;
        pds.AllowPaging = true;//允许分页
        pds.PageSize = 5;//单页显示项数
        this.PageNum.Text = pds.PageCount.ToString();

        int curpage = Convert.ToInt32(curPageNum.Text);
        this.BtnDown.Enabled = true;
        this.BtnUp.Enabled = true;
        pds.CurrentPageIndex = curpage - 1;
        if (curpage == 1)
        {
            this.BtnUp.Enabled = false;
        }
        if (curpage == pds.PageCount)
        {
            this.BtnDown.Enabled = false;
        }
        this.Repeater1.DataSource = pds;
        Repeater1.DataBind();
    }
    protected void BtnUp_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = Convert.ToString(Convert.ToInt32(curPageNum.Text) - 1);
        login_statue();
        Repeater_Databind();
    }

    protected void BtnDown_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = Convert.ToString(Convert.ToInt32(curPageNum.Text) + 1);
        login_statue();
        Repeater_Databind();
    }

    protected void FirstPage_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = "1";
        login_statue();
        Repeater_Databind();
    }


    protected void LastPage_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = this.PageNum.Text;
        login_statue();
        Repeater_Databind();
    }
    /*protected void reserve_book(object sender, EventArgs e)
    {
       
       if (!IsPostBack)
        {
            if (this.Session["username"] == null && this.Session["userid"] == null)
            {
                Response.Write("<script type='text/javascript'>alert('please login first!');</script>");
            }
            else
            {

            }
        }
       
    }*/


    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "MoreInfo")
        {
            //Session["infoResource"] = "booklist";
            //string ISBN = ((Label)Item.FindControl("ISBN")).Text;
            String ISBN = e.CommandArgument.ToString();
            this.Session["ISBN"] = ISBN;
            Response.Write("<script>window.open('morebookinformation.aspx?ISBN=" + ISBN + "','_blank')</script>");
            //Response.Redirect("morebookinformation.aspx?ISBN=" + ISBN);
        }
        
        
    }
}