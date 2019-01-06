<%@ Page Language="C#" AutoEventWireup="true" CodeFile="passwordrecovery.aspx.cs" Inherits="reader_passwordrecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Bibliosoft-passwordrecovery</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link href="../css/navbar.css" rel="stylesheet" />
    <link href="../css/footer.css" rel="stylesheet" />
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
            .textbox{
                margin-top:30px;
            }
            #RecoveryPassword{
               border-radius:2px;
               border:solid 1px;
               background-color:transparent;
               margin-left:115px;
               margin-top:30px;
               color:cornflowerblue;
            }
            #yzm{
                height:25px;
                width:100px;
                margin-left:150px;
                margin-top:15px;
            }
           #Label1{
               color:brown;
               margin-top:0px;
           }
           #Label2{
               color:brown;
               margin-top:0px;
           }
    </style>

    <script type="text/javascript">
    // 检查 E-mail 是否已被注册
     function CheckEmail()
     {
         var e = document.getElementById("Email").value;
         if(e != "")
         {
             if(!/(\S)+[@]{1}(\S)+[.]{1}(\w)+/.test(e)) 
             {
             alert("请输入格式正确的E-mail 地址！");
             var email = document.getElementById ( "Email" );
             email.value = "";
             email.focus ();
             } 
         } 
       }
 
     function checkAll()
     {
         var ee = document.getElementById("user_id").value;
         if(ee == "")
         {
             alert('登录名称不能为空');
             return false; 
         }
 
         var e = document.getElementById("Email").value;
         if(e == "")
         {
             alert('Emial不能为空');
             return false; 
         }
      }
 </script>

 </head>


<body>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <a style="float: left; height: 50px; padding: 10px 10px" href="#">
                    <img src="../image/title.png" height="30px" /></a>
                <a class="navbar-brand" href="#">Bibliosoft</a>
            </div>
            <div>
                <ul class="nav navbar-nav">
                    <li><a href="#">Homepage</a></li>
                    <li><a href="/reader/booklist.aspx">Booklist</a></li>
                    <li><a href="advancedSearch.aspx">Search</a></li>
                </ul>
            </div>
            <%=htmlStr%>
        </div>
    </nav>
    <!--%htmlstr%-->
    <form id="form1" runat="server">
        <div>
            <div><asp:Label ID="GetPw" runat="server">get password</asp:Label></div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;telephone：<asp:TextBox ID="UserId" runat="server" CssClass="textbox" Height="35px"></asp:TextBox></div>
            <div><asp:Label ID="Label1" runat="server"></asp:Label></div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;email：<asp:TextBox ID="Email" runat="server" CssClass="textbox"  Height="35px"></asp:TextBox></div>
            <div><asp:Label ID="Label2" runat="server"></asp:Label></div>
           <!-- <div>security code：<asp:TextBox ID="SecurityCode" runat="server" CssClass="textbox"  Height="35px"></asp:TextBox></div>
            <div><img id="yzm" src="../ValidateCode.aspx" /></div>  !-->
            
            <div><asp:Button ID="RecoveryPassword" runat="server" Text=" password recovery " OnClick="PwRecovery_Click"/></div>
             
        </div>
        <div style="right:0;bottom:0;position:fixed;text-align:center">
        <!--采用container，使得页尾内容居中 -->
        
        <p align="center" style="margin-top: 10px; color: #878B91;">
            Copyright &copy;2018 A-10 &nbsp&nbsp
        </p>

    </div>
    </form>
</body>
</html>
