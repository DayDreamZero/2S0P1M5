<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LibraDamagedBook.aspx.cs" Inherits="Librarian_aspx_LibraDamagedBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>BiblioSoft_DamagedBookHandle</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/Librarian.css" rel="stylesheet" />
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/layui.all.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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

            <a class="navbar-btn btn-sm btn-primary navbar-right" href="../Login.aspx"><asp:Label ID="Label3" runat="server" Text="Logout" Font-Size="X-Small"></asp:Label></a>

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
                    <ul class="list-group-item" style="list-style-type:none">             
						<li class='dropdown'>
						<a href = 'LibraLendBook_Unreserved.aspx' class="dropdown-toggle" data-toggle="dropdown" style="text-decoration:none;color:DimGray">Borrowing books</a>
						<ul class='dropdown-menu'>
							<li><a href="LibraLendBook_Unreserved.aspx"> Unreserved</a></li>
							<li><a href = "LibraLendBook_Reserved.aspx" >Reserved</a></li>
						</ul>
						</li>                
					</ul>
                    <a href="LibraReturnBook.aspx" class="list-group-item">Returning books</a>
                    <a href="LibraAddNewBook.aspx" class="list-group-item">Add new books</a>
                    <a href="LibraDamagedBook.aspx" class="list-group-item active">Damaged book handle</a>
                    <a href="LibraLostBookHandle.aspx" class="list-group-item">Lost book handle</a>
                </div> 
            </div>
            <div class="col-sm-10">
                <form id="form1" runat="server">
                    <div class="layui-content" style="top: 50px; width: 100%; margin: 0 auto;">
                        <div class="layui-row">
                            <div class="layui-card">
                                <div class="layui-card-body">
                                    <div class="form-box">
                                        <div class="layui-form layui-form-item">
                                            <div class="layui-inline">
                                                <div class="layui-form-mid">Barcode of damaged book:</div>
                                                <div class="layui-input-inline" style="width: 100px;">
                                                    <input name="input2" type="text" autocomplete="off" class="layui-input" runat="server" id="barcodeTxt"/>
                                                </div>
                                                <asp:Button ID="Button1" class="layui-btn layui-btn-black" runat="server" Text="OK" OnClick="Button1_Click"></asp:Button>
                                            </div>
                                        </div>
                                        <div id="tablediv">
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
			{ field: 'barcode', title: 'Barcode', minWidth: 100, sort: true }
            , { field: 'bookname', title: 'Book Name', minWidth: 180, sort: true }
            , { field: 'isbn', title: 'ISBN', minWidth: 100 , sort: true}
			, { field: 'time', title: 'Damaged Time', minWidth: 200}
			, { field: 'userid', title: 'Processing Librarian', minWidth: 200 , sort: true}
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