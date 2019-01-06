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
public partial class Librarian_aspx_RepurchaseBook : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            BindData();
            Session.Remove("firstRepurchase");
        }
    }

    protected void BindData()
    {
        
        string Num = Request["Num"];
        string ISBN = Request["ISBN"];
        repurchase(Num,ISBN);
        string sqlStr = "select top "+Num+ " book_name,book_picture,barcode,barcode_image from lm_books,lm_barcode"
            +" where lm_books.ISBN=lm_barcode.ISBN and lm_barcode.ISBN='"+ISBN+"'"
            +"order by barcode desc";
        DataSet ds = DB.getDataSet(sqlStr);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        

    }

    private void repurchase(string num,string ISBN)
    {
        if(Session["firstRepurchase"]!=null)
        {
            string sqlStr = "update lm_books set amount+=" + num + " where ISBN='" + ISBN + "'";
            DB.executeNonQuery(sqlStr);


            //sql
            sqlStr = "select top " + num + " barcode from lm_barcode"
                    + " where ISBN='" + ISBN + "'"
                    + "order by barcode desc";
            DataSet ds = DB.getDataSet(sqlStr);

            BandCode.Code128 _Code = new BandCode.Code128();
            _Code.ValueFont = new Font("宋体", 9);

            for (int i = 0; i < Convert.ToInt32(num); i++)
            {
                string barcode = ds.Tables[0].Rows[i]["barcode"].ToString();

                Bitmap bmp = _Code.GetCodeImage(barcode, BandCode.Code128.Encode.Code128A);
                string path = Server.MapPath("../barcodeImage");
                bmp.Save(path + "\\" + barcode + ".bmp");

                sqlStr = "update lm_barcode set barcode_image=N'../barcodeImage/" + barcode + ".bmp' where barcode=" + barcode;
                DB.executeNonQuery(sqlStr);
            }

        }
            
    }

}