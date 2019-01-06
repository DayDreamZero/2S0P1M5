<%@ Page Language="C#" AutoEventWireup="true" CodeFile="advancedSearch.aspx.cs" Inherits="advancedSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bibliosoft-advancedSearch</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style>
        form {
            color: #696969;
            margin: auto;
            font-size: 15px;
            font-family: 'Times New Roman';
            margin-top: 150px;
        }
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
                    <li><a href="/reader/booklist.aspx">Booklist</a></li>
                    <li class="active"><a href="#">Search</a></li>
                </ul>
            </div>
            <%=htmlStr%>
        </div>
    </nav>
    <form class="pagebody" runat="server" style="margin:80px;">
        <div align="center">
            <h2>Search</h2>
            <asp:DropDownList ID="searchBy" runat="server">
                <asp:ListItem Value="0">title</asp:ListItem>
                <asp:ListItem Value="1">author</asp:ListItem>
                <asp:ListItem Value="2">ISBN</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="searchBox" Height="30px" Width="300px" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Height="30px" Text="Search" OnClick="search" />
        </div>
        <br />
        <div align="center">
            <asp:CheckBox ID="CheckBox1" runat="server" />
            <span>search by category:</span><asp:DropDownList ID="category" runat="server">
                    </asp:DropDownList>
        </div>
        <br />
        <br />
        <div class="col-sm-10">
            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <ItemTemplate>
                    <div class="row book-list">
                        <div class="book-list-item">
                            <div class="col-xs-1"></div>
                            <div class="col-xs-2">
                                <img class="book-image" src='<%#Eval("book_picture") %>' />
                            </div>
                            <div class="book-info col-xs-5">
                                <p>ISBN:<asp:Label ID="Label1" runat="server" Text='<%#Eval("ISBN") %>'></asp:Label></p>
                                <p>barcode:<asp:Label ID="Label2" runat="server" Text='<%#Eval("barcode") %>'></asp:Label></p>
                                <p>title:<asp:Label ID="bookNameLab" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                <p>author:<asp:Label ID="author" runat="server" Text='<%#Eval("author") %>'></asp:Label></p>
                                <p>publishing house:<asp:Label ID="pubHouseLab" runat="server" Text='<%#Eval("publishing_house") %>'></asp:Label></p>
                                <p>price：<asp:Label ID="priceLab" runat="server" Text='<%#Eval("price") %>'></asp:Label></p>
                                <p>   
                                    floor:<asp:Label ID="floor" runat="server" Text='<%#Eval("floor") %>'></asp:Label> &nbsp&nbsp
                                    room:<asp:Label ID="room" runat="server" Text='<%#Eval("room_id") %>'></asp:Label> &nbsp&nbsp
                                    bookshelf:<asp:Label ID="bookshelf" runat="server" Text='<%#Eval("bookshelf_id") %>'></asp:Label>&nbsp&nbsp
                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:LinkButton Text="more information" CommandArgument='<%#Eval("ISBN")%>' CommandName="MoreInfo" runat="server" />
                                </p>
                                <p>
                                    <asp:LinkButton ID="Reserve" runat="server" Text="Reserve" CommandArgument='<%#Eval("barcode")%>' CommandName="Reserve" />
                                </p>
                            </div>
                        </div>
                    </div>
                    <p>
                        <hr />
                    </p>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </form>
        <div style="right:0;bottom:0;position:fixed;text-align:center">
        <!--采用container，使得页尾内容居中 -->
        
        <p align="center" style="margin-top: 10px; color: #878B91;">
            Copyright &copy;2018 A-10 &nbsp&nbsp
        </p>

    </div>
</body>
</html>
