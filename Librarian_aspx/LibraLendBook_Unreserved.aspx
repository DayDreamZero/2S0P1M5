<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraLendBook_Unreserved.aspx.cs" Inherits="Unreserved" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_LendBook</title>
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
                    <a href="LibraIndexLibrarian.aspx" class="list-group-item">All books</a>
                    <ul class="list-group-item active" style="list-style-type:none">             
						<li class='dropdown'>
						<a href = 'LibraLendBook_Unreserved.aspx' class="dropdown-toggle" data-toggle="dropdown" style="text-decoration:none;color:white">Borrowing books</a>
						<ul class='dropdown-menu'>
							<li><a href="LibraLendBook_Unreserved.aspx"> Unreserved</a></li>
							<li><a href = "LibraLendBook_Reserved.aspx" >Reserved</a></li>
						</ul>
						</li>                
					</ul>
				<a href="LibraReturnBook.aspx" class="list-group-item">Returning books</a>
                    <a href="LibraAddNewBook.aspx" class="list-group-item">Add new books</a>
                    <a href="LibraDamagedBook.aspx" class="list-group-item">Damaged book handle</a>
                    <a href="LibraLostBookHandle.aspx" class="list-group-item">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <hr style="border:dotted 1px" />
						<div  style="background-color:#F2F2F2">
							<table>
								<tr style="width:970px;height:40px;" >
									<td >
										<asp:Label ID="Label5" runat="server" Text="ReaderID:" style="margin-left:30px"></asp:Label>
										<asp:TextBox ID="Readerid" runat="server" CssClass="hmf-textbox"></asp:TextBox>
										&nbsp;&nbsp;
									</td>
									<td style="width:350px;text-align:right" class="auto-style3">
										Barcode:<asp:TextBox ID="Barcode" runat="server" CssClass="hmf-textbox"></asp:TextBox>
									</td>
									<td style="text-align:center;width:170px" >
										<asp:Button ID="Button1" runat="server" Text="Borrow" OnClick="Borrow_Click" CssClass="hmf-button" />
									</td>
								</tr>
							</table>

							<table  border="0"  style="
            width: 100%; text-align:center;  padding: 0;border-collapse: separate; border-spacing: 10px;">
								<tr>
									 <td style="vertical-align: top; text-align: center; width: 100%; height: 341px;">                  
										 <asp:DataList ID="dluserBorrow" runat="server" style="width: 100%" >
											 <ItemTemplate>
											 <table style="width: 970px; padding: 0;border-collapse: separate; border-spacing: 15px;text-align:center;font-size:10pt"  >
												<tr>
													<td style="width: 100px; height: 26px;text-align:center">
														<asp:Label ID="labuserid" runat="server" Text='<%# Eval("mybarcode") %>'></asp:Label></td>
													<td style="width: 100px; height: 26px;text-align:center">
														<asp:Image ID="Image1" style="width:100px" runat="server" Imageurl='<%# Eval("book_picture") %>'/></td>
													<td style="width: 270px; height: 26px;text-align:center">
														<asp:Label ID="Label1" runat="server"  Text='<%# Eval("book_name") %>'></asp:Label></td>
													<td style="width: 250px; height: 26px;text-align:center">
														<asp:Label ID="Label4" runat="server"  Text='<%# Eval("borrowed_date") %>'></asp:Label></td>
													<td style="width: 250px; height: 26px;text-align:center">
														<asp:Label ID="textemail" runat="server"  Text='<%# Eval("return_date") %>'></asp:Label></td>
												</tr>
											</table>
											</ItemTemplate>
											<HeaderTemplate>
							<table style="width:970px;height:40px;margin-top:10px;text-align:center;font-size:10pt">
							<tr>
							<td style="width:100px;text-align:center">Barcode</td>
							<td style="width:100px;text-align:center">picture</td>
							<td style="width:270px;text-align:center">name</td>
							<td style="width:250px;text-align:center">Time of Borrowing</td>
							<td style="width:250px;text-align:center">Lastest Time of Returning</td>
							</tr>
							</table>
                        </HeaderTemplate>
										</asp:DataList>
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
