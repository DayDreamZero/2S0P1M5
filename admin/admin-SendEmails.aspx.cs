using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

public partial class aspx_admin_SendEmails : System.Web.UI.Page
{
    public string htmlstr;
    private DataSet ds;
    string pattern_email = @"^$|^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        if (!IsPostBack)
        {
            this.lab.Text = "Send email failed!";
            if (Request["lib_id"]!=null)
            {
                if (sendEmail(GetSendEmail(), GetReEmail()))
                {
                    UpdateStatus();
                    this.lab.Text = "Send email successfully!";
                }
               
            }
            
        }
    }

    public void UpdateStatus()
    {
        string sql = "update lm_libraResetPwd set email_status=1 where libra_id=" + Request["lib_id"] + " and email_status=0;";
        DB.executeNonQuery(sql);
    }
    public string GetReEmail()
    {
        string sql = "select email from lm_librarian where librarian_id =" + Request["lib_id"] + ";";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        return obj.ToString();
    }

    public string GetSendEmail()
    {
        string sql = "select email from lm_admin where Id =" + HttpContext.Current.Session["admin_id"] + ";";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        return obj.ToString();
    }

    public string GetDefaultPsd()
    {
        string sql = "select librarian_pw from lm_librarian where librarian_id =" + Request["lib_id"] + ";";
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        object obj = cmd.ExecuteScalar();
        conn.Close();
        return obj.ToString();
    }

   

    public bool sendEmail(string sender_email, string receiver_email)
    {
        try
            {
                string senduser = sender_email.Trim();// "test@qq.com";
                string sendpwd = "Yp497397";// "test";   
                string receiver = receiver_email.Trim();
                string tmp_host = "smtp.163.com";
                int tmp_port = 25;
                //string[] host_tmp = senduser.Split('@');
                MailMessage message = new MailMessage();
                MailAddress maddr = new MailAddress(senduser);

                message.From = maddr;
                message.To.Add(receiver);//接收方
                                         //message.CC.Add(receiver);//抄送方
                message.SubjectEncoding = Encoding.UTF8;
                message.Subject = "Recovery Password";//主题
                message.BodyEncoding = Encoding.Default;
                message.Body = "Your password is " + GetDefaultPsd() + ", please remember it carefully, thank you!\n";//内容

                SmtpClient client = new SmtpClient();//基于qq邮件发送
                client.Host = tmp_host;
                client.Port = tmp_port;
                client.Credentials = new NetworkCredential(senduser, sendpwd);
                client.EnableSsl = true;
                client.Send(message);
                return true;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Response.Write(ex);
                return false;
            }
     }
    protected void button_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-LibraResetList.aspx");
    }
}
