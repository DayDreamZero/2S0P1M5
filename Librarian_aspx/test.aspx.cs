using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CopyDirectoryInfo;
using System.Drawing;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            BandCode.Code128 _Code = new BandCode.Code128();
            _Code.ValueFont = new Font("宋体", 9);

            for (int i = 1001; i <= 1009; i++)
            {
                string barcode = ""+i;

                Bitmap bmp = _Code.GetCodeImage(barcode, BandCode.Code128.Encode.Code128A);
                string path = Server.MapPath("../readerImage");
                bmp.Save(path + "\\" + barcode + ".bmp");

                
            }
            
            


        }
    }

    void Databind()
    {
        
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../admin/admin-ActiveResetEmail.aspx?libra_id="+HttpContext.Current.Request.Url.Port);
       
    }

    protected void TypeSearchBt_Click(object sender, EventArgs e)
    {

    }
}

