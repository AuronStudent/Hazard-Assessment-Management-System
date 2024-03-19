<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Hazard_Assessment_Management_System.index" %>
 
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
       
                        <asp:HyperLink class="formButton" ID="hlNewForm" NavigateUrl="OHSForm.aspx" runat="server" style="margin-left:300px;">New Form</asp:HyperLink>
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

                <div class="datatable">
                    <h2>All Forms</h2>
                    <asp:TextBox ID="SearchTextBox" runat="server" style="width:50%;"></asp:TextBox>
                    <asp:DropDownList ID="DropDownFilter" runat="server" style="width:30%;">
                  <asp:ListItem Selected-="Filter By..." Value="Filter By..." />
                      <asp:ListItem Text="Department" Value="Department" />
                      <asp:ListItem Text="Control" Value="Control" />
                     <asp:ListItem Text="Hazard" Value="Hazard" />
                    </asp:DropDownList>
                    
                    <asp:Button ID="srchBtn" runat="server" Text="Search" OnClick="searchBtn_click" />
                    <asp:Button ID="clearbtn" runat="server" Text="Clear Search" OnClick="clearBtn_click" />
                <asp:GridView ID="SearchResultsGrid" runat="server" ShowHeader="False" DataKeyNames="Form_ID" CellSpacing="5" class="formDataTable"></asp:GridView>
                    
                    <!--Makes a data table with each form in it-->
                 <asp:GridView onrowcommand="Forms_RowCommand" ID="forms" runat="server" ShowHeader="False" DataKeyNames="Form_ID" CellSpacing="5" class="formDataTable">
                     <Columns>
                <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                          

                            <asp:Button ID="viewbtn" runat="server"  Text="View" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                            
                        </ItemTemplate>
                    </asp:TemplateField></Columns>
                 </asp:GridView>
                    </div>
       
    </form>
</body>
</html>
