<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraIndexLibrarian.aspx.cs" Inherits="IndexLibrarian" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_LibrarianIndex</title>
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
                    <a href="LibraLostBookHandle.aspx" class="list-group-item">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div class="row"  style="height:auto">
                        <div class="col-sm-8" style="text-align:center">
                            <asp:TextBox ID="searchTxt" runat="server" CssClass="hmf-textbox" Width="200px"></asp:TextBox>
                        <asp:Button ID="searchBt" runat="server" Text="Search" OnClick="searchBt_Click" CssClass="hmf-button" />
                        </div>
                        <div class="col-sm-4" style="text-align:right">
                            &nbsp;
                            <asp:Button ID="FirstPage" runat="server" Text="First" OnClick="FirstPage_Click" BackColor="white" ForeColor="#3366FF" style="border:solid;border-color:white"/>
                            <asp:Button ID="BtnUp" runat="server" onclick="BtnUp_Click" Text="&lt;&lt;" BackColor="white" ForeColor="#3366FF" style="border:solid;border-color:white"/>
                            <asp:Label ID="curPageNum" text="1" runat="server"></asp:Label>
                            <asp:Button ID="BtnDown" runat="server" onclick="BtnDown_Click" Text="&gt;&gt;" BackColor="White" ForeColor="#3366FF" style="border:solid;border-color:white" />
                            <asp:Button ID="LastPage" runat="server" Text="Last" OnClick="LastPage_Click" BackColor="white" ForeColor="#3366FF" style="border:solid;border-color:white" />
                            Total pages:<asp:Label ID="PageNum" text="unknown" runat="server"></asp:Label>
                        </div>
                          
                    </div>
                    <hr style="border:dotted 1px" />
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >
                        <ItemTemplate>
                            <div class="row book-list">
                                <div class="book-list-item">
                                    <div class="col-xs-1"></div>
                                    <div class="col-xs-2">
                                        <img class="book-image" src='<%#Eval("book_picture") %>' />
                                    </div>
                                    <div class="book-info col-xs-5">
                                        <p>Book name：<asp:Label ID="bookNameLab" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                        <p>Publishing house：<asp:Label ID="pubHouseLab" runat="server" Text='<%#Eval("publishing_house") %>'></asp:Label></p>
                                        <p>Author：<asp:Label ID="authorLab" runat="server" Text='<%#Eval("author") %>'></asp:Label></p>
                                        <p>Position：
                                            floor:<asp:Label ID="floorLab" runat="server" Text='<%#Eval("floor") %>'></asp:Label>
                                            room:<asp:Label ID="roomLab" runat="server" Text='<%#Eval("room_id") %>'></asp:Label>
                                            bookshelf:<asp:Label ID="shelfLab" runat="server" Text='<%#Eval("bookshelf_id") %>'></asp:Label>
                                        </p>
                                        <p>
                                            <span>Remaining:<asp:Label ID="inventoryLab" runat="server" Text='<%#Eval("inventory") %>'></asp:Label></span>
                                            <span>Lend out:<asp:Label ID="OutNum" runat="server" Text='<%#Eval("amount_inven") %>'></asp:Label></span>
                                            <span>Amount:<asp:Label ID="amountLab" runat="server" Text='<%#Eval("amount") %>'></asp:Label></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="LinkButton1" CommandName="MoreInfo" CommandArgument='<%#Eval("ISBN") %>' runat="server">More Information...</asp:LinkButton>
                                        </p>
                                        <p>
                                            <asp:Button ID="statusBt" CommandName="Status" CommandArgument='<%#Eval("ISBN") %>' runat="server" Text="Copies status" CssClass="hmf-button" Height="25px" />
                                        </p>
                                        <p>
                                            <asp:Button ID="Button1" CommandName="Repurchase" CommandArgument='<%#Eval("ISBN") %>' Text="Add Copies" runat="server" CssClass="hmf-button" Height="25px"></asp:Button>
                                            <asp:DropDownList ID="droplist1" runat="server" >  
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>  
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>  
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>        
                                            </asp:DropDownList> 
                                        </p>
                                        <p>
                                            <asp:Button ID="Button2" CommandName="Update" CommandArgument='<%#Eval("ISBN") %>' Text="Edit" runat="server" CssClass="hmf-button" Height="25px"></asp:Button>
                                        </p>
                                    </div>    
                                </div>
                            </div>
                            <p> <hr/></p>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                </form>
            </div>
        </div>
    </div>
    

     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
