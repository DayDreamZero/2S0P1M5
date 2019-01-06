<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-homepage.aspx.cs" Inherits="aspx_admin_homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="../Image/title.ico" />
    <title>BiblioSoft</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <!--link href="css/main.css" rel="stylesheet" /-->
    <script src="/js/jquery.min.js"></script>
    <script src="/js/jquery.js"></script>
    <script src="/js/bootstrap.min.js"></script>
    <style>
        body{
            
        }
        p {
            position: relative;
            top: 250px;
            padding-bottom: 5px;
            padding-top: 2px;
            padding-right: 5px;
            overflow: hidden;
            width: 100%;
            font-size: 48px;
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
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Settings
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
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Transactions
					<b class="caret"></b>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a href="admin-LibrarianGroup.aspx">LibrarianGroup</a></li>
                            <li><a href="admin-LibraResetList.aspx">LibraResetList</a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>
                        <asp:Label ID="Label1" runat="server" Text="username"></asp:Label></a>
                        <ul class='dropdown-menu'>
                            <li><a href="admin-information-show.aspx">Information Box</a></li>
                            <li><a href="admin-ChangePassword.aspx">ChangePassword</a></li>
                            <li><a href="../Admin-Logout.aspx">Log Out</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container" style="padding-left: 100px;">
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="library" runat="server" Text="Welcome" Visible="True"></asp:Literal>
        </p>
    </div>
    <footer class="navbar-fixed-bottom">
        <div style=" line-height: 23px;bottom:0px;position:static;text-align:center;">
                <p class ="p1" align="center" style="margin-top: 10px;color: #878B91;font-size:14px;padding:0;position:static;">
                    Copyright &copy;2018 A-10
                </p>
        </div>
    </footer>
</body>
</html>
