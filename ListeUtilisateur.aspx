<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" CodeFile="Liste.aspx.cs" Inherits="_Default"%>



<asp:Content ContentPlaceHolderID="cpTitle" runat="server">
    Liste des disques de l'utilisateur 1

</asp:Content>
<asp:Content ID="contentListeDVD" ContentPlaceHolderID="cpBody" runat="server">
   
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
                   
                    <td colspan="3">
                    <asp:Button ID="btnAff" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                
                    
            </table>
            <br/>
            <br/>
            
           

        </div>

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cpHead">
    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }
    </style>
</asp:Content>
