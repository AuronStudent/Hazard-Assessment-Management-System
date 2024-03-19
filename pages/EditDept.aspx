<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EditDept.aspx.cs" Inherits="Hazard_Assessment_Management_System.EditDept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="newHazard.css" />
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">

        
         <div class="datatable">
                Department Name <asp:TextBox ID="depName" runat="server"  /> <br /> <br />
                Department Description <asp:TextBox ID="depDesc" runat="server"  /> <br /> <br />
      
                <asp:Label runat="server" ID="confirmDep" Text=""></asp:Label>
                <asp:LinkButton class="formButton" ID="saveEdit" runat="server" OnClick="saveEdit_Click">Save</asp:LinkButton> <br /><br /><br />
                <asp:LinkButton class="formButton" ID="cancelEdit" runat="server" OnClick="cancelEdit_Click">Cancel</asp:LinkButton> <br /><br />
                <asp:Label runat="server" ID="confirmSave" Text=""></asp:Label>    
         </div>
    </form>
</body>
</html>
