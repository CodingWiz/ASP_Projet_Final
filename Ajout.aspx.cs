using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using librairie_projet2;

public partial class Ajout : System.Web.UI.Page
{
   public libProjet2 librairie = new libProjet2();

   void Page_Load()
   {
      creationFormulaire();
   }

   void rechargePageOnClick(object sender, EventArgs e)
   {
      Response.Redirect(Request.RawUrl);
   }

   void creationFormulaire()
   {
      for (Int32 intIncrement = 1; intIncrement <= 10; intIncrement++)
      {
         /*Label lblAnneSortie = librairie.lblDYN(phDynamique, "lblAnneSortie" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbAnneSortie = librairie.tbDYN(phDynamique, "tbAnneSortie" + intIncrement, "", "15", "sCenter");
         tbAnneSortie.Attributes.Add("placeholder", "Année de sortie");

         Label lblCat = librairie.lblDYN(phDynamique, "lblCat" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbCat = librairie.tbDYN(phDynamique, "tbCat" + intIncrement, "", "15", "sCenter");
         tbCat.Attributes.Add("placeholder", "Année de sortie");

         Label lblFormat = librairie.lblDYN(phDynamique, "lblFormat" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbFormat = librairie.tbDYN(phDynamique, "tbFormat" + intIncrement, "", "15", "sCenter");
         tbFormat.Attributes.Add("placeholder", "Année de sortie");

         Label lblResume = librairie.lblDYN(phDynamique, "lblResume" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbResume = librairie.tbDYN(phDynamique, "tbResume" + intIncrement, "", "15", "sCenter");
         tbResume.Attributes.Add("placeholder", "Année de sortie");

         Label lblDureeMin = librairie.lblDYN(phDynamique, "lblDureeMin" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbDureeMin = librairie.tbDYN(phDynamique, "tbDureeMin" + intIncrement, "", "15", "sCenter");
         tbDureeMin.Attributes.Add("placeholder", "Année de sortie");

         Label lblFilmOriginal = librairie.lblDYN(phDynamique, "lblFilmOriginal" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbFilmOriginal = librairie.tbDYN(phDynamique, "tbFilmOriginal" + intIncrement, "", "15", "sCenter");
         tbFilmOriginal.Attributes.Add("placeholder", "Année de sortie");

         Label lblImgPochette = librairie.lblDYN(phDynamique, "lblImgPochette" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbImgPochette = librairie.tbDYN(phDynamique, "tbImgPochette" + intIncrement, "", "15", "sCenter");
         tbImgPochette.Attributes.Add("placeholder", "Année de sortie");

         Label lblNbDisque = librairie.lblDYN(phDynamique, "lblNbDisque" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbNbDisque = librairie.tbDYN(phDynamique, "tbNbDisque" + intIncrement, "", "15", "sCenter");
         tbNbDisque.Attributes.Add("placeholder", "Année de sortie");*/

         Label lblTitreFr = librairie.lblDYN(phDynamique, "lblTitreFr" + intIncrement, "Écrivez le Titre en français", "sCenter");
         TextBox tbTitreFr = librairie.tbDYN(phDynamique, "tbTitreFr" + intIncrement, "", "15", "sCenter");
         tbTitreFr.Attributes.Add("placeholder", "Titre en français");

         /*Label lblTitreOr = librairie.lblDYN(phDynamique, "lblTitreOr" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbTitreOr = librairie.tbDYN(phDynamique, "tbTitreOr" + intIncrement, "", "15", "sCenter");
         tbTitreOr.Attributes.Add("placeholder", "Année de sortie");

         Label lblVersEtendue = librairie.lblDYN(phDynamique, "lblVersEtendue" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbVersEtendue = librairie.tbDYN(phDynamique, "tbVersEtendue" + intIncrement, "", "15", "sCenter");
         tbVersEtendue.Attributes.Add("placeholder", "Année de sortie");

         Label lblNoRealisateur = librairie.lblDYN(phDynamique, "lblNoRealisateur" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbNoRealisateur = librairie.tbDYN(phDynamique, "tbNoRealisateur" + intIncrement, "", "15", "sCenter");
         tbNoRealisateur.Attributes.Add("placeholder", "Année de sortie");

         Label lblNoProducteur = librairie.lblDYN(phDynamique, "lblNoProducteur" + intIncrement, "Année de sortie", "sCenter");
         TextBox tbNoProducteur = librairie.tbDYN(phDynamique, "tbNoProducteur" + intIncrement, "", "15", "sCenter");
         tbNoProducteur.Attributes.Add("placeholder", "Année de sortie");*/

         //Button btnAjout = librairie.btnDYN(phDynamique, "btnAjout" + intIncrement, "Ajouter le DVD", "sCenter");

         librairie.brDYN(phDynamique);
         librairie.brDYN(phDynamique);
         librairie.brDYN(phDynamique);
      }

      Button btnAjout = librairie.btnDYN(phDynamique, "btnAjout", "Ajouter le DVD", "sCenter");
   }
}