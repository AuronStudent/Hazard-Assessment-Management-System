<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PleaseLogIn.aspx.cs" Inherits="Hazard_Assessment_Management_System.pages.PleaseLogIn" %>

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
                <h3>You need to be logged in to view this page!</h3><br />
                <asp:HyperLink class="formButton" ID ="loginPage" runat="server" NavigateUrl="LoginPage.aspx">Login</asp:HyperLink>
                <h3>Continue as Guest</h3><br />
                <asp:HyperLink class="formButton" ID ="GuestLogin" runat="server" NavigateUrl="indexGuest1.aspx">Guest</asp:HyperLink>
                 
                <asp:Label style="color:red" runat="server" ID="loginError" Text=""></asp:Label>
            </center>
        </div>
    </form>
</body>
</html>
