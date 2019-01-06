<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bookrecordReturn.aspx.cs" Inherits="reader_bookrecordReturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Bibliosoft-returnrecord</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
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
                    <li><a href="homepage.aspx">Homepage</a></li>
                    <li><a href="booklist.aspx">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>

            </div>
            <%=htmlStr%>
        </div>
    </nav>

    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="border-right: solid 1px">
                <div class="list-group side-bar">
                    <a href="bookrecordReserve.aspx" class="list-group-item">Reserve</a>
                    <a href="bookrecordBorrow.aspx" class="list-group-item">Borrow</a>
                    <a href="bookrecordReturn.aspx" class="list-group-item active">Return</a>
                </div>
            </div>
            <form id="from1" runat="server">
                <div class="col-sm-10">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="row book-list">
                                <div class="book-list-item">
                                    <div class="col-xs-1"></div>
                                    <div class="col-xs-2">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("book_picture") %>' />
                                    </div>
                                    <div class="book-info col-xs-5">
                                        <p>book name：<asp:Label ID="Label1" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                        <p>barcode：<asp:Label ID="barcodeLab" runat="server" Text='<%#Eval("barcode") %>'></asp:Label></p>
                                        <p>borrow date:<asp:Label ID="borrowDateLab" runat="server" Text='<%#Eval("borrowed_date") %>'></asp:Label></p>
                                        <p>return date:<asp:Label ID="returnDateLab" runat="server" Text='<%#Eval("return_date") %>'></asp:Label></p>
                                        <p>fine:<asp:Label ID='fine' runat='server' Text='<%#Eval("fine") %>'></asp:Label>&nbsp &nbsp pay status:<asp:Label ID='pay' runat='server' Text='<%#Eval("fine_status") %>'></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <hr />
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <div class="row book-list">
                                <div class="book-list-item">
                                    <div class="col-xs-1"></div>
                                    <div class="col-xs-2">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("book_picture") %>' />
                                    </div>
                                    <div class="book-info col-xs-5">
                                        <p>book name：<asp:Label ID="Label1" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                        <p>barcode：<asp:Label ID="barcodeLab" runat="server" Text='<%#Eval("barcode") %>'></asp:Label></p>
                                        <p>borrow date:<asp:Label ID="borrowDateLab" runat="server" Text='<%#Eval("borrowed_date") %>'></asp:Label></p>
                                        <p>return date:<asp:Label ID="returnDateLab" runat="server" Text='<%#Eval("return_date") %>'></asp:Label></p>
                                    </div>
                                </div>
                            </div>
                            <p>
                                <hr />
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="row book-list">
                                <div class="book-list-item">
                                    <div class="col-xs-1"></div>
                                    <div class="col-xs-2">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("book_picture") %>' />
                                    </div>
                                    <div class="book-info col-xs-5">
                                        <p>book name：<asp:Label ID="Label1" runat="server" Text='<%#Eval("book_name") %>'></asp:Label></p>
                                        <p>barcode：<asp:Label ID="barcodeLab" runat="server" Text='<%#Eval("barcode") %>'></asp:Label></p>
                                        <p>borrow date:<asp:Label ID="borrowDateLab" runat="server" Text='<%#Eval("borrowed_date") %>'></asp:Label></p>
                                        <p>return date:<asp:Label ID="returnDateLab" runat="server" Text='<%#Eval("return_date") %>'></asp:Label></p>
                                        <p>fine:<asp:Label ID='fine' runat='server' Text='<%#Eval("fine") %>'></asp:Label>&nbsp &nbsp pay status:<asp:Label ID='pay' runat='server' Text='<%#Eval("fine_status") %>'></asp:Label></p>
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
        </div>
    </div>
    <div style="right:0;bottom:0;position:fixed;text-align:center">
        <!--采用container，使得页尾内容居中 -->
        
        <p align="center" style="margin-top: 10px; color: #878B91;">
            Copyright &copy;2018 A-10 &nbsp&nbsp
        </p>

    </div>
</body>
</html>
