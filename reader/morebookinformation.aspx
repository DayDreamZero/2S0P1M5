<%@ Page Language="C#" AutoEventWireup="true" CodeFile="morebookinformation.aspx.cs" Inherits="morebookinformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-moreinfo</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style>
        body{
            margin:0px;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <a style="float: left;height: 50px;padding:10px 10px" href="homepage.aspx"><img src="../image/title.png" height="30px"/></a>
                <a class="navbar-brand" href="homepage.aspx">Bibliosoft</a>
            </div>
            <div>
                <ul class="nav navbar-nav ">
                    <li><a href="/reader/homepage.aspx">Homepage</a></li>
                    <li class="active"><a href="booklist.aspx">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>

            </div>
            <%=htmlStr%>
        </div>
    </nav> 
    <div class="container" style="margin:80px;">
        <div class="row">
            
            <div class="col-sm-10">
                <form id="form1" runat="server">

                    <asp:FormView ID="FormView1" style="width:100%;font-size:14px" runat="server">
                        <ItemTemplate>

                        <div class="row book-list">
                            <div class="book-list-item">
                                <div class="col-xs-4">
                                    <img class="book-image" src='<%#Eval("book_picture") %>' />
                                </div>
                                <div class="book-info col-xs-8" style="background-color:antiquewhite">
                                    <p>Book name：<asp:Label ID="bookNameLab" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                    <p>Book ISBN：<asp:Label ID="Label1" runat="server" Text='<%#Eval("ISBN") %>'></asp:Label></p>
                                    <p>Publishing house：<asp:Label ID="pubHouseLab" runat="server" Text='<%#Eval("publishing_house") %>'></asp:Label></p>
                                    <p>Publishing date：<asp:Label ID="dateLab" runat="server" Text='<%#Eval("date") %>'></asp:Label></p>
                                    <p>Author：<asp:Label ID="Label4" runat="server" Text='<%#Eval("author") %>'></asp:Label></p>
                                    <p>Description：<asp:Label ID="Label2" runat="server" Text='<%#Eval("description") %>'></asp:Label></p>
                                    <p>Price：<asp:Label ID="priceLab" runat="server" Text='<%#Eval("price") %>'></asp:Label></p>
                                    <p>Type：<asp:Label ID="TypeLab" runat="server" Text='<%#Eval("type_id") %>'></asp:Label></p>
                                    <p>Position：<asp:Label ID="PosiLab" CommandName="Position" CommandArgument='<%#Eval("bookshelf_id") %>' runat="server" 
                                                        Text='<%#"On the bookshelf "+Eval("bookshelf_id").ToString()+" of room "+Eval("room_id").ToString()+" at the floor "+Eval("floor").ToString() %>'></asp:Label></p>
                                    <p>Borrow frequency：<asp:Label ID="Label3" runat="server" Text='<%#Eval("borrow_frequency") %>'></asp:Label></p>
                                
                                    <p>
                                        <span>Remaining:<asp:Label ID="inventoryLab" runat="server" Text='<%#Eval("inventory") %>'></asp:Label></span>
                                        <span>Lend out:<asp:Label ID="OutNum" runat="server" Text='<%#Eval("amount_inven") %>'></asp:Label></span>
                                        <span>Amount:<asp:Label ID="amountLab" runat="server" Text='<%#Eval("amount") %>'></asp:Label></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </p>
                                </div>    
                            </div>
                        </div>
                       </ItemTemplate> 
                   </asp:FormView>

                </form>
            </div>
        </div>
    </div>
    <div >
        <p align="center" style="padding-top: 0px;color: #878B91;">
            Copyright &copy;2018 A-10
        </p>
    </div>
</body>
</html>
