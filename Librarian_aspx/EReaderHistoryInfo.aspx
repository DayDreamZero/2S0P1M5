<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EReaderHistoryInfo.aspx.cs" Inherits="HistoryInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_HistoryInfo</title>
    <link rel="shortcut icon" href="../image/title.ico" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/layui.all.js"></script>
    <script src="../js/jquery.min.js"></script>
    <link href="../css/layui.css" rel="stylesheet" />
	<link href="../css/layer.css" rel="stylesheet" />
    <link href="../css/look.css" rel="stylesheet" />

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
            <div class="col-sm-2" style="border-right: solid 1px">
                <div class="list-group side-bar">
                     <a href="EReaderManage.aspx" class="list-group-item active">Manage Reader</a>
                    <a href="EReaderRegister.aspx" class="list-group-item">Register Reader</a>
					<a href="EReaderHistory.aspx" class="list-group-item">Reader History</a>                    
                </div>
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
					<div>
						 <asp:Label ID="Label6" runat="server" Text="Reader ID:"></asp:Label>
                         <asp:Image ID="Image1" runat="server"/>
						&nbsp;<asp:Label ID="Label1" runat="server" Text="Book_Name:"></asp:Label>
                        <asp:TextBox ID="name" runat="server" AUTOCOMPLETE="OFF" CssClass="hmf-textbox" style="width: 100px;"></asp:TextBox>
						&nbsp;<asp:Label ID="Label4" runat="server" Text="Book_Status:"></asp:Label>
						<asp:DropDownList ID="DropDownList1" runat="server" style="width: 80px;height:35px">
							<asp:ListItem Value="5">all</asp:ListItem>
							<asp:ListItem Value="0">out</asp:ListItem>
							<asp:ListItem Value="1">return</asp:ListItem>
							<asp:ListItem Value="2">overdue</asp:ListItem>
							<asp:ListItem Value="3">lost</asp:ListItem>
							<asp:ListItem Value="4">damaged</asp:ListItem>
						</asp:DropDownList>
						&nbsp;<asp:Label ID="Label5" runat="server" Text="Fine_Status:"></asp:Label>
						<asp:DropDownList ID="DropDownList2" runat="server" style="width: 50px;height:35px">
							<asp:ListItem>all</asp:ListItem>
							<asp:ListItem>yes</asp:ListItem>
							<asp:ListItem>no</asp:ListItem>
						</asp:DropDownList>
						 &nbsp; <asp:Button ID="Button1" class="layui-btn layui-btn-black" runat="server" Text="Query" OnClick="Button1_Click"></asp:Button>
						 <asp:Button ID="Button3" class="layui-btn layui-btn-black" runat="server" Text="ShowAll" OnClick="Button2_Click"></asp:Button>
						<asp:Button ID="Button2" class="layui-btn layui-btn-black" runat="server" Text="Back" OnClick="Button3_Click"></asp:Button>
						</div>
                    <hr style="border:dotted 1px" />
                    <div class="layui-content" style="top: 50px; width: 100%; margin: 0 auto;margin:0;border:0;padding:0;">						
                        <div class="layui-row" style="margin:0;border:0;padding:0;">
                            <div class="layui-card" style="margin:0;border:0;padding:0;">
                                <div class="layui-card-body" style="margin:0;border:0;padding:0;">
                                    <div class="form-box" style="margin:0;border:0;padding:0;">
                                        <div id="tablediv" style="margin:0;border:0;padding:0;">
											<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>	
                                            <table id="demo" class="layui-table" lay-filter="test"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>


    <script>
        var element = layui.element;
        var table = layui.table;
        var form = layui.form;

        //展示已知数据
        table.render({
            elem: '#demo'
          , cols: [[ //标题栏
			{ field: 'userid', title: 'ReaderID', minWidth: 100 , sort: true, fixed: 'left'}
            , { field: 'bookname', title: 'Book_Name', minWidth: 180, sort: true }
            , { field: 'barcode', title: 'Barcode', minWidth: 100, sort: true }
            , { field: 'book_status', title: 'Book_Status', minWidth: 150, sort: true}
			, { field: 'fine', title: 'Fine', minWidth: 100 , sort: true }
			, { field: 'fine_status', title: 'Fine_Status', minWidth: 150, sort: true }
			, { field: 'borrowtime', title: 'Time of Borrowing', minWidth: 200}
			, { field: 'returntime', title: 'Time of Returning', minWidth: 200}
          ]]
          , data: [
            <%=htmlstr%>
			]
          , skin: 'line' //表格风格
          , id: 'lib'
			, even: true
		  
          , page: {
              layout: ['limit','count','prev', 'page', 'next'] //自定义分页布局
              , curr: 1 //设定初始在第 5 页
              , groups: 3 //只显示3个连续页码
              , first: 'First'
              , last: 'Last'
              , prev: '<em><</em>'
              , next: '<em>></em>'
          }
          , limits: [5, 10, 20]
          , limit: 5 //每页默认显示的数量
          , text: {
              none: 'None'
          }
        });

    </script>
    
     </div>
    
    <div class="footer" style="height:20px;background-color:white;"><font color="black"> Copyright &copy;2018 A-10</font></div>
    </div>

</body>
</html>
