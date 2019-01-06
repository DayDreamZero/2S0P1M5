<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraUpdatePassword.aspx.cs" Inherits="Librarian_aspx_LibraUpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_UpdateLibrarianPwd</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <style type="text/css">
        .auto-style1 {
            height: 27px;
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
                <li><a href="LibraIndexLibrarian.aspx">Book management</a></li>
                <li><a href="EReaderManage.aspx">Reader management</a></li>
                <li><a href="LibraFineIndex.aspx">Fine handle</a></li>
                <li><a href="LibraHistoryIncome.aspx">Library Information</a></li>

            </ul>

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../Login.aspx"><asp:Label ID="Label1" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

            <ul class="nav navbar-nav navbar-right">
                <li class='dropdown'>
                    <a href = '#' class='dropdown-toggle' data-toggle='dropdown'><asp:Label ID="LibraNameLab" runat="server" Text="LibraNameLab" ForeColor="#FFFFCC" Font-Size="X-Small" ></asp:Label><b class="caret"></b></a>
                    <ul class='dropdown-menu'>
                        <li><a href="LibraperCenterLibra.aspx"> personal Information</a></li>
                        <li><a href = "LibraUpdatePassword.aspx" >update password</a></li>
                    </ul>
                </li>
			</ul>
        </div>
    </div> 
    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="border-right:solid 1px">
                
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div class="row form-control" style="border:none">
                        <div class="col-xs-2"></div>
                        <div class="col-xs-8">
                            <table style="border-collapse:separate; border-spacing:0px 10px;">
                                <tr>
                                    <td><asp:Label ID="oldPwd" runat="server" Text="Old password:"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="oldPwdTxt" runat="server" TextMode="Password" CssClass="hmf-textbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label2" runat="server" Text="New password:"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="newPwdTxt" runat="server" TextMode="Password" CssClass="hmf-textbox"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Can input A-Z ,a-z,or 0-9,limit 8-12 characters" ControlToValidate="newPwdTxt" ValidationExpression="^[0-9a-zA-Z]{8,12}$" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="Label3" runat="server" Text=" Enter again:"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="newPwdAgainTxt" runat="server" TextMode="Password" CssClass="hmf-textbox"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Not the same as the privious one." ControlToCompare="newPwdTxt" ControlToValidate="newPwdAgainTxt" ForeColor="Red"></asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="OkBt" runat="server" Text="OK" OnClick="OkBt_Click" CssClass="hmf-button" />
                        </div>
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