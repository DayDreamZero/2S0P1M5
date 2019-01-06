<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraAddNewBook.aspx.cs" Inherits="Librarian_aspx_LibraAddNewBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_AddBook</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <script type="text/javascript">
         function noDefaultShelf() {
             var obj = document.getElementById('ShelfTb');
            obj.style.color = '#000';
            with (event.srcElement)
                if (value == defaultValue) value = ""
        }

        function isDefaultShelf() {
            var obj = document.getElementById('ShelfTb');
            obj.style.color = 'Gray';
            with (event.srcElement)
                if (value == "") value = defaultValue;
        }

        function noDefaultType() {
            var obj = document.getElementById('TypeTb');
            obj.style.color = '#000';
            with (event.srcElement)
                if (value == defaultValue) value = ""
        }

        function isDefaultType() {
            var obj = document.getElementById('TypeTb');
            obj.style.color = 'Gray';
            with (event.srcElement)
                if (value == "") value = defaultValue;
        }

    </script>

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
                    <a href="LibraIndexLibrarian.aspx" class="list-group-item ">All books</a>
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
                    <a href="LibraAddNewBook.aspx" class="list-group-item active">Add new books</a>
                    <a href="LibraDamagedBook.aspx" class="list-group-item">Damaged book handle</a>
                    <a href="LibraLostBookHandle.aspx" class="list-group-item">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div class="row"  style="border-color:transparent;height:auto;text-align:center">
                        <asp:Label ID="Label1" runat="server" Text="ISBN"></asp:Label>
                        <asp:TextBox ID="searchTxt" runat="server" CssClass="hmf-textbox"></asp:TextBox>
                        <asp:Button ID="searchBt" runat="server" Text="Search" OnClick="searchBt_Click" CssClass="hmf-button" />
                        <br />
                        <asp:Label ID="ErrLab" runat="server" visible="false" Text="Not found.Please check your ISBN or input by yourself." Font-Size="Medium" ForeColor="Red"></asp:Label>
                    </div>
                    <hr style="border:dotted 1px" />
                    <div class="row book-list">
                        <div class="book-list-item">
                            <div class="col-xs-4">
                                <asp:ImageMap ID="imgPhoto" runat="server"></asp:ImageMap>
                                <asp:FileUpload ID="fulPhoto" runat="server" />
                                <asp:Button ID="btnShow" runat="server" CausesValidation="False" Height="25px" OnClick="btnShow_Click" Text="Submit image" CssClass="hmf-button" />
                            </div>
                            <div class="book-info col-xs-8">
                                <table style="border-collapse:separate; border-spacing:0px 10px;">
                                    <tr>
                                        <td>Book name:</td>
                                        <td><asp:TextBox ID="bookNameTxt"  runat="server" Width="200px" CssClass="hmf-textbox"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>Book ISBN:</td>
                                        <td><asp:TextBox ID="ISBNTxt" runat="server" Width="200px" CssClass="hmf-textbox" ></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Be sure it is a ISBN string with lengh 13!" ControlToValidate="ISBNTxt" ValidationExpression="[0-9]{13}" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Publishing house:</td>
                                        <td><asp:TextBox ID="pubHouseTxt" runat="server" Width="200px" CssClass="hmf-textbox" ></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>Publishing date:</td>
                                        <td>
                                            <asp:DropDownList ID="Yearddl" runat="server"></asp:DropDownList>year
                                            <asp:DropDownList ID="Monthddl" runat="server"></asp:DropDownList>month
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Author:</td>
                                        <td><asp:TextBox ID="authorTxt" runat="server" Width="400px" CssClass="hmf-textbox"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>Description:</td>
                                        <td><asp:TextBox ID="descriptionTxt" runat="server" TextMode="MultiLine" Height="70px" Width="400px"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <td>Price:</td>
                                        <td>
                                            <asp:TextBox ID="priceTxt" Width="200px" runat="server" CssClass="hmf-textbox" ></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Be sure it is a number!" ControlToValidate="priceTxt" ValidationExpression="^[0-9]+([.]{1}[0-9]+){0,1}$" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Type:</td>
                                        <td><asp:DropDownList ID="Typeddl" runat="server" Width="150px"></asp:DropDownList>
                                            <asp:TextBox ID="TypeTb" Text="typeID-typeName" Style="color: Gray" runat="server" onfocus="noDefaultType()" onblur="isDefaultType()"></asp:TextBox>
                                            <asp:Button ID="TypeAddBt" runat="server" Text="Add type" OnClick="TypeAddBt_Click" CssClass="hmf-button"/>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="format incorrect!" ControlToValidate="TypeTb" ValidationExpression="[a-zA-Z0-9]+[-][a-zA-Z0-9]+" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Position_Shelf:</td>
                                        <td><asp:DropDownList ID="Shelfddl" runat="server"></asp:DropDownList>
                                            <asp:TextBox ID="ShelfTb" Text="floor-room-shelfNum" Style="color: Gray" runat="server" onfocus="noDefaultShelf()" onblur="isDefaultShelf()"></asp:TextBox>
                                            <asp:Button ID="ShelfAddBt" runat="server" Text="Add shelf" CssClass="hmf-button" OnClick="ShelfAddBt_Click"/>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="format incorrect!" ControlToValidate="ShelfTb" ValidationExpression="[A-Za-z0-9]+[-][A-Za-z0-9]+[-][A-Za-z0-9]+[ ]*" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Amount</td>
                                        <td>
                                            <asp:TextBox ID="amountTxt" runat="server" CssClass="hmf-textbox" ></asp:TextBox> 
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Be sure it is a number!" ControlToValidate="amountTxt" ValidationExpression="\d+" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>

                                </table>
                                
                                <p>
                                    <asp:Button ID="OKbt" runat="server" Text="OK" OnClick="OKbt_Click" CssClass="hmf-button"/>
                                    &nbsp;<asp:Button ID="ClearBt" runat="server" Text="Clear" OnClick="ClearBt_Click" CssClass="hmf-button" />
                                </p>
                            </div>    
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div style="height:5px"></div>
     </div>
    
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>