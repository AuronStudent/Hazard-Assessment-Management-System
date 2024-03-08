<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hazards.aspx.cs" Inherits="Hazard_Assessment_Management_System.Hazards" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="main.css" />
    <title></title>
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">
                    <%-- Navigation --%>
            <div class="navContainer">
            <div class="sidenav">
                 <asp:HyperLink ID="hlHome" NavigateUrl="index.aspx" runat="server">All Forms</asp:HyperLink><br />
                 <asp:HyperLink ID="hlHazards" NavigateUrl="Hazards.aspx" runat="server">Hazards</asp:HyperLink><br />
                 <asp:HyperLink ID="hlControls" NavigateUrl="Controls.aspx" runat="server">Controls</asp:HyperLink><br />
                 <asp:HyperLink ID="hlDepartments" NavigateUrl="Departments.aspx" runat="server">Departments</asp:HyperLink><br />
                 <asp:HyperLink ID="hlReports" NavigateUrl="Reports.aspx" runat="server">Report Making</asp:HyperLink><br />
                 <asp:HyperLink ID="hlLogout" NavigateUrl="LoginPage.aspx" runat="server">Logout</asp:HyperLink><br />
            </div>
            </div>
        <div id="HazardData">
            <div style="margin-top:-550px" class="datatable">
                <asp:DropDownList ID="ddlHazardCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHazardCategories_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select Hazard Category --" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:LinkButton style="margin-left:300px;" class="formButton" ID="NewHazard" runat="server" Text="New Hazard" OnClick="btnNewHazard_Click" />
            
            <div class="datatable" id="EditCategories" runat="server" style="display: none;">
                <!-- Category editing controls will be shown here -->
                <label for="txtCategoryName">Hazard Category Name:</label><br />
                <asp:TextBox ID="txtCategoryName" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox><br />
                <label for="txtCategoryDescription">Hazard Category Description:</label><br />
                <asp:TextBox ID="txtCategoryDescription" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox><br />
                <label for="ddlHazardInCategory">Hazards in the Category:</label><br />
                <asp:DropDownList ID="ddlHazardInCategory" runat="server" Width="200px"></asp:DropDownList><br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
    </form>
</body>
</html>