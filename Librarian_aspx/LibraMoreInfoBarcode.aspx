<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraMoreInfoBarcode.aspx.cs" Inherits="Librarian_aspx_LibraMoreInfoBarcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_AllBarcode</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
</head>
<body>

    <div class="wrapper">
    <div class="content">

    <div class="navbar navbar-inverse">
        <div class="container">
            <div class="navbar-header">
                <a style="float: left;height: 50px;padding:10px 10px" href="#"><img src="../image/title.ico" height="30px"/></a>
                <a class="navbar-brand" href="LibraHomepage.aspx">Bibliosoft</a>
            </div>
            <ul class="nav navbar-nav">
                <li><a href="LibraIndexLibrarian.aspx">Book management</a></li>
                <li><a href="EReaderManage.aspx">Reader management</a></li>
                <li><a href="LibraFineIndex.aspx">Fine handle</a></li>
                <li><a href="LibraHistoryIncome.aspx">Library Information</a></li>

            </ul>

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../Login.aspx"><asp:Label ID="Label2" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

            <ul class="nav navbar-nav navbar-right">

                <li class='dropdown'>
                    <a href = '#' class='dropdown-toggle' data-toggle='dropdown'><asp:Label ID="LibraNameLab" runat="server" Text="LibraNameLab" ForeColor="#FFFFCC" Font-Size="X-Small" ></asp:Label><b class="caret"></b></a>
                    <ul class='dropdown-menu'>
                        <li><a href="LibraperCenterLibra.aspx"> personal Information</a></li>
                        <li><a href = "LibraUpdatePassword.aspx" >update password</a></li>
                    </ul>
                </li>
			</ul>
        </div>
    </div> 
    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="border-right:solid 1px">
                <div class="list-group side-bar">
                    <a href="LibraIndexLibrarian.aspx" class="list-group-item active">All books</a>
                     <ul class="list-group-item" style="list-style-type:none">             
						<li class='dropdown'>
						<a href = 'LibraLendBook_Unreserved.aspx' class="dropdown-toggle" data-toggle="dropdown" style="text-decoration:none;color:DimGray">Borrowing books</a>
						<ul class='dropdown-menu'>
							<li><a href="LibraLendBook_Unreserved.aspx"> Unreserved</a></li>
							<li><a href = "LibraLendBook_Reserved.aspx" >Reserved</a></li>
						</ul>
						</li>                
					</ul>
                    <a href="LibraReturnBook.aspx" class="list-group-item">Returning books</a>
                    <a href="LibraAddNewBook.aspx" class="list-group-item">Add new books</a>
                    <a href="LibraDamagedBook.aspx" class="list-group-item">Damaged book handle</a>
                    <a href="LibraLostBookHandle.aspx" class="list-group-item ">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div style="width:100%;height:100%;">
                        <div style="text-align:center">
                            &nbsp;&nbsp;
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowHeaderWhenEmpty="True" UseAccessibleHeader="false" EmptyDataText="None" CellSpacing="4">
                                <Columns>
                                    <asp:BoundField DataField="barcode" HeaderText="Barcode" />
                                    <asp:BoundField DataField="book_name" HeaderText="Book Name" />
                                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                                    <asp:BoundField DataField="status" HeaderText="book status" />
                                    <asp:TemplateField HeaderText="Barcode image">
                                        <ItemTemplate>
                                            <div style="height:3px"></div>
                                            <asp:Image ID="barcodeImage" style="margin: 0 auto;" ImageUrl='<%#Eval("barcode_image") %>' runat="server" />
                                            <div style="height:3px"></div>
                                        </ItemTemplate> 
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#ECEFF3" ForeColor="Black" />
                                <HeaderStyle BackColor="#F2F2F2" Font-Bold="False" ForeColor="#666666" Font-Italic="False" CssClass="hmf-gridviewHeader" Height="50px" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            </asp:GridView>
                            
                        </div>
                        <input type="button" onclick="Javascript:window.history.go(-1);"value="Comeback" class="hmf-button" />
                    </div>
                    
                </form>
            </div>
        </div>
    </div>

     </div>
   <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
