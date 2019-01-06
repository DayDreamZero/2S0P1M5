<%@ Page Language="C#" AutoEventWireup="true" CodeFile="booklist.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bibliosoft-booklist</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <link href="../css/footer.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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
                    <li class="active"><a href="#">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>

            </div>
            <%=htmlStr%>
        </div>
    </nav>

    <div class="container">
        <div class="row">
            <form id="from1" runat="server">
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
                                        <p>title:<asp:Label ID="bookNameLab" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                        <p>author:<asp:Label ID="author" runat="server" Text='<%#Eval("author") %>'></asp:Label></p>
                                        <p>publishing house:<asp:Label ID="pubHouseLab" runat="server" Text='<%#Eval("publishing_house") %>'></asp:Label></p>
                                        <p>price:<asp:Label ID="priceLab" runat="server" Text='<%#Eval("price") %>'></asp:Label></p>
                                        <p>
                                            <span>inventory:<asp:Label ID="inventoryLab" runat="server" Text='<%#Eval("inventory") %>'></asp:Label></span>
                                            <span>borrowed:<asp:Label ID="OutNum" runat="server" Text='<%#Eval("amount_inven") %>'></asp:Label></span>
                                            <span>total:<asp:Label ID="amountLab" runat="server" Text='<%#Eval("amount") %>'></asp:Label></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:LinkButton Text="more information" CommandArgument='<%#Eval("ISBN")%>' CommandName="MoreInfo" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <hr />
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="col-sm-6" style="left: 40%">

                        <asp:Button ID="FirstPage" runat="server" Text="First" OnClick="FirstPage_Click" />
                        <asp:Button ID="BtnUp" runat="server" OnClick="BtnUp_Click" Text="previous" />
                        Current page：<asp:Label ID="curPageNum" Text="1" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            Total：<asp:Label ID="PageNum" Text="unknown" runat="server"></asp:Label>pages
                            <asp:Button ID="BtnDown" runat="server" OnClick="BtnDown_Click" Text="next" />
                        <asp:Button ID="LastPage" runat="server" Text="Last" OnClick="LastPage_Click" />
                    </div>
                </div>
            </form>
        </div>
    </div>
    <div >
        <p align="center" style="margin-top: 70px;color: #878B91;">
            Copyright &copy;2018 A-10
        </p>
    </div>
</body>
</html>
