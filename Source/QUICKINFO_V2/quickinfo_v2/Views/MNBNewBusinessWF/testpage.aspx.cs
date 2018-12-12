using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class testpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {


            string jobNoText = "";
            jobNoText = "Generated Job Nuber is ";
             Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('" + jobNoText + "');", true);
           // Page.ClientScript.RegisterStartupScript(GetType(), "Message", "jAlert('" + jobNoText + "','fffff');", true);


        }
    }
}