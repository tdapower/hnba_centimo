using quickinfo_v2.Controllers.Dashboard;
using quickinfo_v2.Controllers.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Common
{
    public partial class NotScannedYetJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadNotScannedYetJobsSummary();

           


            }
        }

        private void loadNotScannedYetJobsSummary()
        {
            grdNotScannedYetJobs.DataSource = null;
            grdNotScannedYetJobs.DataBind();


            DashboardController dashboardController = new DashboardController();

            DataTable dtSummary = dashboardController.getNotScannedYetJobsSummary();

            grdNotScannedYetJobs.DataSource = dtSummary;

            if (grdNotScannedYetJobs.DataSource != null)
            {
                grdNotScannedYetJobs.DataBind();
            }

        }

     


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            loadNotScannedYetJobsSummary();

        }



    }
}