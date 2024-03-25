<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Hazard_Assessment_Management_System.Controls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="main.css" />
    <title></title>
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">
         <asp:LinkButton style="margin-left:300px;" class="formButton" ID="NewControl" runat="server" Text="New Control" OnClick="btnNewControl_Click" />
                    <%-- Navigation --%>
            <div class="navContainer">
            <div class="sidenav">
                 <asp:HyperLink ID="hlHome" NavigateUrl="index.aspx" runat="server">All Forms</asp:HyperLink><br />
                 <asp:HyperLink ID="hlHazards" NavigateUrl="Hazards.aspx" runat="server">Hazards</asp:HyperLink><br />
                 <asp:HyperLink ID="hlControls" NavigateUrl="Controls.aspx" runat="server">Controls</asp:HyperLink><br />
                 <asp:HyperLink ID="hlDepartments" NavigateUrl="Departments.aspx" runat="server">Departments</asp:HyperLink><br />
                 
                 <asp:HyperLink ID="hlLogout" NavigateUrl="LoginPage.aspx" runat="server">Logout</asp:HyperLink><br />
            </div>
            </div>
        <div id="ControlData">
            <div style="margin-top:-550px" class="datatable">
                
                <asp:DropDownList ID="ddlControlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlControlCategories_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select Control Category --" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
           
            
            <div class="datatable2" id="EditCategories" runat="server" style="display: none;">
                
                <!-- Category editing controls will be shown here -->
                <label for="txtCategoryName">Control Category Name:</label><br />
                <asp:TextBox ID="txtCategoryName" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox><br />
                <label for="txtCategoryDescription">Control Category Description:</label><br />
                <asp:TextBox ID="txtCategoryDescription" runat="server" TextMode="MultiLine" Rows="3" Columns="50"></asp:TextBox><br />
                <label for="ddlControlsInCategory">Controls in the Category:</label><br />
                <asp:DropDownList ID="ddlControlsInCategory" runat="server" Width="200px"></asp:DropDownList><br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />

            </div>
            
        </div>
    </form>
</body>
</html>