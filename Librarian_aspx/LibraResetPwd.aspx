<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraResetPwd.aspx.cs" Inherits="Librarian_aspx_LibraForgetPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_LibraResetPwd</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
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
            #GetPw{
                font-size:30px;
                margin-right:20px;
                margin-bottom:100px;
            }
            #ResetPassword{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               
               color:cornflowerblue;
            }
            
    </style>

    

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
                <li><a href="#">Book management</a></li>
                <li><a href="#">Reader management</a></li>
                <li><a href="#">Fine handle</a></li>
                <li><a href="#">Library Information</a></li>

            </ul>           
        </div>
    </div> 
    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="border-right:solid 1px">
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                           
                    <div >
                        <asp:Label ID="GetPw" runat="server">Librarian recovery password</asp:Label>
                        <div style="height:10px;"></div>
                        <table style="border-collapse:separate; border-spacing:0px 10px;">
                            
                            <tr>
                               <td> <asp:Label ID="Label3" runat="server" Text="Librarian ID：" Height="35px"></asp:Label></td>
                                <td><asp:TextBox ID="LibraIdTxt" runat="server" Height="35px"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td><asp:Label ID="Label2" runat="server" Text="Email：" Height="35px"></asp:Label></td>
                                <td><asp:TextBox ID="EmailTxt" runat="server" Height="35px"></asp:TextBox></td>
                            </tr>
                           
                        </table>
                        <div style="height:10px;"></div>
                        <asp:Button ID="ResetPassword" runat="server" Text="Contact admin for recovery password" OnClick="ResetPassword_Click"/>
                    </div>

                </form>
            </div>
        </div>
    </div>

     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
