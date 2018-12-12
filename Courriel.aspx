<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" %>
<%@ Import Namespace="System.Data.SqlClient" %>


<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Request.UrlReferrer!=null)
            ViewState["URLPrecedent"] = Request.UrlReferrer.ToString();
        }
    }

    protected void btnPrecedent_Click(object sender, EventArgs e)
    {
         object urlPrec = ViewState["URLPrecedent"];
        if (urlPrec != null)
            Response.Redirect((string)urlPrec);

    }

    protected void btnEnvoyer_Click(object sender, EventArgs e)
    {
        if(sujetFieldValidator.IsValid && CustomValidator1.IsValid && RequiredFieldValidator1.IsValid && RequiredFieldValidator2.IsValid)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openwindow", "alert('Le message a été envoyé')", true);
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strRequete = "select COUNT(Utilisateurs.Courriel) FROM Utilisateurs WHERE Utilisateurs.Courriel=@email";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
        con.Open();
        SqlCommand sqlComU = new SqlCommand(strRequete, con);
        SqlParameter paramU = new SqlParameter("@email",tbAdresse.Text);
        sqlComU.Parameters.Add(paramU);
        var dyn = sqlComU.ExecuteScalar();
        if ((int)dyn < 1)
        {
            args.IsValid = false;
        }
    }
</script>


<asp:Content ContentPlaceHolderID="cpTitle" runat="server">
    Courriel
</asp:Content>

<asp:Content ContentPlaceHolderID="cpBar" runat="server">
    <asp:Button runat="server" ID="btnPrecedent" OnClick="btnPrecedent_Click" Text="Page précédente" autopostback="false"/>
   <h1>Courriel</h1>

   <table border="1" runat="server" id="table" style="border: 3px solid #000000; border-spacing: 4px; padding: 2px; margin: 2px; line-height: normal">
       <tr>   
       <td style="padding: 2px;border-color: #000000; border-style: solid; background-color: #3399FF; color: #F4F4F4;">Adresse du récipient</td>
         <td style="padding: 2px;border-color: #000000; border-style: solid" class="auto-style1">
             <asp:TextBox ID="tbAdresse" runat="server" Width="492px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="tbAdresse"  runat="server" ErrorMessage="L'email doit avoir un récipient." EnableClientScript="False"></asp:RequiredFieldValidator>
             <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="tbAdresse" ErrorMessage="L'adresse du récipient n'est pas valide." OnServerValidate="CustomValidator1_ServerValidate" EnableClientScript="False"></asp:CustomValidator>
         </td>
         
        </tr>
       <tr>
           <td style="padding: 2px;border-color: #000000; border-style: solid; background-color: #3399FF; color: #E5E5E5;">
               Sujet   
           </td>
         <td style="padding: 2px; border-color: #000000; border-style: solid;">
            <asp:TextBox ID="tbSujet" runat="server" Width="491px" OnLoad="Page_Load"></asp:TextBox>
             <asp:RequiredFieldValidator ID="sujetFieldValidator" ControlToValidate="tbSujet" runat="server" ErrorMessage="L'email doit avoir un sujet." EnableClientScript="False"></asp:RequiredFieldValidator>
         </td>
           </tr>
       <tr>
           <td style="padding: 2px;border-color: #000000; border-style: solid; background-color: #3399FF; color: #E9E9E9;">Contenu</td>
         <td style="padding: 2px;border-color: #000000; border-style: solid">
             <asp:TextBox ID="tbContenu" runat="server" TextMode="MultiLine" Height="200px" Width="500px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tbContenu" runat="server" ErrorMessage="L'email ne peut pas être vide." EnableClientScript="False"></asp:RequiredFieldValidator>
         </td>
      </tr>
   </table>
    <br />
    <asp:Button runat="server" ID="btnEnvoyer" Text="Envoyer" OnClick="btnEnvoyer_Click" OnClientClick="alert('Le message a été envoyé')" />
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cpHead">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
