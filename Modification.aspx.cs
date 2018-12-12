using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using librairie_projet2;

public partial class Modification : System.Web.UI.Page
{
   public libProjet2 librairie = new libProjet2();
   public libProjet2 lib = new libProjet2();

   void Page_Load()
   {
      SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      con.Open();

      SqlCommand sqlComDDL = new SqlCommand("SELECT Films.NoFilm, Films.TitreFrancais FROM Films order by Films.TitreFrancais", con);
      SqlDataReader sqlReaderDDL = sqlComDDL.ExecuteReader();
      while (sqlReaderDDL.Read())
      {
         ListItem li = new ListItem();
         li.Text = sqlReaderDDL.GetValue(1).ToString();
         li.Value = sqlReaderDDL.GetValue(0).ToString();
         ddl.Items.Add(li);
      }
      sqlReaderDDL.Close();

      if (Request.QueryString["id"] != null)
      {
         string strResume = "", strImagePochette = "", strTitreFr = "", strTitreOriginal = "", strNomU = "", strCategorie = "", strPoducteur = "", strRealisateur = "", strFormat = "", strVersioneEtendue = "", strDateMAJ = "", strAnneeSortie = "";
         Int32 intNoFilm = 0, intAnneeSortie = 0;

         List<string> lsActeurNom = new List<string>(), lsSupplement = new List<string>(), lsLangue = new List<string>(), lsSousTitre = new List<string>();

         SqlCommand cmdRequeteInfo = new SqlCommand("SELECT Films.ImagePochette as ImagePochette, Films.NoFilm as NoFilm, Films.AnneeSortie as AnneeSortie, " +
                  "Utilisateurs.NomUtilisateur as NomUtilisateur, Films.Resume as Resume, Films.TitreFrancais as TitreFrancais, Films.TitreOriginal as TitreOriginal, " +
                  "Categories.Description AS Categorie, Producteurs.Nom as Producteur, Realisateurs.Nom as Realisateur, " +
                  "Formats.Description as Format, Films.DateMAJ as DateMAJ, Films.VersionEtendue as VersionEtendue FROM Films " +
                  "left join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur " +
                  "left join Formats ON Films.Format = Formats.NoFormat " +
                  "left join Realisateurs on Films.NoRealisateur = Realisateurs.NoRealisateur " +
                  "left join Categories on Films.Categorie = Categories.NoCategorie " +
                  "left join Producteurs on Films.NoProducteur = Producteurs.NoProducteur WHERE Films.NoFilm = @id ORDER BY Films.TitreFrancais", con);
         cmdRequeteInfo.Parameters.Add("@id", SqlDbType.NVarChar);
         cmdRequeteInfo.Parameters["@id"].Value = Request.QueryString["id"];
         SqlDataReader reader = cmdRequeteInfo.ExecuteReader();

         while (reader.Read())
         {
            strResume = reader["Resume"].ToString();
            strImagePochette = reader["ImagePochette"].ToString();
            strTitreFr = reader["TitreFrancais"].ToString();
            strTitreOriginal = reader["TitreOriginal"].ToString();
            strNomU = reader["NomUtilisateur"].ToString();
            strCategorie = reader["Categorie"].ToString();
            strPoducteur = reader["Producteur"].ToString();
            strRealisateur = reader["Realisateur"].ToString();
            strFormat = reader["Format"].ToString();
            strVersioneEtendue = reader["VersionEtendue"].ToString();
            strDateMAJ = reader["DateMAJ"].ToString();

            intNoFilm = int.Parse(reader["NoFilm"].ToString());
            //int temp;
            //intAnneeSortie = /*(reader["AnneeSortie"].ToString() != "NULL" ? (int.TryParse(reader["AnneeSortie"].ToString(), out temp)==true ?*/ int.Parse(reader["AnneeSortie"].ToString())/* : 0) : 0)*/;
            strAnneeSortie = reader["AnneeSortie"].ToString();
         }
         reader.Close();

         cmdRequeteInfo = new SqlCommand("select Nom from FilmsActeurs inner join Acteurs on FilmsActeurs.NoActeur=Acteurs.NoActeur where NoFilm=@id", con);
         cmdRequeteInfo.Parameters.Add("@id", SqlDbType.NVarChar);
         cmdRequeteInfo.Parameters["@id"].Value = Request.QueryString["id"];
         reader = cmdRequeteInfo.ExecuteReader();

         while (reader.Read())
         {
            lsActeurNom.Add(reader["Nom"].ToString());
         }
         reader.Close();

         cmdRequeteInfo = new SqlCommand("select Supplements.Description as Description from FilmsSupplements inner join Supplements on FilmsSupplements.NoSupplement = Supplements.NoSupplement inner join Films on Films.NoFilm = FilmsSupplements.NoFilm where Films.NoFilm = @id", con);
         cmdRequeteInfo.Parameters.Add("@id", SqlDbType.NVarChar);
         cmdRequeteInfo.Parameters["@id"].Value = Request.QueryString["id"];
         reader = cmdRequeteInfo.ExecuteReader();

         while (reader.Read())
         {
            lsSupplement.Add(reader["Description"].ToString());
         }
         reader.Close();

         cmdRequeteInfo = new SqlCommand("select Langue from FilmsLangues inner join Langues on FilmsLangues.NoLangue = Langues.NoLangue inner join Films on Films.NoFilm = FilmsLangues.NoFilm where Films.NoFilm = @id", con);
         cmdRequeteInfo.Parameters.Add("@id", SqlDbType.NVarChar);
         cmdRequeteInfo.Parameters["@id"].Value = Request.QueryString["id"];
         reader = cmdRequeteInfo.ExecuteReader();

         while (reader.Read())
         {
            lsLangue.Add(reader["Langue"].ToString());
         }
         reader.Close();

         cmdRequeteInfo = new SqlCommand("select LangueSousTitre from FilmsSousTitres  inner join SousTitres on FilmsSousTitres.NoSousTitre = SousTitres.NoSousTitre inner join Films on Films.NoFilm = FilmsSousTitres.NoFilm where Films.NoFilm = @id", con);
         cmdRequeteInfo.Parameters.Add("@id", SqlDbType.NVarChar);
         cmdRequeteInfo.Parameters["@id"].Value = Request.QueryString["id"];
         reader = cmdRequeteInfo.ExecuteReader();

         while (reader.Read())
         {
            lsSousTitre.Add(reader["LangueSousTitre"].ToString());
         }
         reader.Close();

         DataTable dataTable;
         SqlDataAdapter adapter;

         // Main
         string strNoFilm = "";

         librairie.lblDYN(phDynamique, "lblTitreFr", strTitreFr, "titre");
         librairie.brDYN(phDynamique);

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

         DropDownList ddlImagePochette = librairie.ddlDYN(phDynamique, "ddlImagePochette");
         ddlImagePochette.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoFilm, ImagePochette from Films", con);
         adapter.Fill(dataTable);
         ddlImagePochette.DataSource = dataTable;
         ddlImagePochette.DataValueField = "NoFilm";
         ddlImagePochette.DataTextField = "ImagePochette";
         ddlImagePochette.DataBind();
         ddlImagePochette.Items.Insert(0, new ListItem("Aucune", "NULL"));

         ddlImagePochette.SelectedItem.Text = strImagePochette;

         phDynamique.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         for (int i = 0; i < 12; i++)
         {
            //if (i - 1 == 0)
            //{
            //   strNoFilm = sqlReader.GetValue(i).ToString();
            //}

            phDynamique.Controls.Add(new Literal { Text = "<tr>" });
            phDynamique.Controls.Add(new Literal { Text = "<td>" });
            phDynamique.Controls.Add(new Literal { Text = lstTitre[i] });
            phDynamique.Controls.Add(new Literal { Text = "</td>" });
            phDynamique.Controls.Add(new Literal { Text = "<td>" });
            Panel pnl = librairie.divDYN(phDynamique, "pnlCompletMain" + i);
            phDynamique.Controls.Add(new Literal { Text = "</td>" });
            phDynamique.Controls.Add(new Literal { Text = "</tr>" });

            switch (i)
            {
               case 0:
                  TextBox tbNoFilm = librairie.tbDYN(pnl, "tbNoFilm", "", "10", "sCenter");

                  SqlCommand cmd = new SqlCommand("select top 1 NoFilm from Films order by NoFilm desc", con);
                  tbNoFilm.Text = intNoFilm.ToString();
                  tbNoFilm.Enabled = false; break;
               case 1:
                  /*TextBox tbAnneeDeSortie = librairie.tbDYN(pnl, "tbAnneeDeSortie", intAnneeSortie.ToString(), "10", "sCenter");
                  tbAnneeDeSortie.Attributes.Add("placeholder", "Titre en français");*/
                  DropDownList tbAnneeDeSortie = librairie.ddlDYN(pnl, "tbAnneeDeSortie");
                  tbAnneeDeSortie.Items.Insert(0, new ListItem("Aucune", "NULL"));

                  int j = 1;
                  for (int m = int.Parse(DateTime.Now.ToString("yyyy")); m >= 1870; m--)
                  {
                     tbAnneeDeSortie.Items.Insert(j++, new ListItem(m.ToString(), m.ToString()));
                  }

                  tbAnneeDeSortie.SelectedItem.Text = strAnneeSortie;

                  break;
               case 2:
                  TextBox tbNomU = librairie.tbDYN(pnl, "tbNomU", strNomU, "50", "sCenter");
                  tbNomU.Enabled = false; break;
               case 3:
                  TextBox tbResume = librairie.tbDYN(pnl, "tbResume", strResume, "500", "sCenter");
                  tbResume.TextMode = TextBoxMode.MultiLine;
                  tbResume.Attributes.Add("placeholder", "Titre en français"); break;
               case 4:
                  TextBox tbTitreFr = librairie.tbDYN(pnl, "tbTitreFr", strTitreFr, "50", "sCenter");
                  tbTitreFr.Attributes.Add("placeholder", "Titre en français"); break;
               case 5:
                  TextBox tbTitreOriginal = librairie.tbDYN(pnl, "tbTitreOriginal", strTitreOriginal, "50", "sCenter");
                  tbTitreOriginal.Attributes.Add("placeholder", "Titre en français"); break;
               case 6:
                  DropDownList ddlCategorie = librairie.ddlDYN(pnl, "ddlCategorie");
                  ddlCategorie.Items.Clear();
                  dataTable = new DataTable();
                  adapter = new SqlDataAdapter("select NoCategorie, Description from Categories", con);
                  adapter.Fill(dataTable);
                  ddlCategorie.DataSource = dataTable;
                  ddlCategorie.DataValueField = "NoCategorie";
                  ddlCategorie.DataTextField = "Description";
                  ddlCategorie.DataBind();
                  ddlCategorie.Items.Insert(0, new ListItem("Aucune", "NULL"));

                  ddlCategorie.SelectedItem.Text = strCategorie; break;
               case 7:
                  DropDownList ddlProducteur = librairie.ddlDYN(pnl, "ddlProducteur");
                  ddlProducteur.Items.Clear();
                  dataTable = new DataTable();
                  adapter = new SqlDataAdapter("select NoProducteur, Nom from Producteurs", con);
                  adapter.Fill(dataTable);
                  ddlProducteur.DataSource = dataTable;
                  ddlProducteur.DataValueField = "NoProducteur";
                  ddlProducteur.DataTextField = "Nom";
                  ddlProducteur.DataBind();
                  ddlProducteur.Items.Insert(0, new ListItem("Aucun(e)", "NULL"));

                  ddlProducteur.SelectedItem.Text = strPoducteur; break;
               case 8:
                  DropDownList ddlRealisateur = librairie.ddlDYN(pnl, "ddlRealisateur");
                  ddlRealisateur.Items.Clear();
                  dataTable = new DataTable();
                  adapter = new SqlDataAdapter("select NoRealisateur, Nom from Realisateurs", con);
                  adapter.Fill(dataTable);
                  ddlRealisateur.DataSource = dataTable;
                  ddlRealisateur.DataValueField = "NoRealisateur";
                  ddlRealisateur.DataTextField = "Nom";
                  ddlRealisateur.DataBind();
                  ddlRealisateur.Items.Insert(0, new ListItem("Aucun(e)", "NULL"));

                  ddlRealisateur.SelectedItem.Text = strRealisateur; break;
               case 9:
                  DropDownList ddlFormat = librairie.ddlDYN(pnl, "ddlFormat");
                  ddlFormat.Items.Clear();
                  dataTable = new DataTable();
                  adapter = new SqlDataAdapter("select NoFormat, Description from Formats", con);
                  adapter.Fill(dataTable);
                  ddlFormat.DataSource = dataTable;
                  ddlFormat.DataValueField = "NoFormat";
                  ddlFormat.DataTextField = "Description";
                  ddlFormat.DataBind();
                  ddlFormat.Items.Insert(0, new ListItem("Aucun", "NULL"));

                  ddlFormat.SelectedItem.Text = strFormat; break;
               case 10:
                  TextBox tbDateMAJ = librairie.tbDYN(pnl, "tbDateMAJ", strDateMAJ, "50", "sCenter");
                  tbDateMAJ.Enabled = false; break;
               case 11:
                  DropDownList ddlVersionEtendue = librairie.ddlDYN(pnl, "ddlVersionEtendue");
                  ddlVersionEtendue.Items.Clear();
                  ddlVersionEtendue.Items.Insert(0, new ListItem("Aucune", "NULL"));
                  ddlVersionEtendue.Items.Insert(1, new ListItem("Oui", "1"));
                  ddlVersionEtendue.Items.Insert(2, new ListItem("Non", "0"));

                  ddlVersionEtendue.SelectedItem.Text = (strVersioneEtendue == "0" ? "Non" : (strVersioneEtendue == "1" ? "Oui" : "Aucune")); break;
            }
         }
         phDynamique.Controls.Add(new Literal { Text = "</table>" });

         librairie.brDYN(phDynamique);

         // Acteur
         librairie.lblDYN(phDynamique, "lblTitreActeurs", "Acteur(s)", "titre");

         Panel pnlActeur = librairie.divDYN(phDynamique, "pnlActeur");

         pnlActeur.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         pnlActeur.Controls.Add(new Literal { Text = "<tr>" });
         pnlActeur.Controls.Add(new Literal { Text = "<td>" });
         //ImageButton imgBtnEfface = librairie.imgBtn(pnlActeur, "imgBtnEfface", "../imagesPerso/efface_desactive.png", "btnEfface center", imgBtnEffaceEvent);
         pnlActeur.Controls.Add(new Literal { Text = "Nom de l'acteur" });
         pnlActeur.Controls.Add(new Literal { Text = "</td>" });
         pnlActeur.Controls.Add(new Literal { Text = "<td>" });
         //TextBox tbNomActeur = librairie.tbDYN(pnlActeur, "tbNomActeur", "", "50", "sCenter");

         /*DropDownList ddlActeurNom = librairie.ddlDYN(pnlActeur, "ddlActeurNom");
         ddlActeurNom.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoActeur, Nom from Acteurs", con);
         adapter.Fill(dataTable);
         ddlActeurNom.DataSource = dataTable;
         ddlActeurNom.DataValueField = "NoActeur";
         ddlActeurNom.DataTextField = "Nom";
         ddlActeurNom.DataBind();
         ddlActeurNom.Items.Insert(0, new ListItem("Aucun(e)", "NULL"));*/

         ListBox ddlActeurNom = librairie.lbDYN(pnlActeur, "ddlActeurNom");
         ddlActeurNom.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoActeur, Nom from Acteurs", con);
         adapter.Fill(dataTable);
         ddlActeurNom.DataSource = dataTable;
         ddlActeurNom.DataValueField = "NoActeur";
         ddlActeurNom.DataTextField = "Nom";
         ddlActeurNom.DataBind();
         ddlActeurNom.Items.Insert(0, new ListItem("Aucun(e)", "NULL"));

         foreach (string item in lsActeurNom)
         {
            ddlActeurNom.Items.FindByText(item).Selected = true;
         }

         //ImageButton imgBtnAjoute = librairie.imgBtn(pnlActeur, "imgBtnAjoute", "../imagesPerso/ajoute_desactive.png", "btnEfface center", imgBtnAjouteEvent);
         pnlActeur.Controls.Add(new Literal { Text = "</td>" });
         /*pnlActeur.Controls.Add(new Literal { Text = "<td>" });
         DropDownList ddlActeurSexe = librairie.ddlDYN(pnlActeur, "ddlActeurSexe");
         ddlActeurSexe.Items.Clear();
         pnlActeur.Controls.Add(new Literal { Text = "</td>" });*/
         pnlActeur.Controls.Add(new Literal { Text = "</tr>" });

         //tbNomActeur.Attributes.Add("placeholder", "Nom de l'acteur");
         /*ddlActeurSexe.Items.Insert(0, new ListItem("Homme", "H"));
         ddlActeurSexe.Items.Insert(1, new ListItem("Femme", "F"));*/

         pnlActeur.Controls.Add(new Literal { Text = "</table>" });

         librairie.brDYN(phDynamique);

         // Supplement
         librairie.lblDYN(phDynamique, "lblTitreSupplements", "Supplement(s)", "titre");

         Panel pnlSupplement = librairie.divDYN(phDynamique, "pnlSupplement");

         pnlSupplement.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         pnlSupplement.Controls.Add(new Literal { Text = "<tr>" });
         pnlSupplement.Controls.Add(new Literal { Text = "<td>" });
         //TextBox tbSupplement = librairie.tbDYN(pnlSupplement, "tbSupplement", "", "50", "");

         ListBox ddlSupplement = librairie.lbDYN(pnlSupplement, "ddlSupplement");
         ddlSupplement.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoSupplement, Description from Supplements", con);
         adapter.Fill(dataTable);
         ddlSupplement.DataSource = dataTable;
         ddlSupplement.DataValueField = "NoSupplement";
         ddlSupplement.DataTextField = "Description";
         ddlSupplement.DataBind();
         ddlSupplement.Items.Insert(0, new ListItem("Aucun", "NULL"));

         foreach (string item in lsSupplement)
         {
            ddlSupplement.Items.FindByText(item).Selected = true;
         }

         pnlSupplement.Controls.Add(new Literal { Text = "</td>" });
         pnlSupplement.Controls.Add(new Literal { Text = "</tr>" });

         //tbSupplement.Attributes.Add("placeholder", "Supplement");

         pnlSupplement.Controls.Add(new Literal { Text = "</table>" });

         // Locataire
         /*librairie.lblDYN(phDynamique, "lblTitreLocataires", "Locataires", "titre");

         Panel pnlLocataire = librairie.divDYN(phDynamique, "pnlLocataire");

         pnlLocataire.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         pnlLocataire.Controls.Add(new Literal { Text = "<tr>" });
         pnlLocataire.Controls.Add(new Literal { Text = "<td>" });
         TextBox tbLocataire = librairie.tbDYN(pnlLocataire, "tbLocataire", "", "50", "");
         pnlLocataire.Controls.Add(new Literal { Text = "</td>" });
         pnlLocataire.Controls.Add(new Literal { Text = "</tr>" });

         tbLocataire.Attributes.Add("placeholder", "Locataire");

         pnlLocataire.Controls.Add(new Literal { Text = "</table>" });*/

         // Langue
         librairie.lblDYN(phDynamique, "lblTitreLangues", "Langue(s)", "titre");

         Panel pnlLangue = librairie.divDYN(phDynamique, "pnlLangue");

         pnlLangue.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         pnlLangue.Controls.Add(new Literal { Text = "<tr>" });
         pnlLangue.Controls.Add(new Literal { Text = "<td>" });
         //TextBox tbLangue = librairie.tbDYN(pnlLangue, "tbLangue", "", "50", "");

         ListBox ddlLangue = librairie.lbDYN(pnlLangue, "ddlLangue");
         ddlLangue.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoLangue, Langue from Langues", con);
         adapter.Fill(dataTable);
         ddlLangue.DataSource = dataTable;
         ddlLangue.DataValueField = "NoLangue";
         ddlLangue.DataTextField = "Langue";
         ddlLangue.DataBind();
         ddlLangue.Items.Insert(0, new ListItem("Aucune", "NULL"));

         foreach (string item in lsLangue)
         {
            ddlLangue.Items.FindByText(item).Selected = true;
         }

         pnlLangue.Controls.Add(new Literal { Text = "</td>" });
         pnlLangue.Controls.Add(new Literal { Text = "</tr>" });

         //tbLangue.Attributes.Add("placeholder", "Langue");

         pnlLangue.Controls.Add(new Literal { Text = "</table>" });

         // SousTitre
         librairie.lblDYN(phDynamique, "lblSousTitres", "Sous-titre(s)", "titre");

         Panel pnlSousTitre = librairie.divDYN(phDynamique, "pnlSousTitre");

         pnlSousTitre.Controls.Add(new Literal { Text = "<table border = '1' class='center'>" });

         pnlSousTitre.Controls.Add(new Literal { Text = "<tr>" });
         pnlSousTitre.Controls.Add(new Literal { Text = "<td>" });

         ListBox ddlSousTitre = librairie.lbDYN(pnlSousTitre, "ddlSousTitre");
         ddlSousTitre.Items.Clear();
         dataTable = new DataTable();
         adapter = new SqlDataAdapter("select NoSousTitre, LangueSousTitre from SousTitres", con);
         adapter.Fill(dataTable);
         ddlSousTitre.DataSource = dataTable;
         ddlSousTitre.DataValueField = "NoSousTitre";
         ddlSousTitre.DataTextField = "LangueSousTitre";
         ddlSousTitre.DataBind();
         ddlSousTitre.Items.Insert(0, new ListItem("Aucun", "NULL"));

         foreach (string item in lsSousTitre)
         {
            ddlSousTitre.Items.FindByText(item).Selected = true;
         }

         pnlSousTitre.Controls.Add(new Literal { Text = "</td>" });
         pnlSousTitre.Controls.Add(new Literal { Text = "</tr>" });

         pnlSousTitre.Controls.Add(new Literal { Text = "</table>" });
      }
   }

   void rechargePageOnClick(object sender, EventArgs e)
   {
      Response.Redirect(Request.RawUrl);
   }

   protected void btnModification_Click(object sender, EventArgs e)
   {
      bool blnBonComplet = true;

      TextBox tbNoFilm = null, tbNomU = null, tbResume = null, tbTitreFr = null, tbTitreOriginal = null, tbDateMAJ = null;
      DropDownList ddlCategorie = null, ddlProducteur = null, ddlRealisateur = null, ddlFormat = null, ddlVersionEtendue = null, tbAnneeDeSortie = null;

      for (int i = 0; i < 12; i++)
      {
         Panel pnl = (Panel)librairie.b(phDynamique, "pnlCompletMain" + i);

         switch (i)
         {
            case 0:
               tbNoFilm = (TextBox)librairie.b(pnl, "tbNoFilm"); break;
            case 1:
               tbAnneeDeSortie = (DropDownList)librairie.b(pnl, "tbAnneeDeSortie"); break;
            case 2:
               tbNomU = (TextBox)librairie.b(pnl, "tbNomU"); break;
            case 3:
               tbResume = (TextBox)librairie.b(pnl, "tbResume"); break;
            case 4:
               tbTitreFr = (TextBox)librairie.b(pnl, "tbTitreFr"); break;
            case 5:
               tbTitreOriginal = (TextBox)librairie.b(pnl, "tbTitreOriginal"); break;
            case 6:
               ddlCategorie = (DropDownList)librairie.b(pnl, "ddlCategorie"); break;
            case 7:
               ddlProducteur = (DropDownList)librairie.b(pnl, "ddlProducteur"); break;
            case 8:
               ddlRealisateur = (DropDownList)librairie.b(pnl, "ddlRealisateur"); break;
            case 9:
               ddlFormat = (DropDownList)librairie.b(pnl, "ddlFormat"); break;
            case 10:
               tbDateMAJ = (TextBox)librairie.b(pnl, "tbDateMAJ"); break;
            case 11:
               ddlVersionEtendue = (DropDownList)librairie.b(pnl, "ddlVersionEtendue"); break;
         }
      }

      Panel pnlActeur = (Panel)librairie.b(phDynamique, "pnlActeur");
      /*TextBox tbNomActeur = (TextBox)librairie.b(pnlActeur, "tbNomActeur");
      DropDownList ddlActeurSexe = (DropDownList)librairie.b(pnlActeur, "ddlActeurSexe");*/
      ListBox ddlActeurNom = (ListBox)librairie.b(pnlActeur, "ddlActeurNom");

      Panel pnlSupplement = (Panel)librairie.b(phDynamique, "pnlSupplement");
      ListBox ddlSupplement = (ListBox)librairie.b(pnlSupplement, "ddlSupplement");

      /*Panel pnlLocataire = (Panel)librairie.b(phDynamique, "pnlLocataire");
      DropDownList ddlLocataire = (DropDownList)librairie.b(pnlLocataire, "ddlLocataire");*/

      Panel pnlLangue = (Panel)librairie.b(phDynamique, "pnlLangue");
      ListBox ddlLangue = (ListBox)librairie.b(pnlLangue, "ddlLangue");

      Panel pnlSousTitre = (Panel)librairie.b(phDynamique, "pnlSousTitre");
      ListBox ddlSousTitre = (ListBox)librairie.b(pnlSousTitre, "ddlSousTitre");

      DropDownList ddlImagePochette = (DropDownList)librairie.b(phDynamique, "ddlImagePochette");

      if (blnBonComplet)
      {
         SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
         con.Open();

         if (tbTitreFr.Text.Length != 0)
         {
            SqlCommand cmdExiste = new SqlCommand("select count(*) from Films where TitreFrancais = '" + tbTitreFr.Text + "'", con);
            dynamic checkExiste = cmdExiste.ExecuteScalar();

            // check si le dvd existe deja dans la bd
            if (checkExiste == 0) // le dvd n'existe pas dans la bd
            {
               /*SqlCommand cmdInsertionFilm = new SqlCommand("insert into Films " +
                                          "(NoFilm, AnneeSortie, Categorie, Format, DateMAJ, NoUtilisateurMAJ, Resume, ImagePochette, TitreFrancais, TitreOriginal, VersionEtendue, NoRealisateur, NoProducteur) " +
                                          "values (" + int.Parse(tbNoFilm.Text) +
                                          ", " + (tbAnneeDeSortie.Text.Length != 0 && tbAnneeDeSortie.Text != "0" ? "'" + tbAnneeDeSortie.Text + "'" : "NULL") +
                                          ", " + (ddlCategorie.SelectedItem.Value != "NULL" ? "'" + ddlCategorie.SelectedItem.Value + "'" : "NULL") +
                                          ", " + (ddlFormat.SelectedItem.Value != "NULL" ? "'" + ddlFormat.SelectedItem.Value + "'" : "NULL") +
                                          ", cast(getdate() as date)" +
                                          ", " + int.Parse(Session["NoU"].ToString()) +
                                          ", '" + tbResume.Text + "'" +
                                          //", " + (tbDureeEnMinute.Text.Length != 0 ? "'" + tbDureeEnMinute.Text + "'" : "NULL") +
                                          //", " + (ddlFilmOriginal.SelectedItem.Value != "NULL" ? "'" + ddlFilmOriginal.SelectedItem.Value + "'" : "NULL") +
                                          //", " + (strPath.Length != 0 ? "'" + strPath + "'" : "''") +
                                          //", " + (tbNbDisques.Text.Length != 0 ? "'" + tbNbDisques.Text + "'" : "NULL") +
                                          ", '" + tbTitreFr.Text + "'" +
                                          ", '" + tbTitreOriginal.Text + "'" +
                                          ", " + (ddlVersionEtendue.SelectedItem.Value != "NULL" ? "'" + ddlVersionEtendue.SelectedItem.Value + "'" : "NULL") +
                                          ", " + (ddlRealisateur.SelectedItem.Value != "NULL" ? "'" + ddlRealisateur.SelectedItem.Value + "'" : "NULL") +
                                          ", " + (ddlProducteur.SelectedItem.Value != "NULL" ? "'" + ddlProducteur.SelectedItem.Value + "'" : "NULL") +
                                          ", '" + (tbXTra.Text.Length != 0 ? tbXTra.Text : " ") + "'")", con);*/

               SqlCommand cmdInsertionFilm = new SqlCommand("update Films " +
                                          "set AnneeSortie=" + (tbAnneeDeSortie.SelectedItem.Value != "NULL" ? "'" + tbAnneeDeSortie.SelectedItem.Value + "'" : "NULL") + 
                                          ", Categorie=" + (ddlCategorie.SelectedItem.Value != "NULL" ? "'" + ddlCategorie.SelectedItem.Value + "'" : "NULL")  + 
                                          ", Format=" + (ddlFormat.SelectedItem.Value != "NULL" ? "'" + ddlFormat.SelectedItem.Value + "'" : "NULL") +
                                          ", DateMAJ=cast(getdate() as date)" + 
                                          ", NoUtilisateurMAJ=" + int.Parse(Session["NoU"].ToString()) +
                                          ", Resume=" + "'" + tbResume.Text + "'" +
                                          ", ImagePochette=" + (ddlImagePochette.SelectedItem.Value != "NULL" ? "'" + ddlImagePochette.SelectedItem.Value + "'" : "NULL") +
                                          ", TitreFrancais=" + "'" + tbTitreFr.Text + "'" +
                                          ", TitreOriginal=" + "'" + tbTitreOriginal.Text + "'" +
                                          ", VersionEtendue=" + (ddlVersionEtendue.SelectedItem.Value != "NULL" ? "'" + ddlVersionEtendue.SelectedItem.Value + "'" : "NULL") +
                                          ", NoRealisateur=" + (ddlRealisateur.SelectedItem.Value != "NULL" ? "'" + ddlRealisateur.SelectedItem.Value + "'" : "NULL") +
                                          ", NoProducteur=" + (ddlProducteur.SelectedItem.Value != "NULL" ? "'" + ddlProducteur.SelectedItem.Value + "'" : "NULL") + 
                                          " where NoFilm=" + int.Parse(tbNoFilm.Text), con);
               cmdInsertionFilm.ExecuteNonQuery();

               SqlCommand cmdInsertionActeur1 = new SqlCommand("delete from FilmsActeurs where NoFilm=" + int.Parse(tbNoFilm.Text), con);
               cmdInsertionActeur1.ExecuteNonQuery();
               if (ddlActeurNom.SelectedIndex > -1 && ddlActeurNom.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionActeur;
                  foreach (ListItem item in ddlActeurNom.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionActeur = new SqlCommand("insert into FilmsActeurs (NoFilm, NoActeur) " +
                                          "values (" + int.Parse(tbNoFilm.Text) + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionActeur.ExecuteNonQuery();
                     }
                  }
               }

               SqlCommand cmdInsertionSupplement1 = new SqlCommand("delete from FilmsSupplements where NoFilm=" + int.Parse(tbNoFilm.Text), con);
               cmdInsertionSupplement1.ExecuteNonQuery();
               if (ddlSupplement.SelectedIndex > -1 && ddlSupplement.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionSupplement;
                  foreach (ListItem item in ddlSupplement.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionSupplement = new SqlCommand("insert into FilmsSupplements (NoFilm, NoSupplement) " +
                                          "values (" + int.Parse(tbNoFilm.Text) + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionSupplement.ExecuteNonQuery();
                     }
                  }
               }

               SqlCommand cmdInsertionLangue1 = new SqlCommand("delete from FilmsLangues where NoFilm=" + int.Parse(tbNoFilm.Text), con);
               cmdInsertionLangue1.ExecuteNonQuery();
               if (ddlLangue.SelectedIndex > -1 && ddlLangue.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionLangue;
                  foreach (ListItem item in ddlLangue.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionLangue = new SqlCommand("insert into FilmsLangues (NoFilm, NoLangue) " +
                                          "values (" + int.Parse(tbNoFilm.Text) + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionLangue.ExecuteNonQuery();
                     }
                  }
               }

               SqlCommand cmdInsertionSousTitre1 = new SqlCommand("delete from FilmsSousTitres where NoFilm=" + int.Parse(tbNoFilm.Text), con);
               cmdInsertionSousTitre1.ExecuteNonQuery();
               if (ddlSousTitre.SelectedIndex > -1 && ddlSousTitre.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionSousTitre;
                  foreach (ListItem item in ddlSousTitre.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionSousTitre = new SqlCommand("insert into FilmsSousTitres (NoFilm, NoSousTitre) " +
                                          "values (" + int.Parse(tbNoFilm.Text) + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionSousTitre.ExecuteNonQuery();
                     }
                  }
               }

               Server.Transfer("ListeTous.aspx", true);
            }
            else // le dvd existe dans la bd, genere une erreur au user
            {
               ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Le DVD séléctionné existe déjà dans la base de donnée')", true);

               tbTitreFr.BackColor = Color.Red;
               tbTitreFr.ForeColor = Color.White;
            }
         }
         else // titre fr obligatoire
         {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Le titre en français est obligatoire')", true);

            tbTitreFr.BackColor = Color.Red;
            tbTitreFr.ForeColor = Color.White;
         }

         con.Close();
      }

      /*string strDelSupplements = "DELETE FROM FilmsSupplements WHERE FilmsSupplements.NoFilm=" + Convert.ToInt32(Request.QueryString["id"]);
      SqlConnection sqlCoSupp = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlCoSupp.Open();
      SqlCommand sqlCommandSupp = new SqlCommand(strDelSupplements, sqlCoSupp);
      sqlCommandSupp.ExecuteNonQuery();

      string strDelLangues = "DELETE FROM FilmsLangues WHERE FilmsLangues.NoFilm=" + Convert.ToInt32(Request.QueryString["id"]);
      SqlConnection sqlCoLang = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlCoLang.Open();
      SqlCommand sqlCommandLang = new SqlCommand(strDelLangues, sqlCoLang);
      sqlCommandLang.ExecuteNonQuery();

      string strDelST = "DELETE FROM FilmsSousTitres WHERE FilmsSousTitres.NoFilm=" + Convert.ToInt32(Request.QueryString["id"]);
      SqlConnection sqlCoST = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlCoST.Open();
      SqlCommand sqlCommandDelST = new SqlCommand(strDelST, sqlCoST);
      sqlCommandDelST.ExecuteNonQuery();


      string strDelAct = "DELETE FROM FilmsActeurs WHERE FilmsActeurs.NoFilm=" + Convert.ToInt32(Request.QueryString["id"]);
      SqlConnection sqlCoAct = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlCoAct.Open();
      SqlCommand sqlCommandDelAct = new SqlCommand(strDelAct, sqlCoAct);
      sqlCommandDelAct.ExecuteNonQuery();

      string strUpdate1 = "DELETE FROM Films WHERE Films.NoFilm=" + Convert.ToInt32(Request.QueryString["id"]);
      SqlConnection sqlConUpdate = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlConUpdate.Open();
      SqlCommand sqlCommandUpdate = new SqlCommand(strUpdate1, sqlConUpdate);
      sqlCommandUpdate.ExecuteNonQuery();

      string strDelExemplaire = "DELETE FROM Exemplaires WHERE Exemplaires.NoExemplaire=" + Convert.ToInt32(Request.QueryString["id"]) + "01";
      SqlConnection sqlCoExemplaire = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      sqlCoExemplaire.Open();
      SqlCommand sqlCommandDelExemplaire = new SqlCommand(strDelExemplaire, sqlCoExemplaire);
      sqlCommandDelExemplaire.ExecuteNonQuery();*/

      Server.Transfer("ListeTous.aspx", true);

   }

   protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
   {
      Response.Redirect(String.Format("Modification.aspx?id={0}", ddl.SelectedValue));
   }
}