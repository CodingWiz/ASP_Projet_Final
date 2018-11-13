<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style>
body {
    background-color: lightblue;
}
.titre{
    text-shadow: 3px 6px 2px #333;
    font-size: medium;
}
</style>
    <title>Disques</title>
    
</head>


<body>
        <form id="form1" runat="server">

        <div id="divConnexion" runat="server">
            <h1><asp:Label ID="Label3" runat="server" Text="Connexion" CssClass="titre"></asp:Label></h1>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                    <asp:Label ID="Label1" runat="server" Text="Nom d'utilisateur"></asp:Label>
                    </td>
                    <td>
                    <asp:TextBox ID="tbNomU" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                    <asp:Label ID="Label2" runat="server" Text="Mot de Passe"></asp:Label>
                    </td>
                    <td>
                    <asp:TextBox ID="tbMDP" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            
            <br />
            
            
            <br />
             
            
            <br />
            <asp:Button ID="btnConnexion" runat="server" Text="Se Connecter" OnClick="btnConnexion_Click" />

        </div>
            </form>
</body>
</html>
