using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modifier : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLienMod_Click(object sender, EventArgs e)
    {
        string strpath = HttpContext.Current.Request.Url.AbsolutePath;
        if (HttpContext.Current.Request.Url.AbsolutePath!="/Modification.aspx")
        Server.Transfer("Modification.aspx", true);
    }

    protected void btnLienAjout_Click(object sender, EventArgs e)
    {
        string strpath = HttpContext.Current.Request.Url.AbsolutePath;
        if (HttpContext.Current.Request.Url.AbsolutePath != "/Ajout.aspx")
            Server.Transfer("Ajout.aspx", true);
    }

    protected void btnLienSup_Click(object sender, EventArgs e)
    {
        string strpath = HttpContext.Current.Request.Url.AbsolutePath;
        if (HttpContext.Current.Request.Url.AbsolutePath != "/Suppression.aspx")
            Server.Transfer("Suppression.aspx", true);
    }
}
