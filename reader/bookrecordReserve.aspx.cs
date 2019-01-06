using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reader_bookrecordReserve : System.Web.UI.Page
{
    public string htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Session["username"] == null && this.Session["userid"] == null)
            {
                Response.Redirect("../login.aspx");
            }
            else
            {
                htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = 'myprofileshow.aspx'>my profile</a></li><li><a href = 'bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
            }
            
            //Timer1_Tick();
        }
        Repeater_Databind();
        UpdateReserveFailBook();
    }


    public void UpdateReserveFailBook()
    {
        //< asp:ScriptManager ID = "ScriptManager1" runat = "server" EnablePartialRendering = "true" ></ asp:ScriptManager >
        // < asp:UpdatePanel ID = "UpdatePanel1" runat = "server" ></ asp:UpdatePanel >
        // < asp:Timer ID = "Timer1" runat = "server" Interval = "10000" ontick = "Timer1_Tick" Enabled = "true" ></ asp:Timer >
        string sqlStr = "select set_reserve_period from lm_adminSet where id=10000000";
        DataSet ds = DB.getDataSet(sqlStr);
        int reserve_period = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        sqlStr = "select reserve_id from lm_reserve_record" + " where DATEDIFF(MINUTE,starttime,GETDATE())>" + reserve_period + " and reserve_status=0 and user_id = " + HttpContext.Current.Session["userid"];
        ds = DB.getDataSet(sqlStr);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_reserve_record set reserve_status=1"
            + " where reserve_id=" + ds.Tables[0].Rows[i]["reserve_id"].ToString();
            DB.executeNonQuery(sqlStr);
        }
    }
    void Repeater_Databind()
    {
        string sqlStr = "select book_picture,book_name,lm_reserve_record.barcode,starttime,borrowtime,lm_books.bookshelf_id,room_id,floor,endtime from lm_bookshelf,lm_books,lm_reserve_record,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and lm_reserve_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and reserve_status=0 and user_id=" + HttpContext.Current.Session["userid"];
        DataSet ds = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
    }
    //,(DATEADD(Minute,(select set_reserve_period from lm_adminSet where Id = 10000000),starttime) as endtime) 
    //where user_id = "+HttpContext.Current.Session["userid"].ToString()+" +"and lm_reserve_record.barcode = "+HttpContext.Current.Session["barcode"].ToString()+" and user_id = "+HttpContext.Current.Session["userid"].ToString()+" and ISBN = "+HttpContext.Current.Session["ISBN"].ToString();

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cancelReserve")
        {
            String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String barcode = e.CommandArgument.ToString();
            SqlCommand cmd = new SqlCommand("delete from lm_reserve_record where barcode=@barcode and reserve_status=0", conn);
            cmd.Parameters.Add(new SqlParameter("@barcode", SqlDbType.Int));
            cmd.Parameters["@barcode"].Value = barcode;
            //判断inverntory>amount
            cmd.ExecuteNonQuery();
            Response.Write("<script language=javascript>alert('Cancel successfully!');</" + "script>");
            conn.Close();
            Server.Transfer("bookrecordReserve.aspx");
        }
    }
}