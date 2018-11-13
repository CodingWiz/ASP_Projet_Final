using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using librairie_projet2;

public partial class Suppression : System.Web.UI.Page
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
      //DropDownList ddlModification = new DropDownList();

      Label lblTitreFr = librairie.lblDYN(phDynamique, "lblTitreFr", "C'est l'apocalypse", "sCenter");

      Button btnModification = librairie.btnDYN(phDynamique, "btnModification", "Supprimer le DVD", "sCenter");
      /*for (Int32 intIncrement = 1; intIncrement <= 10; intIncrement++)
      {
         Label lblTitreFr = librairie.lblDYN(phDynamique, "lblTitreFr" + intIncrement, "Titre en français", "sCenter");
         TextBox tbTitreFr = librairie.tbDYN(phDynamique, "tbTitreFr" + intIncrement, "", "15", "sCenter");
         tbTitreFr.Attributes.Add("placeholder", "Titre en français");

         Button btnModification = librairie.btnDYN(phDynamique, "btnModification" + intIncrement, "Modificationer le DVD", "sCenter");

         librairie.brDYN(phDynamique);
         librairie.brDYN(phDynamique);
         librairie.brDYN(phDynamique);
      }*/
   }
}