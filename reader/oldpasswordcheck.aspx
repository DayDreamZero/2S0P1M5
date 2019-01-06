<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oldpasswordcheck.aspx.cs" Inherits="oldpasswordcheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-oldpwdcheck</title>
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
               border:solid 1px;
               background:rgba(0, 0, 0, 0);
               color:#696969;
               height:60px;
               margin-bottom:15px;
               margin-top:3px;
           }
           #ConfirmButton{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               margin-left:0px;
               margin-top:10px;
               color:cornflowerblue;
           }
           #Image1{
               height:150px;
               width:150px;
               margin-left:100px;
               margin-bottom:50px;
           }
           #Label2{
               color:brown;
               margin-top:0px;
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
                        <a href="myprofileshow.aspx" class="list-group-item" >myprofile</a>
                        <a href="oldpasswordcheck.aspx" class="list-group-item active">modify password</a>
        </div>
    </div>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Label ID="Label1" runat="server">old password</asp:Label>
        </div>
         <div>
            <asp:TextBox ID="Oldpassword" runat="server" type="password" placeholder="please input your old password" CssClass="textbox" Height="40px" Width="350px"></asp:TextBox>
         </div>
        <div>
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Button ID="ConfirmButton" runat="server" Text="confirm" Height="30px" Width="85px" OnClick="ConfirmButton_Click"></asp:Button>
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

