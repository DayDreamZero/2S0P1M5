<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-information-show.aspx.cs" Inherits="admin_admin_information_show" %>

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
    <script src="../js/layui.all.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/layui.css" rel="stylesheet" />
    <link href="../css/look.css" rel="stylesheet" />
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
                   <li><a href = "../Admin-Logout.aspx" >Log Out</a></li></ul>
            </li>
            </ul>
	</div>
	</div>         
</nav>
    <div class="container" style="top:50px;  width:80%; margin:0 auto; margin-top:80px;" >   
      <form id="from1" runat="server" onkeydown="return(event.keyCode!=13)">
        <div class="row">
                <div class="col-sm-10">
                            <div class="row librarian-list">
                                <div class="librarian-list-item">
                                    <div class="col-xs-2"></div>
                                    <div class="col-xs-4">
                                        <asp:Image ID="tmpPic" runat="server" CssClass="inputc" Height="200px" Width="200px" /><br />     
                                    </div>                                  
                                    <div class="librarian-info col-xs-5">
                                        <label>AdminName：</label><asp:Label ID="username" runat ="server" Height="20px" Width="240px"></asp:Label>
                                        <p><hr /></p>
                                        <label>Admin_ID：</label><asp:Label ID="admin_id" runat ="server" Height="20px" Width="240px"></asp:Label>
                                        <p><hr /></p>
                                        <label>Telephone：</label><asp:Label ID="tel" runat ="server" Height="20px" Width="240px"></asp:Label>
                                        <p><hr /></p>
                                        <label>Emial：</label><asp:Label ID="email" runat ="server" Height="20px" Width="240px"></asp:Label>
                                        <p><hr /></p>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="button2" Text ="Return" runat="server" Width="70px" Height="30px" OnClick ="Button2_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;"/>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="button1" Text ="Edit" runat="server" Width="70px" Height="30px" OnClick ="Button1_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;"/>
                                        
                                        
                                        
                                    </div>
                                </div>
                            </div>
                </div>
        </div>
      </form>
    </div>
    <footer class="navbar-fixed-bottom" style="bottom:10px;">
        <div style=" line-height: 23px; text-decoration:none" style="bottom:10px;">
                <p align="center" style="margin-top: 10px;color: #878B91;">
                    Copyright &copy;2018 A-10
                </p>
        </div>
    </footer>
</body>
</html>
