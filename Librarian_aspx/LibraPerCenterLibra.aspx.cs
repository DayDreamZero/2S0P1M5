using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Librarian_aspx_perCenterLibra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
       
        if (!IsPostBack)
        {
            FormBindData();

        }
    }
    protected void FormBindData()
    {
        string sqlStr = "select librarian_id,librarian_name,telephone,picture,email from lm_librarian "
            + "where librarian_id=" + Session["LibraId"].ToString().Trim();
        DataSet ds = DB.getDataSet(sqlStr);
        ds.Tables[0].Rows[0][3] = "../admin/" + ds.Tables[0].Rows[0][3].ToString();
        FormView1.DataSource = ds;
        FormView1.DataBind();

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        FileUpload fulPhoto = (FileUpload)FormView1.FindControl("fulPhoto");
        Image LibraImage = (Image)FormView1.FindControl("LibraImage");
        string P_str_name = fulPhoto.FileName;//获取上载文件的名称
        bool P_bool_fileOK = false;
        if (fulPhoto.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(fulPhoto.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg", ".bmp" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    P_bool_fileOK = true;
                }
            }
        }
        if (P_bool_fileOK)
        {

            fulPhoto.PostedFile.SaveAs(Server.MapPath("../admin/user_head/") + P_str_name);//将文件保存在相应的路径下
            LibraImage.ImageUrl = "../admin/user_head/" + P_str_name;//将图片显示在Image控件上

        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please select the image in gif,png,jpeg,jpg,bmp format!');</script>");
            //Response.Write("<script>alert('Please select the image in gif,png,jpeg,jpg,bmp format!');</script>");
        }
    }

    protected void EditBt_Click(object sender, EventArgs e)
    {
        TextBox NameTxt = (TextBox)FormView1.FindControl("NameTxt");
        TextBox TeleTxt = (TextBox)FormView1.FindControl("TeleTxt");
        TextBox EmailTxt = (TextBox)FormView1.FindControl("EmailTxt");
        Image LibraImage = (Image)FormView1.FindControl("LibraImage");
        string Name = LibraCheckInput.transApostrophe(NameTxt.Text.Trim());
        string Tele = LibraCheckInput.transApostrophe(TeleTxt.Text.Trim());
        string Email = LibraCheckInput.transApostrophe(EmailTxt.Text.Trim());
        string picture = LibraCheckInput.transApostrophe(LibraImage.ImageUrl.ToString().Substring(9));
        if (Name == "" || Tele == "" || Email == "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Be sure all information has been inputed.');</script>");
        else
        {
            string sqlStr = "update lm_librarian set librarian_name=N'" + Name + "',telephone='" + Tele + "',email='" + Email + "',picture=N'" + picture + "' where librarian_id=" + Session["LibraId"].ToString().Trim();
            if (DB.executeNonQuery(sqlStr) > 0)
            { 
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Update personal information successfully.');</script>");
                Session["LibraName"] = Name;
                LibraNameLab.Text = Session["LibraName"].ToString();
                FormBindData();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Failed for updating personal information.');</script>");

            }
        }
    }

    protected void CancelBt_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraHomePage.aspx");
    }
}