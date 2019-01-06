<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraFineIndex.aspx.cs" Inherits="Librarian_aspx_LibraFineIndex" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_FineHandle</title>
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
            <div class="col-sm-1">
              
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div>
                        <asp:Label ID="Label6" runat="server" Text="Reader ID"></asp:Label>
                        <asp:TextBox ID="readerIDtext" runat="server" AUTOCOMPLETE="OFF" CssClass="hmf-textbox"></asp:TextBox>
                        <asp:Button ID="searchBt" runat="server" Text="Search" OnClick="searchBt_Click" CssClass="hmf-button" />
                        &nbsp;<asp:Button ID="resetBt" runat="server" Text="Reset" OnClick="resetBt_Click" CssClass="hmf-button" />
                    </div>
                    <hr style="border:dotted 1px" />

                    <div>
                        <asp:GridView ID="GridView1" runat="server" Width=100% AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="borrowed_id" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowHeaderWhenEmpty="True" UseAccessibleHeader="false" EmptyDataText="None" CellSpacing="4" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="user_id" HeaderText="Reader ID" />
                                <asp:BoundField DataField="username" HeaderText="Reader name" />
                                <asp:BoundField DataField="barcode" HeaderText="Barcode" />
                                <asp:BoundField DataField="book_name" HeaderText="Book name" />
                                <asp:BoundField DataField="borrowedTime" HeaderText="Borrow time" />
                                <asp:BoundField DataField="returnTime" HeaderText="Return time" />
                                <asp:BoundField DataField="delay" HeaderText="Delay days" />
                                <asp:BoundField DataField="fine" HeaderText="Fine" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="check" runat="server" AutoPostBack="True" OnCheckedChanged="check_CheckedChanged" BackColor="#00CC99"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                    
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                    
                            <FooterStyle BackColor="#ECEFF3" ForeColor="Black" />
                            <HeaderStyle BackColor="#F2F2F2" Font-Bold="False" ForeColor="#666666" Font-Italic="False" CssClass="hmf-gridviewHeader" Height="50px" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    
                            <PagerTemplate>
                                
                            
                                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                    Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>' Font-Size="Small">First</asp:LinkButton>
                           
                                 &nbsp;<asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                                    CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' Font-Size="Small">&lt;&lt;.</asp:LinkButton>
                           
                                 &nbsp;<asp:Label ID="LabelCurrentPage" runat="server" Font-Size="Small" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>' Font-Size="Small"> .&gt;&gt;</asp:LinkButton>
                            
                                &nbsp;<asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                    Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>' Font-Size="Small">Last</asp:LinkButton>
                            
                                &nbsp;<asp:Label ID="Label5" runat="server" Text="go to page:" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' Font-Size="Small" />
                                   
                                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                                    CommandName="Page" Text="GO" Font-Size="Small" />
                                
                            
                                <asp:Label ID="Label4" runat="server" Text="Total:" Font-Size="Small"></asp:Label>
                                <asp:Label ID="LabelPageCount" runat="server" Text='<%# ((GridView)Container.NamingContainer).PageCount %>' Font-Size="Small"></asp:Label>
                                    
                            </PagerTemplate>
                            <RowStyle BorderColor="#F2F2F2" Height="40px" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="Black" />
                        </asp:GridView>
                    </div>
                    <div>
                        
                        <asp:Button ID="payBt" runat="server" Text="pay" OnClick="payBt_Click" CssClass="hmf-button"/>
                    &nbsp;<asp:Label ID="Label7" runat="server" Text="amount:" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="fineLab" runat="server" Text="0" Font-Size="Medium"></asp:Label>
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
