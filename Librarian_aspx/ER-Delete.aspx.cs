using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ER_Delete : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if(Request["type"]=="1")
			reserveborrow(Request["barcode"]);
		if (Request["type"] == "2")
			deletereader(Request["userid"]);
	}
	public void UpdateReserveFailBook(String barcode)
	{
		//< asp:ScriptManager ID = "ScriptManager1" runat = "server" EnablePartialRendering = "true" ></ asp:ScriptManager >
		// < asp:UpdatePanel ID = "UpdatePanel1" runat = "server" ></ asp:UpdatePanel >
		// < asp:Timer ID = "Timer1" runat = "server" Interval = "10000" ontick = "Timer1_Tick" Enabled = "true" ></ asp:Timer >
		string sqlStr = "select set_reserve_period from lm_adminSet";
		DataSet ds = DB.getDataSet(sqlStr);
		int reserve_period = int.Parse(ds.Tables[0].Rows[0][0].ToString());
		sqlStr = "update lm_reserve_record set reserve_status=1"
			+ " where DATEDIFF(MINUTE,starttime,borrowtime)>" + reserve_period + "and reserve_status=0 and barcode = " + barcode;
		DB.executeNonQuery(sqlStr);
	}

	protected void reserveborrow(String barcode)
	{
		String url = "LibraLendBook_Reserved.aspx";
		System.DateTime currentTime = new System.DateTime();
		currentTime = System.DateTime.Now;

		bool P_bool_reVal = syjDB.ExSql("update lm_reserve_record set borrowtime = '" + currentTime.ToString() + "' where reserve_status=0 and barcode =" + barcode);
		if (P_bool_reVal)
		{
			UpdateReserveFailBook(barcode);
			DataSet ds1 = syjDB.reDs("select reserve_status from lm_reserve_record where reserve_id = (select top 1 reserve_id from lm_reserve_record where barcode=" + barcode + " order by reserve_id desc)");
			if (ds1.Tables[0].Rows[0][0].ToString() == "1")
			{
				HttpContext.Current.Response.Write("<script>alert('TimeOut!');location.href='" + url + "'</script>");
				//Response.Write("<script>alert('Timeout!')</script>");
			}
			else 
			if (ds1.Tables[0].Rows[0][0].ToString() == "2")
			{
				HttpContext.Current.Response.Write("<script>alert('Success!');location.href='" + url + "'</script>");
				//Response.Write("<script>alert('Success!')</script>");
			}
			else
			{
				HttpContext.Current.Response.Write("<script>alert('Fail!');location.href='" + url + "'</script>");
			}
		}
		else
			HttpContext.Current.Response.Write("<script>alert('Fail!');location.href='" + url + "'</script>");
		//Response.Write("<script>alert('Fail!')</script>");
	}

	protected void deletereader(String userid)
	{
		String url = "EReaderManage.aspx";
		DataSet ds2 = syjDB.reDs("select current_borrowed from lm_users where user_id=" + userid);
		if (int.Parse(ds2.Tables[0].Rows[0][0].ToString()) > 0)
			HttpContext.Current.Response.Write("<script>alert('Books have not been returned!');location.href='" + url + "'</script>");
		else
		{
			DataSet ds1 = syjDB.reDs("select fine_status from lm_borrowed_record where fine_status='no'and fine>0 and user_id=" + userid);
			if (ds1.Tables[0].Rows.Count > 0)
				HttpContext.Current.Response.Write("<script>alert('Fine has not been paid!');location.href='" + url + "'</script>");
			else
			{
				bool P_bool_reVal = syjDB.ExSql("update lm_users set reader_status=1 where user_id=" + userid + ";insert into lm_income(stakeholder,transaction_types,money) values (" + userid + ",0,-(select money from lm_income where stakeholder="+ userid +" and transaction_types=0));");
				if (!P_bool_reVal)
					HttpContext.Current.Response.Write("<script>alert('Fail!');location.href='" + url + "'</script>");
				else
					HttpContext.Current.Response.Write("<script>alert('Success!');location.href='" + url + "'</script>");
			}
		}
	}






}