using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class oldpasswordcheck : System.Web.UI.Page
{
    public string htmlStr;
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            login_statue();

        }
    }
    protected void login_statue()
    {
        if (this.Session["username"] == null && this.Session["userid"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = '/reader/myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
        }
    }

    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        //string user_id = this.Session["userid"].ToString();
        //读取web.config配置文件数据库连接字符串
        string strConstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //创建sql数据库连接对象
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = strConstring;
        conn.Open();
        Regex r = new Regex(@"^[a-zA-Z0-9]+$");
        if (Oldpassword.Text.Trim() == "")
        {
            this.Label2.Text = "password cannot be null!";
        }
        else if(!r.IsMatch(Oldpassword.Text.Trim()))
        {
            this.Label2.Text = "password format is wrong, please check!";
        }
        else
        {
            SqlCommand selectedcmd = new SqlCommand("select userpassword from lm_users where user_id='" + Session["userid"].ToString() + "'and userpassword='" + Oldpassword.Text.Trim() + "'", conn);
            SqlDataReader dr = selectedcmd.ExecuteReader();
            if (dr.Read())
            {
                Response.Redirect("passwordmodify.aspx");
            }else
            {
                HttpContext.Current.Response.Write("<script>alert('Your old password is wrong, Please check!');location.href='" + url + "'</script>");
                //Server.Transfer("oldpasswordcheck.aspx");
            }
        }
        login_statue();
        conn.Close();
    }
}