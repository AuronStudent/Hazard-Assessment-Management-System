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
        <button>Add Another Task +</button> <!---TODO not functional--->
        <hr />
        <h2>Hazard Information</h2>
        <hr />

         Hazard <br /> <asp:DropDownList ID="ddlHaz" runat="server" AppendDataBoundItems="true">
             
            </asp:DropDownList> <asp:Label runat="server" ID="errorHaz" Text=""></asp:Label><br />

        Risk <br />
        Likelihood   <asp:TextBox ID="like" runat="server"  />  <br />
        Severity     <asp:TextBox ID="sev" runat="server"  />  <br />
        Frequency     <asp:TextBox ID="freq" runat="server"  /> <br />
        <button>Add Another Hazard +</button><br /> <!---TODO not functional--->
        <hr />
        <h2>Control Information</h2>
        <hr />
        
            Control <br /> <asp:DropDownList  ID="ddlCon" runat="server" AppendDataBoundItems="true">
             
            </asp:DropDownList> <asp:Label runat="server" ID="errorCon" Text=""></asp:Label><br />

        <button>Add Another Control +</button> <!---TODO not functional--->
        <hr />
        <h2>Other Information</h2>
        <hr />
        Comments <br />
        <textarea>comments</textarea><br /><br /> <!---TODO not functional--->

         <asp:LinkButton ID="SubmitForm" class="formButton" runat="server" OnClick="SubmitForm_Click">Submit</asp:LinkButton><asp:LinkButton ID="Cancel" class="formButton" runat="server" OnClick="CancelForm_Click">Go Back</asp:LinkButton>
        <asp:Label runat="server" ID="formError" Text=""></asp:Label>
        <br /><br /><br />
        
        

    </form>
</body>
</html>
