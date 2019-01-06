using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class advancedSearch : System.Web.UI.Page
{
    public String htmlStr, categ;
    protected void Page_Load(object sender, EventArgs e)
    {
        login_statue();
        categ = "select type_name from lm_booktype order by type_name asc";
        DataSet cate = DB.getDataSet(categ);

        if (!IsPostBack)
        {
            if (cate.Tables[0].Rows.Count != 0)
            {
                ArrayList ar = new ArrayList();
                for (int i = 0; i < cate.Tables[0].Rows.Count; i++)
                {
                    ar.Add(cate.Tables[0].Rows[i][0]);
                }
                this.category.DataSource = ar;
                this.category.DataBind();
            }
            if (this.Session["sql"] != null)
            {
                Repeater_Databind(HttpContext.Current.Session["sql"].ToString());
                if (HttpContext.Current.Session["sql"].ToString() != null)
                {
                    String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(HttpContext.Current.Session["sql"].ToString(), conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        Response.Write("<script language=javascript>alert('there are no qualified books in store!');</" + "script>");
                        Session.Remove("sql");
                        Server.Transfer("homepage.aspx");

                    }

                }
                if (Session["box"] != null)
                {
                    searchBox.Text = HttpContext.Current.Session["box"].ToString();
                }
                if (Session["searchBy"] != null)
                {
                    searchBy.SelectedItem.Text = HttpContext.Current.Session["searchBy"].ToString();
                }
                if (Session["checkbox"] != null)
                {
                    CheckBox1.Checked = true;
                }
                if (Session["category"] != null)
                {
                    category.SelectedItem.Text = Session["category"].ToString();
                }
            }
            Session.Remove("sql");
            Session["box"] = null;
            Session["searchBy"] = null;
            Session["checkbox"] = null;
            Session["category"] = null;
        }
    }
    protected void login_statue()
    {
        if (this.Session["username"] == null && this.Session["userid"] == null)
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li><a href = '/Login.aspx'> Login </a></li></ul>";
        }
        else
        {
            htmlStr = "<ul class='nav navbar-nav navbar-right'><li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>" + HttpContext.Current.Session["username"].ToString() + "<b class='caret'></b></a><ul class='dropdown-menu'><li><a href = '/reader/myprofileshow.aspx'>my profile</a></li><li><a href = '/reader/bookrecordReserve.aspx'>books record</a></li><li><a href='logout.aspx'>exit</a></li></ul></li></ul>";
        }
    }
    public String htmlString;
    protected void search(object sender, EventArgs e)
    {
        String sqlStr = null;
        if (searchBox.Text == "" && CheckBox1.Checked != true)
        {
            Response.Write("<script language=javascript>alert('please input fields!');</" + "script>");
            Server.Transfer("advancedSearch.aspx");
        }
        else if (searchBox.Text != "")
        {
            String box = searchBox.Text;
            if (box.Contains("'"))
            {
                Response.Write("<script>alert('Your input cannot contain apostrophe!')</script>");
            }
            else if (CheckBox1.Checked == true)
            {
                String cg = category.SelectedItem.ToString();
                if (searchBy.SelectedItem.ToString() == "title")//按书名+类别筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and book_name like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
                else if (searchBy.SelectedItem.ToString() == "author")//按作者名+类别筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and author like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
                else//ISBN
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%') and lm_books.ISBN like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
            }
            else
            {
                if (searchBy.SelectedItem.ToString() == "title")//按书名筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and book_name like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
                else if (searchBy.SelectedItem.ToString() == "author") //按作者名筛选
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and author like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
                else
                {
                    sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and lm_books.ISBN like N'%" + box + "%'";
                    Repeater_Databind(sqlStr);
                }
            }

        }
        else if (searchBox.Text == "" && CheckBox1.Checked == true)
        {//按类别筛选
            String cg = category.SelectedItem.Text.ToString();
            sqlStr = "select barcode,book_picture,book_name,author,publishing_house,price,lm_books.ISBN,lm_books.bookshelf_id,floor,room_id from lm_bookshelf,lm_books,lm_barcode where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and book_status=0 and lm_books.ISBN=lm_barcode.ISBN and type_id=(select type_id from lm_booktype where type_name like N'%" + cg + "%')";
            Repeater_Databind(sqlStr);
        }
        if (sqlStr != null)
        {
            String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlStr, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Response.Write("<script language=javascript>alert('there are no qualified books in store!');</" + "script>");
                Server.Transfer("advancedSearch.aspx");
            }
        }
    }
    void Repeater_Databind(string sqlStr)
    {
        DataSet ds = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "MoreInfo")
        {
            //Session["infoResource"] = "advancedSearch";
            //string ISBN = ((Label)Item.FindControl("ISBN")).Text;
            String ISBN = e.CommandArgument.ToString();
            this.Session["ISBN"] = ISBN;
            Response.Write("<script>window.open('morebookinformation.aspx?ISBN=" + ISBN + "','_blank')</script>");
            //Response.Redirect("morebookinformation.aspx?ISBN=" + ISBN);
        }
        if (e.CommandName == "Reserve")
        {
            if (this.Session["userid"] == null & this.Session["username"] == null)
            {
                Response.Write("<script language=javascript>alert('You should login first if you want to reserve book!');</" + "script>");
                Server.Transfer("advancedSearch.aspx");
            }
            else
            {
                string sqlStr3 = "select sum(fine) from lm_borrowed_record where user_id=" + HttpContext.Current.Session["userid"] + " and (fine_status='no' or fine_status is null)";
                DataSet fine = DB.getDataSet(sqlStr3);
                if (fine.Tables[0].Rows[0][0].ToString() == "" || fine.Tables[0].Rows[0][0].ToString() == "0.00")
                {
                    String strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO lm_reserve_record(barcode,user_id,starttime,reserve_status,endtime) VALUES (@barcode,@userid,@starttime,@reservestatus,@endtime)", conn);

                    String idStr = this.Session["userid"].ToString();
                    int id = int.Parse(idStr);
                    //string inventory = ((Label)e.Item.Controls[4]).Text;
                    string aa;
                    string inventory = "select distinct inventory,bookshelf_id from lm_books,lm_barcode where lm_barcode.ISBN=lm_books.ISBN and lm_barcode.barcode='" + e.CommandArgument.ToString() + "'";
                    DataSet invent = DB.getDataSet(inventory);
                    foreach (RepeaterItem item in this.Repeater1.Items)
                    {
                        aa = invent.Tables[0].Rows[0]["inventory"].ToString();
                        string sqlStr = "select lm_borrowed_record.barcode from lm_borrowed_record,lm_barcode,lm_books where user_id=" + HttpContext.Current.Session["userid"].ToString() +
                            " and (borrowed_status=0 or borrowed_status=2) and lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and lm_books.ISBN=(select lm_books.ISBN from lm_books,lm_barcode where lm_barcode.ISBN=lm_books.ISBN and lm_barcode.barcode='" + e.CommandArgument.ToString() + "')";
                        string sqlStr1 = "select lm_reserve_record.barcode from lm_reserve_record,lm_barcode,lm_books where user_id=" + HttpContext.Current.Session["userid"].ToString() +
                            " and reserve_status=0 and lm_reserve_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN and lm_books.ISBN=(select lm_books.ISBN from lm_books,lm_barcode where lm_barcode.ISBN=lm_books.ISBN and lm_barcode.barcode='" + e.CommandArgument.ToString() + "')";
                        string sqlStr2 = "select max_borrow from lm_adminSet";
                        DataSet ds1 = DB.getDataSet(sqlStr);
                        DataSet ds2 = DB.getDataSet(sqlStr1);
                        sqlStr = "select lm_borrowed_record.barcode from lm_borrowed_record,lm_barcode,lm_books where user_id=" + HttpContext.Current.Session["userid"].ToString() +
                            " and (borrowed_status=0 or borrowed_status=2) and lm_borrowed_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN";
                        sqlStr1 = "select lm_reserve_record.barcode from lm_reserve_record,lm_barcode,lm_books where user_id=" + HttpContext.Current.Session["userid"].ToString() +
                            " and reserve_status=0 and lm_reserve_record.barcode=lm_barcode.barcode and lm_books.ISBN=lm_barcode.ISBN";
                        DataSet borrow = DB.getDataSet(sqlStr);
                        DataSet reserve = DB.getDataSet(sqlStr1);
                        DataSet ds3 = DB.getDataSet(sqlStr2);
                        if (!aa.Equals("0"))
                        {
                            if (ds1.Tables[0].Rows.Count > 0 || ds2.Tables[0].Rows.Count > 0)
                            {
                                Response.Write("<script language=javascript>alert('Failed!You are reserving or borrowing this book now!');</" + "script>");
                                Server.Transfer("advancedSearch.aspx");
                            }
                            else if ((borrow.Tables[0].Rows.Count + reserve.Tables[0].Rows.Count) >= int.Parse(ds3.Tables[0].Rows[0][0].ToString()))
                            {
                                Response.Write("<script language=javascript>alert('Failed!You have obtain max borrowed number!');</" + "script>");
                                Server.Transfer("advancedSearch.aspx");
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter("@barcode", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@starttime", SqlDbType.DateTime));
                                cmd.Parameters.Add(new SqlParameter("@reservestatus", SqlDbType.Int));
                                cmd.Parameters.Add(new SqlParameter("@endtime", SqlDbType.DateTime));

                                string sqlStr4 = "select set_reserve_period from lm_adminSet";
                                DataSet ds4 = DB.getDataSet(sqlStr4);
                                int reserve_period = Convert.ToInt32(ds4.Tables[0].Rows[0][0].ToString());

                                cmd.Parameters["@barcode"].Value = e.CommandArgument.ToString();
                                cmd.Parameters["@userid"].Value = id;
                                cmd.Parameters["@starttime"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                cmd.Parameters["@reservestatus"].Value = 0;
                                cmd.Parameters["@endtime"].Value = DateTime.Now.AddMinutes(reserve_period).ToString("yyyy-MM-dd HH:mm:ss");
                                string endtime = DateTime.Now.AddMinutes(reserve_period).ToString("yyyy-MM-dd HH:mm:ss");
                                //判断inverntory>amount
                                cmd.ExecuteNonQuery();


                                //sqlStr = "update lm_reserve_record set endtime=DATEADD(MINUTE,(select set_reserve_period from lm_adminSet where Id=10000000),starttime) where barcode = "
                                // + " where DATEDIFF(MINUTE,starttime,GETDATE())>" + reserve_period + "and reserve_status=0 and user_id = " + HttpContext.Current.Session["userid"];
                                DB.executeNonQuery(sqlStr);
                                Response.Write("<script language=javascript>alert('Successfully!You have reserved book " + e.CommandArgument.ToString() + ",please go to " + invent.Tables[0].Rows[0]["bookshelf_id"].ToString() + " to fetch it before " + endtime + "');</" + "script>");
                                conn.Close();
                                Server.Transfer("advancedSearch.aspx");
                            }
                        }
                        else
                        {
                            Response.Write("<script language=javascript>alert('Failed!The book don't have free one!');</" + "script>");
                            Server.Transfer("advancedSearch.aspx");
                        }
                    }
                }
                else
                {
                    Response.Write("<script language=javascript>alert('Failed!You should pay the fine first!');</" + "script>");
                    Server.Transfer("advancedSearch.aspx");
                }
            }
        }
    }
}