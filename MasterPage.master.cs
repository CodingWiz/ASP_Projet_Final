using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de MasterPage
/// </summary>
public partial class MasterPage: System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["NomU"] == null) || (Session["MotDePasse"] == null))
        {
            
            Server.Transfer("Default.aspx", true);

        }
        if (Session["Connecte"] == null)
        {
            
            Server.Transfer("Default.aspx", true);
        }
    }
    
    protected void deconnecte(object sender, EventArgs e)
    {
        
        Session["Connecte"] = false;
        Session["NomU"] = null;
        Session["MotDePasse"] = null;
        Session["Mode"] = null;

        Server.Transfer("Default.aspx", true);
    }


    protected void lbPageA_Click(object sender, EventArgs e)
    {

        Response.Redirect("ListeTous.aspx");
        
    }
}