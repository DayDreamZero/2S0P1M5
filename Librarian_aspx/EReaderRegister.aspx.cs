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
using CopyDirectoryInfo;
using System.Drawing;

public partial class EReaderRegister : System.Web.UI.Page
{
	String url = HttpContext.Current.Request.RawUrl;
	protected void Page_Load(object sender, EventArgs e)
	{
        //UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();

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
			//Response.Write("<script>alert('Please select the image in gif,png,jpeg,jpg,bmp format!');</script>");
		}
	}
	protected int checkphone()
	{
		DataSet ds = DB.getDataSet("select telephone from lm_users where telephone='"+ txtPhone.Text.Trim()+"'");
		return ds.Tables[0].Rows.Count;
	}

	protected void btnInsert_Click(object sender, EventArgs e)
	{

		if (checkphone() == 0)
		{
			bool P_Bool_reVal2 = syjDB.ExSql("insert into [lm_users] (username,telephone,email,user_picture,current_borrowed,reader_status) values( N'" + txtuserName.Text + "','" + txtPhone.Text + "','" + txtEmail.Text + "',N'" + imgGoodsPhoto.ImageUrl + "',0,0)");
			if (!P_Bool_reVal2)
			{
				HttpContext.Current.Response.Write("<script>alert('Failed to register reader,please try agin!');location.href='" + url + "'</script>");
				//Response.Write("<script>alert('Failed to register the reader,please try agin！');</script>");
			}
			else
			{
				DataSet ds1 = DB.getDataSet("select top 1 user_id from lm_users where reader_status=0 order by user_id desc");
				BandCode.Code128 _Code = new BandCode.Code128();
				_Code.ValueFont = new Font("宋体", 9);
				string barcode = ds1.Tables[0].Rows[0][0].ToString();
				Bitmap bmp = _Code.GetCodeImage(barcode, BandCode.Code128.Encode.Code128A);
				string path = Server.MapPath("../readerImage");
				bmp.Save(path + "\\" + barcode + ".bmp");
				String sqlStr = "update lm_users set user_barcode_image=N'../readerImage/" + barcode + ".bmp' where user_id=" + barcode;
				DB.executeNonQuery(sqlStr);

				HttpContext.Current.Response.Write("<script>alert('Success to register reader！');location.href='EReaderBarcode.aspx?userid="+barcode+"'</script>");
			}
		}
		else
			Response.Write("<script>alert('The phone hae been registered!');location='javascript:history.go(-1);'</script>");
	}

	protected void btnBack_Click(object sender, EventArgs e)
	{
		Response.Redirect("EReaderManage.aspx");
	}
}
