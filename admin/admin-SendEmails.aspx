<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin-SendEmails.aspx.cs" Inherits="aspx_admin_SendEmails" %>

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
        p {
            position: relative;
            top: 10px;
            padding-bottom: 5px;
            padding-top: 2px;
            padding-right: 5px;
            overflow: hidden;
            width: 100%;
            font-size: 22px;
        }
    </style>
    
</head>
<body>
    <div class="container" style="top: 50px; width: 80%; margin: 0 auto; margin-top: 80px;">
        <form id ="form1" runat ="server">
            <center><p><asp:Label ID="lab" Text="" runat="server"></asp:Label></p><br />
            <asp:Button ID="button" Text="confirm" runat="server" OnClick="button_Click" class="layui-btn layui-btn-black"></asp:Button>
            </center>
        </form>
    </div>
   
</body>
</html>
