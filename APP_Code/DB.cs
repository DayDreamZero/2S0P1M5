using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// DB 的摘要说明
/// </summary>
public class DB
{
    public DB()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    private static SqlConnection getCon()
    {
        string sqlcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        return new SqlConnection(sqlcon);
        //return new SqlConnection("Server=.\\sqlexpress;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='C:\\Users\\admin\\Desktop\\SPM大作业\\10.01-10.08项目功能开发\\library system-4\\App_Data\\Database.mdf';Integrated Security=True");
       // return new SqlConnection("Server=.\\sqlexpress;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='F:\\学校事宜\\项目管理\\library system-4\\library system-4\\App_Data\\Database.mdf';Integrated Security=True");
    }

    public static  DataSet getDataSet(string sqlStr)
    {
        SqlConnection con = getCon();
        con.Open();
        SqlDataAdapter sda = new SqlDataAdapter(sqlStr,con);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        con.Close();
        return ds;
    }

    //带参数
    //SqlParameter[] parameters = { new SqlParameter("@a", "a1"), new SqlParameter("@b", "b1") };
    public static DataSet getDataSet(string sqlStr, SqlParameter [] param)
    {
        SqlConnection con = getCon();
        con.Open();
        SqlCommand sqlCmd = new SqlCommand(sqlStr, con);
        sqlCmd.Parameters.AddRange(param);
        string newSqlStr = sqlCmd.ToString();
        SqlDataAdapter sda = new SqlDataAdapter(newSqlStr, con);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        con.Close();
        return ds;
    }

    public static int executeNonQuery(string sqlStr)
    {
        SqlConnection con = getCon();
        con.Open();
        SqlCommand sqlCmd = new SqlCommand(sqlStr, con);
        int ncount= sqlCmd.ExecuteNonQuery();
        con.Close();
        return ncount;
    }

    public static int executeSelect(string sqlStr)
    {
        SqlConnection con = getCon();
        con.Open();
        SqlCommand sqlCmd = new SqlCommand(sqlStr, con);
        string ans = "";
        if (sqlCmd.ExecuteScalar() != null)
            ans = sqlCmd.ExecuteScalar().ToString();
        con.Close();
        if (ans == "")
            return 0;
        else
            return 1;
    }

}