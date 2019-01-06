<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-SetPeriod.aspx.cs" Inherits="admin_admin_SetPeriod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>    
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="../Image/title.ico" />
    <title>BiblioSoft</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style>
        body{
            padding-top:0;
        }
        p{
            position:static;
            top:80px;
            bottom:50px;
            left:10%;
            right:10%;
            padding:2px 5px;
            overflow:hidden;
            width:100%;
            font-size:24px;
        }
        .textbox{
               border:solid 1px;
               background:rgba(0, 0, 0, 0);
               color:dimgrey;
               height:150px;
               margin-bottom:10px;
               margin-top:10px;
           }
        #Button1{
               border-radius:2px;
               border:solid 1px;
               margin-left:40px;
               margin-top:10px;
               background-color: #07938A;
               border-radius: 2px;
               color: white;
               border: solid;
               border-color: #07938A;
           }
         #Button4{
               border-radius:2px;
               border:solid 1px;
               margin-left:40px;
               margin-top:10px;
               background-color: #07938A;
               border-radius: 2px;
               color: white;
               border: solid;
               border-color: #07938A;
           }
         #Button2{
               border-radius:2px;
               border:solid 1px;
               margin-left:40px;
               margin-top:10px;
               background-color: #07938A;
               border-radius: 2px;
               color: white;
               border: solid;
               border-color: #07938A;
           }
         #Button6{
               border-radius:2px;
               border:solid 1px;
               margin-left:40px;
               margin-top:10px;
               background-color: #07938A;
               border-radius: 2px;
               color: white;
               border: solid;
               border-color: #07938A;
           }
    </style>
</head>
<body>
     <nav class="navbar navbar-default navbar-inverse navbar-fixed-top" role="navigation">
	<div class="container-fluid">
	<div class="navbar-header">
        <a style="float: left;height: 50px;padding:10px 10px" href="#"><img src="../image/title.png" height="30px"/></a>
		<a class="navbar-brand" href="#">BiblioSoft</a>
	</div>
	<div>
		<ul class="nav navbar-nav">
			<li><a class="active" href="admin-homepage.aspx">Homepage</a></li>
            <li><a class="active" href="admin-ShowAllSettings.aspx">AllSettings</a></li>
			<li class="dropdown">
				<a href="#" class="dropdown-toggle" data-toggle="dropdown">
					Settings
					<b class="caret"></b>
				</a>
				<ul class="dropdown-menu">
					<li><a href="../admin/admin-Setlendnum.aspx">SetMaxBorrow</a></li>
					<li><a href="../admin/admin-SetPenalty.aspx">SetPenalty</a></li>
					<li><a href="../admin/admin-SetDeposit.aspx">SetDeposit</a></li>
                    <li><a href="admin-SetPeriod.aspx">SetPeriod</a></li>
                    <li><a href="admin-SetLibDefaultPwd.aspx">SetDefaultPwdForLib</a></li>
                    <li><a href="admin-SetSendPeriod.aspx">SetSendInterval</a></li>
				</ul>
			</li>
			<li class="dropdown">
				<a href="#" class="dropdown-toggle" data-toggle="dropdown">
					Transactions
					<b class="caret"></b>
				</a>
				<ul class="dropdown-menu">
                    <li><a href="admin-LibrarianGroup.aspx">LibrarianGroup</a></li>
                    <li><a href="admin-LibraResetList.aspx">LibraResetList</a></li>
				</ul>
			</li>
		</ul>
        <ul class="nav navbar-nav navbar-right">
            <li class='dropdown'><a href = '#' class='dropdown-toggle' data-toggle='dropdown'>  <asp:Label ID="Label1" runat="server" Text="username"></asp:Label></a>
               <ul class='dropdown-menu'>
                   <li><a href = '../admin/admin-information-show.aspx' >Information Box</a></li>
                   <li><a href="admin-ChangePassword.aspx">ChangePassword</a></li>
                   <li><a href="../Admin-Logout.aspx">Log Out</a></li></ul>
            </li>
            </ul>
       
	</div>
	</div>
</nav> 
    <form id ="form1" runat="server" onkeydown="return(event.keyCode!=13)">
    <div class = "container">
        <div class ="container">
        <p>
            <asp:Literal ID="literal1" runat ="server" Text ="  " Visible="True"></asp:Literal>
            <br />
            <asp:Literal ID="literal2" runat ="server" Text ="You" Visible="True"></asp:Literal> 
            <br />
            <asp:Literal ID="literal3" runat ="server" Text ="You" Visible="True"></asp:Literal>  
        </p>
           
       </div>
        <div class ="container" align ="center">
            <asp:Label ID="confirm1" runat="server" Text="" style="color:red"></asp:Label>
            <br />
            <asp:TextBox ID ="textbox1" runat="server" Visible ="true"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="submit" Width="100px" Height="30px" OnClick="Button1_Click" />  
            <asp:Button ID="Button4" runat="server" Text="Cancel" Width="100px" Height="30px" OnClick="Button4_Click" />  
        </div>
        <hr />
    </div>
    
    <div class = "container">
        <div class ="container">
        <p>
            <asp:Literal ID="literal4" runat ="server" Text ="  " Visible="True"></asp:Literal>
            <br />
            <asp:Literal ID="literal5" runat ="server" Text ="You" Visible="True"></asp:Literal> 
            <br />
            <asp:Literal ID="literal6" runat ="server" Text ="You" Visible="True"></asp:Literal>  
        </p>
           
       </div>
        <div class ="container" align ="center">
            <asp:Label ID="confirm2" runat="server" Text="" style="color:red"></asp:Label>
            <br />
            <asp:TextBox ID ="textbox2" runat="server" Visible ="true"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="submit" Width="100px" Height="30px" OnClick="Button2_Click" /> 
            <asp:Button ID="Button6" runat="server" Text="Cancel" Width="100px" Height="30px" OnClick="Button6_Click" /> 
        </div>
    </div>
           
    </form>
    <footer class="navbar-fixed-bottom">
        <div style=" line-height: 23px;bottom:0;position:static;text-align:center;">
                <p class ="p1" align="center" style="margin-top: 10px;color: #878B91;font-size:14px;padding:0;position:static;">
                    Copyright &copy;2018 A-10
                </p>
        </div>
    </footer>
</body>
</html>