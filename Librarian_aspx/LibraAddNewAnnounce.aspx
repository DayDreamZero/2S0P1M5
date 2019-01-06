<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraAddNewAnnounce.aspx.cs" Inherits="Librarian_aspx_LibraAddNewAnnounce" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_LibraryNewAnnouncement</title>
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
                    <h2>Release new announcement</h2>
                    <div>
                        <asp:Label ID="title" runat="server" Text="Title" Style="font-size: 16px"></asp:Label>
                        <asp:TextBox ID="title_content" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="title_content"
                            ErrorMessage="Title cannot be null" ForeColor="#cc0000"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="titleLength" runat="server"
                            ErrorMessage="The length of string you input should be less than 50"
                            ValidationExpression=".{0,50}"
                            ControlToValidate="title_content"  ForeColor="#cc0000">
                        </asp:RegularExpressionValidator>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="content" runat="server" Text="Content" Style="font-size: 16px"></asp:Label>&nbsp&nbsp&nbsp
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="contentbox"
                            ErrorMessage="Content can not be null" ForeColor="#cc0000"></asp:RequiredFieldValidator>
                        <br />
                        <asp:TextBox ID="contentbox" TextMode="MultiLine" runat="server" Height="200px" Width="800px"></asp:TextBox>
                    </div>

                    <br />
                    <asp:Button ID="release_btn" runat="server" Text="Release" OnClick="release_btn_Click" CssClass="hmf-button" />
                    &nbsp;<input type="button" onclick="Javascript: window.history.go(-1);" value="Comeback" class="hmf-button" />
                </form>
            </div>
        </div>
    </div>
     </div>
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>
</body>
</html>
