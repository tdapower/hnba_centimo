using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class RenewDocUploadOtherBranchView : System.Web.UI.Page
    {
        System.Timers.Timer aTimer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string UserCode = "";
                string UserBranch = "";
                HttpCookie reqCookies = Request.Cookies["userInfo"];
                if (reqCookies != null)
                {
                    UserCode = reqCookies["UserCode"].ToString();
                    UserBranch = reqCookies["UserBranch"].ToString();
                    txtUserBranch.Text = UserBranch;
                }

                LockComponents();
                LoadUploadedProposal();


            }
        }


        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadUploadedProposal();
        }

        private void LoadUploadedProposal()
        {
            grdUploadedProposal.DataSource = null;
            grdUploadedProposal.DataBind();

            string RENEWAL_ADDEDStatusName = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();
            grdUploadedProposal.DataSource = proposalUploadController.GetJobsForRenewal(RENEWAL_ADDEDStatusName, txtUserBranch.Text);

            if (grdUploadedProposal.DataSource != null)
            {
                grdUploadedProposal.DataBind();
                updPnlProposals.Update();
            }


            pnlSearchGrid.Visible = true;

            //if (grdUploadedProposal.Rows.Count < 1)
            //{
            //    btnTakeJob.Enabled = false;
            //}
            //else
            //{
            //    btnTakeJob.Enabled = true;
            //}

        }


        private void LockComponents()
        {

            //txtQuotationNo.Enabled = false;
            //txtVehicleNo.Enabled = false;
            //txtEngineNo.Enabled = false;
            //txtChassisNo.Enabled = false;
            //txtCoverNotePeriod.Enabled = false;
            //txtAddressLine1.Enabled = false;
            //txtAddressLine2.Enabled = false;
            //txtAddressLine3.Enabled = false;
            //txtYearOfMake.Enabled = false;
            //txtFirstRegDate.Enabled = false;
            //txtCubicCapacity.Enabled = false;

            ClearComponents();
        }
        private void ClearComponents()
        {
            txtJobTypeName.Text = "";
            txtJobType.Text = "";
            txtJobNo.Text = "";
            txtProposalUploadId.Text = "";
            txtProposalUploadUserCode.Text = "";
            txtEnteredBranchCode.Text = "";

            txtRemarks.Text = "";


            divJobNo.Visible = false;
            divPolicyNo.Visible = false;
        }





        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUploadedProposal();
        }

        protected void btnTakeJob_Click(object sender, EventArgs e)
        {
            if (grdUploadedProposal.Rows.Count < 1)
            {
                return;
            }

            if (txtProposalUploadId.Text != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Complete the current job before taking new job');", true);
                return;

            }
            //if (txtProposalUploadId.Text != "")
            //{
            //    lblMessage.Text = "Complete the current job before taking new job";
            //    return;

            //}
            ClearComponents();
            btnTakeJob.Enabled = false;

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();



            string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
            proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusForRenewalDocUpload(RENEWAL_ADDED, txtUserBranch.Text);

            txtJobType.Text = proposalUpload.JobType;
            if (txtJobType.Text == "N")
            {
                txtJobTypeName.Text = "New";

                divJobNo.Visible = false;
                divPolicyNo.Visible = false;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divJobNo.Visible = true;
                divPolicyNo.Visible = true;
            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divJobNo.Visible = true;
                divPolicyNo.Visible = true;
            }


            txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
            txtJobNo.Text = proposalUpload.JobNumber;





            txtPolicyNo.Text = proposalUpload.TCSPolicyNo;




            //To update the status of the ProposalUpload as TAKEN_BY_SCRUTINIZING

            string TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();

            }


            proposalUploadController.UpdateProposalUploadStatus(proposalUpload.ProposalUploadId, UserCode, TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD, "Renewal Docs Uploaded");

        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (txtJobNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please take a job first');", true);

                return;
            }
            if (!checkIsDocumentsUploaded(txtJobNo.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please scan the documents before press Done');", true);

                return;

            }



            ProposalUploadController proposalUploadController = new ProposalUploadController();



            string RENEWAL_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOCS_ADDED"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(Convert.ToInt32(txtProposalUploadId.Text), UserCode, RENEWAL_DOCS_ADDED, txtRemarks.Text);



            ClearComponents();
            LoadUploadedProposal();
            btnTakeJob.Enabled = true;
        }

        private bool checkIsDocumentsUploaded(string jobNo)
        {
            bool returnVal = false;


            string RENEWAL_DOC_UPLOAD_PATH = "";


            RENEWAL_DOC_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOC_UPLOAD_PATH"].ToString();


            if (File.Exists(RENEWAL_DOC_UPLOAD_PATH + jobNo + ".pdf"))
            {
                returnVal = true;
            }


            return returnVal;
        }





        protected void grdUploadedProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }


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

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "../Common/DocumentViewer.aspx?docPath=" + DocPath);

            }
        }



    }
}