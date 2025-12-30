using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNS_Calculations
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Implement your logout logic here
            // For example, if you are using Forms Authentication:
            FormsAuthentication.SignOut();
            Response.Redirect("~/UserLogin.aspx"); // Redirect to your login page
            // Or, if you are using Session:
            // Session.Abandon();
            // Response.Redirect("~/Login.aspx");
        }
    }
}