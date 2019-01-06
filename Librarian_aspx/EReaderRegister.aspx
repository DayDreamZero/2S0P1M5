<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EReaderRegister.aspx.cs" Inherits="EReaderRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_ReaderRegister</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../Login.aspx"><asp:Label ID="Label2" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

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
                <div class="list-group side-bar">
                    <a href="EReaderManage.aspx" class="list-group-item">Manage Reader</a>
                    <a href="EReaderRegister.aspx" class="list-group-item  active">Register Reader</a>
					<a href="EReaderHistory.aspx" class="list-group-item">Reader History</a>
                </div> 
            </div>
            <div class="col-sm-10">
                    <form id="form1" runat="server">
    <div>
        <table  border="0" style="font-size: 10pt; width: 100%;text-align:center; padding: 0;border-collapse: separate; border-spacing: 10px;" class="auto-style4">
            <tr>
                <td class="auto-style2">
                    <table style="font-size: 10pt; width: 100%;text-align:center; padding: 0;border-collapse: separate; border-spacing: 10px;">
                       	<tr>
                            <td style="width: 40px; text-align:center;">
                            </td>
                            <td style="width: 100px; text-align:center;">
                                UserType：</td>
                            <td style="width: 359px;text-align:left; ">
                                <asp:TextBox ID="userType" runat="server" Enabled="False">reader</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align:center;">   
                            </td>
                            <td style="width: 40px; text-align:center;">
                                Name：</td>
                            <td style="width: 359px; text-align:left;">
                                <asp:TextBox ID="txtuserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserName"
                                    ErrorMessage="Please enter user name！" ForeColor="#CC0000"></asp:RequiredFieldValidator></td>
                        </tr>
						<tr>
                            <td style="width: 40px; text-align:center;">
                            </td>
                            <td style="width: 100px; text-align:center;">
                                Password：</td>
                            <td style="width: 359px;text-align:left; ">
                                <asp:TextBox ID="TextBox1" runat="server" Enabled="False">12345678</asp:TextBox></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 19px; height: 19px; text-align:center;">
                            </td>
                            <td style="width: 40px; text-align:center;">
                                Telephone：
                            </td>
                            <td style="width: 359px; text-align:left;">
                                <asp:TextBox ID="txtPhone" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhone"
                                    ErrorMessage="The telephone cannot be empty！" ForeColor="#CC0000" Display="Dynamic"></asp:RequiredFieldValidator>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" ForeColor="#CC0000" ErrorMessage="The telephone format is wrong" ValidationExpression= "^$|^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
							</tr>
						<tr>
                            <td style="width: 19px; height: 19px ;text-align:center;">
                            </td>
                            <td style="width: 40px; text-align:center;">
                                Email：</td>
                            <td style="width: 359px; text-align:left;">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="The email cannot be empty！" ForeColor="#CC0000" Display="Dynamic"></asp:RequiredFieldValidator><font color="red"></font>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="txtEmail" ForeColor="#CC0000" Display="Dynamic" ErrorMessage="The email format is wrong" ValidationExpression="^$|^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
						<tr>
                            <td style="width: 40px; height: 19px; text-align:center;">
                            </td>
							<td style="width: 100px; height: 19px; text-align:center;">
                            </td>
                            <td colspan="3" style=" text-align:center; width: 40px;">
                                <asp:FileUpload ID="fulPhoto" runat="server"  /></td>								
                        </tr>
                        <tr>
                            <td style="width: 19px; height: 82px; text-align:center;">
                            </td>
                            <td style="width: 40px; text-align:center;">
                                Picture：</td>
                            <td style="height: 82px; text-align:left;">
                                <asp:ImageMap ID="imgGoodsPhoto" runat="server" Height="103px" Width="90px">
                                </asp:ImageMap>
								 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Please choose user picture！" ForeColor="#CC0000"></asp:RequiredFieldValidator><font color="red"></font></td>
                        </tr>
						<tr>
							<td style="width: 19px; height: 19px; text-align:center;">
                            </td>
                            <td style="width: 40px; text-align:center;">
                                </td>
							<td style="text-align:left;"> <asp:Button ID="btnShow" runat="server" CausesValidation="False" Height="23px" OnClick="btnShow_Click" Text="submit the picture" CssClass="hmf-button" /></td>

						</tr>
                        <tr>
                            <td style="width: 19px; height: 35px; text-align:center;">
                            </td>
							
                            <td style="width: 40px; text-align:center;">
                              <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Register" CssClass="hmf-button" /></td>
                              <td style="text-align:left;">  <asp:Button ID="btnBack" runat="server" CausesValidation="False" OnClick="btnBack_Click" Text ="Back" CssClass="hmf-button" /></td>
								
                        </tr>
                   </table>
                </td>
            </tr>
        </table>
    
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
