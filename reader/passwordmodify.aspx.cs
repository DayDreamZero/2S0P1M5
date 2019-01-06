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

public partial class passwordmodify : System.Web.UI.Page
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

    protected void ChangeButton_Click(object sender, EventArgs e)
    {
        //string user_id = this.Session["userid"].ToString();
        //读取web.config配置文件数据库连接字符串
        string strConstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //创建sql数据库连接对象
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = strConstring;
        conn.Open();
        string newpd = Newpassword.Text.Trim();
        string newpd1 = Newpassword1.Text.Trim();
        Regex r = new Regex(@"^[a-zA-Z0-9]+$");
        if (newpd == ""&& newpd1 == "")
        {
            this.Label5.Text = "password cannot be null!";
            this.Label6.Text = "password cannot be null!";
        }
        else if(newpd == ""&& newpd1!="")
        {
            this.Label5.Text = "password cannot be null!";
        }else if(newpd1 == ""&&newpd!="")
        {
            this.Label6.Text = "password cannot be null!";
        }
        else
        {
            if (newpd.Length>=8&& newpd.Length <= 16)
            {
                if (r.IsMatch(Newpassword.Text))
                {
                    if (newpd == newpd1)
                    {
                        SqlCommand selectedcmd = new SqlCommand("select userpassword from lm_users where user_id='" + Session["userid"].ToString() + "'", conn);
                        SqlDataReader dr = selectedcmd.ExecuteReader();
                        if (dr.Read())
                        {
                            string oldpassword = dr.GetString(dr.GetOrdinal("userpassword"));
                            dr.Close();
                            if (oldpassword != newpd)
                            {
                                SqlCommand updatecmd = new SqlCommand("update lm_users set userpassword='" + Newpassword.Text.Trim() + "'where user_id='" + Session["userid"].ToString() + "'", conn);
                                int i = updatecmd.ExecuteNonQuery();
                                if (i == 1)
                                {

                                    Response.Write("<script>alert('Successful!')</script>");
                                    Server.Transfer("homepage.aspx");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<script language=javascript>alert('Failed!Your password modified failed.');location.href='" + url + "'</script>");
                                    //Server.Transfer("passwordmodify.aspx");
                                }
                            }else
                            {
                                HttpContext.Current.Response.Write("<script language=javascript>alert('Failed,New and old passwords are consistent!');location.href='" + url + "'</script>");
                                //Server.Transfer("passwordmodify.aspx");
                            }
                        }
                        
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Two password entries are inconsistent,please check!');location.href='" + url + "'</script>");
                        //Server.Transfer("passwordmodify.aspx");
                    }
                }else
                {
                    //Response.Write("<script>alert('password must be digits and letter combinations,please check!');</" + "script>");
                    //Server.Transfer("passwordmodify.aspx");
                    this.Label5.Text = "password must be digits and letter combinations";
                }
            }
            else
            {
                //Response.Write("<script>alert('password is too short or too long, the length must be 8-16, please check!');</" + "script>");
                //Server.Transfer("passwordmodify.aspx");
                this.Label5.Text = "password is too short or too long, the length must be 8-16!";
            }
        }
        conn.Close();
        login_statue();
    }
}