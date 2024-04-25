<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecificForm.aspx.cs" Inherits="Hazard_Assessment_Management_System.pages.SpecificForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Hazard Assessment Management System</title>
    
    <link rel="stylesheet" href="main.css" />
    

    <!-- Script to print the content of a div -->
    <script> 
        function printDiv() { 
            var divContents = document.getElementById("formData").innerHTML; 
            var a = window.open('', '', 'height=500, width=500'); 
            a.document.write('<html><head><link rel="stylesheet" href="main.css" /></head>'); 
            a.document.write('<body><div class=formTable>'); 
            a.document.write(divContents); 
            a.document.write('</div></body></html>'); 
            a.document.close(); 
            a.print(); 
        } 
    </script> 
</head> 




<body><%-- Webpage Heading --%>
<header>
   
                  
<h1>Hazard Assessment Management System</h1>
</header>
   
    <form id="form1" method="post" runat="server">
        
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
            
                <div class="formDiv" id="formData">

                    <table class="formTable">
                        <tr>
                            <td>Assessment Performed By: </td>
                            <td>Date of Assessment</td>
                            <td><asp:LinkButton class="formButton" ID="Edit" runat="server" Text="Review" OnClick="EditForm_Click"/></td>
                            <td><input class="formButton" type="button" value="Print" onclick="printDiv()" /></td>
                        </tr>
                        <tr>
                            <td><asp:TextBox ID="name" runat="server" ReadOnly="true" /></td>
                            <td><asp:TextBox ID="dateOf" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td>Email: </td>
                            <td>Review Date</td>
                            <td><asp:LinkButton class="formButton" ID="Delete" runat="server" Text="Delete" OnClick="DeleteForm_Click"/></td>
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
                            
                            <td><asp:TextBox ID="task" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="hazards" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="risk" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="controls" runat="server" ReadOnly="true"/></td>
                            <td><asp:TextBox ID="dateImp" runat="server" ReadOnly="true"/></td>
                        </tr>
                        <placeholder ID="moreTHRCD" runat="server" />

                    </table>
                           
                    <asp:Label runat="server" ID="errorForm" style="color:red;" Text=""></asp:Label>
                </div>
       
    </form>
</body>
</html>
