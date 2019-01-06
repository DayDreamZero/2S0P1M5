using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReturnBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
        {
            LibraNameLab.Text =  Session["LibraName"].ToString();
            UpdateOverdueBook();
            notOverdueShow();
        }
            
        
    }

    private void UpdateOverdueBook()
    {
        string sqlStr = "select set_penalty_overdue,set_return_period from lm_adminSet where Id=10000000;";
        DataSet ds = DB.getDataSet(sqlStr);
        Decimal fineDay = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
        int returnDays = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

        //检查是否没超期的是否超期并更新
        sqlStr = "select borrowed_id from lm_borrowed_record" + " where DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=0";
        ds = DB.getDataSet(sqlStr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set borrowed_status=2,fine_status='no',fine=(DATEDIFF(DAY,borrowed_date,GETDATE())-" + returnDays + ")*" + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }


        //更新已超期的
        sqlStr = "select borrowed_id from lm_borrowed_record " + " where fine_status='no'and DATEDIFF(DAY,borrowed_date,GETDATE())>" + returnDays + " and borrowed_status=2";
        ds = DB.getDataSet(sqlStr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sqlStr = "update lm_borrowed_record set fine = (DATEDIFF(DAY, borrowed_date, GETDATE()) - " + returnDays + ") * " + fineDay
            + " where borrowed_id=" + ds.Tables[0].Rows[i]["borrowed_id"].ToString();

            DB.executeNonQuery(sqlStr);
        }

    }

    private void OverdueShow(string barcode)
    {

        DataSet adminSet = DB.getDataSet("select * from lm_adminSet");
        string returnDays = adminSet.Tables[0].Rows[0]["set_return_period"].ToString();
        string sqlStr = "select DATEDIFF(DAY,borrowed_date,GETDATE())-"+returnDays+",CONVERT(varchar(12) ,borrowed_date , 111 ) ,* from lm_borrowed_record where barcode=" + barcode + " and borrowed_status=2";
        DataSet ds = DB.getDataSet(sqlStr);
        fineNum.Text = ds.Tables[0].Rows[0][8].ToString().Trim();
        borrowTime.Text = ds.Tables[0].Rows[0][1].ToString().Trim();
        dayDelay.Text = ds.Tables[0].Rows[0][0].ToString().Trim();
        PerFine.Text = adminSet.Tables[0].Rows[0][2].ToString()+ "￥/day";
        userIDLab.Text = ds.Tables[0].Rows[0][3].ToString().Trim();
        barcodeLab.Text = ds.Tables[0].Rows[0][4].ToString().Trim();

        alertInfo.Visible = true;
        fineNumLab.Visible = true;
        fineNum.Visible = true;
        borrowTimeLab.Visible = true;
        borrowTime.Visible = true;
        dayDelayLab.Visible = true;
        dayDelay.Visible = true;
        PerFineLab.Visible = true;
        PerFine.Visible = true;
        hasFine.Visible = true;
        FineYesBt.Visible = true;
        FineNoBt.Visible = true;
    }

    private void notOverdueShow()
    {
        alertInfo.Visible = false;
        fineNumLab.Visible = false;
        fineNum.Visible = false;
        borrowTimeLab.Visible = false;
        borrowTime.Visible = false;
        dayDelayLab.Visible = false;
        dayDelay.Visible = false;
        PerFineLab.Visible = false;
        PerFine.Visible = false;
        hasFine.Visible = false;
        FineYesBt.Visible = false;
        FineNoBt.Visible = false;
    }

    protected void OkBt_Click(object sender, EventArgs e)
    {
        string barcode = barcodeTxt.Text.Trim();
        
        if (barcode == "")
            //Response.Write("<script>alert('Please input the barcode.')</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please input the barcode.');</script>");
        else if(!LibraCheckInput.IsNum(barcode))
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Contains illegal characters! Please input a numeric string.');</script>");
        else
        { 
            string sqlStr = "select * from lm_borrowed_record where barcode="+barcode+" and borrowed_status=0;";
            if (DB.executeSelect(sqlStr) == 1)
            {
                sqlStr= "update lm_borrowed_record set borrowed_status=1 where barcode=" + barcode + " and borrowed_status=0;";
                int count=DB.executeNonQuery(sqlStr);
                if (count > 1)
                {
                    
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book with barcode " + barcode + " is returned successfully.');</script>");
                    barcodeTxt.Text = "";
                }
            }
            else
            {
                sqlStr = "select top 1 * from lm_borrowed_record where barcode=" + barcode+ " order by borrowed_id desc";
                DataSet ds = DB.getDataSet(sqlStr);
                string status= "null";
                if (ds.Tables[0].Rows.Count == 1)
                {
                    status = ds.Tables[0].Rows[0][5].ToString();
                    switch (status)
                    {
                        case "1":
                            notOverdueShow();
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book has been returned.');</script>");
                            break;
                        case "2"://overdue
                            OverdueShow(barcode);
                            break;
                        case "3":
                            notOverdueShow();
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book has been damaged.');</script>");
                            break;
                        case "4":
                            notOverdueShow();
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book has been lost.');</script>");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book may be not lent out or exits.So you do not need to return.');</script>");
                   
                }

            }
        }
    }

    protected void FineYesBt_Click(object sender, EventArgs e)
    {

        string barcode = barcodeTxt.Text.ToString().Trim();
        if (barcode != barcodeLab.Text)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please not to change the barcode.');</script>");
        }
        else
        {
            string sqlStr = "update lm_borrowed_record set borrowed_status=1,fine_status='yes' where barcode=" + barcode + " and borrowed_status=2;";
            int count = DB.executeNonQuery(sqlStr);
            if (count > 1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book with barcode " + barcode + " is returned successfully.');</script>");
                sqlStr = "insert into lm_income values('" + userIDLab.Text + "',1,GETDATE()," + fineNum.Text + ")";
                DB.executeNonQuery(sqlStr);
            }
            
        }


        barcodeTxt.Text = "";
        notOverdueShow();

    }

    protected void FineNoBt_Click(object sender, EventArgs e)
    {

        string barcode = barcodeTxt.Text.ToString().Trim();
        if (barcode != barcodeLab.Text)
        {
           
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please not to change the barcode.');</script>");
        }
        else
        {
           
            string sqlStr = "update lm_borrowed_record set borrowed_status=1,fine_status='no' where barcode=" + barcode + " and borrowed_status=2;";
            int count = DB.executeNonQuery(sqlStr);
            if (count > 1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This book with barcode " + barcode + " is returned successfully.But the fine is not paid!');</script>");
               
            }

        }

        barcodeTxt.Text = "";
        notOverdueShow();
    }
}