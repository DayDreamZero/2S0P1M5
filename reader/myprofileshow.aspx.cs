using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class myprofileshow : System.Web.UI.Page
{
    public string htmlStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("../login.aspx");
            }
            else
            {
                htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = 'myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
            }

        }
        if (!this.IsPostBack)
        {
            BindDataToRepeater();
        }
        
    }

    protected void ChangeButton_Click(object sender, EventArgs e)
    {
        string user_id = this.Session["userid"].ToString();
        Response.Redirect("myprofilemodify.aspx");
    }

    private void BindDataToRepeater()
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        conn.Open();
        string user_id = this.Session["userid"].ToString();
        SqlCommand comm = new SqlCommand("select * from lm_users where user_id='"+user_id+"'", conn);
        //SqlCommand comm1 = new SqlCommand("select sum(fine) as fine_sum from lm_borrowed_record where user_id='"+user_id+ "'and (fine_status='no' or fine_status is null)", conn);
        //DataSet ds = new DataSet();
        SqlDataReader dr = comm.ExecuteReader();
        
        if (dr.Read())
        {
            this.Userid.Text = dr.GetInt32(dr.GetOrdinal("user_id")).ToString();
            this.UserName.Text = dr.GetString(dr.GetOrdinal("username"));
           // this.Balance.Text = dr.GetDecimal(dr.GetOrdinal("balance")).ToString();
            this.Telephone.Text = dr.GetString(dr.GetOrdinal("telephone"));
            this.Email.Text = dr.GetString(dr.GetOrdinal("email"));
            this.Image1.ImageUrl = dr.GetString(dr.GetOrdinal("user_picture"));
        }
       
        while(dr.Read())
        {
            Response.Write(dr["user_id"]);
            Response.Write(dr["username"]);
           // Response.Write(dr["balance"]);
            Response.Write(dr["telephone"]);
            Response.Write(dr["email"]);
            Response.Write(dr["user_picture"]);
        }
        
        dr.Close();
        //SqlDataReader dr1 = comm1.ExecuteReader();
        string sqlStr3 = "select sum(fine) from lm_borrowed_record where user_id=" + HttpContext.Current.Session["userid"] + " and (fine_status='no' or fine_status is null)";
        DataSet fine = DB.getDataSet(sqlStr3);
        if (fine.Tables[0].Rows[0][0].ToString() == "" || fine.Tables[0].Rows[0][0].ToString() == "0.00")
        {
            this.Fine.Text = "0.00";
        }
        else
        {
            this.Fine.Text = fine.Tables[0].Rows[0][0].ToString();
        }
        /*while (dr1.Read())
        {
            Response.Write(dr1["fine_sum"]);
        }
        dr1.Close();*/
        if(conn.State==ConnectionState.Open)
        {
            conn.Close();
        }
    }
}