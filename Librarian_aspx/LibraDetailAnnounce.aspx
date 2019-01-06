<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraDetailAnnounce.aspx.cs" Inherits="Librarian_aspx_LibraDetailAnnounce" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_DetailAnnouncement</title>
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

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../Login.aspx">
                <asp:Label ID="Label2" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

            <ul class="nav navbar-nav navbar-right">

                

                <li class='dropdown'>
                    <a href='#' class='dropdown-toggle' data-toggle='dropdown'>
                        <asp:Label ID="LibraNameLab" runat="server" Text="LibraNameLab" ForeColor="#FFFFCC" Font-Size="X-Small"></asp:Label><b class="caret"></b></a>
                    <ul class='dropdown-menu'>
                        <li><a href="LibraperCenterLibra.aspx">personal Information</a></li>
                        <li><a href="LibraUpdatePassword.aspx">update password</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="border-right: solid 1px">
                <div class="list-group side-bar">
                    <ul class="list-group-item" style="list-style-type:none">             
						<li class='dropdown'>
						<a href = 'LibraHistoryIncome.aspx' class="dropdown-toggle" data-toggle="dropdown" style="text-decoration:none;color:dimgray">History income</a>
						<ul class='dropdown-menu'>
							<li><a href="LibraHistoryIncome.aspx"> Total Income</a></li>
							<li><a href="LibraHistoryIncome_Deposit.aspx">ToTal Deposit</a></li>
							<li><a href="LibraHistoryIncome_Fine.aspx" >Total Fine</a></li>
						</ul>
						</li>                
					</ul>
                    <a href="LibraAnnouncement.aspx" class="list-group-item active">Announcement</a>
                </div>
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-9">
                            <asp:FormView ID="FormView1" style="width:100%" runat="server">
                                <ItemTemplate>                           
                                
                                    <h3><%#Eval("title") %></h3>
                                    <table style="border-collapse:separate; border-spacing:0px 10px;">
                                        <tr>
                                            <td>Publicist:</td>
                                            <td><asp:Label ID="publicist" runat="server" Text='<%#Eval("publicist") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td>Release time:</td>
                                            <td><asp:Label ID="releaseLab" runat="server" Text='<%#Eval("release_time") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td>Modifier:</td>
                                            <td><asp:Label ID="modifier" runat="server" Text='<%#Eval("modifier") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td >Modify time:</td>
                                            <td><asp:Label ID="modifyLab" runat="server" Text='<%#Eval("last_modify_time") %>'></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td style="height:150px;vertical-align:top">Content:</td>
                                            <td>
                                                <asp:TextBox ID="content" runat="server" Text='<%#Eval("content") %>' Height="150px" ReadOnly="True" TextMode="MultiLine" Width="600px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    
                               </ItemTemplate> 
                           </asp:FormView>
                            <input type="button" onclick="Javascript:window.history.go(-1);"value="Comeback" class="hmf-button" />
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
