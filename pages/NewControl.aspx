<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewControl.aspx.cs" Inherits="Hazard_Assessment_Management_System.NewControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Hazard Assessment Management System</h1>
    <link rel="stylesheet" href="newHazard.css" />

    <form id="form1" runat="server">
        <div>
            <div>


                Hazard Name
                <asp:TextBox ID="controlName" runat="server" />
                <br />
                <br />
                Hazard Description
                <asp:TextBox ID="controlDesc" runat="server" />
                <br />
                <br />

                Hazard Category
                <asp:DropDownList ID="conCate" runat="server">
                    <asp:ListItem Value="1">Administrative Controls</asp:ListItem>
                    <asp:ListItem Value="2">Engineering Controls</asp:ListItem>
                    <asp:ListItem Value="3">Personal Protective Equipment (PPE)</asp:ListItem>
                    

                </asp:DropDownList><br /><br />
                <asp:LinkButton ID="makeNewControl" runat="server" OnClick="MakeNewControl_Click">Submit
                </asp:LinkButton><br /><br /><br />
                                <asp:LinkButton ID="cancelbtn" runat="server" OnClick="Cancel_Click">Cancel
                </asp:LinkButton><br /><br /><br />
                <asp:Label runat="server" ID="confirmControl" Text=""></asp:Label>


            </div>
        </div>
    </form>
</body>
</html>