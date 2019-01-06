using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

public partial class Login : System.Web.UI.Page
{
    //public string htmlstr;
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        //clear session information
        Session.Clear();
        UpdateOverdueBook();
        CheckMailSend();
    }
    public void UpdateReserveFailBook()
    {
        //< asp:ScriptManager ID = "ScriptManager1" runat = "server" EnablePartialRendering = "true" ></ asp:ScriptManager >
        // < asp:UpdatePanel ID = "UpdatePanel1" runat = "server" ></ asp:UpdatePanel >
        // < asp:Timer ID = "Timer1" runat = "server" Interval = "10000" ontick = "Timer1_Tick" Enabled = "true" ></ asp:Timer >
        string sqlStr = "select set_reserve_period from lm_adminSet where id=10000000";
        DataSet ds = DB.getDataSet(sqlStr);
        int reserve_period = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        sqlStr = "select reserve_id from lm_reserve_record" + " where DATEDIFF(MINUTE,starttime,GETDATE())>" + reserve_period + " and reserve_status=0";
        ds = DB.getDataSet(sqlStr);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_reserve_record set reserve_status=1"
            + " where reserve_id=" + ds.Tables[0].Rows[i]["reserve_id"].ToString();
            DB.executeNonQuery(sqlStr);
        }
    }

    private void UpdateOverdueBook()
    {
        string sqlStr = "select set_penalty_overdue,set_return_period from lm_adminSet where Id=10000000;";
        DataSet ds = DB.getDataSet(sqlStr);
        Decimal fineDay = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
        int returnDays = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

        //检查是否没超期的是否超期并更新
        sqlStr = "select borrowed_id from lm_borrowed_record" + " where DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=0";
        ds = DB.getDataSet(sqlStr);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set borrowed_status=2,fine_status='no',fine=(DATEDIFF(DAY,borrowed_date,GETDATE())-" + returnDays + ")*" + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }


        //更新已超期的
        sqlStr = "select borrowed_id from lm_borrowed_record " + " where fine_status='no'and DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=2";
        ds = DB.getDataSet(sqlStr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set fine = (DATEDIFF(DAY, borrowed_date, GETDATE()) - " + returnDays + ") * " + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }
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
                    text += Convert.ToString(mdr1[3].ToString())+";";
                    i = 1;
                }
                else if (sendmail_status == 1)
                {
                    sql = "update lm_borrowed_record set sendmail_date=GETDATE() where DATEDIFF(DAY,sendmail_date,GETDATE())>=(select sendmail_interval from lm_adminSet where Id=10000000) and sendmail_status=1 and user_id='" + Convert.ToInt32(mdr[0].ToString()) + "' and borrowed_id = '" + Convert.ToInt32(mdr1[0].ToString()) + "'";
                    int itt = DB.executeNonQuery(sql);
                    if (itt != 0)
                    {
                        text2 += Convert.ToString(mdr1[3].ToString())+";";
                        j = 1;
                    }
                }
            }
            if (text != ""||text2 != "")
            {
                sendEmail(email, id, text,text2,i,j);
            }
        }
    }
    public void sendEmail(string email, string id, string text, string text2,int i,int j)
    {
        try
        {
            string senduser = "18291884205@163.com";// "test@qq.com";
            string sendpwd = "ciery123";// "test";
            //string senduser = "liuyang655@163.com";
            //string sendpwd = "miman17648250";
            //string senduser = "ypg100500@163.com";
            //string sendpwd = "Yp497397";

            string receiver = email;
            string sendmsg = "";
            if (i == 1)
            {
                sendmsg += "<br>There may be one/more books which will be overdue one day after, please return in time,thank you!<br> This is the booklist:"+text+ "<br>";
                //sendmsg += "Below is the booklist:" + text;
            }
            if (j == 1)
            {
                sendmsg +="The overdue book list:" + text2+"<br>Please return the book quickly!";
            }
            MailMessage message = new MailMessage();
            MailAddress maddr = new MailAddress(senduser);
            message.From = maddr;

            message.To.Add(receiver);//接收方
            message.CC.Add(senduser);//抄送方
            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = "From library, you have books which will be overdue, please return timely";//主题
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
    protected void log_Click(object sender, EventArgs e)
    {
        if(lN.Text.Trim() != string.Empty && Pw.Text.Trim() != string.Empty)
        {
            long b;
            if(!long.TryParse(lN.Text.Trim(),out b))
            {
                HttpContext.Current.Response.Write("<script>alert('Invalid Account ID! ');location.href='" + url + "'</script>");
            }
            else
            {
                if (long.Parse(lN.Text.Trim()) < 1000)
                {
                    string sqlStr = "select * from lm_librarian where librarian_id =" + lN.Text.Trim() + " and librarian_pw ='" + Pw.Text.Trim() + "';";
                    DataSet ds = DB.getDataSet(sqlStr);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][6].ToString().Trim() == "0")
                        {
                            this.Session["LibraId"] = ds.Tables[0].Rows[0][0].ToString().Trim();
                            this.Session["LibraName"] = ds.Tables[0].Rows[0][2].ToString().Trim();
                            Response.Redirect("Librarian_aspx/LibraHomepage.aspx");
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<script>alert('Invalid Account! ');location.href='" + url + "'</script>");
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Incorrect account or password!');location.href='" + url + "'</script>");
                    }
                }
                else
                {
                    string pattern_telephone = @"^$|^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";
                    if (!Regex.IsMatch(lN.Text.Trim(), pattern_telephone))
                    {
                        Response.Write("<script>alert('Telephone format error!')</script>");
                    }
                    else
                    {
                        string sqlStr = "select * from lm_users where telephone ='" + lN.Text.Trim() + "' and userpassword ='" + Pw.Text.Trim() + "';";
                        DataSet ds = DB.getDataSet(sqlStr);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0][8].ToString().Trim() == "0")
                            {
                                this.Session["userid"] = ds.Tables[0].Rows[0][0].ToString().Trim();
                                this.Session["username"] = ds.Tables[0].Rows[0][1].ToString().Trim();
                                Response.Redirect("reader/homepage.aspx");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<script>alert('Invalid Account! ');location.href='" + url + "'</script>");
                            }
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<script>alert('Incorrect accountID or password!');location.href='" + url + "'</script>");

                        }
                    }
                }
            }
            
        }
        else
        {
            HttpContext.Current.Response.Write("<script>alert('Please input completely!');location.href='" + url + "'</script>");
        }
    }



    protected void ForgetPwdBt_Click(object sender, EventArgs e)
    {
        LibraLink.Visible = true;
        ReaderLink.Visible = true;
    }

    protected void ReaderLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("reader/passwordrecovery.aspx");
    }

    protected void LibraLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("Librarian_aspx/LibraResetPwd.aspx");
    }
}