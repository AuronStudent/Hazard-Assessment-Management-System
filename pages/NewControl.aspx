<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewControl.aspx.cs" Inherits="Hazard_Assessment_Management_System.NewControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="main.css" />
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">
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
        <!--This tables does NOT take into account new control categories. PLEASE REMEMBER TO CHANGE THIS-->
            <div class="datatable">
                Control Name <asp:TextBox ID="controlName" runat="server"  /> <br /> <br />
                Control Description <asp:TextBox ID="controlDesc" runat="server"  /> <br /> <br />
                Control Category <asp:DropDownList id="conCate" runat="server" >
                    <asp:ListItem Value="1">Engineering Controls</asp:ListItem>
                    <asp:ListItem Value="2">Administrative Controls</asp:ListItem>
                    <asp:ListItem Value="3">Personal Protective Equipment (PPE)</asp:ListItem>

                    </asp:DropDownList> <br />
                <asp:Label runat="server" ID="confirmControl" Text=""></asp:Label>
                         
        </div>
        <asp:LinkButton class="formButton" ID="makeNewControl" runat="server" OnClick="MakeNewControl_Click" style="margin-left:300px;">Submit</asp:LinkButton>  
    </form>
</body>
</html>
