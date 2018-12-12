using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using librairie_projet2;
/// <summary>
/// Description résumée de AffichageDetaille
/// </summary>
public partial class AffichageDetaille : System.Web.UI.Page
{
    public libProjet2 lib = new libProjet2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
                ViewState["URLPrecedent"] = Request.UrlReferrer.ToString();
        }

        if ((Request.QueryString["id"] != null))
        {

            string strNoFilm = "";
            string strRequeteInfo = "SELECT Films.ImagePochette, Films.NoFilm, Films.AnneeSortie, Utilisateurs.NomUtilisateur, Films.Resume, Films.TitreFrancais,Films.TitreOriginal,Categories.Description AS Categorie, Producteurs.Nom, Realisateurs.Nom, Formats.Description, Films.DateMAJ, Films.VersionEtendue FROM Films inner join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur inner join Formats ON Films.Format = Formats.NoFormat inner join Realisateurs on Films.NoRealisateur = Realisateurs.NoRealisateur inner join Categories on Films.Categorie = Categories.NoCategorie inner join Producteurs on Films.NoProducteur = Producteurs.NoProducteur WHERE Films.NoFilm=@id ORDER BY Films.TitreFrancais";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

            con.Open();
            SqlCommand sqlComFilm = new SqlCommand(strRequeteInfo, con);
            sqlComFilm.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComFilm.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataReader sqlReader = sqlComFilm.ExecuteReader();
            StringBuilder html = new StringBuilder();

            //Table start.

            List<String> lstTitre = new List<String>();
            lstTitre.Add("No. du Film");
            lstTitre.Add("Année de sortie");
            lstTitre.Add("Nom d'utilisateur du locataire");
            lstTitre.Add("Résumé");
            lstTitre.Add("Titre en français");
            lstTitre.Add("Titre original");
            lstTitre.Add("Catégorie");
            lstTitre.Add("Producteur");
            lstTitre.Add("Réalisateur");
            lstTitre.Add("Format");
            lstTitre.Add("Date de mise à jour");
            lstTitre.Add("Version étendue");


            while (sqlReader.Read())
            {
                lib.img(divDVDs, "imgID", "~/imagespochette/" + sqlReader.GetValue(0).ToString(), "");
                html.Append("<table border = '1' class='center'>");
                for (int i = 1; i < sqlReader.FieldCount; i++)
                {
                    html.Append("<tr>");
                    html.Append("<td>");
                    html.Append(lstTitre[i - 1]);
                    if (i - 1 == 0)
                    {
                        strNoFilm = sqlReader.GetValue(i).ToString();
                    }
                    html.Append("</td>");
                    html.Append("<td>");
                    html.Append(sqlReader.GetValue(i));
                    html.Append("</td>");
                    html.Append("</tr>");
                }


            }

            html.Append("</table>");
            con.Close();

            //Table end.
            divDVDs.Controls.Add(new Literal { Text = html.ToString() });
            lib.brDYN(divDVDs);
            lib.lblDYN(divDVDs, "lblTitreActeurs", "Acteurs", "titre");

            //Acteurs
            string strRequeteActeur = "SELECT Acteurs.Nom FROM Acteurs INNER JOIN FilmsActeurs ON Acteurs.NoActeur = FilmsActeurs.NoActeur WHERE FilmsActeurs.NoFilm=@id";
            SqlConnection con2 = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

            con2.Open();
            SqlCommand sqlComActeur = new SqlCommand(strRequeteActeur, con2);
            sqlComActeur.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComActeur.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataReader sqlReader2 = sqlComActeur.ExecuteReader();
            StringBuilder html2 = new StringBuilder();
            while (sqlReader2.Read())
            {
                html2.Append("<table border = '1' class='center'>");
                for (int i = 0; i < sqlReader2.FieldCount; i++)
                {
                    html2.Append("<tr>");
                    html2.Append("<td>");
                    html2.Append("Nom de l'acteur");
                    html2.Append("</td>");
                    html2.Append("<td>");
                    html2.Append(sqlReader2.GetValue(i));
                    html2.Append("</td>");
                    html2.Append("</tr>");
                }

            }
            html2.Append("</table>");
            con2.Close();
            divDVDs.Controls.Add(new Literal { Text = html2.ToString() });
            lib.brDYN(divDVDs);
            lib.lblDYN(divDVDs, "lblTitreSupplements", "Supplements", "titre");
            //Supplements
            string strRequeteSupplements = "SELECT Supplements.Description FROM Supplements INNER JOIN FilmsSupplements ON Supplements.NoSupplement = FilmsSupplements.NoSupplement WHERE FilmsSupplements.NoFilm=@id";
            SqlConnection con3 = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

            con3.Open();
            SqlCommand sqlComSupplements = new SqlCommand(strRequeteSupplements, con3);
            sqlComSupplements.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComSupplements.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataReader sqlReader3 = sqlComSupplements.ExecuteReader();
            StringBuilder html3 = new StringBuilder();
            while (sqlReader3.Read())
            {
                html3.Append("<table border = '1' class='center'>");
                for (int i = 0; i < sqlReader3.FieldCount; i++)
                {
                    html3.Append("<tr>");
                    html3.Append("<td>");
                    html3.Append(sqlReader3.GetValue(i));
                    html3.Append("</td>");
                    html3.Append("</tr>");
                }

            }
            html3.Append("</table>");
            con3.Close();
            divDVDs.Controls.Add(new Literal { Text = html3.ToString() });
            

            //Locataire
            string strLoc = "SELECT Utilisateurs.NomUtilisateur FROM Utilisateurs inner join EmpruntsFilms ON Utilisateurs.NoUtilisateur = EmpruntsFilms.NoUtilisateur WHERE CONVERT(varchar(8),EmpruntsFilms.NoExemplaire) LIKE \'" + strNoFilm+"%\' ";
            SqlConnection sqlConLoc = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            sqlConLoc.Open();
            SqlCommand sqlComLoc = new SqlCommand(strLoc, sqlConLoc);
            SqlDataReader sqlReaderLoc = sqlComLoc.ExecuteReader();
            StringBuilder htmlLoc = new StringBuilder();
            while (sqlReaderLoc.Read())
            {
                htmlLoc.Append("<table border = '1' class='center'>");
                for (int i = 0; i < sqlReaderLoc.FieldCount; i++)
                {
                    htmlLoc.Append("<tr>");
                    htmlLoc.Append("<td>");
                    htmlLoc.Append("Ce film est loué par " + sqlReaderLoc.GetValue(i));
                    htmlLoc.Append("</td>");
                    htmlLoc.Append("</tr>");
                }
            }
            htmlLoc.Append("</table>");
            divDVDs.Controls.Add(new Literal { Text = htmlLoc.ToString() });


            lib.brDYN(divDVDs);
            lib.lblDYN(divDVDs, "lblTitreLangues", "Langues", "titre");
            //Langues

            string strRequeteLangues = "SELECT Langues.Langue FROM Langues INNER JOIN FilmsLangues ON Langues.NoLangue = FilmsLangues.NoLangue WHERE FilmsLangues.NoFilm=@id";
            SqlConnection con4 = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

            con4.Open();
            SqlCommand sqlComLangues = new SqlCommand(strRequeteLangues, con4);
            sqlComLangues.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComLangues.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataReader sqlReader4 = sqlComLangues.ExecuteReader();
            StringBuilder html4 = new StringBuilder();
            while (sqlReader4.Read())
            {
                html4.Append("<table border = '1' class='center'>");
                for (int i = 0; i < sqlReader4.FieldCount; i++)
                {
                    html4.Append("<tr>");
                    html4.Append("<td>");
                    html4.Append("Ce film peut être en " + sqlReader4.GetValue(i));
                    html4.Append("</td>");
                    html4.Append("</tr>");
                }

            }
            html4.Append("</table>");
            con4.Close();
            divDVDs.Controls.Add(new Literal { Text = html4.ToString() });
            //Sous-titres

            string strRequeteST = "SELECT SousTitres.LangueSousTitre FROM SousTitres INNER JOIN FilmsSousTitres ON SousTitre.NoSousTitre = FilmsSousTitres.NoSousTitre WHERE FilmsSousTitres.NoFilm=@id";
            SqlConnection con5 = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

            con4.Open();
            SqlCommand sqlComST = new SqlCommand(strRequeteST, con5);
            sqlComST.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComST.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataReader sqlReader5 = sqlComLangues.ExecuteReader();
            Label lblST = new Label();
            while (sqlReader5.Read())
            {
                lblST.Text = "Ce film a des sous-titres en: ";
                for (int i = 0; i < sqlReader5.FieldCount; i++)
                {
                    lblST.Text += sqlReader5.GetValue(i) + " ";

                }

            }

            con5.Close();
            divDVDs.Controls.Add(lblST);
            lib.btnDYN(divDVDs, "btnAppropriation", "Voulez-vous emprunter ce DVD?", "",btnAppropriation);

        }
    }
    protected void btnAppropriation(object sender, EventArgs e)
    {
        string strUpdate1 = "UPDATE Films SET Films.DateMAJ="+DateTime.Now + " AND Films.NoUtilisateurMAJ="+ Session["NoU"] + " WHERE Films.NoFilm="+ Convert.ToInt32(Request.QueryString["id"]);
        SqlConnection sqlConUpdate = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
        sqlConUpdate.Open();
        SqlCommand sqlCommandUpdate = new SqlCommand(strUpdate1, sqlConUpdate);
        sqlCommandUpdate.ExecuteNonQuery();
    }

    protected void btnPrecedent_Click(object sender, EventArgs e)
    {
        object urlPrec = ViewState["URLPrecedent"];
        if (urlPrec != null)
            Response.Redirect((string)urlPrec);

    }
}