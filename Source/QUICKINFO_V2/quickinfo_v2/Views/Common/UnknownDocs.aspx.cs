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
    public partial class UnknownDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadUnknownSummary();

                loadUploadedDocumentsToGridNew();
                loadUploadedDocumentsToGridRenewal();
                loadUploadedDocumentsToGridEndorsement();
                loadUploadedDocumentsToGridCancellation();



            }
        }

        private void loadUnknownSummary()
        {
            grdUnknownJobSummary.DataSource = null;
            grdUnknownJobSummary.DataBind();


            DashboardController dashboardController = new DashboardController();

            DataTable dtSummary = dashboardController.getUnknownSummary();

            grdUnknownJobSummary.DataSource = dtSummary;

            if (grdUnknownJobSummary.DataSource != null)
            {
                grdUnknownJobSummary.DataBind();
            }

        }

        protected void grdUploadedDocsNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocPath = e.Row.Cells[4].Text;

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "DocumentViewer.aspx?docPath=" + DocPath);

            }
        }

        private void loadUploadedDocumentsToGridNew()
        {
            string DOCUMENT_UPLOAD_PATH = "";

            //<add key="NEW_BUSINESS_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\NEW\\" />
            //<add key="ENDORSEMENT_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\ENDORSEMENT\\" />
            //<add key="RENEWAL_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\RENEWAL\\" />
            //<add key="CANCELLATION_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\CANCELLATION\\" />

            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["NEW_BUSINESS_UNKNOWN_PATH"].ToString();


            string currentDate = "";
            currentDate = System.DateTime.Today.ToString("dd_MM_yyyy");


            string folderPath = @DOCUMENT_UPLOAD_PATH + currentDate;

            if (Directory.Exists(folderPath))
            {
                if (Directory.GetFiles(folderPath) == null)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            string[] filePaths = Directory.GetFiles(folderPath);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            grdUploadedDocsNew.DataSource = files;
            grdUploadedDocsNew.DataBind();


        }

        protected void grdUploadedDocsRenewal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocPath = e.Row.Cells[4].Text;

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "DocumentViewer.aspx?docPath=" + DocPath);

            }
        }

        private void loadUploadedDocumentsToGridRenewal()
        {
            string DOCUMENT_UPLOAD_PATH = "";

            //<add key="NEW_BUSINESS_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\NEW\\" />
            //<add key="ENDORSEMENT_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\ENDORSEMENT\\" />
            //<add key="RENEWAL_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\RENEWAL\\" />
            //<add key="CANCELLATION_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\CANCELLATION\\" />




            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UNKNOWN_PATH"].ToString();



            string currentDate = "";
            currentDate = System.DateTime.Today.ToString("dd_MM_yyyy");


            string folderPath = @DOCUMENT_UPLOAD_PATH + currentDate;

            if (Directory.Exists(folderPath))
            {
                if (Directory.GetFiles(folderPath) == null)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            string[] filePaths = Directory.GetFiles(folderPath);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            grdUploadedDocsRenewal.DataSource = files;
            grdUploadedDocsRenewal.DataBind();


        }
        protected void grdUploadedDocsEndorsement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocPath = e.Row.Cells[4].Text;

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "DocumentViewer.aspx?docPath=" + DocPath);

            }
        }

        private void loadUploadedDocumentsToGridEndorsement()
        {
            string DOCUMENT_UPLOAD_PATH = "";

            //<add key="NEW_BUSINESS_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\NEW\\" />
            //<add key="ENDORSEMENT_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\ENDORSEMENT\\" />
            //<add key="RENEWAL_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\RENEWAL\\" />
            //<add key="CANCELLATION_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\CANCELLATION\\" />


            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UNKNOWN_PATH"].ToString();

            string currentDate = "";
            currentDate = System.DateTime.Today.ToString("dd_MM_yyyy");



            string folderPath = @DOCUMENT_UPLOAD_PATH + currentDate;

            if (Directory.Exists(folderPath))
            {
                if (Directory.GetFiles(folderPath) == null)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            string[] filePaths = Directory.GetFiles(folderPath);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            grdUploadedDocsEndorsement.DataSource = files;
            grdUploadedDocsEndorsement.DataBind();


        }



        protected void grdUploadedDocsCancellation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocPath = e.Row.Cells[4].Text;

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "DocumentViewer.aspx?docPath=" + DocPath);

            }
        }

        private void loadUploadedDocumentsToGridCancellation()
        {
            string DOCUMENT_UPLOAD_PATH = "";

            //<add key="NEW_BUSINESS_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\NEW\\" />
            //<add key="ENDORSEMENT_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\ENDORSEMENT\\" />
            //<add key="RENEWAL_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\RENEWAL\\" />
            //<add key="CANCELLATION_UNKNOWN_PATH" value="\\\\192.168.10.103\\HNBGI\\UNKNOWN_SCN_DOCS\\CANCELLATION\\" />


            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UNKNOWN_PATH"].ToString();

            string currentDate = "";
            currentDate = System.DateTime.Today.ToString("dd_MM_yyyy");



            string folderPath = @DOCUMENT_UPLOAD_PATH + currentDate;

            if (Directory.Exists(folderPath))
            {
                if (Directory.GetFiles(folderPath) == null)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            string[] filePaths = Directory.GetFiles(folderPath);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            grdUploadedDocsCancellation.DataSource = files;
            grdUploadedDocsCancellation.DataBind();


        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            loadUnknownSummary();

            loadUploadedDocumentsToGridNew();
            loadUploadedDocumentsToGridRenewal();
            loadUploadedDocumentsToGridEndorsement();
            loadUploadedDocumentsToGridCancellation();
        }



    }
}