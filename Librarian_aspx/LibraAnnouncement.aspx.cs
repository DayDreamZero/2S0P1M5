using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Librarian_aspx_LibraAnnouncement : System.Web.UI.Page
{
    string url = HttpContext.Current.Request.RawUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text = Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            BindData();
            //alertInfo.Visible = false;
            //DamagedOkBt.Visible = false;
            //DamagedCancelBt.Visible = false;
            //barcodeTxt.Text = "";
        }
    }
    protected void BindData()
    {
        string sqlStr = "select title,content,publicist,release_time,last_modify_time,notice_id,modifier from lm_notice order by release_time desc";
        DataSet ds = DB.getDataSet(sqlStr);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
    }
    protected void btnDetail_Click(object sender, EventArgs e)

    {
        Button btn = (Button)sender;//注意控件类型的转换
        string id = btn.CommandArgument;//获取得到控件绑定的对应值
        Response.Redirect("LibraDetailAnnounce.aspx?notice_id="+id);
    }
    protected void btnEdit_Click(object sender, EventArgs e)

    {
        Button btn = (Button)sender;//注意控件类型的转换
        string id = btn.CommandArgument;//获取得到控件绑定的对应值
        Response.Redirect("LibraEditAnnounce.aspx?notice_id=" + id);
    }
    protected void btnDelete_Click(object sender, EventArgs e)

    {
        Button btn = (Button)sender;//注意控件类型的转换
        string id = btn.CommandArgument;//获取得到控件绑定的对应值
        string sqlStr = "delete from lm_notice where notice_id="+id;
        if (DB.executeNonQuery(sqlStr) > 0)
        {
            BindData();
        }
        else
        {
            HttpContext.Current.Response.Write("<script>alert('failed!');location.href='" + url + "'</script>");
        }
    }

    protected void newAnn(object sender, EventArgs e)
    {
        Response.Redirect("LibraAddNewAnnounce.aspx");
    }
}