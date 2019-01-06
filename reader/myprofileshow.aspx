<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myprofileshow.aspx.cs" Inherits="myprofileshow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-myprofileshow</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
     <style>
            form{
                 color:#696969;  
                 width:500px;
                 margin: auto;
                 font-size:18px;
                 font-family:'Times New Roman';
                 margin-top:50px;
	         }
           .divs{
               margin:30px auto;
               align-content:center;
               height:50px;
           } 
           .textbox{
               border:solid 0px;
               background:rgba(0, 0, 0, 0);
               color:dimgrey;
               height:50px;
               margin-bottom:10px;
           }
           .textbox1{
               border:solid 0px;
               background:rgba(0, 0, 0, 0);
               color:firebrick;
               height:50px;
               margin-bottom:25px;
               margin-right:-185px;
           }
           .textbox2{
               border:solid 0px;
               background:rgba(0, 0, 0, 0);
               color:dimgrey;
               height:50px;
               margin-bottom:10px;
               margin-right:-185px;
           }
           #ChangeButton{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               margin-left:150px;
               margin-top:10px;
               color:cornflowerblue;
           }
           #Image1{
               height:150px;
               width:150px;
               margin-left:100px;
               margin-bottom:50px;
           }
           #Literal1{
               color:firebrick;
           }
           #Label1{
               color:firebrick;
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
                    <li><a href="homepage.aspx">Homepage</a></li>
                    <li><a href="booklist.aspx">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>
            </div>
           <%=htmlStr%>
        </div>
    </nav>
    <!--%htmlstr%-->
    <div class="col-sm-2" style="border-right: solid 1px">
        <div class="list-group side-bar">
                        <a href="myprofileshow.aspx" class="list-group-item active" >myprofile</a>
                        <a href="oldpasswordcheck.aspx" class="list-group-item">modify password</a>
        </div>
    </div>
     <form id="form1" runat="server">
        
        <div class="divs" id="profile">
            <div id="a">
            <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("user_picture") %>'/>
            </div>
             <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Literal1" runat="server">fine：</asp:Label><asp:Label ID="Fine" runat="server" CssClass="textbox1" Height="15px" Width="240px" Text='<%#Eval("fine") %>'></asp:Label><asp:Label ID="Label1" runat="server">yuan</asp:Label>
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;userID：<asp:Label ID="Userid" runat="server" CssClass="textbox" Height="30px" Width="240px" Text='<%#Eval("user_id") %>'></asp:Label>
            </div>
            <div>
                username：<asp:Label ID="UserName" runat="server" CssClass="textbox" Height="30px" Width="240px" Text='<%#Eval("username") %>'></asp:Label>
            </div>
            <!--<div>
                &nbsp;&nbsp;&nbsp;balance：<asp:Label ID="Balance" runat="server" CssClass="textbox2" Height="30px" Width="240px" Text='<%#Eval("balance") %>'></asp:Label><asp:Label ID="Label2" runat="server">yuan</asp:Label>
            </div>
            !-->
           <div>
               telephone：<asp:Label ID="Telephone" runat="server" CssClass="textbox" Height="30px" Width="240px" Text='<%#Eval("telephone") %>'></asp:Label>
            </div>
            <div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;email：<asp:Label ID="Email" runat="server" CssClass="textbox" Height="30px" Width="240px" Text='<%#Eval("email") %>'></asp:Label>
            </div>
            <div>
                <asp:Button ID="ChangeButton" runat="server" Text="modify" Height="30px" Width="100px" OnClick="ChangeButton_Click"></asp:Button>
            </div>
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
