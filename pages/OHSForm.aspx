<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OHSForm.aspx.cs" Inherits="Hazard_Assessment_Management_System.OHSForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="main.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <h1>Hazard Assessment Form <br /> Lethbridge College OH&S</h1>
    
        <hr />
        <h2>Personal Information</h2>
        <hr />
        Full Legal Name <br /><asp:TextBox ID="legalName" runat="server"  /> <br /> 
        Email Address <br /><asp:TextBox ID="email" runat="server"  /> <br />
        <hr />
        <h2>Task Information</h2>
        <hr />

        Department <br /> <asp:DropDownList ID="ddlDep" runat="server" AppendDataBoundItems="true">
             
            </asp:DropDownList> <asp:Label runat="server" ID="errorDep" Text=""></asp:Label><br />
      

        Job/Worksite/Work Type <br /><asp:TextBox ID="jobsite" runat="server"  /> <br /> 
        Task <br /><asp:TextBox ID="task" runat="server"  /> <br /> 
        <asp:PlaceHolder ID="phTask" runat="server" /> <br />
        <asp:button ID="btnAddTask" runat="server" OnClick="btnAddTask_Click" Text="Add Another Task "></asp:button> <!---TODO not functional--->
        <hr />
        <h2>Hazard Information</h2>
        
        <hr />
        <h2>Task 1</h2>
        Hazard Category <br />
        <asp:DropDownList ID="ddlCatHaz" runat="server" AppendDataBoundItems="true" > </asp:DropDownList> <br />
         Hazard <br /> <asp:DropDownList ID="ddlHaz" runat="server" AppendDataBoundItems="true">
             
            </asp:DropDownList> <asp:Label runat="server" ID="errorHaz" Text=""></asp:Label><br />

        Risk <br /><br />
        Likelihood   <asp:RadioButtonList ID="likeGroup0" runat="server" RepeatDirection="Horizontal" CellPadding="20">
                     <asp:ListItem Text="1" Value="1" />
                     <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                     </asp:RadioButtonList><br />
        
        
        Severity     <asp:RadioButtonList ID="sevGroup0" runat="server" RepeatDirection="Horizontal" CellPadding="20">
                     <asp:ListItem Text="1" Value="1" />
                     <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                     </asp:RadioButtonList><br />

        Frequency    <asp:RadioButtonList ID="freqGroup0" runat="server" RepeatDirection="Horizontal" CellPadding="20">
                     <asp:ListItem Text="1" Value="1" />
                     <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                     </asp:RadioButtonList><br />

        <asp:button ID="btnAddHazard" runat="server" OnClick="btnAddHazard_Click" Text="Add Another Hazard +">   </asp:button><br /> <!---TODO not functional--->

        <PlaceHolder ID="moreTasks" runat="server"></PlaceHolder>
        
        <asp:PlaceHolder ID="phHaz" runat="server" /> <br />
        
        <hr />
        <h2>Control Information</h2>
        <hr />
        
            Control <br /> <asp:DropDownList  ID="ddlCon" runat="server" AppendDataBoundItems="true">
             
            </asp:DropDownList> <asp:Label runat="server" ID="errorCon" Text=""></asp:Label><br />
        <asp:PlaceHolder  ID= "phCon" runat="server" /> <br />

        <asp:button ID="btnAddControl" runat="server" OnClick="btnAddControl_Click" Text="Add Another Control +"></asp:button> <!---TODO not functional--->
        <hr />
        <h2>Other Information</h2>
        <hr />
        Comments <br />
        <asp:TextBox ID="comments" runat="server" TextMode="MultiLine"></asp:TextBox><br /><br /> <!---TODO not functional--->

         <asp:LinkButton ID="SubmitForm" class="formButton" runat="server" OnClick="SubmitForm_Click">Submit</asp:LinkButton><asp:LinkButton ID="Cancel" class="formButton" runat="server" OnClick="CancelForm_Click">Go Back</asp:LinkButton>
        <asp:Label runat="server" ID="formError" Text=""></asp:Label>
        <br /><br /><br />
        
        

    </form>
</body>
</html>
