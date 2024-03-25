<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="indexGuest1.aspx.cs" Inherits="Hazard_Assessment_Management_System.indexGuest1" %>
 
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
                <div class="datatable" >
                    <h2>All Forms</h2>
                    <p style="margin:10px;">You are not logged in, you will not be able to edit or delete forms, however you can still view them.</p><asp:HyperLink ID="login" runat="server" NavigateUrl="LoginPage.aspx" style="margin:10px;">Log In</asp:HyperLink><br /><br /><br />
                    <asp:HyperLink class="formButton" ID="hlNewForm" NavigateUrl="OHSForm.aspx" runat="server" >New Form</asp:HyperLink><br /><br />
                    <asp:TextBox ID="SearchTextBox" placeholder="Search..." runat="server" style="width:50%;"></asp:TextBox>
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
