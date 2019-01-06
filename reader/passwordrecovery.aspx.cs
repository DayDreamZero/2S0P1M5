using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

public partial class reader_passwordrecovery : System.Web.UI.Page
{
    //string lspass = "";
    public string htmlStr;
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        htmlStr = "<ul class='nav navbar-nav navbar-right'><li><a href = '/Login.aspx'> Login </a></li></ul>";
        if (!IsPostBack)
        {
            
            this.RecoveryPassword.Attributes.Add("onclick", "javascript:return checkAll();");
        }
    }
    

    protected void PwRecovery_Click(object sender, EventArgs e)
    {
        /* HttpCookie myCookie_validcode = Request.Cookies["FWCX_CheckCode"];
         if (myCookie_validcode.Value.ToString() != SecurityCode.Text)
         {
             Response.Write("<script>");
             Response.Write("alert('验证码错误')");
             Response.Write("</script>");
             return;
         }*/
        Regex r = new Regex(@"^[a-zA-Z0-9_\.\-]+\@[a-zA-Z0-9_\-]+(\.[a-zA-Z0-9_\-]+)+$");
        Regex r1 = new Regex(@"^1[0-9]{10}$");
        string u_id = UserId.Text.Trim();
        string u_email = Email.Text.Trim();
        if (u_id == "" && u_email != "")
        {
            Label1.Text = "id cannot be null!";
            Label2.Text = "";
        }
        else if(u_email==""&& u_id != "")
        {
            Label1.Text = "";
            Label2.Text = "email cannot be null!";
        }
        else if (u_id == "" && u_email == "")
        {
            Label1.Text = "id cannot be null!";
            Label2.Text = "email cannot be null!";
        }
        else if (!r1.IsMatch(u_id) && !r.IsMatch(u_email))
        {
            Label1.Text = "telephone format is wrong, please check!";
        }
        else if (!r1.IsMatch(u_id) && r.IsMatch(u_email))
        {
            Label1.Text = "telephone format is wrong, please check!";
            Label2.Text = "";
        }
        else if(!r.IsMatch(u_email)&& r1.IsMatch(u_id))
        {
            Label1.Text = "";
            Label2.Text = "email format is wrong, please check!";
        }
        else
        {
            String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            string sql = "select * from lm_users where telephone='" + u_id + "' and email='" + u_email + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            //DataSet ds = new DataSet();
            SqlDataReader dr = comm.ExecuteReader();
            if (!dr.Read())
            {
                Response.Write("<script>alert('your telephone or email maybe wrong, please check!');</script>");
            }
            else
            {
                if (sendEmail(u_email, u_id) == true)
                {
                    HttpContext.Current.Response.Write("<script>alert('your password was sent to your email, please get it via your email!');location.href='../login.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>alert('There is something wrong, please check again!');</" + "script>");
                }
            }
        }
    }

    public bool sendEmail(string email, string id)
    {
        try
        {
            string senduser = "18291884205@163.com";// "test@qq.com";
            string sendpwd = "ciery123";// "test";

            String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand comm = new SqlCommand("select userpassword from lm_users where telephone='" + id + "' and email='"+email+"'", conn);
            SqlDataReader dr = comm.ExecuteReader();
            if(dr.Read())
            {
                string text = dr.GetString(dr.GetOrdinal("userpassword"));
                string receiver = this.Email.Text;
                string sendmsg = "The old password is " +text+", please remember it carefully, thank you!";

                MailMessage message = new MailMessage();
                MailAddress maddr = new MailAddress(senduser);
                message.From = maddr;

                message.To.Add(receiver);//接收方
                //message.CC.Add(receiver);//抄送方
                message.SubjectEncoding = Encoding.UTF8;
                message.Subject = "Retrive Password";//主题
                message.BodyEncoding = Encoding.Default;
                message.Body = sendmsg;//内容

                SmtpClient client = new SmtpClient();//基于qq邮件发送
                client.Host = "smtp.163.com";
                client.Credentials = new NetworkCredential(senduser, sendpwd);
                client.EnableSsl = true;
                client.Send(message);
                return true;
            }
            else{
                Response.Write("<script>alert('sorry, the account donot exit, please check!')</script>");
                return false;
            }
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            Response.Write(ex);
            return false;
        }
    }

   /* private string CreateRandomCode(int codecount)
    {
        // 数字和字符混合字符串   
        string allchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n";
        //分割成数组   
        string[] allchararray = allchar.Split(',');
        string randomcode = "";

        //随机数实例   
        System.Random rand = new System.Random(unchecked((int)DateTime.Now.Ticks));
        for (int i = 0; i < codecount; i++)
        {
            //获取一个随机数   
            int t = rand.Next(allchararray.Length);
            //合成随机字符串   
            randomcode += allchararray[t];
        }
        return randomcode;
    }*/
}