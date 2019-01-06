using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_admin_delete_lib : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = null;
        if (Request.Params["type"] == "0")
        {
            sql = "Update [dbo].[lm_librarian] set librarian_status = 1 WHERE librarian_id = " + Request.Params["lib_id"] + ";";
        }
        else if (Request.Params["type"] == "1")
        {
            string[] lib_arr = Request.Params["lib_ids"].Split(',');
            for (int i = 0; i < lib_arr.Length - 1; i++)
            {
                sql += "Update [dbo].[lm_librarian] set librarian_status = 1 WHERE librarian_id = " + lib_arr[i] + ";";
            }
        }
        DB.executeNonQuery(sql);
        Response.Redirect("admin-LibrarianGroup.aspx");
    }
}