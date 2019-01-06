<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraHomepage.aspx.cs" Inherits="homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BiblioSoft-homepage</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <link href="../css/footer.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style>
        body {
            margin: 0px;
            background-image: url("../image/homebg.jpg");
            background-size: 100%;
        }

        #hpbody {
            margin-top:3%;
        }

        .hp-rank {
            margin-left: 2%;
            float: left;
            width: 26%;
            height: 80%;
            background-color: rgba(255, 255, 255, 0.95);
        }

        .hp-notice {
            float: left;
            width: 68%;
            margin-left: 2%;
            height: 80%;
            background-color: rgba(255, 255, 255, 0.95);
        }

        hr {
            background-color: #d9d9d9;
            height:1px;
            width:90%;
            margin-top:5px
        }

        #search-module {
            margin-top: 10%;
            font-family: 'Times New Roman';
        }
        
    </style>
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <a style="float: left;height: 50px;padding:10px 10px" href="#"><img src="../image/title.ico" height="30px"/></a>
                <a class="navbar-brand" href="LibraHomepage.aspx">Bibliosoft</a>
            </div>
            <div>
                <ul class="nav navbar-nav">
                    <li><a href="LibraIndexLibrarian.aspx">Book management</a></li>
                    <li><a href="EReaderManage.aspx">Reader management</a></li>
                    <li><a href="LibraFineIndex.aspx">Fine handle</a></li>
                    <li><a href="LibraHistoryIncome.aspx">Library Information</a></li>
                </ul>
            </div>
            <%=htmlStr%>
        </div>
    </nav>
    <form runat="server">
        <div align="center" id="search-module"></div>
            <div style="height:2px">
            </div>
            <div style="height: 241px;" id="hpbody">
                <div class="hp-rank">
                    <div style="text-align: center; margin-top: 10px; font-size: 16px; font-weight: bold;color:#4d4d4d;">Rank</div>
                    <div style="line-height: 10px; margin-top: 12px; margin-left: 20px; margin-right: 20px;">
                        <table style="table-layout: fixed;width:100% ;height:20px; font-size: 14px; line-height: 20px;margin-left:10px">
                            <tr>
                                <!--td style="text-align: center">title</!--td>
                                <td style="text-align: center">frequency</td-->
                                <%=rank %>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">

                                <ItemTemplate>
                                    <tr>
                                        <td style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                            <%# Container.ItemIndex + 1%>.<asp:LinkButton ID="title" runat="server" Style="color: black;" CommandArgument='<%#Eval("ISBN")%>' CommandName="ISBN" title='<%#Eval("title")%>'><%#Eval("book_name") %></asp:LinkButton>
                                        </td>
                                        <td style="text-align: center; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("borrow_frequency") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
                <div class="hp-notice">
                    <div style="text-align: center; margin-top: 10px; font-size: 16px; font-weight: bold;color:#4d4d4d;">Announcement</div>
                    <hr />
                    <div>
                        <ol>
                            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                <ItemTemplate>
                                    <li><span style="float:left"><asp:LinkButton ID="titleNotice" runat="server" Text='<%#Eval("title")%>' CommandArgument='<%#Eval("notice_id")%>' CommandName="detailNotice"/></span><span style="float:right;margin-right:25px"><%#Eval("release_time")%></span></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>

            </div>
    </form>
    <!--footer>
           <p style="color:white">&nbsp&nbsp&nbsp&nbsp&nbsp&copy; A-10</p>
    </footer-->
    <div class="container-fluid foot-wrap">
        <!--采用container，使得页尾内容居中 -->
        <div class="container">
            <div class="row">
                <div class="row-content col-lg-2 col-sm-4 col-xs-6">
                    <h3>ABOUT</h3>
                    <ul>
                        <li><a href="#">Weibo</a></li>
                        <li><a href="#">Github</a></li>
                    </ul>
                </div>
                <div class="row-content col-lg-2 col-sm-4 col-xs-6">
                    <h3>JOIN</h3>
                    <ul>
                        <li><a href="#">Librarian</a></li>
                        <li><a href="#">Developer</a></li>
                    </ul>
                </div>
                <div class="row-content col-lg-2 col-sm-4 col-xs-6">
                    <h3>CONTACT</h3>
                    <ul>
                        <li><a href="#">e-mail</a></li>
                        <li><a href="#">WeChat</a></li>
                        <li><a href="#">QQ</a></li>
                        <li><a href="#">Telephone</a></li>
                    </ul>
                </div>

            </div>
            <!--/.row -->
        </div>
        <!--/.container-->
        <p align="center" style="margin-top: 10px; color: #878B91;">
            Copyright &copy;2018 A-10
        </p>

    </div>
</body>
</html>
