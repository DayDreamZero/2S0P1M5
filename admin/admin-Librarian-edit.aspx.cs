using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text.RegularExpressions;

public partial class aspx_admin_Librarian_edit : System.Web.UI.Page
{
    public string uid;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin_name"] == null && Session["admin_id"] == null)
        {
            Response.Redirect("../Admin-Login.aspx");
        }
        uid = Request.Params["uid"];
        this.Label1.Text = HttpContext.Current.Session["admin_name"].ToString();
        if (!IsPostBack)
        { 
                BindDataToRepeater();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        username.Text = null;
        tmpPic.ImageUrl = null;
        psd.Text = null;
        tel.Text = null;
        email.Text = null;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("admin-LibrarianGroup.aspx");
    }
    

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (username.Text.TrimStart().TrimEnd() != string.Empty && psd.Text.Trim() != string.Empty && tel.Text.Trim() != string.Empty && email.Text.Trim() != string.Empty)
        {
                //创建一个新的Librarian
                string sql = "UPDATE lm_librarian set [librarian_name] = N'" + username.Text.ToString() + "',[librarian_pw] = '" + psd.Text.ToString() + "',[telephone] = '" + tel.Text.ToString() + "',[email] = '" + email.Text.ToString() + "' where librarian_id = " + uid + ";";
                // string sql = "UPDATE lm_librarian set [librarian_name] = '" + username.Text.ToString() + "',[librarian_pw] = '" + psd.Text.ToString() + "',[telephone] = '" + tel.Text.ToString() + "',[email] = '" + email.Text.ToString() + "' where librarian_id = " + uid + ";";
                string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                //将头像的保存到Librarian的信息中
                string myPath = "user_head/" + username.Text + ".jpg";
                string virpath = "user_head/" + "myPic.jpg";
                if (System.IO.File.Exists(Server.MapPath(virpath)))
                {
                    File.Copy(Server.MapPath(virpath), Server.MapPath(myPath), true);
                    File.Delete(Server.MapPath(virpath));
                    sql = "UPDATE lm_librarian SET [picture] = '" + myPath + "' WHERE librarian_name = '" + username.Text + "';";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                /*else
                {
                    Response.Write("<script>alert('" + virpath + "')</script>");
                }*/
                conn.Close();

                username.Text = null;
                psd.Text = null;
                tel.Text = null;
                email.Text = null;
                this.tmpPic.ImageUrl = null;
                string url = "admin-LibrarianGroup.aspx";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Modify Successfully！');window.location='" + url + "';</script>");
           
            }
        else
        {
            Response.Write("<script>alert('Please fill information completely!')</script>");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean fileOk = false;
        if (FileUpload.HasFile)//验证是否包含文件
        {
            //取得文件的扩展名,并转换成小写
            string fileExtension = Path.GetExtension(FileUpload.FileName).ToLower();
            //验证上传文件是否图片格式
            fileOk = IsImage(fileExtension);
            if (fileOk)
            {
                //对上传文件的大小进行检测，限定文件最大不超过8M
                if (FileUpload.PostedFile.ContentLength < 8192000)
                {
                    string filepath = "user_head/";
                    if (Directory.Exists(Server.MapPath(filepath)) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(Server.MapPath(filepath));
                    }
                    //string virpath = filepath + CreatePasswordHash(FileUpload.FileName, 4) + fileExtension;//这是存到服务器上的虚拟路径
                    string virpath = filepath + "myPic" + ".jpg";//这是存到服务器上的虚拟路径
                    string mappath = Server.MapPath(virpath);//转换成服务器上的物理路径
                    FileUpload.PostedFile.SaveAs(mappath);//保存图片                                                           
                    tmpPic.ImageUrl = virpath;
                    //清空提示
                    lbl_pic.Text = "";
                }
                else {
                    tmpPic.ImageUrl = "";
                    lbl_pic.Text = "File size over 8M!";
                }
            }
            else
            {
                tmpPic.ImageUrl = "";
                lbl_pic.Text = "Wrong file type to upload!";
            }
        }
        else
        {
            tmpPic.ImageUrl = "";
            lbl_pic.Text = "Please select the image to upload!";
        }
    }
    /// 验证是否指定的图片格式      
    public bool IsImage(string str)
    {
        bool isimage = false;
        string thestr = str.ToLower();
        //限定只能上传jpg和gif图片
        string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
        //对上传的文件的类型进行一个个匹对
        for (int i = 0; i < allowExtension.Length; i++)
        {
            if (thestr == allowExtension[i])
            {
                isimage = true;
                break;
            }
        }
        return isimage;
    }

    protected void allFileSize_ValueChanged(object sender, EventArgs e)
    {

    }
    private void BindDataToRepeater()
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from lm_librarian where librarian_id='" + uid + "'", conn);
        DataSet ds = new DataSet();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            this.username.Text = dr.GetString(dr.GetOrdinal("librarian_name"));
            this.psd.Text = dr.GetString(dr.GetOrdinal("librarian_pw"));
            if (!dr.IsDBNull(dr.GetOrdinal("telephone")))
            {
                this.tel.Text = dr.GetString(dr.GetOrdinal("telephone"));
            }
            else
            {
                this.tel.Text = "";
            }
            if (!dr.IsDBNull(dr.GetOrdinal("email")))
            {
                this.email.Text = dr.GetString(dr.GetOrdinal("email"));
            }
            else
            {
                this.tel.Text = "";
            }
            if (!dr.IsDBNull(dr.GetOrdinal("picture")))
            {
                this.tmpPic.ImageUrl = dr.GetString(dr.GetOrdinal("picture"));
            }
            else
            {
                this.tel.Text = "";
            }
        }
        while (dr.Read())
        {
            Response.Write(dr["librarian_name"]);
            Response.Write(dr["librarian_pw"]);
            Response.Write(dr["telephone"]);
            Response.Write(dr["email"]);
            Response.Write(dr["picture"]);
        }
        dr.Close();
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }
}