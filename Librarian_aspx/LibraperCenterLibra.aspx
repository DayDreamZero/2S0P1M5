<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraPerCenterLibra.aspx.cs" Inherits="Librarian_aspx_perCenterLibra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_PersonCenter</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <style type="text/css">
        .auto-style6 {
            width: 114px;
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

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../login.aspx"><asp:Label ID="Label2" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

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
                <form id="form1" runat="server" onkeydown="return(event.keyCode!=13)">
                    <asp:FormView ID="FormView1" style="width:100%" runat="server">
                        <ItemTemplate>

                        <div class="row book-list">
                           
                                <div class="col-xs-3" style="height:160px; top: 0px; left: 1px;">
                                    <asp:Image ID="LibraImage" style="height:155px" ImageUrl='<%#"../admin/"+Eval("picture") %>' runat="server" />
                                    <asp:FileUpload ID="fulPhoto" runat="server" Width="100%" />
                                    <asp:Button ID="btnShow" runat="server" CausesValidation="False" CssClass="hmf-button" Height="23px" OnClick="btnShow_Click" Text="submit the picture" />
                                </div>
                                <div class="book-info col-xs-8">
                                    <div style="background-color:#F2F2F2;height:160px;width:100%">
                                        <table style="border-collapse:separate; border-spacing:0px 10px;">
                                            <tr>
                                                <td class="auto-style6">ID:</td>
                                                <td><asp:Label ID="IDLab" runat="server" Text='<%#Eval("librarian_id") %>'></asp:Label></td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style6">Name:</td>
                                                <td><asp:TextBox ID="NameTxt" runat="server" Text='<%#Eval("librarian_name") %>' CssClass="hmf-textbox"></asp:TextBox></td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style6">Telephone:</td>
                                                <td><asp:TextBox ID="TeleTxt" runat="server" Text='<%#Eval("telephone") %>' CssClass="hmf-textbox"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TeleTxt" Display="Dynamic" ErrorMessage="The telephone format is wrong" ForeColor="#CC0000" ValidationExpression="^$|^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style6">Email:</td>
                                                <td><asp:TextBox ID="EmailTxt" runat="server" Text='<%#Eval("email") %>' CssClass="hmf-textbox"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="EmailTxt" Display="Dynamic" ErrorMessage="The email format is wrong" ForeColor="#CC0000" ValidationExpression="^$|^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>


                                        </table>
                                                                     
                                    </div>
                                    <div>    
                                        <asp:Button ID="EditBt" runat="server" Text="Edit" CssClass="hmf-button" OnClick="EditBt_Click" />
                                        &nbsp;<asp:Button ID="CancelBt" runat="server" Text="Cancel" CausesValidation="false" CssClass="hmf-button" OnClick="CancelBt_Click" />  
                                    </div>
                                </div>
                        </div>
                       </ItemTemplate> 
                   </asp:FormView>
                </form>
            </div>
        </div>
    </div>

     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
