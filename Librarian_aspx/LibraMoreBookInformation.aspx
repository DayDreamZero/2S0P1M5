<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraMoreBookInformation.aspx.cs" Inherits="Librarian_aspx_MoreBookInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_BookInformation</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <style type="text/css">
        .auto-style6 {
            width: 114px;
        }
        
    </style>

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

                    <asp:FormView ID="FormView1" style="width:100%" runat="server">
                        <ItemTemplate>

                        <div class="row book-list">
                           
                                <div class="col-xs-3">
                                    <img class="book-image" src='<%#Eval("book_picture") %>' />
                                </div>
                                <div class="book-info col-xs-8" style="background-color:#F2F2F2">
                                    
                                    <table style="border-collapse:separate; border-spacing:0px 10px;">
                                        <tr >
                                            <td class="auto-style6">Book name:</td>
                                            <td><asp:Label ID="bookNameLab" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Book ISBN:</td>
                                            <td class="auto-style1"><asp:Label ID="Label1" runat="server" Text='<%#Eval("ISBN") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Publishing house:</td>
                                            <td><asp:Label ID="pubHouseLab" runat="server" Text='<%#Eval("publishing_house") %>'></asp:Label></td>
                                        </tr >

                                        <tr>
                                            <td class="auto-style6">Publishing date</td>
                                            <td><asp:Label ID="dateLab" runat="server" Text='<%#Eval("date") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Author:</td>
                                            <td><asp:Label ID="Label4" runat="server" Text='<%#Eval("author") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Description:</td>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("description") %>' ReadOnly="True" TextMode="MultiLine " Height="100px" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Price:</td>
                                            <td><asp:Label ID="priceLab" runat="server" Text='<%#Eval("price") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Type:</td>
                                            <td><asp:Label ID="TypeLab" runat="server" Text='<%#Eval("type_name") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Position:</td>
                                            <td>
                                                <asp:Label ID="PosiLab" CommandName="Position" CommandArgument='<%#Eval("bookshelf_id") %>' runat="server" 
                                                        Text='<%#"On the bookshelf "+Eval("bookshelf_id").ToString()+" of room "+Eval("room_id").ToString()+" at the floor "+Eval("floor").ToString() %>'></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Borrow frequency:</td>
                                            <td><asp:Label ID="Label3" runat="server" Text='<%#Eval("borrow_frequency") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td class="auto-style6">Numbers:</td>
                                            <td>
                                                Remaining:<asp:Label ID="inventoryLab" runat="server" Text='<%#Eval("inventory") %>'></asp:Label>
                                                Lend out:<asp:Label ID="OutNum" runat="server" Text='<%#Eval("amount_inven") %>'></asp:Label>
                                                Amount:<asp:Label ID="amountLab" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                                            </td>
                                        </tr>


                                    </table>
                                    
                                    <input type="button" onclick="Javascript:window.history.go(-1);"value="Comeback" class="hmf-button" />
                                    &nbsp;
                                    <asp:LinkButton ID="moreBarcode" runat="server" OnClick="moreBarcode_Click" Font-Size="Medium">all barcode</asp:LinkButton>
                                   
                                </div>    
                            
                        </div>
                       </ItemTemplate> 
                   </asp:FormView>

                </form>
            </div>
        </div>
    </div>

     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
