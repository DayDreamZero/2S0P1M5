using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Librarian_aspx_UpdateBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =  Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            FormViewBindData();
            ViewState["retu"] = Request.UrlReferrer.ToString();
        }
    }

    protected void FormViewBindData()
    {
        string ISBN = Request["ISBN"];
        string sqlStr = "select ISBN,book_name,type_id,author,publishing_house,CONVERT(varchar(7),publication_date, 23) as date,"
            + "borrow_frequency,price, lm_books.bookshelf_id,amount,amount-inventory as amount_inven, inventory,description,book_picture,"
            + "room_id,floor from lm_books,lm_bookshelf"
            + " where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and ISBN='" + ISBN + "'";
        DataSet ds = DB.getDataSet(sqlStr);
        FormView1.DataSource = ds;
        FormView1.DataBind();
        DateDataBind();
        TypeDataBind();
        ShelfDataBind();

    }

    protected void DateDataBind()
    {

        string dateTxt = ((TextBox)FormView1.FindControl("dateTxt")).Text.Trim();
        
        DropDownList Yearddl = ((DropDownList)FormView1.FindControl("Yearddl"));
        DropDownList Monthddl = ((DropDownList)FormView1.FindControl("Monthddl"));
        string oldPubYear = dateTxt.Substring(0, 4);
        string large10 = dateTxt.Substring(5, 1);
        string oldPubMonth = "1";
        if (large10=="1")
            oldPubMonth = dateTxt.Substring(5, 2);
        else
            oldPubMonth = dateTxt.Substring(6, 1);
        DateTime tnow = DateTime.Now;//现在时间
        ArrayList AlYear = new ArrayList();
        int i;
        int nowyear = tnow.Year;
 
        for (i = 1990; i <=nowyear ; i++)
            AlYear.Add(i);
        ArrayList AlMonth = new ArrayList();
        for (i = 1; i <= 12; i++)
            AlMonth.Add(i);

        Yearddl.DataSource = AlYear;
        Yearddl.DataBind();//绑定年
        Yearddl.SelectedValue = oldPubYear;

        Monthddl.DataSource = AlMonth;
        Monthddl.DataBind();//绑定月
        Monthddl.SelectedValue = oldPubMonth;
        
        
    }

    protected void TypeDataBind()
    {
        DropDownList Typeddl = (DropDownList)FormView1.FindControl("Typeddl");
        string sqlStr = "select type_id,type_name from lm_booktype order by type_name";
        DataSet ds = DB.getDataSet(sqlStr);
        Typeddl.DataSource = ds.Tables[0].DefaultView;
        Typeddl.DataTextField = "type_name";
        Typeddl.DataValueField = "type_id";
        Typeddl.DataBind();

        string typeID = ((Button)FormView1.FindControl("oldTypeID")).Text;
        if (typeID != "")
        {
            sqlStr = "select type_name from lm_booktype where type_id='" + typeID+"'";
            ds = DB.getDataSet(sqlStr);
            Typeddl.SelectedItem.Text = ds.Tables[0].Rows[0][0].ToString();
        }
        

    }

    protected void ShelfDataBind()
    {
        DropDownList Shelfddl = (DropDownList)FormView1.FindControl("Shelfddl");
        string sqlStr = "select bookshelf_id from lm_bookshelf order by bookshelf_id";
        DataSet ds = DB.getDataSet(sqlStr);
        Shelfddl.DataSource = ds.Tables[0].DefaultView;
        Shelfddl.DataTextField = "bookshelf_id";
        Shelfddl.DataValueField = "bookshelf_id";
        Shelfddl.DataBind();

        string ShelfID = ((Button)FormView1.FindControl("oldShelf")).Text;
        if (ShelfID != "")
        {
           
            Shelfddl.SelectedItem.Text = ShelfID;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        FileUpload fulPhoto = (FileUpload)FormView1.FindControl("fulPhoto");
        ImageMap imgPhoto = (ImageMap)FormView1.FindControl("imgPhoto");
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
            fulPhoto.PostedFile.SaveAs(Server.MapPath("../image/") + P_str_name);//将文件保存在相应的路径下
            imgPhoto.ImageUrl = "../image/" + P_str_name;//将图片显示在Image控件上
            
        }
        else
        {
            Response.Write("<script>alert('请选择.gif,.png,.jpeg,.jpg,.bmp格式的图片文件!');</script>");
        }
    }

    protected void CancelBt_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["retu"].ToString());
    }

    protected void OKbt_Click(object sender, EventArgs e)
    {
        string ImageUrl = ((ImageMap)FormView1.FindControl("imgPhoto")).ImageUrl.ToString();
        string bookName = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("bookNameTxt")).Text.Trim());
        string ISBNTxt = ((TextBox)FormView1.FindControl("ISBNTxt")).Text.Trim();
        string pubHouseTxt = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("pubHouseTxt")).Text.Trim());

        string year = ((DropDownList)FormView1.FindControl("Yearddl")).SelectedValue.ToString().Trim();
        string month = ((DropDownList)FormView1.FindControl("Monthddl")).SelectedValue.ToString().Trim();
        string dateTxt = "1990-1-1";
        if (month.Length == 2)
            dateTxt = year + "-" + month + "-1";
        else
            dateTxt = year + "-0" + month + "-1";

        string authorTxt = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("authorTxt")).Text.Trim());
        string descriptionTxt = LibraCheckInput.transApostrophe(((TextBox)FormView1.FindControl("descriptionTxt")).Text.Trim());
        string priceTxt = ((TextBox)FormView1.FindControl("priceTxt")).Text.Trim();
        string TypeID = ((DropDownList)FormView1.FindControl("Typeddl")).SelectedValue.ToString();
        string ShelfID = ((DropDownList)FormView1.FindControl("Shelfddl")).SelectedItem.Text;

        if (ImageUrl == "" || bookName == "" || ISBNTxt == "" || pubHouseTxt == "" || priceTxt == "" || authorTxt == "")
            Response.Write("<script>alert('Be sure all information has inputed')</script>");
        else
        {
            int yes = 0;
            string oldISBN = Request["ISBN"];
            if (oldISBN != ISBNTxt)
            {
                string sqlStr = "select * from lm_books where ISBN='" + ISBNTxt + "'";
                yes = DB.executeSelect(sqlStr);
            }

            if (yes == 1)
            {
                Response.Write("<script>alert('This ISBN has existed!please input a new ISBN')</script>");
            }
            else
            {
                string sqlStr = "update lm_books set book_picture=N'" + ImageUrl + "',"
                    + "book_name=N'" + bookName + "',"
                    + "ISBN=N'" + ISBNTxt + "',"
                    + "publishing_house=N'" + pubHouseTxt + "',"
                    + "publication_date=N'" + dateTxt + "',"
                    + "author=N'" + authorTxt + "',"
                    + "description=N'" + descriptionTxt + "',"
                    + "price=" + priceTxt + ","
                    + "type_id=N'" + TypeID + "',"
                    + "bookshelf_id=N'" + ShelfID + "'"
                    + " where ISBN=N'" + oldISBN + "'";
                DB.executeNonQuery(sqlStr);
                string url = "LibraMoreBookInformation.aspx?ISBN=" + ISBNTxt;
                Response.Write("<script>alert('your updating success!');window.location='" + url + "';</script>");

            }
        }

        
    }

    protected void ShelfAddBt_Click(object sender, EventArgs e)
    {
        string shelfID = "";
        string room = "";
        string floor = "";
        TextBox ShelfTb = (TextBox)FormView1.FindControl("ShelfTb");
        if (ShelfTb.Text.Trim() != "" && ShelfTb.Text != "floor-room-shelfNum")
        {
            string addShelf = ShelfTb.Text.Trim();
            int firstIndex = addShelf.IndexOf("-");
            int lastIndex = addShelf.LastIndexOf("-");
            floor = addShelf.Substring(0, firstIndex);
            room = addShelf.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            shelfID = addShelf.Substring(lastIndex + 1);
            if (DB.executeSelect("select bookshelf_id from lm_bookshelf where bookshelf_id=N'" + addShelf + "';") > 0)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The bookshelf has exist!');</script>");
            else
            {
                string sqlStr = "insert into lm_bookshelf values(N'" + addShelf + "',N'" + room + "',N'" + floor + "');";
                if (DB.executeNonQuery(sqlStr) > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Add bookshelf successfully!');</script>");
                    ShelfDataBind();
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please input the Shelf!');</script>");
        }
    }

    protected void TypeAddBt_Click(object sender, EventArgs e)
    {

        string typeID = "";
        string typeName = "";
        TextBox TypeTb = (TextBox)FormView1.FindControl("TypeTb");
        if (TypeTb.Text.Trim() != "" && TypeTb.Text != "typeID-typeName")
        {
            string addType = TypeTb.Text.Trim();
            int index = addType.IndexOf("-");
            typeID = addType.Substring(0, index);
            typeName = addType.Substring(index + 1);
            if (DB.executeSelect("select type_id from lm_booktype where type_id=N'" + typeID + "';") > 0)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The typeID has exist!');</script>");
            else if (DB.executeSelect("select type_name from lm_booktype where type_name=N'" + typeName + "';") > 0)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The typeName has exist!');</script>");
            else
            {
                if (DB.executeNonQuery("insert into lm_booktype values(N'" + typeID + "',N'" + typeName + "')") > 0)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Send successfully!');window.location.href='../Login.aspx';</script>");
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Add type successfully!');</script>");
                    TypeDataBind();
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please input the type!');</script>");
        }

        
    }
}