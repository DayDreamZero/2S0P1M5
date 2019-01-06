using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraForgetPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ResetPassword_Click(object sender, EventArgs e)
    {
        string libraID = LibraIdTxt.Text.Trim();
        string libraEmail = LibraCheckInput.transApostrophe(EmailTxt.Text.Trim());
        if(libraID==""&& libraEmail!="")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Librarian ID can not be null!');</script>");
        else if(libraEmail==""&& libraID!= "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Email can not be null!')</script>");
        else if(libraID == "" && libraEmail == "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Librarian ID and Email can not be null!')</script>");
        else     // 输入不为空
        {
            if (!LibraCheckInput.IsNum(libraID))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string about Librarian ID.');</script>");
            else
            {
                if (DB.executeSelect("select * from lm_librarian where librarian_id=" + libraID + " and librarian_status=0") != 1)
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The librarian ID is not exist!')</script>");
                else //id正确
                {
                    if (DB.executeSelect("select * from lm_librarian where librarian_id=" + libraID + " and email=N'" + libraEmail + "';") != 1)
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The email is wrong!')</script>");
                    else // email 正确
                    {
                        string sqlStr = "select * from lm_libraResetPwd where libra_id=" + libraID + " and email_status=0";
                        if (DB.executeSelect(sqlStr) == 0) //没发送过重置消息
                            sqlStr = "insert into lm_libraResetPwd values(" + libraID + ",N'" + libraEmail + "',0,getdate());";
                        else //更新发送的重置消息的时间
                            sqlStr = "update lm_libraResetPwd set email_sendTime=getdate() where libra_id=" + libraID + " and email_status=0";

                        if (DB.executeNonQuery(sqlStr) > 0)
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Send successfully!');window.location.href='../Login.aspx';</script>");

                    }
                }
            }
            
        }

    }
}