<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Ajout.aspx.cs"
   Inherits="Ajout"
   MasterPageFile="~/Modifier.master"%>

 <%-- <asp:Content ID="contentAjout" ContentPlaceHolderID="cpMod" runat="server">
    <asp:Button ID="abrege" runat="server" OnClick="linkButton" Text="Mode abrégé" />
    <asp:Button ID="complet" runat="server" OnClick="linkButton" Text="Mode complet" />
    <br />
    <br /> 

     <asp:Label runat="server"> </asp:Label>  
     <asp:Label runat="server" CssClass="sCenter">Ajout de DVD</asp:Label>
       <br />
       <br />

       <asp:PlaceHolder id="phDynamique" runat="server" />
     </asp:Content>
 --%>

<asp:Content ID="content1" ContentPlaceHolderID="cpMod" runat="server">



<div id="divDVDs" runat="server">

   <asp:Button ID="abrege" runat="server" OnClick="linkButton" Text="Mode abrégé" />
    <asp:Button ID="complet" runat="server" OnClick="linkButton" Text="Mode complet" />
    <br />

                   <asp:Label runat="server" CssClass="titre">Infos du film à ajouter</asp:Label>

            
            <br/>
            <br/>
            
           

        </div>

<asp:Label runat="server" CssClass="sCenter">Ajout de DVD</asp:Label>
<br />
<br />
<br />
<br />
       
<br />
<br />
       
<asp:PlaceHolder id="phDynamique" runat="server" />
    <br />
    <style type="text/css">
       .btnEfface {
          height: 20px;
       }

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

<%-- <asp:Button ID="btnSupprimer" runat="server" Text="Supprimer" OnClick="btnSuppression_Click" OnClientClick="return confirm('Voulez vous vraiment l'effacer?');" /> --%>
    
</asp:Content>