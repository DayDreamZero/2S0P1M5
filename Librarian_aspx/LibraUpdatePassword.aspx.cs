using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Librarian_aspx_LibraUpdatePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
    }

    protected void OkBt_Click(object sender, EventArgs e)
    {
        if (oldPwdTxt.Text.Trim() == "" || newPwdTxt.Text.Trim() == "" || newPwdAgainTxt.Text.Trim() == "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please make sure all information has been input');</script>");
        else
        {
            string oldPwd = oldPwdTxt.Text.Trim();
            string newPwd = newPwdTxt.Text.Trim();
            string PwdAgain = newPwdAgainTxt.Text.Trim();
            string LibraID = Session["LibraID"].ToString();
            string sqlStr = "select * from lm_librarian  where librarian_id=" + LibraID + " and librarian_pw='"+oldPwd+"';";
            if (DB.executeSelect(sqlStr) == 1)
            {
                sqlStr = "update lm_librarian set librarian_pw='" + newPwd + "' where librarian_id=" + LibraID;
                if (DB.executeNonQuery(sqlStr) == 1)
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Updating password is successful.');window.location.href='../Login.aspx';</script>");
                else
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Updating password is Failed.');</script>");
                    Response.Redirect("LibraUpdatePassword.aspx");
               
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The old Password is not correct.');</script>");
            }

        }
    }
}