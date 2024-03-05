<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Hazard_Assessment_Management_System.index" %>
 
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Hazard Assessment Management System</title>
    <link rel="stylesheet" href="main.css" />
</head>
<body>

    <form id="form1" runat="server">
        <div class="container">
 
            <%-- Webpage Heading --%>
            <div>
                <div>
                    <h1>Hazard Assessment Management System</h1>
                </div>
            </div>
 
            <%-- Menu / Message --%>
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
