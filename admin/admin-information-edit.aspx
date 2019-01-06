<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-information-edit.aspx.cs" Inherits="aspx_admin_information_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                            <li><a href='../admin/admin-information-show.aspx'>Information Box</a></li>
                            <li><a href="admin-ChangePassword.aspx">ChangePassword</a></li>
                            <li><a href="../Admin-Logout.aspx">Log Out</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container" style="top: 50px; width: 80%; margin: 0 auto; margin-top: 80px;">
        <form id="from1" runat="server" onkeydown="return(event.keyCode!=13)">
            <div class="row">
                <div class="col-sm-10">
                    <div class="row librarian-list">
                        <div class="librarian-list-item">
                            <div class="col-xs-2"></div>
                            <div class="col-xs-4">
                                <asp:Image ID="tmpPic" runat="server" CssClass="inputc" Height="200px" Width="200px" /><br />
                                <asp:Label ID="lbl_pic" runat="server" class="pic_text"></asp:Label>
                                <hr />
                                <div>
                                    <asp:FileUpload ID="FileUpload" Text="SelectFile" Onclick="fileUpLoad" runat="server" />
                                    <asp:HiddenField ID="allFileSize" runat="server" Value="0" OnValueChanged="allFileSize_ValueChanged" />
                                </div>
                                <p>
                                    <br />
                                </p>
                                <asp:Button ID="btnSave" Text="SubmitPic" runat="server" CausesValidation="false" Width="80px" Height="30px" OnClick="btnSave_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;height: 35px;"/>
                            </div>

                            <div class="librarian-info col-xs-5">
                                <p>Username：</p>
                                <p>
                                    <br />
                                </p>
                                <asp:TextBox ID="username" runat="server" Height="30px" Width="240px"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="username" ErrorMessage="The username cannot be empty！" ForeColor="#FF0000" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="username" Display="Dynamic" ErrorMessage="Your username may contain invalid characters!" ForeColor="#FF0000" ValidationExpression="^[A-Za-z_0-9\u4e00-\u9fa5\s]{0,20}$"></asp:RegularExpressionValidator>
                                <p>
                                    <br />
                                </p>
                                <p>Admin_ID：<asp:Label ID="admin_id" runat="server" Text =""></asp:Label></p>
                                <p>
                                    <br />
                                </p>
                                <p>Telephone：</p>
                                <p>
                                    <br />
                                </p>
                                <asp:TextBox ID="tel" runat="server" Height="30px" Width="240px"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tel" ErrorMessage="The telephone cannot be empty！" ForeColor="#FF0000" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tel" Display="Dynamic" ErrorMessage="Please enter a real and valid phone number with 11 digits!" ForeColor="#FF0000" ValidationExpression="^$|^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"></asp:RegularExpressionValidator>
                                <p>
                                    <br />
                                </p>
                                <p>Email：</p>
                                <p>
                                    <br />
                                </p>
                                <asp:TextBox ID="email" runat="server" Height="30px" Width="240px"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="email" ErrorMessage="The email cannot be empty！" ForeColor="#FF0000" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="email" Display="Dynamic" ErrorMessage="Please enter a real and valid email!" ForeColor="#FF0000" ValidationExpression="^[a-zA-Z0-9_\.\-]+\@[a-zA-Z0-9_\-]+(\.[a-zA-Z]+)+$"></asp:RegularExpressionValidator>
                                <p>
                                    <br />
                                </p>
                                <asp:Label ID="comfirm4" runat="server" Text="" style="color:red"></asp:Label>
                                <br />
                                <asp:Button ID="button3" Text="Cancel" runat="server" CausesValidation="false" Width="70px" Height="30px" OnClick="Button3_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;"/>
                                &nbsp;&nbsp;
                                        <asp:Button ID="button1" Text="Reset" runat="server" CausesValidation="false" Width="70px" Height="30px" OnClick="Button1_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;"/>
                                &nbsp;&nbsp;
                                        <asp:Button ID="button2" Text="Submit" runat="server" Width="70px" Height="30px" OnClick="Button2_Click" style="background-color: #07938A;border-radius: 2px;color: white;border: solid;border-color: #07938A;"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <footer class="navbar-fixed-bottom" style="bottom:10px;">
            <div style=" line-height: 23px; text-decoration:none">
                    <p align="center" style="margin-top: 0;color: #878B91;">
                        Copyright &copy;2018 A-10
                    </p>
            </div>
        </footer>
</body>
</html>
