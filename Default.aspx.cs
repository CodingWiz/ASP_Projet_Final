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
        if (valMDP.IsValid && valNomU.IsValid)
        {
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
                    Server.Transfer("ListeTous.aspx", true);
                }
            }
            else
            {
                
            }
            con.Close();
        }
    }
}
