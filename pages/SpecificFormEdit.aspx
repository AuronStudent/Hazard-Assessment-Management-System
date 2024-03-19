<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecificFormEdit.aspx.cs" Inherits="Hazard_Assessment_Management_System.pages.SpecificFormEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Hazard Assessment Management System</title>
    <link rel="stylesheet" href="main.css" />

</head>


<body><%-- Webpage Heading --%>
<header>
   
                  
<h1>Hazard Assessment Management System</h1>
</header>
   
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
            
                <div class="formDiv">

                    <table class="formTable">
                        <tr>
                            <td>Assessment Performed By: </td>
                            <td>Date of Assessment</td>
                            <td><asp:LinkButton class="formButton" ID="Save" runat="server" Text="Save" OnClick="SaveForm_Click"/></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="name" runat="server" /></td>
                            <td><asp:TextBox ID="dateOf" runat="server" ReadOnly="True"/></td>
                        </tr>
                        <tr>
                            <td>Email: </td>
                            <td>Review Date</td>
                            <td><asp:LinkButton class="formButton" ID="Cancel" runat="server" Text="Cancel" OnClick="CancelForm_Click"/></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="email" runat="server" /></td>
                            <td><asp:TextBox ID="reviewDate" runat="server" ReadOnly="True"/></td>
                        </tr>
                         <tr>
                            <td>Job/Position/Work Type: </td>
                            <td>Reviewed By</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="jobite" runat="server"  /></td>
                            <td><asp:TextBox ID="reviewBy" runat="server"  /></td>
                        </tr>
                    </table>
                    <br /><br />
                    <table class="formTable"  style="border:2px;border-style:solid;">
                        <tr>
                            <th>Tasks</th>
                            <th>Hazards</th>
                            <th>Risk</th>
                            <th>Controls</th>
                            <th>Date Implemented</th>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="task" runat="server" /></td>
                            <td><asp:TextBox ID="hazards" runat="server" /></td>
                            <td><asp:TextBox ID="risk" runat="server" /></td>
                            <td><asp:TextBox ID="controls" runat="server"/></td>
                            <td><asp:TextBox ID="dateImp" runat="server"/></td>
                        </tr>

                    </table>
                    <asp:Label runat="server" ID="errorForm" style="color:red;" Text=""></asp:Label>
                </div>
       
    </form>
</body>
</html>
