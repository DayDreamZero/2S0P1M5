using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class EReaderDetail : System.Web.UI.Page
{
	String url = HttpContext.Current.Request.RawUrl;

	protected void Page_Load(object sender, EventArgs e)
	{

		//UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
		if (Session["LibraId"] == null)
			Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text = Session["LibraName"].ToString();


		if (!IsPostBack)
		{
			Bind();
		}
			txtuserName.Enabled = false;
			txtPhone.Enabled = false;
			txtEmail.Enabled = false;
			TextBox1.Enabled = false;
			fulPhoto.Visible = false;
			btnShow.Visible = false;
			btnInsert.Visible = false;


	}
	public void Bind()
	{

		string P_str_GoodsID = Request["user_id"];
		DataSet ds = syjDB.reDs("select * from lm_users where user_id=" + P_str_GoodsID);
		txtuserName.Text = ds.Tables[0].Rows[0][1].ToString();
		txtPhone.Text = ds.Tables[0].Rows[0][4].ToString();
		txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
		TextBox1.Text = ds.Tables[0].Rows[0][7].ToString();
		TextBox1.Enabled = false;
		Image1.ImageUrl = ds.Tables[0].Rows[0][9].ToString();
		imgGoodsPhoto.ImageUrl = ds.Tables[0].Rows[0][6].ToString();
	}

	//显示用户头像
	protected void btnShow_Click(object sender, EventArgs e)
	{
		string P_str_name = this.fulPhoto.FileName;//获取上载文件的名称
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
			this.fulPhoto.PostedFile.SaveAs(Server.MapPath("../image/") + P_str_name);//将文件保存在相应的路径下
			this.imgGoodsPhoto.ImageUrl = "../image/" + P_str_name;//将图片显示在Image控件上
		}
		else
		{
			HttpContext.Current.Response.Write("<script>alert('Please select the image in gif,png,jpeg,jpg,bmp format!');location.href='" + url + "'</script>");
		}
	}



	protected void btnInsert_Click(object sender, EventArgs e)
	{
		string P_str_ReaderID = Request["user_id"];

		bool P_Bool_reVal2 = syjDB.ExSql("update lm_users set username=N'" + txtuserName.Text + "',telephone='" + txtPhone.Text + "',email='" + txtEmail.Text + "',user_picture=N'" + imgGoodsPhoto.ImageUrl + "' where user_id=" + P_str_ReaderID);
		if (!P_Bool_reVal2)
		{
			HttpContext.Current.Response.Write("<script>alert('Failed to modify reader,please try agin!');location.href='" + url + "'</script>");
		}
		else
		{
			//Response.Write("<script>alert('Success to modify reader！');</script>");
			//Response.Redirect("EReaderModify.aspx");
			HttpContext.Current.Response.Write("<script>alert('Success to modify reader！');location.href='EReaderManage.aspx'</script>");
		}
	}

	protected void btnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("EReaderManage.aspx");
	}
}
