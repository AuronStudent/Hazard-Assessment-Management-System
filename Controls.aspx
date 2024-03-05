<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Controls.aspx.cs" Inherits="Hazard_Assessment_Management_System.Controls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <form id="form1" runat="server">
        <div id="ControlData">
                        <div>
                
                    <ul>
                        <li>
                            <asp:HyperLink ID="hlHome" NavigateUrl="~/index.aspx" runat="server">Home</asp:HyperLink><br />
                        </li>
                        <li>
                            <asp:HyperLink ID="hlControls" NavigateUrl="~/Controls.aspx" runat="server">Controls</asp:HyperLink><br />
                        </li>
                        <li>
                            <asp:HyperLink ID="hlHazards" NavigateUrl="~/Hazards.aspx" runat="server">Hazards</asp:HyperLink><br />
                        </li>
                                                <li>
                            <asp:HyperLink ID="hlDepartments" NavigateUrl="~/Departments.aspx" runat="server">Departments</asp:HyperLink><br />
                        </li>
                    </ul>
        </div>
        </div>
    </form>
</body>
</html>
