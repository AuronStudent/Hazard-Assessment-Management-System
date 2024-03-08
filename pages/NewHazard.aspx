<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewHazard.aspx.cs" Inherits="Hazard_Assessment_Management_System.NewHazard" %>

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
        <!--This tables does NOT take into account new hazard categories. PLEASE REMEMBER TO CHANGE THIS-->
            <div class="datatable">
                Hazard Name <asp:TextBox ID="hazName" runat="server"  /> <br /> <br />
                Hazard Description <asp:TextBox ID="hazDesc" runat="server"  /> <br /> <br />
                Hazard Category <asp:DropDownList id="hazCate" runat="server" >
                    <asp:ListItem Value="1">Physical Hazards</asp:ListItem>
                    <asp:ListItem Value="2">Chemical Hazards</asp:ListItem>
                    <asp:ListItem Value="3">Biological Hazards</asp:ListItem>
                    <asp:ListItem Value="4">Psychosocial Hazards</asp:ListItem>
                    <asp:ListItem Value="5">Enviormental Hazards</asp:ListItem>

                    </asp:DropDownList> <br />
                <asp:Label runat="server" ID="confirmHazard" Text=""></asp:Label>
                         
        </div>
        <asp:LinkButton class="formButton" ID="makeNewHazard" runat="server" OnClick="MakeNewHazard_Click" style="margin-left:300px;">Submit</asp:LinkButton>  
    </form>
</body>
</html>
