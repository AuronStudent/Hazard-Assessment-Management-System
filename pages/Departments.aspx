<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="Hazard_Assessment_Management_System.Departments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="main.css" />
</head>
<body>
    <h1>Hazard Assessment Management System</h1>

    <form id="form1" runat="server">
        <asp:LinkButton style="margin-left:300px;" class="formButton" ID="NewDept" runat="server" Text="New Department" OnClick="btnNewDept_Click" />
         <%-- Navigation --%>
            <div class="navContainer">
            <div class="sidenav">
                 <asp:HyperLink ID="HyperLink1" NavigateUrl="index.aspx" runat="server">All Forms</asp:HyperLink><br />
                 <asp:HyperLink ID="hlHazards" NavigateUrl="Hazards.aspx" runat="server">Hazards</asp:HyperLink><br />
                 <asp:HyperLink ID="hlControls" NavigateUrl="Controls.aspx" runat="server">Controls</asp:HyperLink><br />
                 <asp:HyperLink ID="hlDepartments" NavigateUrl="Departments.aspx" runat="server">Departments</asp:HyperLink><br />
                 <asp:HyperLink ID="hlReports" NavigateUrl="Reports.aspx" runat="server">Report Making</asp:HyperLink><br />
                 <asp:HyperLink ID="hlLogout" NavigateUrl="LoginPage.aspx" runat="server">Logout</asp:HyperLink><br />
            </div>
            </div>
         
            
        <div id="DepartmentData" class="datatable2" style="background-color:white;">
            <h2>All Departments</h2>
        <div id="dataContainer" runat="server">
            
                <!-- Table will be populated here -->
            </div>
            <asp:Label ID="errorLabel" runat="server" Text="" style="color:red;"></asp:Label>
        </div>
        
    </form>
</body>
</html>
