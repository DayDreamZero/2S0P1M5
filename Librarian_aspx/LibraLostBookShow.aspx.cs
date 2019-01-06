using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Librarian_aspx_LibraLostBookShow : System.Web.UI.Page
{
    public string htmlstr;
    private DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        if (!IsPostBack)
        {

        }
        string sql = "select [lm_barcode].[ISBN], [lm_barcode].[barcode], [lm_books].[book_name], [lm_barcode].[stakeholder],[lm_barcode].[status_date],[lm_borrowed_record].[user_id]"
                       + "from lm_books, lm_barcode, lm_borrowed_record where"
                       + "[lm_borrowed_record].[borrowed_status] =3 and "
                       + "[lm_borrowed_record].[barcode] = [lm_barcode].[barcode] and "
                       + "[lm_barcode].[ISBN] = [lm_books].[ISBN];";

        ds = DB.getDataSet(sql);
        foreach (DataRow col in ds.Tables[0].Rows)
        {
            htmlstr += "{\"isbn\":\"" + col["ISBN"].ToString() + "\","
                           + "\"barcode\":\"" + col["barcode"].ToString() + "\","
                           + "\"bookname\":\"" + col["book_name"].ToString() + "\","
                           + "\"user_id\":\"" + col["user_id"].ToString() + "\","
                           + "\"stakeholder\":\"" + col["stakeholder"].ToString() + "\","
                           + "\"lost_time\":\"" + col["status_date"].ToString() + "\","
                           + "},";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String input1 = Request["input1"].Trim().ToString();
        String input2 = Request["input2"].Trim().ToString();
        String input3 = Request["input3"].Trim().ToString();
        htmlstr = null;
        string sql = "select [lm_barcode].[ISBN],[lm_barcode].[barcode],[lm_books].[book_name],[lm_barcode].[stakeholder],"
                        + "[lm_barcode].[status_date],[lm_borrowed_record].[user_id]"
                        + "from lm_books, lm_barcode, lm_borrowed_record where"
                        + "[lm_borrowed_record].[borrowed_status] =3 and "
                        + "CONVERT(VARCHAR(50),[lm_borrowed_record].[user_id]) like '%" + input1 + "%' and"
                        + "[lm_barcode].[ISBN] like '%" + input2 + "%' and"
                        + "[lm_borrowed_record].[barcode] = [lm_barcode].[barcode] and"
                        + "[lm_barcode].[ISBN] = [lm_books].[ISBN] and"
                        + "[lm_books].[book_name] like N'%" + input3 + "%';";

            ds = DB.getDataSet(sql);
            foreach (DataRow col in ds.Tables[0].Rows)
            {
                htmlstr += "{\"isbn\":\"" + col["ISBN"].ToString() + "\","
                           + "\"barcode\":\"" + col["barcode"].ToString() + "\","
                           + "\"bookname\":\"" + col["book_name"].ToString() + "\","
                           + "\"user_id\":\"" + col["user_id"].ToString() + "\","
                           + "\"stakeholder\":\"" + col["stakeholder"].ToString() + "\","
                           + "\"lost_time\":\"" + col["status_date"].ToString() + "\","
                           + "},";
            }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        String input1 = Request["input1"].ToString();
        String input2 = Request["input2"].ToString();
        String input3 = Request["input2"].ToString();
        input1 = null;
        input2 = null;
        input3 = null;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("LibraLostBookHandle.aspx");
    }
}