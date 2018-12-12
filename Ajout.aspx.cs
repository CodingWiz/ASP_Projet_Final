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

public partial class Ajout : System.Web.UI.Page
{
   public libProjet2 librairie = new libProjet2();

   void Page_Load()
   {
      if (Session["Mode"] == "abrege")
      {
         modeAbrege();
      }
      else if (Session["Mode"] == "complet")
      {
         modeComplet();
      }
   }

   void rechargePageOnClick(object sender, EventArgs e)
   {
      Response.Redirect(Request.RawUrl);
   }

   protected void linkButton(object sender, EventArgs e)
   {
      phDynamique.Controls.Clear();

      if (sender == abrege)
      {
         Session["Mode"] = "abrege";
         modeAbrege();
      }
      else if (sender == complet)
      {
         Session["Mode"] = "complet";
         modeComplet();
      }
   }

   void modeAbrege()
   {
      for (Int16 i = 1; i <= 10; i++)
      {
         Panel pnl = librairie.divDYN(phDynamique, "pnl" + i);

         Label lblTitreFrAbrege = librairie.lblDYN(pnl, "lblTitreFrAbrege" + i, "Écrivez le Titre en français", "sCenter");
         TextBox tbTitreFrAbrege = librairie.tbDYN(pnl, "tbTitreFrAbrege" + i, "", "15", "sCenter");
         tbTitreFrAbrege.Attributes.Add("placeholder", "Titre en français");

         librairie.brDYN(phDynamique);
         librairie.brDYN(phDynamique);
      }

      Button btnAjout = librairie.btnDYN(phDynamique, "btnAjoutAbrege", "Ajouter le DVD", "sCenter", ajoutDVDAbrege);
   }
   void modeComplet()
   {
      SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
      con.Open();

      DataTable dataTable;
      SqlDataAdapter adapter;

      // Main
      string strNoFilm = "";

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

      FileUpload fileUploadImagePochette = librairie.fileUploadDYN(phDynamique, "fileUploadImagePochette");

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
               tbNoFilm.Text = (int.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
               tbNoFilm.Enabled = false; break;
            case 1:
               TextBox tbAnneeDeSortie = librairie.tbDYN(pnl, "tbAnneeDeSortie", "", "10", "sCenter");
               tbAnneeDeSortie.Attributes.Add("placeholder", "Titre en français"); break;
            case 2:
               TextBox tbNomU = librairie.tbDYN(pnl, "tbNomU", Session["NomU"].ToString(), "50", "sCenter");
               tbNomU.Enabled = false; break;
            case 3:
               TextBox tbResume = librairie.tbDYN(pnl, "tbResume", "", "500", "sCenter");
               tbResume.TextMode = TextBoxMode.MultiLine;
               tbResume.Attributes.Add("placeholder", "Titre en français"); break;
            case 4:
               TextBox tbTitreFr = librairie.tbDYN(pnl, "tbTitreFr", "", "50", "sCenter");
               tbTitreFr.Attributes.Add("placeholder", "Titre en français"); break;
            case 5:
               TextBox tbTitreOriginal = librairie.tbDYN(pnl, "tbTitreOriginal", "", "50", "sCenter");
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
               ddlCategorie.Items.Insert(0, new ListItem("Aucune", "null")); break;
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
               ddlProducteur.Items.Insert(0, new ListItem("Aucun(e)", "null")); break;
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
               ddlRealisateur.Items.Insert(0, new ListItem("Aucun(e)", "null")); break;
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
               ddlFormat.Items.Insert(0, new ListItem("Aucun", "null")); break;
            case 10:
               TextBox tbDateMAJ = librairie.tbDYN(pnl, "tbDateMAJ", DateTime.Now.ToString("yyyy-MM-dd"), "50", "sCenter");
               tbDateMAJ.Enabled = false; break;
            case 11:
               DropDownList ddlVersionEtendue = librairie.ddlDYN(pnl, "ddlVersionEtendue");
               ddlVersionEtendue.Items.Clear();
               ddlVersionEtendue.Items.Insert(0, new ListItem("Aucune", "null"));
               ddlVersionEtendue.Items.Insert(1, new ListItem("Oui", "1"));
               ddlVersionEtendue.Items.Insert(2, new ListItem("Non", "0")); break;
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

      ListBox ddlActeurNom = librairie.lbDYN(pnlActeur, "ddlActeurNom");
      ddlActeurNom.Items.Clear();
      dataTable = new DataTable();
      adapter = new SqlDataAdapter("select NoActeur, Nom from Acteurs", con);
      adapter.Fill(dataTable);
      ddlActeurNom.DataSource = dataTable;
      ddlActeurNom.DataValueField = "NoActeur";
      ddlActeurNom.DataTextField = "Nom";
      ddlActeurNom.DataBind();
      ddlActeurNom.Items.Insert(0, new ListItem("Aucun(e)", "null"));

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
      ddlSupplement.Items.Insert(0, new ListItem("Aucun", "null"));

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
      ddlLangue.Items.Insert(0, new ListItem("Aucune", "null"));

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
      ddlSousTitre.Items.Insert(0, new ListItem("Aucun", "null"));

      //ddlSousTitre.SelectedIndexChanged += listBox_SelectedIndexChanged;

      pnlSousTitre.Controls.Add(new Literal { Text = "</td>" });
      pnlSousTitre.Controls.Add(new Literal { Text = "</tr>" });

      pnlSousTitre.Controls.Add(new Literal { Text = "</table>" });

      /*Panel pnl = librairie.divDYN(phDynamique, "pnl", "btnAjout");

      Label lblNoFilm = librairie.lblDYN(pnl, "lblNoFilm", "No du film", "sCenter");
      TextBox tbNoFilm = librairie.tbDYN(pnl, "tbNoFilm", "", "10", "sCenter");

      SqlCommand cmd = new SqlCommand("select top 1 NoFilm from Films order by NoFilm desc", con);
      tbNoFilm.Text = (int.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
      tbNoFilm.Enabled = false;

      Label lblAnneeDeSortie = librairie.lblDYN(pnl, "lblAnneeDeSortie", "Année de sortie", "sCenter");
      TextBox tbAnneeDeSortie = librairie.tbDYN(pnl, "tbAnneeDeSortie", "", "10", "sCenter");

      Label lblCategorie = librairie.lblDYN(pnl, "lblCategorie", "Catégorie");
      DropDownList ddlCategorie = librairie.ddlDYN(pnl, "ddlCategorie");
      ddlCategorie.Items.Clear();
      dataTable = new DataTable();
      adapter = new SqlDataAdapter("select NoCategorie, Description from Categories", con);
      adapter.Fill(dataTable);
      ddlCategorie.DataSource = dataTable;
      ddlCategorie.DataValueField = "NoCategorie";
      ddlCategorie.DataTextField = "Description";
      ddlCategorie.DataBind();
      ddlCategorie.Items.Insert(0, new ListItem("Aucune", "null"));

      Label lblFormat = librairie.lblDYN(pnl, "lblFormat", "Format");
      DropDownList ddlFormat = librairie.ddlDYN(pnl, "ddlFormat");
      ddlFormat.Items.Clear();
      dataTable = new DataTable();
      adapter = new SqlDataAdapter("select NoFormat, Description from Formats", con);
      adapter.Fill(dataTable);
      ddlFormat.DataSource = dataTable;
      ddlFormat.DataValueField = "NoFormat";
      ddlFormat.DataTextField = "Description";
      ddlFormat.DataBind();
      ddlFormat.Items.Insert(0, new ListItem("Aucun", "null"));

      Label lblResume = librairie.lblDYN(pnl, "lblResume", "Résumé");
      TextBox tbResume = librairie.tbDYN(pnl, "tbResume", "", "500", "sCenter");

      Label lblDureeEnMinute = librairie.lblDYN(pnl, "lblDureeEnMinute", "Durée en minute");
      TextBox tbDureeEnMinute = librairie.tbDYN(pnl, "tbDureeEnMinute", "", "10", "sCenter");

      Label lblFilmOriginal = librairie.lblDYN(pnl, "lblFilmOriginal", "FIlm original", "sCenter");
      DropDownList ddlFilmOriginal = librairie.ddlDYN(pnl, "ddlFilmOriginal");
      ddlFilmOriginal.Items.Clear();
      ddlFilmOriginal.Items.Insert(0, new ListItem("Aucun", "null"));
      ddlFilmOriginal.Items.Insert(1, new ListItem("Oui", "1"));
      ddlFilmOriginal.Items.Insert(2, new ListItem("Non", "0"));

      Label lblImagePochette = librairie.lblDYN(pnl, "lblImagePochette", "Image du dvd");
      FileUpload fileUploadImagePochette = librairie.fileUploadDYN(pnl, "fileUploadImagePochette");

      Label lblNbDisques = librairie.lblDYN(pnl, "lblNbDisques", "Nombre de disque", "sCenter");
      TextBox tbNbDisques = librairie.tbDYN(pnl, "tbNbDisques", "", "10", "sCenter");
      tbNbDisques.Attributes.Add("placeholder", "0");

      Label lblTitreFr = librairie.lblDYN(pnl, "lblTitreFr", "Écrivez le Titre en français", "sCenter");
      TextBox tbTitreFr = librairie.tbDYN(pnl, "tbTitreFr", "", "50", "sCenter");
      tbTitreFr.Attributes.Add("placeholder", "Titre en français");

      Label lblTitreOriginal = librairie.lblDYN(pnl, "lblTitreOriginal", "Titre original");
      TextBox tbTitreOriginal = librairie.tbDYN(pnl, "tbTitreOriginal", "", "50", "sCenter");

      Label lblVersionEtendue = librairie.lblDYN(pnl, "lblVersionEtendue", "Version étendue");
      DropDownList ddlVersionEtendue = librairie.ddlDYN(pnl, "ddlVersionEtendue");
      ddlVersionEtendue.Items.Clear();
      ddlVersionEtendue.Items.Insert(0, new ListItem("Aucune", "null"));
      ddlVersionEtendue.Items.Insert(1, new ListItem("Oui", "1"));
      ddlVersionEtendue.Items.Insert(2, new ListItem("Non", "0"));

      Label lblRealisateur = librairie.lblDYN(pnl, "lblRealisateur", "Réalisateur");
      DropDownList ddlRealisateur = librairie.ddlDYN(pnl, "ddlRealisateur");
      ddlRealisateur.Items.Clear();
      dataTable = new DataTable();
      adapter = new SqlDataAdapter("select NoRealisateur, Nom from Realisateurs", con);
      adapter.Fill(dataTable);
      ddlRealisateur.DataSource = dataTable;
      ddlRealisateur.DataValueField = "NoRealisateur";
      ddlRealisateur.DataTextField = "Nom";
      ddlRealisateur.DataBind();
      ddlRealisateur.Items.Insert(0, new ListItem("Aucun(e)", "null"));

      Label lblProducteur = librairie.lblDYN(pnl, "lblProducteur", "Réalisateur");
      DropDownList ddlProducteur = librairie.ddlDYN(pnl, "ddlProducteur");
      ddlProducteur.Items.Clear();
      dataTable = new DataTable();
      adapter = new SqlDataAdapter("select NoProducteur, Nom from Producteurs", con);
      adapter.Fill(dataTable);
      ddlProducteur.DataSource = dataTable;
      ddlProducteur.DataValueField = "NoProducteur";
      ddlProducteur.DataTextField = "Nom";
      ddlProducteur.DataBind();
      ddlProducteur.Items.Insert(0, new ListItem("Aucun(e)", "null"));

      Label lblXTra = librairie.lblDYN(pnl, "lblXTra", "XTra");
      TextBox tbXTra = librairie.tbDYN(pnl, "tbXTra", "", "255", "sCenter");

      librairie.brDYN(phDynamique);
      librairie.brDYN(phDynamique);*/

      con.Close();

      Button btnAjout = librairie.btnDYN(phDynamique, "btnAjout", "Ajouter le DVD", "sCenter", ajoutDVDComplet);
   }

   void ajoutDVDAbrege(object sender, EventArgs e)
   {
      bool blnBon = true;

      if (blnBon)
      {
         SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
         con.Open();

         bool blnToutOk = true;

         for (Int16 i = 1; i <= 10; i++)
         {
            TextBox tb = (TextBox)librairie.b(phDynamique, "tbTitreFrAbrege" + i);

            if (tb.Text.Length != 0)
            {
               SqlCommand cmdExiste = new SqlCommand("select count(*) from Films where TitreFrancais = '" + tb.Text + "'", con);
               dynamic checkExiste = cmdExiste.ExecuteScalar();

               // check si le dvd existe deja dans la bd
               if (checkExiste == 0) // le dvd n'existe pas dans la bd
               {

               }
               else // le dvd existe dans la bd, genere une erreur au user
               {
                  ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Le(s) DVD séléctionné(s) existe(nt) déjà dans la base de donnée')", true);

                  blnToutOk = false;

                  tb.BackColor = Color.Red;
                  tb.ForeColor = Color.White;
               }
            }
         }

         if (blnToutOk)
         {
            for (Int16 i = 1; i <= 10; i++)
            {
               TextBox tb = (TextBox)librairie.b(phDynamique, "tbTitreFrAbrege" + i);

               if (tb.Text.Length != 0)
               {
                  SqlCommand cmd = new SqlCommand("select count(*) from Films", con);
                  Int32 intNoFilm = int.Parse(cmd.ExecuteScalar().ToString()) + 1;
                  Int32 strnoFilm=0;
                  if (intNoFilm < 10)
                        {
                            strnoFilm = int.Parse(DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + "0" + intNoFilm.ToString());
                        }
                  else {
                            strnoFilm = int.Parse(DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + intNoFilm.ToString());
                        }
                  

                  SqlCommand cmdInsertion = new SqlCommand("insert into Films (NoFilm, DateMAJ, NoUtilisateurMAJ, TitreFrancais) " +
                                             "values (" + strnoFilm + ", cast(getdate() as date), " + int.Parse(Session["NoU"].ToString()) + ", '" + tb.Text + "')", con);
                  cmdInsertion.ExecuteNonQuery();

                  SqlCommand cmdInsertionExemplaire = new SqlCommand("insert into Exemplaires " +
                                          "(NoExemplaire, NoUtilisateurProprietaire) " +
                                          "values (" + strnoFilm + "01, " + int.Parse(Session["NoU"].ToString()) + ")", con);
                  cmdInsertionExemplaire.ExecuteNonQuery();
               }
            }

            Server.Transfer("ListeTous.aspx", true);
         }

         con.Close();
      }
   }
   void ajoutDVDComplet(object sender, EventArgs e)
   {
      bool blnBonComplet = true;

      /*Panel pnl = (Panel)librairie.b(phDynamique, "pnlMain");

      TextBox tbNoFilm = (TextBox)librairie.b(pnl, "tbNoFilm"); // != null
      TextBox tbAnneeDeSortie = (TextBox)librairie.b(pnl, "tbAnneeDeSortie");
      TextBox tbResume = (TextBox)librairie.b(pnl, "tbResume");
      //TextBox tbDureeEnMinute = (TextBox)librairie.b(pnl, "tbDureeEnMinute"); // ?
      //TextBox tbNbDisques = (TextBox)librairie.b(pnl, "tbNbDisques"); // ?
      TextBox tbTitreFr = (TextBox)librairie.b(pnl, "tbTitreFr"); // != null
      TextBox tbTitreOriginal = (TextBox)librairie.b(pnl, "tbTitreOriginal");
      //TextBox tbXTra = (TextBox)librairie.b(pnl, "tbXTra"); // ?

      DropDownList ddlCategorie = (DropDownList)librairie.b(pnl, "ddlCategorie");
      DropDownList ddlFormat = (DropDownList)librairie.b(pnl, "ddlFormat");
      //DropDownList ddlFilmOriginal = (DropDownList)librairie.b(pnl, "ddlFilmOriginal"); // ?
      DropDownList ddlVersionEtendue = (DropDownList)librairie.b(pnl, "ddlVersionEtendue");
      DropDownList ddlRealisateur = (DropDownList)librairie.b(pnl, "ddlRealisateur");
      DropDownList ddlProducteur = (DropDownList)librairie.b(pnl, "ddlProducteur");*/

      TextBox tbNoFilm = null, tbAnneeDeSortie = null, tbNomU = null, tbResume = null, tbTitreFr = null, tbTitreOriginal = null, tbDateMAJ = null;
      DropDownList ddlCategorie = null, ddlProducteur = null, ddlRealisateur = null, ddlFormat = null, ddlVersionEtendue = null;

      for (int i = 0; i < 12; i++)
      {
         Panel pnl = (Panel)librairie.b(phDynamique, "pnlCompletMain" + i);

         switch (i)
         {
            case 0:
               tbNoFilm = (TextBox)librairie.b(pnl, "tbNoFilm"); break;
            case 1:
               tbAnneeDeSortie = (TextBox)librairie.b(pnl, "tbAnneeDeSortie"); break;
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

      FileUpload fileUpload = (FileUpload)librairie.b(phDynamique, "fileUploadImagePochette");
      string strPath = "";
      
      if (fileUpload.HasFile)
      {
         bool blnExtension = false;

         string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
         string[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
         foreach (string strExtension in allowedExtensions)
         {
            if (fileExtension == strExtension)
            {
               blnExtension = true;
            }
         }

         if (blnExtension)
         {
            if (fileUpload.PostedFile != null)
            {
               strPath = Path.GetFileName(fileUpload.PostedFile.FileName);
               fileUpload.PostedFile.SaveAs(Server.MapPath("imagespochette/" + strPath));
            }
            else
            {
               ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Erreur de sauvegarde du fichier')", true);

               blnBonComplet = false;
            }
         }
         else
         {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Les extensions des images doivent se terminer par .gif, .png, .jpeg ou par .jpg')", true);

            blnBonComplet = false;
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
               SqlCommand cmdNoFilm = new SqlCommand("select count(*) from Films", con);
               Int32 intNoFilm = int.Parse(cmdNoFilm.ExecuteScalar().ToString()) + 1;
                    Int32 strNoFilm = 0;
                    if (intNoFilm < 10)
                    {
                        strNoFilm = int.Parse(DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + "0" + intNoFilm.ToString());
                    }
                    else
                    {
                        strNoFilm = int.Parse(DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + intNoFilm.ToString());
                    }
                    SqlCommand cmdInsertionFilm = new SqlCommand("insert into Films " +
                                          "(NoFilm, AnneeSortie, Categorie, Format, DateMAJ, NoUtilisateurMAJ, Resume, ImagePochette, TitreFrancais, TitreOriginal, VersionEtendue, NoRealisateur, NoProducteur) " +
                                          "values (" + strNoFilm +
                                          ", " + (tbAnneeDeSortie.Text.Length != 0 ? "'" + tbAnneeDeSortie.Text + "'" : "null") +
                                          ", " + (ddlCategorie.SelectedItem.Value != "null" ? "'" + ddlCategorie.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlFormat.SelectedItem.Value != "null" ? "'" + ddlFormat.SelectedItem.Value + "'" : "null") +
                                          ", cast(getdate() as date)" +
                                          ", " + int.Parse(Session["NoU"].ToString()) +
                                          ", '" + tbResume.Text + "'" +
                                          //", " + (tbDureeEnMinute.Text.Length != 0 ? "'" + tbDureeEnMinute.Text + "'" : "null") +
                                          //", " + (ddlFilmOriginal.SelectedItem.Value != "null" ? "'" + ddlFilmOriginal.SelectedItem.Value + "'" : "null") +
                                          ", " + (strPath.Length != 0 ? "'" + strPath + "'" : "''") +
                                          //", " + (tbNbDisques.Text.Length != 0 ? "'" + tbNbDisques.Text + "'" : "null") +
                                          ", '" + tbTitreFr.Text + "'" +
                                          ", '" + tbTitreOriginal.Text + "'" +
                                          ", " + (ddlVersionEtendue.SelectedItem.Value != "null" ? "'" + ddlVersionEtendue.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlRealisateur.SelectedItem.Value != "null" ? "'" + ddlRealisateur.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlProducteur.SelectedItem.Value != "null" ? "'" + ddlProducteur.SelectedItem.Value + "'" : "null") +
                                          /*", '" + (tbXTra.Text.Length != 0 ? tbXTra.Text : " ") + "'*/")", con);

               /*SqlCommand cmdInsertion = new SqlCommand("insert into Films " +
                                          "(NoFilm, AnneeSortie, Categorie, Format, DateMAJ, NoUtilisateurMAJ, Resume, DureeMinutes, FilmOriginal, ImagePochette, NbDisques, TitreFrancais, TitreOriginal, VersionEtendue, NoRealisateur, NoProducteur, XTra) " +
                                          "values (" + intNoFilm +
                                          ", " + (tbAnneeDeSortie.Text.Length != 0 ? "'" + tbAnneeDeSortie.Text + "'" : "null") +
                                          ", " + (ddlCategorie.SelectedItem.Value != "null" ? "'" + ddlCategorie.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlFormat.SelectedItem.Value != "null" ? "'" + ddlFormat.SelectedItem.Value + "'" : "null") +
                                          ", cast(getdate() as date)" +
                                          ", " + int.Parse(Session["NoU"].ToString()) +
                                          ", '" + tbResume.Text + "'" +
                                          ", " + (tbDureeEnMinute.Text.Length != 0 ? "'" + tbDureeEnMinute.Text + "'" : "null") +
                                          ", " + (ddlFilmOriginal.SelectedItem.Value != "null" ? "'" + ddlFilmOriginal.SelectedItem.Value + "'" : "null") +
                                          ", '" + strPath + "'" +
                                          ", " + (tbNbDisques.Text.Length != 0 ? "'" + tbNbDisques.Text + "'" : "null") +
                                          ", '" + tbTitreFr.Text + "'" +
                                          ", '" + tbTitreOriginal.Text + "'" +
                                          ", " + (ddlVersionEtendue.SelectedItem.Value != "null" ? "'" + ddlVersionEtendue.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlRealisateur.SelectedItem.Value != "null" ? "'" + ddlRealisateur.SelectedItem.Value + "'" : "null") +
                                          ", " + (ddlProducteur.SelectedItem.Value != "null" ? "'" + ddlProducteur.SelectedItem.Value + "'" : "null") +
                                          ", '" + (tbXTra.Text.Length != 0 ? tbXTra.Text : " ") + "')", con);*/
               cmdInsertionFilm.ExecuteNonQuery();

               SqlCommand cmdInsertionExemplaire = new SqlCommand("insert into Exemplaires " +
                                          "(NoExemplaire, NoUtilisateurProprietaire) " +
                                          "values (" + strNoFilm + "01, " + int.Parse(Session["NoU"].ToString()) + ")", con);
               cmdInsertionExemplaire.ExecuteNonQuery();

               // !!! EST-CE QU ON AJOUTE UN ACTEUR OU EST-CE QUE C'EST UN DROPDOWNLIST ???? !!!
               /*if (tbNomActeur.Text.Length != 0)
               {
                  SqlCommand cmdNoActeur = new SqlCommand("select top 1 NoActeur from Acteurs order by NoActeur desc", con);
                  Int32 intNoActeur = int.Parse(cmdNoActeur.ExecuteScalar().ToString()) + 1;

                  SqlCommand cmdInsertionActeur = new SqlCommand("insert into Acteurs (NoActeur, Nom, Sexe) " +
                                          "values (" + intNoActeur + ", " + tbNomActeur.Text + ", " + ddlActeurSexe.SelectedItem.Value + ")", con);
                  cmdInsertionActeur.ExecuteNonQuery();

                  
               }*/
               if (ddlActeurNom.SelectedIndex > -1 && ddlActeurNom.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionActeur;
                  foreach (ListItem item in ddlActeurNom.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionActeur = new SqlCommand("insert into FilmsActeurs (NoFilm, NoActeur) " +
                                          "values (" + strNoFilm + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionActeur.ExecuteNonQuery();
                     }
                  }
               }
               if (ddlSupplement.SelectedIndex > -1 && ddlSupplement.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionSupplement;
                  foreach (ListItem item in ddlSupplement.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionSupplement = new SqlCommand("insert into FilmsSupplements (NoFilm, NoSupplement) " +
                                          "values (" + strNoFilm + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionSupplement.ExecuteNonQuery();
                     }
                  }
               }
               if (ddlLangue.SelectedIndex > -1 && ddlLangue.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionLangue;
                  foreach (ListItem item in ddlLangue.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionLangue = new SqlCommand("insert into FilmsLangues (NoFilm, NoLangue) " +
                                          "values (" + strNoFilm + ", " + item.Value.ToString() + ")", con);
                        cmdInsertionLangue.ExecuteNonQuery();
                     }
                  }
               }
               if (ddlSousTitre.SelectedIndex > -1 && ddlSousTitre.Items.FindByValue("null").Selected == false)
               {
                  SqlCommand cmdInsertionSousTitre;
                  foreach (ListItem item in ddlSousTitre.Items)
                  {
                     if (item.Selected)
                     {
                        cmdInsertionSousTitre = new SqlCommand("insert into FilmsSousTitres (NoFilm, NoSousTitre) " +
                                          "values (" + strNoFilm + ", " + item.Value.ToString() + ")", con);
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
   }

   /*protected void listBox_SelectedIndexChanged(object sender, EventArgs e)
   {
      ListBox ls = (ListBox)sender;
      if (ls.Items.FindByValue("null").Selected == true)
      {
         ls.Items.Clear();
      }
   }*/
}