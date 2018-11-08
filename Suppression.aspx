<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Suppression.aspx.cs"
   Inherits="Suppression" MasterPageFile="~/MasterPage1.master"%>

<asp:Content ID="content1" ContentPlaceHolderID="contentPlaceHolderMain" runat="server">

<asp:Label runat="server" CssClass="sCenter">Suppression de DVD</asp:Label>
<br />
<br />
<br />
<br />

<asp:DropDownList ID="ddl" runat="server">
<asp:ListItem Text="C'est l'apocalypse" Value="1" Selected="True" />
</asp:DropDownList>
       
<br />
<br />
       
<asp:PlaceHolder id="phDynamique" runat="server" />

<asp:HyperLink runat="server" NavigateUrl="~/Ajout.aspx">Ajout</asp:HyperLink>

</asp:Content>