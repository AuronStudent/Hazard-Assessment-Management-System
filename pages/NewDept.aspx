<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NewDept.aspx.cs" Inherits="Hazard_Assessment_Management_System.NewDept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="newHazard.css" />
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">

        <!--This tables does NOT take into account new control categories. PLEASE REMEMBER TO CHANGE THIS-->
            <div class="datatable">
                Department Name <asp:TextBox ID="depName" runat="server"  /> <br /> <br />
                Department Description <asp:TextBox ID="depDesc" runat="server"  /> <br /> <br />
      
                
                         
        </div>
        <asp:LinkButton class="formButton" ID="makeNewDept" runat="server" OnClick="MakeNewDept_Click">Submit</asp:LinkButton>  <br /><br /><br />
                <asp:LinkButton class="formButton" ID="cancelbtn" runat="server" OnClick="Cancel_Click">Cancel</asp:LinkButton>  <br /><br />
        <asp:Label runat="server" ID="confirmDep" Text=""></asp:Label>
    </form>
</body>
</html>
