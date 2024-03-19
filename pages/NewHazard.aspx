<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewHazard.aspx.cs" Inherits="Hazard_Assessment_Management_System.NewHazard" %>

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
                <asp:TextBox ID="hazName" runat="server" />
                <br />
                <br />
                Hazard Description
                <asp:TextBox ID="hazDesc" runat="server" />
                <br />
                <br />

                Hazard Category
                <asp:DropDownList ID="hazCate" runat="server">
                    <asp:ListItem Value="1">Physical Hazards</asp:ListItem>
                    <asp:ListItem Value="2">Chemical Hazards</asp:ListItem>
                    <asp:ListItem Value="3">Biological Hazards</asp:ListItem>
                    <asp:ListItem Value="4">PsychoSocial Hazards</asp:ListItem>
                    <asp:ListItem Value="5">Enviromental Hazards</asp:ListItem>

                </asp:DropDownList><br /><br />
                <asp:LinkButton ID="makeNewHazard" runat="server" OnClick="MakeNewHazard_Click">Submit
                </asp:LinkButton><br /><br /> <br />
                                <asp:LinkButton ID="cancelbtn" runat="server" OnClick="Cancel_Click">Cancel
                </asp:LinkButton><br /><br /><br />
                <asp:Label runat="server" ID="confirmHazard" Text=""></asp:Label>


            </div>
        </div>
    </form>
</body>
</html>