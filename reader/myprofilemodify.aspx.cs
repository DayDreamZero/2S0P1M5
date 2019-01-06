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

public partial class myprofile : System.Web.UI.Page
{
    public String htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null && Session["userid"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = 'myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";

        }
        if (!this.IsPostBack)
        {
            BindDataToRepeater();
        }
    }
    string url = HttpContext.Current.Request.RawUrl;
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        // SqlCommand comm = new SqlCommand();
        string username = UserName.Text.ToString().TrimStart().TrimEnd();
        string telephone = Telephone.Text.ToString().Trim();
        string email = Email.Text.ToString().Trim();
        string name = FileUpload1.PostedFile.FileName;
        //string fileType = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf("."));
        if (name != "")
        {
            string path = Server.MapPath("~/image/") + name;
            FileUpload1.SaveAs(path);
            //Response.Write(path);
        }
        string user_picture = "../image/" + name;
        string user_id = this.Session["userid"].ToString();
        string sql;
        // Regex r3 = new Regex(@"^[A-Za-z]|[\u4e00-\u9fa5]{0,}$");
        int count = 0;
        if (!username.Contains("'"))
        {
            //sql = "update lm_users set username='" + username + "' where user_id=1000";
            sql = "update lm_users set username=N'" + username + "' where user_id='" + user_id + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            count += comm.ExecuteNonQuery();
            String teleSql = "select * from lm_users where telephone='" + telephone + "' and user_id!=" + user_id;
            DataSet TeleCheck = DB.getDataSet(teleSql);
            if (TeleCheck.Tables[0].Rows.Count != 0)
            {
                Response.Write("<script>alert('The telephone has been used by other reader.')</script>");
            }
            else
            {
                sql = "update lm_users set telephone='" + telephone + "' where user_id='" + user_id + "'";
                SqlCommand comm3 = new SqlCommand(sql, conn);
                count += comm3.ExecuteNonQuery();
                String mailSql = "select * from lm_users where email='" + email + "' and user_id!=" + user_id;
                DataSet mailCheck = DB.getDataSet(mailSql);
                if (mailCheck.Tables[0].Rows.Count != 0)
                {
                    Response.Write("<script>alert('The emailbox has been used by other reader.')</script>");
                }
                else
                {
                    //sql = "update lm_users set email='" + email + "' where user_id=1000";
                    sql = "update lm_users set email='" + email + "' where user_id='" + user_id + "'";
                    SqlCommand comm1 = new SqlCommand(sql, conn);
                    count += comm1.ExecuteNonQuery();
                    if (name != "")
                    {
                        sql = "update lm_users set user_picture='" + user_picture + "' where user_id='" + user_id + "'";
                        //sql = "update lm_users set user_picture='" + user_picture + "' where user_id=1000";
                        SqlCommand comm2 = new SqlCommand(sql, conn);
                        count += comm2.ExecuteNonQuery();
                        HttpContext.Current.Session["username"] = username;
                    }
                    if (count != 0)
                    {
                        HttpContext.Current.Session["username"] = username;
                        Response.Write("<script>alert('modify successful!')</script>");
                        Server.Transfer("myprofileshow.aspx");
                    }
                }

            }
            //Server.Transfer("myprofilemodify.aspx");
            conn.Close();
        }
    }
    protected void DrawbackButton_Click(object sender, EventArgs e)
    {
        UserName.Text = "";
        Telephone.Text = "";
        Email.Text = "";
        Response.Redirect("myprofileshow.aspx");
    }

    private void BindDataToRepeater()
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        string user_id = this.Session["userid"].ToString();
        SqlCommand comm = new SqlCommand("select * from lm_users where user_id='" + user_id + "'", conn);
        //SqlCommand comm = new SqlCommand("select * from lm_users where user_id=1000", conn);
        DataSet ds = new DataSet();
        SqlDataReader dr = comm.ExecuteReader();
        if (dr.Read())
        {
            this.UserName.Text = dr.GetString(dr.GetOrdinal("username"));
            this.Telephone.Text = dr.GetString(dr.GetOrdinal("telephone"));
            this.Email.Text = dr.GetString(dr.GetOrdinal("email"));
            this.Image1.ImageUrl = dr.GetString(dr.GetOrdinal("user_picture"));
        }
        while (dr.Read())
        {
            Response.Write(dr["username"]);
            Response.Write(dr["telephone"]);
            Response.Write(dr["email"]);
            Response.Write(dr["user_picture"]);
        }
        dr.Close();
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }
    }