<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailAnnounce.aspx.cs" Inherits="reader_DetailAnnounce" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-announcement</title>
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
                    <li><a href="/reader/homepage.aspx">Homepage</a></li>
                    <li><a href="booklist.aspx">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>

            </div>
            <%=htmlStr%>
        </div>
    </nav> 
    <div class="container">
        <div class="row">
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <asp:FormView ID="FormView1" style="width:100%" runat="server">
                        <ItemTemplate>

                        <div class="row">
                           
                                <div class="col-xs-8" style="background-color:antiquewhite">
                                    <h2><%#Eval("title") %></h2>
                                    <div style="width:600px">
                                            <p>Publicist:<asp:Label ID="publicist" runat="server" Text='<%#Eval("publicist") %>'></asp:Label></p>
                                            <p>Release time:<asp:Label ID="releaseLab" runat="server" Text='<%#Eval("release_time") %>'></asp:Label></p>
                                            <p>Modifier:<asp:Label ID="modifier" runat="server" Text='<%#Eval("modifier") %>'></asp:Label></p>
                                            <p>Last modify time:<asp:Label ID="modifyLab" runat="server" Text='<%#Eval("last_modify_time") %>'></asp:Label></p>
                                            <p style="word-wrap:break-word">Content:<asp:Label ID="content" runat="server" Text='<%#Eval("content") %>'></asp:Label></p>
                                    </div>
                                    <asp:Button ID="ComebackBt" runat="server" Text="Comeback" OnClick="ComebackBt_Click" />
                                   
                                </div>    
                            
                        </div>
                       </ItemTemplate> 
                   </asp:FormView>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
