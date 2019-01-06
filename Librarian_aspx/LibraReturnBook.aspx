<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraReturnBook.aspx.cs" Inherits="ReturnBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_ReturnBook</title>
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
                    <a href="LibraIndexLibrarian.aspx" class="list-group-item">All books</a>
                    <ul class="list-group-item" style="list-style-type:none">             
						<li class='dropdown'>
						<a href = 'LibraLendBook_Unreserved.aspx' class="dropdown-toggle" data-toggle="dropdown" style="text-decoration:none;color:DimGray">Borrowing books</a>
						<ul class='dropdown-menu'>
							<li><a href="LibraLendBook_Unreserved.aspx"> Unreserved</a></li>
							<li><a href = "LibraLendBook_Reserved.aspx" >Reserved</a></li>
						</ul>
						</li>                
					</ul>
                    <a href="LibraReturnBook.aspx" class="list-group-item active">Returning books</a>
                    <a href="LibraAddNewBook.aspx" class="list-group-item">Add new books</a>
                    <a href="LibraDamagedBook.aspx" class="list-group-item">Damaged book handle</a>
                    <a href="LibraLostBookHandle.aspx" class="list-group-item ">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div style="width:100%;height:500px;background-color:#F2F2F2">
                        <div  style="height:120px"></div>
                        <div style="text-align:center">
                            <asp:Label ID="Label1" runat="server" Text="Barcode" Height="40px" Font-Size="Larger"></asp:Label>
                            <asp:TextBox ID="barcodeTxt" runat="server" Height="30px" Width="280px"></asp:TextBox>
                            <asp:Button ID="OkBt" runat="server" Text="OK" Height="38px" Font-Size="Small" OnClick="OkBt_Click" CssClass="hmf-button" />
                        </div>
                        <div class="row">
                            <div class="col-sm-4"></div>
                             <div class="col-sm-8">
                                 <asp:Label ID="alertInfo" runat="server" Text="This book is overdue,need to pay a fine:" Font-Size="Medium" Height="25px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                 <br />
                                 <asp:Label ID="userIDLab" runat="server" Text="userID" Visible="False"></asp:Label>
                                 <asp:Label ID="barcodeLab" runat="server" Text="barcode" Visible="False"></asp:Label>
                                 <asp:Label ID="fineNumLab" runat="server" Text="Fine:" Font-Size="Medium" Height="25px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                 <asp:Label ID="fineNum" runat="server" Text="＄" Font-Size="Medium" Height="25px" ForeColor="#990000" Visible="False"></asp:Label>
                                 <br />
                                 <asp:Label ID="borrowTimeLab" runat="server" Text="Borrow time:" Font-Size="Medium" Height="25px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                 <asp:Label ID="borrowTime" runat="server" Text="2018-12-2 00:00:00" Font-Size="Medium" Height="25px" ForeColor="#990000" Visible="False"></asp:Label>
                                 <br />
                                 <asp:Label ID="dayDelayLab" runat="server" Text="Overdue Days:" Font-Size="Medium" Height="25px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                 <asp:Label ID="dayDelay" runat="server" Text="5" Font-Size="Medium" Height="25px" ForeColor="#990000" Visible="False"></asp:Label>
                                 <br />
                                 <asp:Label ID="PerFineLab" runat="server" Text="Fine/Day:" Font-Size="Medium" Height="25px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:Label>
                                 <asp:Label ID="PerFine" runat="server" Text="1＄/day" Font-Size="Medium" Height="25px" ForeColor="#990000" Visible="False"></asp:Label>
                                 <br />
                                 <asp:Label ID="hasFine" runat="server" Text="has paid?" Font-Size="Medium" Height="25px" Visible="False"></asp:Label>
                            <asp:Button ID="FineYesBt" runat="server" Text="Yes" Font-Size="Small" Height="25px" OnClick="FineYesBt_Click" Visible="False" CssClass="hmf-button" />
                            &nbsp;<asp:Button ID="FineNoBt" runat="server" Text="No" Font-Size="Small" Height="25px" OnClick="FineNoBt_Click" Visible="False" CssClass="hmf-button" />
                            </div>
                        </div>
                        
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
