using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using CopyDirectoryInfo;
using System.Drawing;

public partial class Librarian_aspx_LibraAddNewBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LibraId"] == null)
            Response.Redirect("../Login.aspx");
        else
            LibraNameLab.Text =Session["LibraName"].ToString();
        if (!IsPostBack)
        {
            ErrLab.Visible = false;
            DateDataBind();
            TypeDataBind();
            ShelfDataBind();
        }

    }

    protected void DateDataBind()
    {

       
        DateTime tnow = DateTime.Now;//现在时间
        ArrayList AlYear = new ArrayList();
        int i;
        int nowyear = tnow.Year;
        int nowMonth = tnow.Month;

        for (i = 1990; i <= nowyear; i++)
            AlYear.Add(i);
        ArrayList AlMonth = new ArrayList();
        for (i = 1; i <= 12; i++)
            AlMonth.Add(i);

        Yearddl.DataSource = AlYear;
        Yearddl.DataBind();//绑定年
        Yearddl.SelectedValue = nowyear.ToString();

        Monthddl.DataSource = AlMonth;
        Monthddl.DataBind();//绑定月
        Monthddl.SelectedValue = nowMonth.ToString();

    }

    protected void TypeDataBind()
    {
        
        string sqlStr = "select type_id,type_name from lm_booktype order by type_name";
        DataSet ds = DB.getDataSet(sqlStr);
        Typeddl.DataSource = ds.Tables[0].DefaultView;
        Typeddl.DataTextField = "type_name";
        Typeddl.DataValueField = "type_id";
        Typeddl.DataBind();
        
    }

    protected void ShelfDataBind()
    {
        
        string sqlStr = "select bookshelf_id from lm_bookshelf order by bookshelf_id";
        DataSet ds = DB.getDataSet(sqlStr);
        Shelfddl.DataSource = ds.Tables[0].DefaultView;
        Shelfddl.DataTextField = "bookshelf_id";
        Shelfddl.DataValueField = "bookshelf_id";
        Shelfddl.DataBind();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('请选择.gif,.png,.jpeg,.jpg,.bmp格式的图片文件!');</script>");
        }
    }

    

    protected void OKbt_Click(object sender, EventArgs e)
    {
        string ImageUrl = imgPhoto.ImageUrl;
        string bookName = LibraCheckInput.transApostrophe(bookNameTxt.Text.Trim());
        string ISBN = ISBNTxt.Text.Trim();
        string pubHouse = LibraCheckInput.transApostrophe(pubHouseTxt.Text.Trim());

        string year =Yearddl.SelectedItem.ToString().Trim();
        string month = Monthddl.SelectedItem.ToString().Trim();
        string dateTxt = "1990-1-1";
        if (month.Length == 2)
            dateTxt = year + "-" + month + "-1";
        else
            dateTxt = year + "-0" + month + "-1";

        string author = LibraCheckInput.transApostrophe(authorTxt.Text.Trim());
        string description = LibraCheckInput.transApostrophe(descriptionTxt.Text.Trim());
        string price =priceTxt.Text.Trim();
        string TypeID = Typeddl.SelectedValue.ToString();
        string ShelfID = Shelfddl.SelectedItem.Text;
        string amount = amountTxt.Text;

        if(ImageUrl==""|| bookName==""|| ISBN==""|| pubHouse==""|| price==""|| author ==""|| amount=="")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Be sure all information has inputed.');</script>"); 
        else
        {
            string sqlStr = "select * from lm_books where ISBN='" + ISBN + "'";
            int yes = DB.executeSelect(sqlStr);

            if (yes == 1)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('This ISBN has existed!please input a new ISBN.');</script>");
            }
            else
            {
                sqlStr = "insert into lm_books values(N'" + ISBN + "',"
                    + "N'" + bookName + "',"
                    + "'"+TypeID + "',"
                    + "N'" + author + "',"
                    + "N'" + pubHouse + "',"
                    + "N'" + dateTxt + "',"
                    + price + ","
                    + "N'" + ShelfID + "',"
                    + "N'" + description + "',"
                    + "N'" + ImageUrl + "',"
                    + amount + ","
                    + amount + ",0)";

                DB.executeNonQuery(sqlStr);

                sqlStr = "select top " + amount + " barcode from lm_barcode"
                    + " where ISBN='" + ISBN + "'"
                    + "order by barcode desc";
                DataSet ds = DB.getDataSet(sqlStr);

                BandCode.Code128 _Code = new BandCode.Code128();
                _Code.ValueFont = new Font("宋体", 9);

                for (int i = 0; i < Convert.ToInt32(amount); i++)
                {
                    string barcode = ds.Tables[0].Rows[i]["barcode"].ToString();
               
                    Bitmap bmp = _Code.GetCodeImage(barcode, BandCode.Code128.Encode.Code128A);
                    string path = Server.MapPath("../barcodeImage");
                    bmp.Save(path + "\\"+barcode+".bmp");

                    sqlStr= "update lm_barcode set barcode_image=N'../barcodeImage/"+barcode+".bmp' where barcode=" + barcode;
                    DB.executeNonQuery(sqlStr);
                }
                string url = "LibranewBookBarcode.aspx?ISBN=" + ISBN+"&Num="+amount;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('adding books success!');window.location='" + url + "';</script>");
                

            }
        }
        
    }


    protected void ClearBt_Click(object sender, EventArgs e)
    {
        searchTxt.Text = "";
        clear();
    }

    private void clear()
    {
        //searchTxt.Text = "";
        imgPhoto.ImageUrl = "";
        bookNameTxt.Text = "";
        ISBNTxt.Text = "";
        pubHouseTxt.Text = "";
        authorTxt.Text = "";
        descriptionTxt.Text = "";
        priceTxt.Text = "";
        amountTxt.Text = "";
        DateDataBind();
        TypeDataBind();
        ShelfDataBind();
    }

    protected void searchBt_Click(object sender, EventArgs e)
    {
        ErrLab.Visible = false;
        clear();
        if (searchTxt.Text.Trim() == "")
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('if searching,the ISBN can't be null');</script>");
        else
        {
            try
            {
                HttpWebRequest myRequest = null;
                HttpWebResponse myHttpResponse = null;
                string doubanurl = "http://api.douban.com/book/subject/isbn/";
                string geturl = doubanurl + searchTxt.Text.Trim();
            
                myRequest = (HttpWebRequest)WebRequest.Create(geturl);
                myHttpResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpResponse.GetResponseStream());
                string xmldetail = reader.ReadToEnd();
                reader.Close();
                myHttpResponse.Close();

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmldetail);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("db", "http://www.w3.org/2005/Atom");
                XmlElement root = xml.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/db:entry", nsmgr);

                foreach (XmlNode nodeData in nodes)
                {
                    foreach (XmlNode childnode in nodeData.ChildNodes)
                    {
                        string str = childnode.Name;
                        switch (str)
                        {
                            case "title":
                                bookNameTxt.Text = childnode.InnerText;
                                break;
                            case "link":
                                if (childnode.Attributes[1].Value == "image")
                                {
                                    string Imageurl = childnode.Attributes[0].Value;
                                    string filePath = Server.MapPath("../image");
                                    string imagePath = filePath+"\\" + searchTxt.Text.Trim()+".jpg";
                                    string imageName = searchTxt.Text.Trim() + ".jpg";
                                    System.Net.WebClient client = new System.Net.WebClient();
                                    client.DownloadFile(Imageurl, imagePath);
                                    imgPhoto.ImageUrl = "../image/"+imageName;
                                    imgPhoto.DataBind();
                                }
                                break;
                            case "summary":
                                descriptionTxt.Text = childnode.InnerText;
                                break;
                            case "db:attribute":
                                {
                                    switch (childnode.Attributes[0].Value)
                                    {
                                        case "isbn13":
                                            ISBNTxt.Text = childnode.InnerText;
                                            break;
                                        case "author":
                                            authorTxt.Text += childnode.InnerText + ";";
                                            break;
                                        case "price":
                                            string price = childnode.InnerText;
                                            int len = price.Length;
                                            if (len>0)
                                            {
                                                // 正则表达式剔除非数字字符（不包含小数点.）
                                                price = Regex.Replace(price, @"[^\d.\d]", "");

                                                // 如果是数字
                                                if (Regex.IsMatch(price, @"^[+-]?\d*[.]?\d*$"))
                                                {
                                                    priceTxt.Text = price;
                                                }
                                            }
                                            break;
                                        case "publisher":
                                            pubHouseTxt.Text = childnode.InnerText;
                                            break;
                                        case "pubdate":
                                            string date = childnode.InnerText;
                                            string year = null;
                                            string month = null;
                                            if (date.Length >= 7)
                                            {
                                                year = date.Substring(0, 4);
                                                string line = date.Substring(6, 1);
                                                month = date.Substring(5, 2);

                                                if (line == "-")
                                                    month = date.Substring(5, 1);

                                            }
                                            else
                                            {
                                                    year = date.Substring(0, 4);
                                                    month = date.Substring(5);
                                                }
                                            
                                            Yearddl.SelectedItem.Text = year;
                                            Monthddl.SelectedItem.Text = month;
                                            break;
                                    }//end switch
                                    break;
                                }
                        }//end switch
                    }//end foreach
                }//end foreach
            }
            catch(System.Net.WebException x)
            {
                ErrLab.Visible = true;
                clear();
            }   
            
        }
        
    }

    protected void TypeAddBt_Click(object sender, EventArgs e)
    {
        string typeID = "";
        string typeName = "";
        if(TypeTb.Text.Trim()!=""&&TypeTb.Text!= "typeID-typeName")
        {
            string addType = TypeTb.Text.Trim();
            int index = addType.IndexOf("-");
            typeID = addType.Substring(0, index);
            typeName = addType.Substring(index + 1);
            if(DB.executeSelect("select type_id from lm_booktype where type_id=N'"+typeID+"';")>0)
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('The typeID has exist!');</script>");
            else if(DB.executeSelect("select type_name from lm_booktype where type_name=N'" + typeName + "';") > 0)
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

    protected void ShelfAddBt_Click(object sender, EventArgs e)
    {
        string shelfID = "";
        string room = "";
        string floor = "";
        if(ShelfTb.Text.Trim()!=""&&ShelfTb.Text!= "floor-room-shelfNum")
        {
            string addShelf = ShelfTb.Text.Trim();
            int firstIndex = addShelf.IndexOf("-");
            int lastIndex = addShelf.LastIndexOf("-");
            floor = addShelf.Substring(0, firstIndex);
            room = addShelf.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            shelfID = addShelf.Substring(lastIndex + 1);
            if(DB.executeSelect("select bookshelf_id from lm_bookshelf where bookshelf_id=N'"+ addShelf + "';")>0)
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script type='text/javascript'>alert('Please input the shelf!');</script>");
        }
    }
}