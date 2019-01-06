using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EReaderBarcode : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
		else
			LibraNameLab.Text = Session["LibraName"].ToString();
		if (!IsPostBack)
		{
			gridviewBindata();
		}
	}

	private void gridviewBindata()
	{
		string id = Request["userid"];
		string sqlStr;
			sqlStr = "select * from lm_users where  user_id=" + id;
		GridView1.DataSource = DB.getDataSet(sqlStr);
		GridView1.DataBind();
	}
    protected void ok_Click(object sender, EventArgs e)
    {
        Response.Redirect("EReaderManage.aspx");
    }
}