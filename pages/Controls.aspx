<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Hazard_Assessment_Management_System.Controls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="main.css" />
</head>
<body>
    <h1>Hazard Assessment Management System</h1>

    <form id="form1" runat="server">

         <%-- Navigation --%>
            <div class="navContainer">
            <div class="sidenav">
                 <asp:HyperLink ID="HyperLink1" NavigateUrl="index.aspx" runat="server">All Forms</asp:HyperLink><br />
                 <asp:HyperLink ID="hlHazards" NavigateUrl="Hazards.aspx" runat="server">Hazards</asp:HyperLink><br />
                 <asp:HyperLink ID="hlControls" NavigateUrl="Controls.aspx" runat="server">Controls</asp:HyperLink><br />
                 <asp:HyperLink ID="hlDepartments" NavigateUrl="Departments.aspx" runat="server">Departments</asp:HyperLink><br />
                 
                 <asp:HyperLink ID="hlLogout" NavigateUrl="LoginPage.aspx" runat="server">Logout</asp:HyperLink><br />
            </div>
            </div>
         
            
        <div id="ControlData" class="datatable2" style="margin-top:-600px;">
            <h2>All Controls</h2>
        <div id="dataContainer" runat="server">
            
                <!-- Table will be populated here -->
            </div>
                    

            <asp:Label ID="errorLabel" runat="server" Text="" style="color:red;"></asp:Label>
        </div>
        
        <div class="datatable" id="newConTable" runat="server" style="margin-left:1150px;">
            <asp:Label ID="addOrEdit" runat="server" Text="<h2>Add New Control</h2>" />
                Control Name <asp:TextBox ID="conName" runat="server"  /> <br /> <br />

            Control Category<asp:DropDownList ID="ddlControlCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlControlCategories_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select Control Category --" Value="-1"></asp:ListItem>
                </asp:DropDownList><br /><br />

                Control Description <asp:TextBox ID="conDesc" runat="server"  /> <br /> <br />
                <asp:TextBox type="hidden" ID="controlID" name="conID" value="" runat="server" />

        <asp:LinkButton class="formButton" ID="makeNewCon" runat="server" OnClick="MakeNewCon_Click">Submit</asp:LinkButton>  
         <asp:LinkButton class="formButton" ID="saveEdit" runat="server" OnClick="saveEdit_Click">Save</asp:LinkButton> <br /><br /><br />

        <asp:LinkButton class="formButton" ID="cancelbtn" runat="server" OnClick="Cancel_Click">Cancel</asp:LinkButton>  <br /><br /><br />
            <asp:Label runat="server" ID="confirmCon" Text=""></asp:Label>

                <br /><br />
    </div>
    </form>
</body>
</html>
