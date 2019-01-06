<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraAnnouncement.aspx.cs" Inherits="Librarian_aspx_LibraAnnouncement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_LibraryAnnouncement</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <style>
        .showTable,.showTable tr td{
            border:1px solid #808080; 
        }
        .auto-style1 {
            height: 39px;
        }
        .auto-style2 {
            overflow: hidden;
            height: 39px;
        }
        .auto-style3 {
            width: 200px;
            height: 39px;
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
                    <div class="row" style=" height: auto;">
                        <div class="col-sm-9">
                            
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Button2" runat="server" Text="release new announcement" OnClick="newAnn" CssClass="hmf-button" />
                        </div>
                        
                    </div>
                    <hr />
                    <table style="table-layout:fixed;text-align:center;width:100%; border-collapse:separate; border-spacing:0px 10px;">
                        <tr style="background-color:#F2F2F2; font-size:medium;height:40px">
                            <td style="width:30px;text-align:center">No.</td>
                            <td style="width:100px;text-align:center">title</td>
                            <td style="width:200px;text-align:center">content</td>
                            <td style="width:60px;text-align:center">publicist</td>
                            <td style="width:150px;text-align:center">ReleaseTime</td>
                            <td style="width:80px;text-align:center">modifier</td>
                            <td style="width:150px;text-align:center">modifyTime</td>
                            <td style="width:200px"></td>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex + 1%>
                                    </td>
                                    <td style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("title") %>'></asp:Label>
                                    </td>
                                    <td style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("content") %>'></asp:Label>
                                    </td>
                                    <td style="text-align:center;overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("publicist") %>'></asp:Label>
                                    </td>
                                    <td style="text-align:center;overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("release_time") %>'></asp:Label>
                                    </td>
                                    <td style="text-align:center;overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("modifier") %>'></asp:Label>
                                    </td>
                                    <td style="text-align:center;overflow: hidden; text-overflow: ellipsis; white-space: nowrap; word-break: keep-all;">
                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("last_modify_time") %>'></asp:Label>
                                    </td>
                                    <td style="width:200px">
                                        <asp:Button ID="detail" style="width:65px" CssClass="hmf-button" runat="server" Text="Detail" CommandArgument='<%#Eval("notice_id")%>' OnClick="btnDetail_Click" />
                                        <asp:Button ID="update" style="width:55px" CssClass="hmf-button" runat="server" Text="Edit" CommandArgument='<%#Eval("notice_id")%>' OnClick="btnEdit_Click" />
                                        <asp:Button ID="Button1" style="width:65px" CssClass="hmf-button" runat="server" Text="Delete" CommandArgument='<%#Eval("notice_id")%>' OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </form>
            </div>
        </div>
    </div>

     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
