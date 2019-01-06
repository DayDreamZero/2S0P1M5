<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-ShowAllSettings.aspx.cs" Inherits="admin_admin_ShowAllSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="../Image/title.ico" />
    <title>BiblioSoft</title>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <script src="../js/layui.all.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/layui.css" rel="stylesheet" />
    <link href="../css/layer.css" rel="stylesheet" />
    <style>
        #p{
            position:relative ;
            top:250px;
            left:10%;
            padding:2px 5px;
            overflow:hidden;
            width:100%;
            font-size:35px;
        }
    </style>
</head>
<body>
    <nav class="navbar navbar-default navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <a style="float: left;height: 50px;padding:10px 10px" href="#"><img src="../image/title.png" height="30px"/></a>
                <a class="navbar-brand" href="#">BiblioSoft</a>
            </div>
            <div>
                <ul class="nav navbar-nav">
                    <li><a class="active" href="admin-homepage.aspx">Homepage</a></li>
                    <li><a class="active" href="admin-ShowAllSettings.aspx">AllSettings</a></li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Settings
					<b class="caret"></b>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a href="../admin/admin-Setlendnum.aspx">SetMaxBorrow</a></li>
                            <li><a href="../admin/admin-SetPenalty.aspx">SetPenalty</a></li>
                            <li><a href="../admin/admin-SetDeposit.aspx">SetDeposit</a></li>
                            <li><a href="admin-SetPeriod.aspx">SetPeriod</a></li>
                            <li><a href="admin-SetLibDefaultPwd.aspx">SetDefaultPwdForLib</a></li>
                            <li><a href="admin-SetSendPeriod.aspx">SetSendInterval</a></li>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">Transactions
					<b class="caret"></b>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a href="admin-LibrarianGroup.aspx">LibrarianGroup</a></li>
                            <li><a href="admin-LibraResetList.aspx">LibraResetList</a></li>
                        </ul>
                    </li>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>
                        <asp:Label ID="label1" runat="server" Text="username"></asp:Label></a>
                        <ul class='dropdown-menu'>
                            <li><a href='../admin/admin-information-show.aspx'>Information Box</a></li>
                            <li><a href="admin-ChangePassword.aspx">ChangePassword</a></li>
                            <li><a href="../Admin-Logout.aspx">Log Out</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="layui-content" style="top: 30px;width: 80%; margin: 0 auto; margin-top: 60px;">
        <form id="form1" runat="server" onkeydown="return(event.keyCode!=13)">
            <div align="center">
                <p style="font-family: 'Times New Roman'; font-size: 24px">All Settings Admin Can Set</p>
            </div>
            <div class="layui-row" style="margin-top:-20px;">
                <div class="layui-card">
                    <div class="layui-card-body">
                        <div class="form-box">
                            <div class="layui-form layui-form-item">
                            </div>
                            <div id="tablediv">
                                <table id="demo" class="layui-table" lay-filter="test" style="width: 80%"></table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <script type="text/html" id="barDemo">
        <a class="layui-btn  layui-btn-xs" lay-event="edit">Edit</a>
        <a class="layui-btn  layui-btn-xs" lay-event="del">Delete</a>
    </script>

    <script>
        var element = layui.element;
        var table = layui.table;
        var form = layui.form;

        //展示已知数据
        table.render({
            elem: '#demo'
          , title:
              {text:'The settings of Bibliosoft'}
          , cols: [[ //标题栏
            { field: 'setting', title: 'Settings', minWidth: 120 }
            , { field: 'value', title: 'Value', minWidth: 120 }
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
          , limit: 10 //每页默认显示的数量
          , text: {
              none: 'None'
          }
        });

        table.on('row(test)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
            var data = obj.data; //获得当前行数据
            var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
            var tr = obj.tr; //获得当前行 tr 的DOM对象
            if (data["setting"] == "max_borrow")
                window.location.href = "admin-Setlendnum.aspx";
            else if (data["setting"] == "reader_deposit")
                window.location.href = "admin-SetDeposit.aspx";
            else if (data["setting"] == "penalty of lost" || data["setting"] == "penalty of overdue")
                window.location.href = "admin-SetPenalty.aspx";
            else if (data["setting"] == "period of return"||data["setting"] == "period of reserve")
                window.location.href = "admin-SetPeriod.aspx";
            else if (data["setting"] == "default psd for librarian")
                window.location.href = "admin-SetLibDefaultPwd.aspx";
            else if (data["setting"] == "interval for send emails")
                window.location.href = "admin-SetSendPeriod.aspx";
        });
        table.on()
    </script>
    <footer class="navbar-fixed-bottom" style="bottom:10px">
        <div style=" line-height: 23px; text-decoration:none">
                <p align="center" style="margin-top: 10px;color: #878B91;">
                    Copyright &copy;2018 A-10
                </p>
        </div>
    </footer>
</body>
</html>
