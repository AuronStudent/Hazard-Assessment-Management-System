<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Hazard_Assessment_Management_System.pages.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="main.css" />
    <title></title>
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">
        <div>
            <center>
                <h3>Username</h3><br />
                <asp:TextBox ID="username" runat="server" style="width:40%;"  /> <br />
                <h3>Password</h3><br />
                <asp:TextBox type="password" ID="password" runat="server" style="width:40%;" /> <br /><br />
                 <asp:LinkButton ID="LoginForm" class="formButton" runat="server" OnClick="LoginForm_Click">Login</asp:LinkButton><br /><br />
                <asp:Label style="color:red" runat="server" ID="loginError" Text=""></asp:Label><br />
                                <h3>Continue as Guest</h3><br />
                <asp:HyperLink class="formButton" ID ="GuestLogin" runat="server" NavigateUrl="indexGuest1.aspx">Guest</asp:HyperLink>
            </center>
        </div>
    </form>
</body>
</html>
