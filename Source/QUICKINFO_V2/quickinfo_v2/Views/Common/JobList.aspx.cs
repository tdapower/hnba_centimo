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
    public partial class JobList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string Status = "";

                if (Request.QueryString["Status"] != null)
                {

                    Status = Request.QueryString["Status"].ToString();



                    loadJobFollowup(Status);



                    ProposalUploadController proposalUploadController = new ProposalUploadController();
                    string jobNo = "";
                    //jobNo = proposalUploadController.GetJobNoFromProposalUploadId(ProposalUploadId);

                    //loadUploadedDocumentsToGrid(jobNo, JobType, Status);
                }





            }
        }

        private void loadJobFollowup(string Status)
        {
            grdResults.DataSource = null;
            grdResults.DataBind();

            ProposalUploadController proposalUploadController = new ProposalUploadController();
            DataTable completedJobs = new DataTable();
            completedJobs = proposalUploadController.GetJobsOfStatus(Status);
            grdResults.DataSource = completedJobs;


            if (grdResults.DataSource != null)
            {
                grdResults.DataBind();
            }

        }
        protected void grdUploadedDocs_RowDataBound(object sender, GridViewRowEventArgs e)
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

        private void loadUploadedDocumentsToGrid(string quotationNo, string JobType, string Status)
        {
            string DOCUMENT_UPLOAD_PATH = "";

            string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();

            if (Status == REJECTED_BY_SCRUTINIZING)
            {

                if (JobType == "New")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["NEW_BUSINESS_REJECTED_PATH"].ToString();
                }
                else if (JobType == "Endorsement")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_REJECTED_PATH"].ToString();
                }
                else if (JobType == "Renewal")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_REJECTED_PATH"].ToString();
                }
                else if (JobType == "Cancellation")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_REJECTED_PATH"].ToString();
                }
            }
            else
            {

                if (JobType == "New")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "Endorsement")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "Renewal")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "Cancellation")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();
                }
            }




            string folderPath = @DOCUMENT_UPLOAD_PATH + quotationNo;
            // string folderPath = @DOCUMENT_UPLOAD_PATH;

            // string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); 

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
            grdUploadedDocs.DataSource = files;
            grdUploadedDocs.DataBind();


        }
    }
}