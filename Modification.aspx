<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Modification.aspx.cs"
   Inherits="Modification"
   MasterPageFile="~/Modifier.master"%>

<asp:Content ID="content1" ContentPlaceHolderID="cpMod" runat="server">



<div id="divDVDs" runat="server">

                   <asp:Label runat="server" CssClass="titre">Infos du film à modifier</asp:Label>

            
            <br/>
            <br/>
            
           

        </div>

<asp:Label runat="server" CssClass="sCenter">Modification de DVD</asp:Label>
<br />
<br />
<br />
<br />

<asp:DropDownList ID="ddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
    <asp:ListItem Value="0">Sélectionner</asp:ListItem>
</asp:DropDownList>
       
<br />
<br />
       
<asp:PlaceHolder id="phDynamique" runat="server" />
    <br />
    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }
          table.center {
    margin-left:auto; 
    margin-right:auto;
  }
table{
    border: 1px solid black;
    table-layout: fixed;
    width: 400px;
}

th, td {
    border: 1px solid black;
    width: 300px;
}
.titre{
    color: #82a9bf; font-family: 'Trocchi', serif; font-size: 45px; font-weight: normal; line-height: 48px; margin: 0;text-align: center;
    text-shadow: 2px 3px 3px #333;
}

    </style>
<!--<asp:HyperLink runat="server" NavigateUrl="~/Ajout.aspx">Ajout</asp:HyperLink>-->

<asp:Button ID="btnModifier" runat="server" Text="Modifier" OnClick="btnModification_Click" />
    
</asp:Content>
