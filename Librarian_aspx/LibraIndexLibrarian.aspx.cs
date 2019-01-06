using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IndexLibrarian : System.Web.UI.Page
{
    private static string searchStr = "all";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
        
        if (!IsPostBack)
        {
            searchStr = "all";
            Repeater_Databind();
            
        }
    }

    

    void Repeater_Databind()
    {
        string sqlStr = "";
        if (searchStr.Trim() == "all")
            sqlStr = "select book_picture,book_name,publishing_house,author,price,inventory, amount - inventory as amount_inven ,amount,ISBN, "
                + " lm_books.bookshelf_id, room_id,floor from lm_books,lm_bookshelf where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id";
        else
            sqlStr = "select distinct book_picture,book_name,publishing_house,author,price,inventory, amount - inventory as amount_inven ,amount,lm_books.ISBN, "
                + "lm_books.bookshelf_id, room_id,floor from lm_books,lm_barcode,lm_bookshelf "
                + "where lm_books.bookshelf_id=lm_bookshelf.bookshelf_id and"
                + " (book_name like N'%" + searchStr + "%' "
                + "or publishing_house like N'%" + searchStr + "%' "
                + "or author like N'%" + searchStr + "%' "
                + "or lm_books.ISBN like N'%" + searchStr + "%' "
                +"or(lm_books.ISBN=lm_barcode.ISBN and barcode like N'%"+searchStr+"%') );";
        DataSet ds = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = ds.Tables[0].DefaultView;
        pds.AllowPaging = true;//允许分页
        pds.PageSize = 4;//单页显示项数
        this.PageNum.Text = pds.PageCount.ToString();

        int curpage = Convert.ToInt32(curPageNum.Text);
        this.BtnDown.Enabled = true;
        this.BtnUp.Enabled = true;
        pds.CurrentPageIndex = curpage - 1;
        if (curpage == 1)
        {
            this.BtnUp.Enabled = false;
        }
        if (curpage == pds.PageCount)
        {
            this.BtnDown.Enabled = false;
        }
        this.Repeater1.DataSource = pds;

        Repeater1.DataBind();
    }

    protected void BtnUp_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = Convert.ToString(Convert.ToInt32(curPageNum.Text) - 1);
        Repeater_Databind();
    }

    protected void BtnDown_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = Convert.ToString(Convert.ToInt32(curPageNum.Text) + 1);
        Repeater_Databind();
    }

    protected void FirstPage_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = "1";
        Repeater_Databind();
    }


    protected void LastPage_Click(object sender, EventArgs e)
    {
        this.curPageNum.Text = this.PageNum.Text;
        Repeater_Databind();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string ISBN = e.CommandArgument.ToString().Trim();
        if (e.CommandName== "MoreInfo")
        {
            Response.Redirect("LibraMoreBookInformation.aspx?ISBN=" + ISBN);
        }
        else if (e.CommandName == "Status")
        {
            Response.Redirect("LibraMoreInfoBarcode.aspx?ISBN=" + ISBN);
        }
        else if(e.CommandName== "Repurchase")
        {
            string Num = "1";
            RepeaterItemCollection ReItems = Repeater1.Items;
            foreach (RepeaterItem item in ReItems)
            {
                DropDownList dl = (DropDownList)item.FindControl("droplist1");
                if (dl.SelectedValue != "1")
                {
                    Num = dl.SelectedValue;
                    break;
                }

            }
            string url = "LibraRepurchaseBook.aspx?ISBN=" + ISBN + "&Num=" + Num;
            Session["firstRepurchase"] = "yes";
            Response.Write("<script>var ret=confirm('Are you sure?');if(ret){window.location='"+url+ "';};</script>");
            
        }
        else if(e.CommandName== "Update")
        {
            string url = "LibraUpdateBook.aspx?ISBN=" + ISBN;
            Response.Redirect(url);
        }
    }


    protected void searchBt_Click(object sender, EventArgs e)
    {
        //searchStr=searchTxt.Text.Trim();
        searchStr = LibraCheckInput.transApostrophe(searchTxt.Text.Trim());
        this.curPageNum.Text = "1";
        if (searchStr == "")
            searchStr = "all";
        Repeater_Databind();
    }
}