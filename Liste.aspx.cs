using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
       
        if (!IsPostBack)
        {
            



            ListeTousTitreFR("TitreFrancais");
            ViewState["SortExprValue"] = "TitreFrancais";
        }
       


    }

    protected void linkButton(object sender, EventArgs e)
    {



        if (sender == Tous)
        {
            /*dgTable.DataSource = null;
            dgTable.DataBind();*/

            hidUtilisateur.Value = "";

            string strRequete = "select Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm, Utilisateurs.NoUtilisateur from Exemplaires " +
                                    "inner join Utilisateurs on Utilisateurs.NoUtilisateur = Exemplaires.NoUtilisateurProprietaire " +
                                    "inner join Films on Films.NoFilm = SUBSTRING(CAST(Exemplaires.NoExemplaire as nvarchar), 1, 6) ORDER BY TitreFrancais";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            con.Open();
            SqlCommand sqlComFilm = new SqlCommand(strRequete, con);

            SqlDataAdapter myAdapter = new SqlDataAdapter(sqlComFilm);
            DataTable maTable = new DataTable();
            myAdapter.Fill(maTable);
            dgTable.DataSource = maTable;
            
            dgTable.VirtualItemCount = 200;
            dgTable.DataBind();
            
            con.Close();



        }
        else if (sender == moi)
        {
            hidUtilisateur.Value = "";

            string strRequete = "select Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm, Utilisateurs.NoUtilisateur from Exemplaires " +
                                    "inner join Utilisateurs on Utilisateurs.NoUtilisateur = Exemplaires.NoUtilisateurProprietaire " +
                                    "inner join Films on Films.NoFilm = SUBSTRING(CAST(Exemplaires.NoExemplaire as nvarchar), 1, 6) " +
                                    "where Utilisateurs.NomUtilisateur = '" + Session["NomU"] + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            con.Open();
            SqlCommand sqlComFilm = new SqlCommand(strRequete, con);

            SqlDataAdapter myAdapter = new SqlDataAdapter(sqlComFilm);
            DataTable maTable = new DataTable();
            myAdapter.Fill(maTable);
            dgTable.DataSource = maTable;
            dgTable.VirtualItemCount = 200;
            dgTable.DataBind();

            con.Close();
        }
        else if (sender == autre)
        {
            if (hidUtilisateur.Value.Length != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);

                string strRequete1 = "select Count(*) from Utilisateurs where NomUtilisateur = '" + hidUtilisateur.Value + "'";

                con.Open();
                SqlCommand sqlComFilm1 = new SqlCommand(strRequete1, con);

                dynamic compteur = sqlComFilm1.ExecuteScalar();

                con.Close();

                if (compteur > 0) 
                {
                    string strRequete2 = "select Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm, Utilisateurs.NoUtilisateur from Exemplaires " +
                                     "inner join Utilisateurs on Utilisateurs.NoUtilisateur = Exemplaires.NoUtilisateurProprietaire " +
                                     "inner join Films on Films.NoFilm = SUBSTRING(CAST(Exemplaires.NoExemplaire as nvarchar), 1, 6) " +
                                     "where Utilisateurs.NomUtilisateur = '" + hidUtilisateur.Value + "'";

                    con.Open();
                    SqlCommand sqlComFilm2 = new SqlCommand(strRequete2, con);

                    SqlDataAdapter myAdapter = new SqlDataAdapter(sqlComFilm2);
                    DataTable maTable = new DataTable();
                    myAdapter.Fill(maTable);
                    dgTable.DataSource = maTable;
                    dgTable.VirtualItemCount = 200;
                    dgTable.DataBind();

                    con.Close();
                }
                else 
                {
                    //Script.RegisterStartupScript(this.GetType(), "alert", "alert('L\'utilisateur séléctionner n'existe pas')", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Utilisateur séléctionné inexistant')", true);
                    dgTable.DataSource = null;
                    dgTable.DataBind();
                }
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insert is successfull')", true);
                dgTable.DataSource = null;
                dgTable.DataBind();
            }
        }
        int pageIndex = dgTable.CurrentPageIndex;
        if (pageIndex == 0)
        {
            btnPremier.Enabled = false;
            btnPrecedent.Enabled = false;
        }
        else
        {
            btnPremier.Enabled = true;
            btnPrecedent.Enabled = true;
        }
        if (pageIndex == (dgTable.PageCount - 1))
        {
            btnDernier.Enabled = false;
            btnSuivant.Enabled = false;
        }
        else
        {
            btnDernier.Enabled = true;
            btnSuivant.Enabled = true;
        }
    }

    protected void ListeTousTitreFR(string orderby)
    {
        if (Request.QueryString["id"] == null)
        {
            string strRequete = "SELECT Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm,Utilisateurs.NoUtilisateur FROM Films inner join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur ORDER BY " + orderby;
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            con.Open();
            SqlCommand sqlComFilm = new SqlCommand(strRequete, con);
            
            SqlDataAdapter myAdapter = new SqlDataAdapter(sqlComFilm);
            DataTable maTable = new DataTable();
            myAdapter.Fill(maTable);
            dgTable.DataSource = maTable;
            dgTable.VirtualItemCount = 200;
            dgTable.DataBind();
            con.Close();

        }
        else
        {
            string strRequete = "SELECT Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm, Utilisateurs.NoUtilisateur FROM Films inner join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur WHERE Utilisateurs.NoUtilisateur = @id ORDER BY " + orderby;

            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
            con.Open();
            SqlCommand sqlComFilm = new SqlCommand(strRequete, con);
            sqlComFilm.Parameters.Add("@id", SqlDbType.NVarChar);
            sqlComFilm.Parameters["@id"].Value = Request.QueryString["id"];
            SqlDataAdapter myAdapter = new SqlDataAdapter(sqlComFilm);
            DataTable maTable = new DataTable();
            myAdapter.Fill(maTable);
            dgTable.DataSource = maTable;
            dgTable.VirtualItemCount = 200;
            dgTable.DataBind();
            
            con.Close();

        }
        int pageIndex = dgTable.CurrentPageIndex;
        if (pageIndex == 0)
        {
            btnPremier.Enabled = false;
            btnPrecedent.Enabled = false;
        }
        else
        {
            btnPremier.Enabled = true;
            btnPrecedent.Enabled = true;
        }
        if (pageIndex == (dgTable.PageCount - 1))
        {
            btnDernier.Enabled = false;
            btnSuivant.Enabled = false;
        }
        else
        {
            btnDernier.Enabled = true;
            btnSuivant.Enabled = true;
        }
    }

    protected void ListeTousNomU()
    {

        string strRequete = "SELECT Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm, Utilisateurs.NoUtilisateur FROM Films inner join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur ORDER BY Utilisateurs.NomUtilisateur";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
        con.Open();
        SqlCommand sqlComFilm = new SqlCommand(strRequete, con);
        SqlDataReader sqlReader = sqlComFilm.ExecuteReader();

        dgTable.DataSource = sqlReader;
        dgTable.VirtualItemCount = 200;
        dgTable.DataBind();
        sqlReader.Close();
        con.Close();
    }

    protected void ListeTousLesDeux()
    {

        string strRequete = "SELECT Films.TitreFrancais, Films.ImagePochette, Utilisateurs.NomUtilisateur, Films.NoFilm FROM Films inner join Utilisateurs ON Films.NoUtilisateurMAJ = Utilisateurs.NoUtilisateur ORDER BY Utilisateurs.NomUtilisateur, Films.TitreFrancais";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["strConnexionDVD"]);
        con.Open();
        SqlCommand sqlComFilm = new SqlCommand(strRequete, con);
        SqlDataReader sqlReader = sqlComFilm.ExecuteReader();

        dgTable.DataSource = sqlReader;
        dgTable.DataBind();
        sqlReader.Close();
        con.Close();
    }
    protected void urlimage()
    {

    }

    protected void btnAff_Click(object sender, EventArgs e)
    {
        Server.Transfer("AffichageDetaille.aspx", true);
    }

    

    protected void btn10Items_Click(object sender, EventArgs e)
    {
        dgTable.PageSize = 10;
        ListeTousTitreFR(ViewState["SortExprValue"].ToString());
    }

    protected void btn15Items_Click(object sender, EventArgs e)
    {
        dgTable.PageSize = 15;
        ListeTousTitreFR(ViewState["SortExprValue"].ToString());
    }
    
    protected void dgTable_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgTable.CurrentPageIndex = e.NewPageIndex;
        ListeTousTitreFR(ViewState["SortExprValue"].ToString());
    }

    protected void dgTable_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        //ListeTousTitreFR(e.SortExpression);
        dgTable.CurrentPageIndex = 0;
        ViewState["SortExprValue"] = e.SortExpression;
        ListeTousTitreFR(e.SortExpression);
    }

    protected void dgTable_PageIndexChanged1(object source, DataGridPageChangedEventArgs e)
    {
        dgTable.CurrentPageIndex = e.NewPageIndex;
        ListeTousTitreFR(ViewState["SortExprValue"].ToString());
    }

    protected void btnSoiMeme_Click(object sender, EventArgs e)
    {
        Response.Redirect("");
    }

    protected void btnDernier_Click(object sender, EventArgs e)
    {
        if (dgTable.CurrentPageIndex < dgTable.PageCount - 1)
        {
            dgTable.CurrentPageIndex = dgTable.PageCount - 1;
            ListeTousTitreFR(ViewState["SortExprValue"].ToString());
        }
    }

    protected void btnPremier_Click(object sender, EventArgs e)
    {
        if (dgTable.CurrentPageIndex > 0)
        {
            dgTable.CurrentPageIndex = 0;
            ListeTousTitreFR(ViewState["SortExprValue"].ToString());
        }
    }

    protected void btnPrecedent_Click(object sender, EventArgs e)
    {
        if (dgTable.CurrentPageIndex > 0)
        {
            dgTable.CurrentPageIndex = dgTable.CurrentPageIndex - 1;
            ListeTousTitreFR(ViewState["SortExprValue"].ToString());
        }
    }

    protected void btnSuivant_Click(object sender, EventArgs e)
    {
        if (dgTable.CurrentPageIndex < dgTable.PageCount - 1)
        {
            dgTable.CurrentPageIndex = dgTable.CurrentPageIndex + 1;
            ListeTousTitreFR(ViewState["SortExprValue"].ToString());
        }
    }
}
