<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" CodeFile="AffichageDetaille.aspx.cs" Inherits="AffichageDetaille"%>

<asp:Content ID="contentListeDVD" ContentPlaceHolderID="cpBody" runat="server">
   <asp:Button runat="server" ID="btnPrecedent" OnClientClick="JavaScript:window.history.back(1); return false;" Text="Page précédente" autopostback="false"/> 
               <div id="divDVDs" runat="server">

                   <asp:Label runat="server" CssClass="titre">Infos du film </asp:Label>
                   <br/>
                   <asp:PlaceHolder id="phDynamique" runat="server" />

<!--<asp:HyperLink runat="server" NavigateUrl="~/Ajout.aspx">Ajout</asp:HyperLink>-->


                    <br/>
            
           

        </div>

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cpHead">
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
    
</asp:Content>
