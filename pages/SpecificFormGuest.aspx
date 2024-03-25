<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecificFormGuest.aspx.cs" Inherits="Hazard_Assessment_Management_System.pages.SpecificFormGuest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Hazard Assessment Management System</title>
    
    <link rel="stylesheet" href="mainGuest.css" />
    

</head>


<body><%-- Webpage Heading --%>
<header>
   
                  
<h1>Hazard Assessment Management System</h1>
</header>
   
    <form id="form1" runat="server">
        
            <%-- Navigation --%>

            <asp:HyperLink class="formButton" ID="hlNewForm" NavigateUrl="indexGuest1.aspx" runat="server" style="margin:10px;">Back</asp:HyperLink><br /><br />
                <div class="formDiv">

                    <table class="formTable">
                        <tr>
                            <td>Assessment Performed By: </td>
                            <td>Date of Assessment</td>
                            
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="name" runat="server" ReadOnly="true" /></td>
                            <td><asp:TextBox ID="dateOf" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td>Email: </td>
                            <td>Review Date</td>
                            
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="email" runat="server"  ReadOnly="true"/></td>
                            <td><asp:TextBox ID="reviewDate" runat="server" ReadOnly="true" /></td>
                        </tr>
                         <tr>
                            <td>Job/Position/Work Type: </td>
                            <td>Reviewed By</td>
                             <td>Department</td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="jobite" runat="server"  ReadOnly="true"/></td>
                            <td><asp:TextBox ID="reviewBy" runat="server"  ReadOnly="true"/></td>
                            <td><asp:TextBox ID="department" runat="server" ReadOnly="true" /></td>
                        </tr>
                    </table>
                    <br /><br />
                    <table class="formTable" style ="border:2px;border-style:solid;">
                        <tr>
                            <th>Tasks</th>
                            <th>Hazards</th>
                            <th>Risk</th>
                            <th>Controls</th>
                            <th>Date Implemented</th>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="task" runat="server" ReadOnly="true" /></td>
                            <td><asp:TextBox ID="hazards" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="risk" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="controls" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="dateImp" runat="server" ReadOnly="true"/></td>
                        </tr>

                    </table>
                    <asp:Label runat="server" ID="errorForm" style="color:red;" Text=""></asp:Label>
                </div>
       
    </form>
</body>
</html>

