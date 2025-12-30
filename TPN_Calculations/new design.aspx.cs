using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TNS_Calculations
{
    public partial class new_design : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblA.Text = "4.5";
                lblOralType.Text = "EBM/PDHM";
                lblPreNanStr.Text = "None";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string dosingWt = txtDosingWt.Text;
            string tfr = txtTFR.Text;
            string feed = txtFeed.Text;
            string ivm = txtIVM.Text;
            string overfill = txtOverfill.Text;

            // Save logic here (Database insert or processing)
        }

    }
}