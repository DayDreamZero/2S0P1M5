<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myprofilemodify.aspx.cs" Inherits="myprofile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-myprofilechange</title>
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
                 font-size:15px;
                 font-family:'Times New Roman';
                 margin-top:50px;
	         }
           .divs{
               margin:50px auto;
               align-content:center;
               height:450px;
               margin-bottom:20px;
           } 
           .textbox{
               border:solid 1px;
               background:rgba(0, 0, 0, 0);
               color:dimgrey;
               height:70px;
               margin-bottom:20px;
               margin-top:10px;
           }
           #SubmitButton{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               margin-left:80px;
               margin-top:0px;
               color:cornflowerblue;
           }
           #DrawbackButton{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               margin-left:80px;
               margin-top:0px;
               color:cornflowerblue;
           }
           #Image1{
               height:150px;
               width:150px;
               margin-left:100px;
               margin-bottom:50px;
           }
           .div1{
               margin-bottom:5px;
           }
           #Label2{
               color:brown;
               margin-top:0px;
               margin-bottom:0px;
               margin-left:70px;
               height:5px;
           }
           #Label1{
               color:brown;
               margin-top:0px;
               margin-left:70px;
           }
           #Label3{
               color:brown;
               margin-top:0px;
               margin-left:70px;
           }
           #Email{
               margin-top:0px;
           }
           #Telephone{
               margin-bottom:0px;
               margin-top:-5px;
           }
           #UserName{
               margin-bottom:7px;
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
    <form id="form1" runat="server">
    <div class="divs">
        <div>
        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("user_picture") %>'/>
        </div>
        <div class="div1">
        Update your profile photo:
        </div>
        <div>
           <asp:FileUpload ID="FileUpload1" runat="server"  ClientMode="Static" accept="image/png,image/jpeg,image/gif" />
        </div>
        <div>
            username：<asp:TextBox ID="UserName" runat="server" CssClass="textbox" Height="30px" Width="240px"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" ErrorMessage="The username cannot be empty！" ForeColor="#FF0000" Display="Dynamic" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="UserName" Display="Dynamic" ErrorMessage="Your username may contain invalid characters!" ForeColor="#FF0000" ValidationExpression="^[A-Za-z_0-9\u4e00-\u9fa5\s]{0,}$"></asp:RegularExpressionValidator>
        <br />
        <div>
           telephone：<asp:TextBox ID="Telephone" runat="server" CssClass="textbox" Height="30px" Width="240px"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Telephone" ErrorMessage="The telephone cannot be empty！" ForeColor="#FF0000" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Telephone" Display="Dynamic" ErrorMessage="Please enter a real and valid phone number with 11 digits!" ForeColor="#FF0000" ValidationExpression="^$|^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"></asp:RegularExpressionValidator>
        <br />
        <div>
            email：&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox ID="Email" runat="server" CssClass="textbox" Height="30px" Width="240px"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Email" ErrorMessage="The email cannot be empty！" ForeColor="#FF0000" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="Please enter a real and valid email!" ForeColor="#FF0000" ValidationExpression="^[a-zA-Z0-9_\.\-]+\@[a-zA-Z0-9_\-]+(\.[a-zA-Z]+)+$"></asp:RegularExpressionValidator>
        <div>
            <asp:Button ID="SubmitButton" runat="server" Text="Confirm" Height="30px" Width="80px" OnClick="SubmitButton_Click" CausesValidation="false"></asp:Button>
            <asp:Button ID="DrawbackButton" runat="server" Text="Cancel" Height="30px" Width="80px" OnClick="DrawbackButton_Click" CausesValidation="false"></asp:Button>
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
