using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using librairie_projet2;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConnexion_Click(object sender, EventArgs e)
    {

        if (valMDP.IsValid && valNomU.IsValid &&rangeMDP.IsValid)
        {
            lblErreurSQL.Text = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            con.Open();
            String strNomU = tbNomU.Text;
            String strmdp = tbMDP.Text;
            string strRequete = "SELECT COUNT(*) FROM Utilisateurs WHERE MotPasse='" + strmdp + "' AND NomUtilisateur='" + strNomU + "'";
            SqlCommand sqlCom = new SqlCommand(strRequete, con);
            dynamic compteur = sqlCom.ExecuteScalar();
            if (compteur > 0)
            {

                if (HttpContext.Current.Request.Url.AbsolutePath != "/ListeTous.aspx")
                {
                    Session["NomU"] = tbNomU.Text;
                    Session["MotDePasse"] = tbMDP.Text;
                    string strRequete2 = "SELECT NoUtilisateur FROM Utilisateurs WHERE MotPasse='" + strmdp + "' AND NomUtilisateur='" + strNomU + "'";
                    SqlCommand sqlCom2 = new SqlCommand(strRequete2, con);
                    dynamic noU = sqlCom2.ExecuteScalar();
                    Session["Connecte"] = true;
                    Session["NoU"] = noU;
                    Session["Mode"] = "abrege";
                    Response.Redirect("ListeTous.aspx");
                }
            }
            else
            {
                string strRequeteENomU = "SELECT COUNT(*) FROM Utilisateurs WHERE NomUtilisateur='" + strNomU + "'";
                SqlCommand sqlComNomU = new SqlCommand(strRequeteENomU, con);
                dynamic compteurNomU = sqlComNomU.ExecuteScalar();
                if (compteurNomU < 1)
                {
                    lblErreurSQL.Text += "Le nom d'utilisateur n'est pas valide. Êtes-vous sûr que c'est votre nom d'utilisateur?";
                }
                else
                {
                    
                        lblErreurSQL.Text += "Le mot de passe n'est pas valide. Êtes-vous sûr que c'est votre mot de passe?";
                    
                }

            }
            con.Close();
        }
    }
}