<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" CodeFile="Liste.aspx.cs" Inherits="_Default"%>


<script runat="server">
    int noRangee = 0;
</script>


<asp:Content ContentPlaceHolderID="cpTitle" runat="server">
    Liste des disques de tous

</asp:Content>

<asp:Content ContentPlaceHolderID="cpBar" runat="server">
   <!--Barre de recherche-->
        <div class="container">
            <div class="row">    
                <div class="col-xs-8 col-xs-offset-2">
		            <div class="input-group">
                        <div class="input-group-btn search-panel">
                            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    	        <span id="search_concept">Recherche par nom</span> <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                              <li><a href="#contains">Toutes les catégories</a></li>
                              <li><a href="#its_equal">It's equal</a></li>
                              <li><a href="#greather_than">Greather than ></a></li>
                              <li><a href="#less_than">Less than < </a></li>
                              <li class="divider"></li>
                              <li><a href="#all">Anything</a></li>
                            </ul>
                        </div>
                        <input type="hidden" name="search_param" value="all" id="search_param"/>     
                        <input type="text" class="form-control" name="x" placeholder="Terme de recherche..."/>
                        <span class="input-group-btn">
                            <button class="btn btn-default" type="button"><span class="glyphicon glyphicon-search"></span></button>
                        </span>
                    </div>
                </div>
	        </div>
        </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="typeAffichageDVD" runat="server">
   <div class="menu">
      <div class="main-container">
         <div class="fixer-container">
            <ul>
               <li class="first_menu"><asp:LinkButton ID="Tous" runat="server" OnClick="linkButton">Tous les utilisateurs</asp:LinkButton></li>
               <li class="other_menu"><asp:LinkButton ID="moi" runat="server" OnClick="linkButton">DVD en mains</asp:LinkButton></li>
               <li class="other_menu"><asp:LinkButton ID="autre" runat="server" OnClick="linkButton" OnClientClick="jsPrompt();">Pour un autre utilisateur</asp:LinkButton></li>
            </ul>
         </div>
      </div>
   </div>

   <asp:ScriptManager ID="smHid" runat="server"></asp:ScriptManager>
   <asp:HiddenField ID="hidUtilisateur" Value="" runat="server" />
   
   <script type="text/javascript">
      function jsPrompt() {
         //console.log(document.getElementById('%= hidUtilisateur.ClientID %>*/').value);
         var utilisateur = prompt("Please enter your name:", "Veillez entrer le nom d'utilisateur");

         if (utilisateur == null || utilisateur == "" || utilisateur == "Veillez entrer le nom d'utilisateur") {
            b('<%= hidUtilisateur.ClientID %>', "");
            alert("Vous n'avez rien entrer");
          }
         else {
            b('<%= hidUtilisateur.ClientID %>', utilisateur.toString());
            //alert("Vous avez entrer " + utilisateur);
         }

         //b('hidUtilisateur', utilisateur.toString());
      }
   </script>
</asp:Content>

<asp:Content ID="contentListeDVD" ContentPlaceHolderID="cpBody" runat="server">
   
              <asp:Panel runat="server" ID="divDVDs">

                   <br />
                   <asp:Button ID="btn10Items" runat="server" OnClick="btn10Items_Click" Text="Afficher 10 objets par page" />
                   <br />
                   <asp:Button ID="btn15Items" runat="server" OnClick="btn15Items_Click" Text="Afficher 15 objets par page" />
                   <br />

                 <asp:DataGrid ID="dgTable" runat="server"
                HeaderStyle-ForeColor="Black"
                HeaderStyle-BackColor="#CCCCCC"
                HeaderStyle-Font-Bold="true"
                CellPadding="3" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                     AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dgTable_PageIndexChanged" AllowSorting="True" OnSortCommand="dgTable_SortCommand" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" ShowFooter="True" >   
                     <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                     <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                     <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                     <PagerStyle HorizontalAlign="Right" Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                     <AlternatingItemStyle BackColor="#F7F7F7" />
               <Columns>
                   
                   <asp:TemplateColumn>
                    <ItemTemplate>
                    
               <%# Container.DataSetIndex+1 %>

                    </ItemTemplate>

                   </asp:TemplateColumn>
                   
                   <asp:TemplateColumn HeaderText="Film" SortExpression="Utilisateurs.NomUtilisateur, Films.TitreFrancais" ><ItemTemplate><asp:Image ID="imagePochette" runat="server" ImageURL='<%# !Eval("ImagePochette").Equals("") ? String.Format("~/imagespochette/{0}",DataBinder.Eval(Container.DataItem, "ImagePochette")):"~/imagespochette/null.png" %>' Height="261" Width="140" /></ItemTemplate></asp:TemplateColumn>
                   
                   <asp:HyperLinkColumn DataNavigateUrlField="NoFilm" DataNavigateUrlFormatString="AffichageDetaille.aspx?id={0}" DataTextField="TitreFrancais" HeaderText="Titre Français" SortExpression="TitreFrancais"></asp:HyperLinkColumn>
                   <asp:HyperLinkColumn DataNavigateUrlField="NoUtilisateur" DataNavigateUrlFormatString="ListeTous.aspx?id={0}" DataTextField="NomUtilisateur" HeaderText="Locataire" SortExpression="NomUtilisateur"></asp:HyperLinkColumn>
                   <asp:TemplateColumn HeaderText="Actions">
                       <ItemTemplate>
                           <asp:HyperLink Visible='<%# Eval("NoUtilisateur").ToString().Equals(Session["NoU"].ToString()) ? false : true %>' ID="courriel" runat="server" NavigateUrl='<%# Eval("NoUtilisateur","~/Courriel.aspx?id={0}") %>'>Courriel</asp:HyperLink>
                           <br />
                           <asp:HyperLink Visible='<%# Eval("NoUtilisateur").ToString().Equals(Session["NoU"].ToString()) ? true : false %>' ID="mod" runat="server" NavigateUrl='<%# Eval("NoFilm","~/Modification.aspx?id={0}") %>'>Modification</asp:HyperLink>
                           <br />
                           <asp:HyperLink Visible='<%# Eval("NoUtilisateur").ToString().Equals(Session["NoU"].ToString()) ? true : false %>' ID="sup" runat="server" NavigateUrl='<%# Eval("NoFilm","~/Suppression.aspx?id={0}") %>'>Suppression</asp:HyperLink>
                           <br />
                           <asp:HyperLink Visible='<%# Eval("NoUtilisateur").ToString().Equals(Session["NoU"].ToString()) ? false : true %>' ID="emprunt" runat="server" NavigateUrl='<%# Eval("NoFilm","~/Appropriation.aspx?id={0}") %>'>Emprunter</asp:HyperLink>
                       </ItemTemplate>
                   </asp:TemplateColumn>
                   
                   
                   
                   
                   
                   
               </Columns>
                     <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                 </asp:DataGrid>

            
            <br/>
                  
                  <asp:Table runat="server"  GridLines="Both" HorizontalAlign="Center" CellSpacing="3" 
      CellPadding="3">
                      <asp:TableRow runat="server">
                          <asp:TableCell runat="server">
                              <asp:Button ID="btnPremier" runat="server" Text="<<" CssClass="" OnClick="btnPremier_Click" />
                          </asp:TableCell>
                          <asp:TableCell runat="server">
                              <asp:Button ID="btnPrecedent" runat="server" Text="<" OnClick="btnPrecedent_Click" />
                          </asp:TableCell>
                          <asp:TableCell runat="server">
                              <asp:Button ID="btnSuivant" runat="server" Text=">" OnClick="btnSuivant_Click" />
                          </asp:TableCell>
                          <asp:TableCell runat="server">
                              <asp:Button ID="btnDernier" runat="server" Text=">>" CssClass="" OnClick="btnDernier_Click" />
                          </asp:TableCell>
                      </asp:TableRow>
                  </asp:Table>
                  
                  
            <br/>
            
           

        </asp:Panel>

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cpHead">
    </asp:Content>
