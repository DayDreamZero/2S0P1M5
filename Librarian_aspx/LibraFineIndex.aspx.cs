using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Librarian_aspx_LibraFineIndex : System.Web.UI.Page
{
    static private string searchWho = "all";
    static private Dictionary<string, bool> checkMap = new Dictionary<string, bool>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            GridviewData();

        }
    }

    private void GridviewData()
    {
        string sqlStr = "select * from lm_adminSet;";
        DataSet ds = DB.getDataSet(sqlStr);
        string returnDays = ds.Tables[0].Rows[0]["set_return_period"].ToString().Trim();
        if (searchWho == "all")
        {
            sqlStr = "select DATEDIFF(DAY,borrowed_date,getdate())-" + returnDays + " as delay,CONVERT(varchar(12) ,borrowed_date , 111 )as borrowedTime,CONVERT(varchar(12) ,return_date , 111 ) as returnTime,* from lm_barcode as C,lm_users as U,lm_borrowed_record as R,lm_books B" +
                " where fine_status='no' and borrowed_status=1 and R.user_id=U.user_id and R.barcode=C.barcode and C.ISBN=B.ISBN;";
            
        }
        else
        {
            sqlStr = "select DATEDIFF(DAY,borrowed_date,getdate())-" + returnDays + " as delay,CONVERT(varchar(12) ,borrowed_date , 111 )as borrowedTime,CONVERT(varchar(12) ,return_date , 111 ) as returnTime,* from lm_barcode as C,lm_users as U,lm_borrowed_record as R,lm_books B" +
                " where fine_status='no' and borrowed_status=1 and R.user_id=U.user_id and R.barcode=C.barcode and C.ISBN=B.ISBN and R.user_id=" + searchWho + ";";
            
        }
        ds = DB.getDataSet(sqlStr);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView theGrid = sender as GridView;
        int newPageIndex = 0;
        if (e.NewPageIndex == -3)
        {
            TextBox txtNewPageIndex = null;
            GridViewRow pagerRow = theGrid.BottomPagerRow;

            if (pagerRow != null)
            {
                //得到text控件
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
            }
            if (txtNewPageIndex != null)
            {
                //得到索引
                string textPage = txtNewPageIndex.Text.Trim();
                if (isNumber(textPage))
                    newPageIndex = int.Parse(textPage) - 1;
                else
                    newPageIndex = 0;
            }
        }
        else
        {
            newPageIndex = e.NewPageIndex;
        }
        //防止新索引溢出
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
        theGrid.PageIndex = newPageIndex;
        GridviewData();
        showCheckFlag();
    }

    protected void payBt_Click(object sender, EventArgs e)
    {
        
        string borrowID_setStr = "";
        List<string> borrowIdList = new List<string>();
        foreach (KeyValuePair<string,bool>kv in checkMap)
        {
            if (kv.Value)
            {
                borrowIdList.Add(kv.Key);
                borrowID_setStr += kv.Key + ",";
            }
                
        }
        if(borrowID_setStr == "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('No one has been selected!')</script>");
        else
        {
            int len = borrowID_setStr.Length;
            borrowID_setStr = borrowID_setStr.Substring(0, len - 1);//去掉最后一个逗号
            string sqlStr = "select distinct user_id  from lm_borrowed_record where borrowed_id in (" + borrowID_setStr + ");";
            DataSet ds = DB.getDataSet(sqlStr);
            if (ds.Tables[0].Rows.Count == 1)//每次只为一个读者服务
            {
                string useID = ds.Tables[0].Rows[0]["user_id"].ToString();
                foreach(var item in borrowIdList)
                {
                    sqlStr = "update lm_borrowed_record set fine_status='yes' where borrowed_id =" + item + ";";
                    DB.executeNonQuery(sqlStr);
                    sqlStr= "select * from lm_borrowed_record where borrowed_id =" + item + ";";
                    ds = DB.getDataSet(sqlStr);
                    string fineNum = ds.Tables[0].Rows[0]["fine"].ToString();
                    sqlStr = "insert into lm_income values(" + useID + ",1,getdate()," + fineNum + ")";
                   DB.executeNonQuery(sqlStr);
                }
                
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Pay successfully!')</script>");
                resetBt_Click(null, null);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Can only serve one reader at a time!')</script>");
            }
                
        }
        
    }

    protected void searchBt_Click(object sender, EventArgs e)
    {
        checkMap.Clear();
        if (readerIDtext.Text.Trim() != "")
        {
            if (!LibraCheckInput.IsNum(readerIDtext.Text.Trim()))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
            else
            {
                searchWho = readerIDtext.Text.Trim();
                string sqlStr = "select * from lm_borrowed_record where fine_status='no' and borrowed_status=1 and user_id='" + searchWho + "';";
                if (DB.executeSelect(sqlStr) == 1)
                {
                    GridviewData();
                }
                else
                {
                    searchWho = "all";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('No fine for the reader!')</script>");
                }
            }
        }
    }

    protected void resetBt_Click(object sender, EventArgs e)
    {
        searchWho = "all";
        readerIDtext.Text = "";
        GridView1.PageIndex = 0;
        checkMap.Clear();
        fineLab.Text = "0";
        GridviewData();
    }

    private void saveCheckFlag()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            checkMap[GridView1.DataKeys[i].Value.ToString().Trim()] = ((CheckBox)GridView1.Rows[i].FindControl("check")).Checked;
           
        }
        
    }

    private void showCheckFlag()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (checkMap.ContainsKey(GridView1.DataKeys[i].Value.ToString().Trim()))
                ((CheckBox)GridView1.Rows[i].FindControl("check")).Checked = checkMap[GridView1.DataKeys[i].Value.ToString().Trim()];
        }
    }


    protected void check_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckbx = (CheckBox)sender;
        int index = ((GridViewRow)(ckbx.NamingContainer)).RowIndex;//通过NamingContainer可以获取当前checkbox所在容器对象，即gridviewrow 
        
        saveCheckFlag();
        float fine = float.Parse(fineLab.Text);
        if (ckbx.Checked)
            fine += float.Parse(GridView1.Rows[index].Cells[7].Text);
        else
            fine -= float.Parse(GridView1.Rows[index].Cells[7].Text);
           
        fineLab.Text = "" + fine.ToString();

    }

    private bool isNumber(string numStr)
    {
        try
        {
            int.Parse(numStr);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
