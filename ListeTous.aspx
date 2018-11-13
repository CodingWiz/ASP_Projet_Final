<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" CodeFile="Liste.aspx.cs" Inherits="_Default"%>


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
                    	        <span id="search_concept">Toutes les catégories</span> <span class="caret"></span>
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
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4423
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
                    <asp:Button ID="Button1" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr><tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4424
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
                    <asp:Button ID="Button2" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                 <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4425
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
                    <asp:Button ID="Button3" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4426
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
                    <asp:Button ID="Button4" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4427
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
                    <asp:Button ID="Button5" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4428
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
                <tr><td></td></tr>
                <tr>
                   
                    <td colspan="3">
                    <asp:Button ID="Button6" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4429
                    </td>
                </tr>
                <tr><td></td></tr>
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
                    <asp:Button ID="Button7" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4430
                    </td>
                </tr>
                <tr><td></td></tr>
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
                    <asp:Button ID="Button8" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4431
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
                    <asp:Button ID="Button9" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
                    </td>
                    
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td >
                    <asp:Image runat="server" ImageUrl="~/imagespochette/r5lldc1bq1bz.jpg" Height="210px" Width="164px" />

                    </td>
                    <td>
                    No:
                    </td>
                    <td >
                        4432
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
                    <asp:Button ID="Button10" Text="Pour plus d'informations, cliquez ici" runat="server" OnClick="btnAff_Click"  />
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
