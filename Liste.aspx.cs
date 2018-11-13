using System;
using System.Collections.Generic;
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
    

    protected void btnAff_Click(object sender, EventArgs e)
    {
        Server.Transfer("AffichageDetaille.aspx", true);
    }
}
