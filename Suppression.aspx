<%@ Page Language="C#" Trace="false" Debug="true"
   CodeFile="~/Suppression.aspx.cs"
   Inherits="Suppression" MasterPageFile="~/Modifier.master"%>

<asp:Content ID="content1" ContentPlaceHolderID="cpMod" runat="server">
   <div id="divDVDs" runat="server">



            <table>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4422
                    </td>
                </tr>
                <tr>
                   
                    <td colspan="2">
                    Titre Francais:  
                    </td>
                    <td>
                    DERP
                    </td>
                </tr>
                <tr>
                    
                    <td colspan="2">
                    Titre Original:  
                    </td>
                    <td>
                    DERP
                    </td>
                </tr>
                <tr>
                    
                    <td colspan="2">
                    Producteur:
                    </td>
                    <td>
                    mkk
                    </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    Réalisateur:
                    </td>
                    <td >
                    blablabla
                    </td>
                    </tr>
                    <tr>
                    
                    <td colspan="2">
                    Acteurs:
                    </td>
                    <td>
                    Der,hjhjh,akskaka
                    </td>
                    </tr>
                    <tr>
                    
                    <td colspan="2">
                    Nombre de disques:
                    </td>
                    <td class="auto-style1">
                    1
                    </td>
                    </tr>
                    <tr>
                    
                    <td colspan="2">
                    Format:
                    </td>
                    <td>
                    Blu-Ray
                    </td>
                    </tr>
                <tr>
                
                    <td colspan="2">
                    Durée:
                    </td>
                    <td>
                    199999 minutes
                    </td>
                    </tr>
                <tr>
                    
                    <td colspan="2">Appartient à:</td>
                    <td >Bob Ross</td>
                    </tr>
                <tr>
                    
                    <td colspan="2">
                    Résumé:
                    </td>
                    <td>
                    Lalilulelo...Lalilulelo...Lalilulelo...Lalilulelo...Lalilulelo...Lalilulelo...
                    </td>
                    </tr>
                    
            </table>
            <br/>
            <br/>
            
           

        </div>

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

<!--<asp:HyperLink runat="server" NavigateUrl="~/Ajout.aspx">Ajout</asp:HyperLink>-->

</asp:Content>