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
        if (!IsPostBack)
        {
            
            
        }
        Repeater_Databind();
        login_statue();
    }
    private void CheckMailSend()
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        string sql = "select user_id from borrow_overdue_record group by user_id";
        DataSet ds = DB.getDataSet(sql);
        foreach (DataRow mdr in ds.Tables[0].Rows)
        {
            string text = "";
            string text2 = "";
            string id = "";
            string email = "";
            string sql1 = "select * from borrow_overdue_record where user_id='" + Convert.ToInt32(mdr[0].ToString()) + "'";
            DataSet ds1 = DB.getDataSet(sql1);
            int i = 0;
            int j = 0;
            foreach (DataRow mdr1 in ds1.Tables[0].Rows)
            {
                int sendmail_status = Convert.ToInt32(mdr1[5].ToString());
                id = mdr1[1].ToString();
                string borrow_id = mdr1[0].ToString();
                email = Convert.ToString(mdr1[2].ToString());
                //string text = Convert.ToString(mdr[2].ToString());
                if (sendmail_status == 0)
                {
                    sql = "update lm_borrowed_record set sendmail_date=GETDATE(),sendmail_status=1 where sendmail_status=0 and user_id='" + Convert.ToInt32(mdr[0].ToString()) + "' and borrowed_id = '" + Convert.ToInt32(mdr1[0].ToString()) + "'";
                    DB.executeNonQuery(sql);
                    text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>" + Convert.ToString(mdr1[3].ToString());
                    i = 1;
                }
                else if (sendmail_status == 1)
                {
                    sql = "update lm_borrowed_record set sendmail_date=GETDATE() where DATEDIFF(DAY,sendmail_date,GETDATE())>=(select sendmail_interval from lm_adminSet where Id=10000000) and sendmail_status=1 and user_id='" + Convert.ToInt32(mdr[0].ToString()) + "' and borrowed_id = '" + Convert.ToInt32(mdr1[0].ToString()) + "'";
                    int itt = DB.executeNonQuery(sql);
                    if (itt != 0)
                    {
                        text2 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>" + Convert.ToString(mdr1[3].ToString());
                        j = 1;
                    }
                }
            }
            if (text != "" || text2 != "")
            {
                sendEmail(email, id, text, text2, i, j);
            }
        }
    }
    public void sendEmail(string email, string id, string text, string text2, int i, int j)
    {
        try
        {
            string senduser = "18291884205@163.com";// "test@qq.com";
            string sendpwd = "ciery123";// "test";

            string receiver = email;
            string sendmsg = "";
            if (i == 1)
            {
                sendmsg += "<br>Dear reader, you have one/more books which will be overdue one day after, please return in time,thank you!<br> This is the booklist:<br>" + text + "<br>";
            }
            if (j == 1)
            {
                sendmsg += "<br><br>The book you borrowed has been overdue over ten days, please return it as soon as possible.<br> Your cooperation is the greatest support for the library work, thank you!" + "<br>The overdue book list:<br>" + text2 + "<br>";
            }
            MailMessage message = new MailMessage();
            MailAddress maddr = new MailAddress(senduser);
            message.From = maddr;

            message.To.Add(receiver);//接收方
                                     //message.CC.Add(receiver);//抄送方
            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = "Overdue books";//主题
            message.BodyEncoding = Encoding.Default;
            message.Body = sendmsg;//内容
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();//基于qq邮件发送
            client.Host = "smtp.163.com";
            client.Credentials = new NetworkCredential(senduser, sendpwd);
            client.EnableSsl = true;
            client.Send(message);
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            Response.Write(ex);
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
        if (this.Session["username"] == null && this.Session["userid"] == null)
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li><a href = '/Login.aspx'> Login </a></li></ul>";
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = '/reader/myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
        }
    }
    protected void search(object sender, EventArgs e)
    {
        String sqlStr = null;
        if (searchBox.Text == "" && CheckBox1.Checked != true)
        {
            Response.Write("<script language=javascript>alert('please input fields!');</" + "script>");
            Server.Transfer("homepage.aspx");
        }
        else if (searchBox.Text != "")
        {
            String box = searchBox.Text;
            if(box.Contains("'"))
            {
                Response.Write("<script>alert('Your input cannot contain apostrophe!')</script>");
            }
            else if (CheckBox1.Checked == true)
            {
                String cg = category.SelectedItem.ToString();
                if (searchBy.SelectedItem.ToString() == "title")//按书名+类别筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and book_name like N'%" + box + "%'";
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["checkbox"] = 1;
                    Session["category"] = cg;
                    Session["searchBy"] = searchBy.SelectedItem.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
                else if (searchBy.SelectedItem.ToString() == "author")//按作者名+类别筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and author like N'%" + box + "%'";
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["checkbox"] = 1;
                    Session["category"] = cg;
                    Session["searchBy"] = searchBy.SelectedItem.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
                else//ISBN
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and lm_books.ISBN like N'%" + box + "%'";
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["checkbox"] = 1;
                    Session["category"] = cg;
                    Session["searchBy"] = searchBy.SelectedItem.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
            }
            else
            {
                if (searchBy.SelectedItem.ToString() == "title")//按书名筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and book_name like N'%" + box + "%'";
                    //Repeater_Databind(sqlStr);
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["searchBy"] = searchBy.SelectedItem.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
                else if (searchBy.SelectedItem.ToString() == "author") //按作者名筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and author like N'%" + box + "%'";
                    //Repeater_Databind(sqlStr);
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["searchBy"] = searchBy.SelectedItem.Text.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
                else
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and lm_books.ISBN like N'%" + box + "%'";
                    Session["sql"] = sqlStr;
                    Session["box"] = searchBox.Text;
                    Session["searchBy"] = searchBy.SelectedItem.Text.ToString();
                    Server.Transfer("advancedSearch.aspx");
                }
            }

        }
        else if (searchBox.Text == "" && CheckBox1.Checked == true)
        {//按类别筛选
            String cg = category.SelectedItem.ToString();
            sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%')";
            //Repeater_Databind(sqlStr);
            Session["sql"] = sqlStr;
            Session["checkbox"] = 1;
            Session["category"] = cg;
            Server.Transfer("advancedSearch.aspx");
        }

    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ISBN")
        {
            Session["infoResource"] = "homepage";
            String ISBN = e.CommandArgument.ToString();
            this.Session["ISBN"] = ISBN;
            Response.Write("<script>window.open('morebookinformation.aspx?ISBN=" + ISBN + "','_blank')</script>");
        }
    }
    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "detailNotice")
        {
            String id = e.CommandArgument.ToString();
            Response.Redirect("DetailAnnounce.aspx?notice_id=" + id);
        }
    }
}