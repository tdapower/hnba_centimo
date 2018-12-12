using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Controllers.TCSPolicy;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class ProcessView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                validatePageAuthentication();
                loadPrefJobTypes();
                LockComponents();
                LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());

                LoadMissedProposal();

                hid_Ticker.Value = new TimeSpan(0, 0, 0).ToString();
                Timer1.Enabled = false;


            }
        }

        private void loadPrefJobTypes()
        {
            ddlPrefJobType.Items.Clear();
            ddlPrefJobType.Items.Add(new ListItem("All", "A"));
            ddlPrefJobType.Items.Add(new ListItem("New", "N"));
            ddlPrefJobType.Items.Add(new ListItem("Renewal", "R"));
            ddlPrefJobType.Items.Add(new ListItem("Endorsement", "E"));
            ddlPrefJobType.Items.Add(new ListItem("Cancellation", "C"));
            ddlPrefJobType.Items.Add(new ListItem("All Without Cancellation", "NC"));


            if (Request.Cookies["userProcJobTypeCookie"] != null)
            {
                string userPrefJobType = Request.Cookies["userProcJobTypeCookie"].Value;

                ddlPrefJobType.ClearSelection();
                ddlPrefJobType.Items.FindByValue(userPrefJobType).Selected = true;
            }
        }



        private void validatePageAuthentication()
        {
            if (Request.Params["pagecode"] != null)
            {
                if (Request.Params["pagecode"] != "")
                {
                    UserAuthentication userAuthentication = new UserAuthentication();
                    if (!userAuthentication.IsAuthorizeForThisPage(Context.User.Identity.Name, Request.Params["pagecode"].ToString()))
                    {
                        Response.Redirect("~/NoPermission.aspx");
                    }
                }
            }
        }

        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());

            LoadMissedProposal();
        }
        private void LoadUploadedProposal(string jobType)
        {
            grdUploadedProposal.DataSource = null;
            grdUploadedProposal.DataBind();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();
            //  grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatus(APPROVED_BY_SCRUTINIZING);

            if (jobType == "" || jobType == "A")
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatus(APPROVED_BY_SCRUTINIZING);
            }
            else if (jobType == "NC")
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusExceptOfSpecificType(APPROVED_BY_SCRUTINIZING, "C");
            }
            else
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfSpecificType(APPROVED_BY_SCRUTINIZING, jobType);
            }

            if (grdUploadedProposal.DataSource != null)
            {
                grdUploadedProposal.DataBind();
            }

            //if (grdUploadedProposal.Rows.Count < 1)
            //{
            //    btnTakeJob.Enabled = false;
            //}
            //else
            //{
            //    btnTakeJob.Enabled = true;
            //}

        }

        private void LoadMissedProposal()
        {
            grdMissedUploadedProposal.DataSource = null;
            grdMissedUploadedProposal.DataBind();

            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }

            ProposalUploadController proposalUploadController = new ProposalUploadController();
            grdMissedUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfUsers(TAKEN_BY_PROCESSING, UserCode);

            if (grdMissedUploadedProposal.DataSource != null)
            {
                grdMissedUploadedProposal.DataBind();
                updPnlMissedProposals.Update();
            }




        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            hid_Ticker.Value = TimeSpan.Parse(hid_Ticker.Value).Add(new TimeSpan(0, 0, 1)).ToString();
            lit_Timer.Text = "<font size=10 color=red>" + hid_Ticker.Value.ToString() + "</font>";
        }


        private void LockComponents()
        {

            //txtQuotationNo.Enabled = false;
            //txtVehicleNo.Enabled = false;
            //txtEngineNo.Enabled = false;
            //txtChassisNo.Enabled = false;
            chkIsCoverNoteAvailable.Enabled = false;
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
            txtProposalUploadId.Text = "";

            lblMessage.Text = "";

            txtJobTypeName.Text = "";
            txtJobType.Text = "";
            txtJobNo.Text = "";

            txtQuotationNo.Text = "";
            txtVehicleNo.Text = "";
            txtEngineNo.Text = "";
            txtChassisNo.Text = "";
            chkIsCoverNoteAvailable.Checked = false;
            txtCoverNotePeriod.Text = "";
            txtAddressLine1.Text = "";
            txtAddressLine2.Text = "";
            txtAddressLine3.Text = "";
            txtYearOfMake.Text = "";
            txtFirstRegDate.Text = "";
            txtCubicCapacity.Text = "";
            txtJobRemarks.Text = "";
            txtScrutinizationRemarks.Text = "";
            txtIssueType.Text = "";
            txtPolicyType.Text = "";

            txtEndorsementType.Text = "";
            txtCancellationType.Text = "";

            txtRemarks.Text = "";
            txtTCSProposalNo.Text = "";
            txtTCSProposalNo.Enabled = true;


            chklPendings.Items.Clear();


            grdUploadedDocs.DataSource = null;
            grdUploadedDocs.DataBind();


            grdPreviousDocuments.DataSource = null;
            grdPreviousDocuments.DataBind();


            divQuotationNo.Visible = false;
            divJobNo.Visible = false;
            divPolicyNo.Visible = false;


            //  btnOpenQuotationCalculation.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());
        }

        protected void btnTakeJob_Click(object sender, EventArgs e)
        {
            if (grdUploadedProposal.Rows.Count < 1)
            {
                return;
            }
            if (grdMissedUploadedProposal.Rows.Count >= 2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please complete missed jobs before taking new job');", true);
                return;
            }

            if (txtProposalUploadId.Text != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Complete the current job before taking new job');", true);
                return;

            }


            ClearComponents();


            //btnTakeJob.Enabled = false;

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
            //  proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatus(APPROVED_BY_SCRUTINIZING);



            string jobType = "";
            jobType = ddlPrefJobType.SelectedValue.ToString();
            if (jobType == "" || jobType == "A")
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatus(APPROVED_BY_SCRUTINIZING);
            }
            else if (jobType == "NC")
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusOfNotSpecifiedType(APPROVED_BY_SCRUTINIZING, "C");
            }
            else
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusOfSpecificType(APPROVED_BY_SCRUTINIZING, jobType);
            }








            if (proposalUpload.ProposalUploadId == 0)
            {
                return;
            }







            //To update the status of the ProposalUpload as TAKEN_BY_SCRUTINIZING

            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();



            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(proposalUpload.ProposalUploadId, UserCode, TAKEN_BY_PROCESSING, "Taken for Process");





            txtJobType.Text = proposalUpload.JobType;
            if (txtJobType.Text == "N")
            {
                txtJobTypeName.Text = "New";

                divQuotationNo.Visible = true;
                divJobNo.Visible = false;
                divPolicyNo.Visible = false;


                //  btnOpenQuotationCalculation.Visible = true;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;

                //   btnOpenQuotationCalculation.Visible = false;
            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;

                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;

            }
            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;
            }



            txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
            txtQuotationNo.Text = proposalUpload.QuotationNo;
            txtJobNo.Text = proposalUpload.JobNumber;

            txtVehicleNo.Text = proposalUpload.VehicleNo;
            txtEngineNo.Text = proposalUpload.EngineNo;
            txtChassisNo.Text = proposalUpload.ChassisNo;

            if (proposalUpload.IsCoverNoteAvailable == 1)
            {
                chkIsCoverNoteAvailable.Checked = true;
            }
            else
            {
                chkIsCoverNoteAvailable.Checked = false;

            }

            txtCoverNotePeriod.Text = proposalUpload.CoverNotePeriod;
            txtAddressLine1.Text = proposalUpload.AddressLine1;
            txtAddressLine2.Text = proposalUpload.AddressLine2;
            txtAddressLine3.Text = proposalUpload.AddressLine3;
            txtYearOfMake.Text = proposalUpload.YearOfMake;
            txtFirstRegDate.Text = proposalUpload.FirstRegDate;
            txtCubicCapacity.Text = proposalUpload.CubicCapacity;


            txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;


            txtEndorsementType.Text = proposalUpload.EndorsementType;
            txtCancellationType.Text = proposalUpload.CancellationType;


            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
            if (txtJobType.Text == "N")
            {

                loadUploadedDocumentsToGrid(proposalUpload.QuotationNo);
            }
            else if (txtJobType.Text == "E")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, ENDORSEMENT_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }
            else if (txtJobType.Text == "R")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, RENEWAL_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }
            else if (txtJobType.Text == "C")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }


            txtScrutinizationRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, APPROVED_BY_SCRUTINIZING);
            txtIssueType.Text = proposalUpload.IssueType;
            txtPolicyType.Text = proposalUpload.PolicyType;

            loadPendingsOfJob(Convert.ToInt32(txtProposalUploadId.Text));

            if (txtPolicyNo.Text != "")
            {

                var previousJobList = new List<ProposalUpload>();

                ProposalUploadController proposalUploadController2 = new ProposalUploadController();
                previousJobList = proposalUploadController2.loadOtherJobsByPolicyNo(txtPolicyNo.Text, txtJobNo.Text, txtJobType.Text);

                List<ListItem> uploadedFiles = new List<ListItem>();

                foreach (ProposalUpload previousJob in previousJobList)
                {
                    List<ListItem> files = new List<ListItem>();
                    files = loadPreviousDocumentsToGrid(previousJob.JobNumber, previousJob.JobType, previousJob.JobStatus);
                    if (files != null)
                    {
                        uploadedFiles.AddRange(files);
                    }

                }

                grdPreviousDocuments.DataSource = uploadedFiles;
                grdPreviousDocuments.DataBind();

            }


            //Start the counter
            hid_Ticker.Value = "0";
            Timer1.Enabled = true;
        }

        protected void btnRefreshMissedUploadedProposal_Click(object sender, EventArgs e)
        {
            LoadMissedProposal();
        }

        protected void btnTakeJobMissedUploadedProposal_Click(object sender, EventArgs e)
        {
            if (grdMissedUploadedProposal.Rows.Count < 1)
            {
                return;
            }
            if (txtProposalUploadId.Text != "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Complete the current job before taking new job');", true);
                return;

            }


            ClearComponents();


            //btnTakeJob.Enabled = false;

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }

            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();

            // proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusAndUser(TAKEN_BY_PROCESSING, UserCode);

            proposalUpload = proposalUploadController.GetUploadedProposalOfProposalUploadId(grdMissedUploadedProposal.Rows[0].Cells[3].Text);


            if (proposalUpload.ProposalUploadId == 0)
            {
                return;
            }

            txtJobType.Text = proposalUpload.JobType;
            if (txtJobType.Text == "N")
            {
                txtJobTypeName.Text = "New";

                divQuotationNo.Visible = true;
                divJobNo.Visible = false;
                divPolicyNo.Visible = false;


                //  btnOpenQuotationCalculation.Visible = true;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;

                //   btnOpenQuotationCalculation.Visible = false;
            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;

                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;

            }
            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;

                txtTCSProposalNo.Enabled = false;
            }



            txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
            txtQuotationNo.Text = proposalUpload.QuotationNo;
            txtJobNo.Text = proposalUpload.JobNumber;

            txtVehicleNo.Text = proposalUpload.VehicleNo;
            txtEngineNo.Text = proposalUpload.EngineNo;
            txtChassisNo.Text = proposalUpload.ChassisNo;

            if (proposalUpload.IsCoverNoteAvailable == 1)
            {
                chkIsCoverNoteAvailable.Checked = true;
            }
            else
            {
                chkIsCoverNoteAvailable.Checked = false;

            }

            txtCoverNotePeriod.Text = proposalUpload.CoverNotePeriod;
            txtAddressLine1.Text = proposalUpload.AddressLine1;
            txtAddressLine2.Text = proposalUpload.AddressLine2;
            txtAddressLine3.Text = proposalUpload.AddressLine3;
            txtYearOfMake.Text = proposalUpload.YearOfMake;
            txtFirstRegDate.Text = proposalUpload.FirstRegDate;
            txtCubicCapacity.Text = proposalUpload.CubicCapacity;


            txtTCSProposalNo.Text = proposalUpload.TCSProposalNo;


            txtEndorsementType.Text = proposalUpload.EndorsementType;
            txtCancellationType.Text = proposalUpload.CancellationType;

            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
            if (txtJobType.Text == "N")
            {

                loadUploadedDocumentsToGrid(proposalUpload.QuotationNo);
            }
            else if (txtJobType.Text == "E")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, ENDORSEMENT_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }
            else if (txtJobType.Text == "R")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, RENEWAL_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }
            else if (txtJobType.Text == "C")
            {

                txtPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;

            }


            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
            txtScrutinizationRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, APPROVED_BY_SCRUTINIZING);
            txtIssueType.Text = proposalUpload.IssueType;
            txtPolicyType.Text = proposalUpload.PolicyType;


            loadPendingsOfJob(Convert.ToInt32(txtProposalUploadId.Text));

            if (txtPolicyNo.Text != "")
            {

                var previousJobList = new List<ProposalUpload>();

                ProposalUploadController proposalUploadController2 = new ProposalUploadController();
                previousJobList = proposalUploadController2.loadOtherJobsByPolicyNo(txtPolicyNo.Text, txtJobNo.Text, txtJobType.Text);

                List<ListItem> uploadedFiles = new List<ListItem>();

                foreach (ProposalUpload previousJob in previousJobList)
                {
                    List<ListItem> files = new List<ListItem>();
                    files = loadPreviousDocumentsToGrid(previousJob.JobNumber, previousJob.JobType, previousJob.JobStatus);
                    if (files != null)
                    {
                        uploadedFiles.AddRange(files);
                    }

                }

                grdPreviousDocuments.DataSource = uploadedFiles;
                grdPreviousDocuments.DataBind();

            }


            string previousTime = "";
            previousTime = proposalUploadController.GetTimeDifferenceFromStatusToNow(Convert.ToInt32(txtProposalUploadId.Text), TAKEN_BY_PROCESSING);


            //Start the counter
            if (previousTime != "")
            {
                TimeSpan previousTimeTimespan = TimeSpan.FromSeconds(Convert.ToDouble(previousTime));
                hid_Ticker.Value = TimeSpan.Parse(previousTimeTimespan.ToString()).Add(new TimeSpan(0, 0, 1)).ToString(); ;
            }
            else
            {

                hid_Ticker.Value = "0";
            }
            Timer1.Enabled = true;

        }



        protected void btnDone_Click(object sender, EventArgs e)
        {
            if (txtProposalUploadId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Take a job first');", true);
                return;

            }
            TCSPolicyController tCSPolicyController = new TCSPolicyController();


            if (txtJobType.Text == "N")
            {

                if (txtTCSProposalNo.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter TCS Proposal Number');", true);
                    return;
                }
                if (!tCSPolicyController.checkIsProposalNoAvailable(txtTCSProposalNo.Text.Trim(), "TCS"))
                {
                    if (!tCSPolicyController.checkIsProposalNoAvailable(txtTCSProposalNo.Text.Trim(), "TAKAFUL"))
                    {

                        lblMessage.Text = "Invalid Proposal Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid Proposal Number');", true);
                        return;
                    }
                }


            }




            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(Convert.ToInt32(txtProposalUploadId.Text), UserCode, COMPLETED_BY_PROCESSING, txtRemarks.Text);


            if (txtJobType.Text == "N")
            {


                proposalUploadController.UpdateTCSProposalNoAndPolicyId(Convert.ToInt32(txtProposalUploadId.Text), txtTCSProposalNo.Text.Trim());


            }

            //stop the counter
            Timer1.Enabled = false;


            ClearComponents();
            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());

            LoadMissedProposal();

            //  btnTakeJob.Enabled = true;
        }




        private void loadUploadedDocumentsToGrid(string quotationNo)
        {
            string DOCUMENT_UPLOAD_PATH = "";

            if (txtJobType.Text == "N")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();
            }
            else if (txtJobType.Text == "E")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();
            }
            else if (txtJobType.Text == "R")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();
            }
            else if (txtJobType.Text == "C")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();
            }

            string folderPath = @DOCUMENT_UPLOAD_PATH + quotationNo;
            // string folderPath = @DOCUMENT_UPLOAD_PATH;
            List<ListItem> files = new List<ListItem>();

            // string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); 
            try
            {

                string[] filePaths = Directory.GetFiles(folderPath);
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


                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }

            }
            catch (Exception ex)
            {

            }
            grdUploadedDocs.DataSource = files;
            grdUploadedDocs.DataBind();
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
        protected void grdPreviousDocuments_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void grdUploadedProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
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
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }
            }
        }
        protected void grdMissedUploadedProposal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;


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
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }
            }
        }

        protected void btnOpenQuotationCalculation_Click(object sender, EventArgs e)
        {
            if (txtQuotationNo.Text != "")
            {
                int quotationJobID = 0;
                int quotationRevisionNo = 0;

                ProposalUploadController proposalUploadController = new ProposalUploadController();


                int[] quoteVals = proposalUploadController.getJobNoAndRevisionNoOfQuotation(txtQuotationNo.Text);
                quotationJobID = quoteVals[0];
                quotationRevisionNo = quoteVals[1];


                string url = "http://192.168.10.212:82/quickinfo-allinone/MotorNewBusiness/Reports/MotorQuotationCalculation.aspx?JobID=" + quotationJobID + "&RevisionNo=" + quotationRevisionNo;
                string s = "window.open('" + url + "', 'popup_window', 'width=600,height=500,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }



        }


        private void loadPendingsOfJob(int ProposalUploadId)
        {
            chklPendings.Items.Clear();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT T.PENDING_CODE,T.PENDING_NAME,P.IS_SELECTED FROM MNBQ_WF_PENDINGS T INNER JOIN MNBQ_WF_PENDINGS_OF_JOBS P ON T.PENDING_CODE=P.PENDING_CODE" +
                 " WHERE P.PROPOSAL_UPLOAD_ID=" + ProposalUploadId + " " +
                " ORDER BY T.PENDING_NAME ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ListItem item = new ListItem();
                    item.Text = dr["PENDING_NAME"].ToString();
                    item.Value = dr["PENDING_CODE"].ToString();
                    item.Selected = Convert.ToBoolean(dr["IS_SELECTED"]);
                    chklPendings.Items.Add(item);


                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        protected void ddlPrefJobType_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());





            HttpCookie userJobTypeCookie = new HttpCookie("userProcJobTypeCookie");
            userJobTypeCookie.Value = ddlPrefJobType.SelectedValue.ToString();
            // aCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userJobTypeCookie);


        }

        private List<ListItem> loadPreviousDocumentsToGrid(string quotationNo, string JobType, string Status)
        {
            string DOCUMENT_UPLOAD_PATH = "";

            string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();

            if (Status == REJECTED_BY_SCRUTINIZING)
            {

                if (JobType == "N")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["NEW_BUSINESS_REJECTED_PATH"].ToString();
                }
                else if (JobType == "E")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_REJECTED_PATH"].ToString();
                }
                else if (JobType == "R")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_REJECTED_PATH"].ToString();
                }
                else if (JobType == "C")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_REJECTED_PATH"].ToString();
                }
            }
            else
            {

                if (JobType == "N")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "E")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "R")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();
                }
                else if (JobType == "C")
                {
                    DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();
                }
            }




            string folderPath = @DOCUMENT_UPLOAD_PATH + quotationNo;
          
            List<ListItem> files = new List<ListItem>();
            try
            {
                string[] filePaths = Directory.GetFiles(folderPath);

                if (Directory.Exists(folderPath))
                {
                    if (Directory.GetFiles(folderPath) == null)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
             
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return files;

        }
    }
}