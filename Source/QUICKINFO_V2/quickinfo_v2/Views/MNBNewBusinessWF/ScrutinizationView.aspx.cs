using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class ScrutinizationView : System.Web.UI.Page
    {
        System.Timers.Timer aTimer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                validatePageAuthentication();
                loadPrefJobTypes();
                LockComponents();
                LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());
                LoadMissedProposal();
                loadIssueTypes();
                loadPolicyTypes();
                btnReject.Attributes.Add("onClick", "if(confirm('Are you sure to Reject this Job?','Motor New Business Workflow')){}else{return false}");


            }
        }

        private void loadPrefJobTypes()
        {
            ddlPrefJobType.Items.Clear();
            ddlPrefJobType.Items.Add(new ListItem("All", "A"));
            ddlPrefJobType.Items.Add(new ListItem("New", "N"));
            ddlPrefJobType.Items.Add(new ListItem("Renewal", "R"));
            ddlPrefJobType.Items.Add(new ListItem("Endorsement", "E"));
            ddlPrefJobType.Items.Add(new ListItem("Endorsement-Certificate", "EC"));
            ddlPrefJobType.Items.Add(new ListItem("Cancellation", "C"));
            ddlPrefJobType.Items.Add(new ListItem("All Without Cancellation", "NC"));


            if (Request.Cookies["userScrutiJobTypeCookie"] != null)
            {
                string userPrefJobType = Request.Cookies["userScrutiJobTypeCookie"].Value;

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

        //private void LoadUploadedProposal()
        //{
        //    grdUploadedProposal.DataSource = null;
        //    grdUploadedProposal.DataBind();

        //    string INITIALStatusName = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();

        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatus(INITIALStatusName);

        //    if (grdUploadedProposal.DataSource != null)
        //    {
        //        grdUploadedProposal.DataBind();
        //        updPnlProposals.Update();
        //    }



        //}


        private void LoadUploadedProposal(string jobType)
        {
            grdUploadedProposal.DataSource = null;
            grdUploadedProposal.DataBind();

            string INITIALStatusName = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            if (jobType == "" || jobType == "A")
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatus(INITIALStatusName);
            }
            else if (jobType == "NC")
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusExceptOfSpecificType(INITIALStatusName, "C");
            }
            else if (jobType == "EC")
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfCertificateConvertion(INITIALStatusName, "E");
            }
            else
            {
                grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfSpecificType(INITIALStatusName, jobType);
            }

            if (grdUploadedProposal.DataSource != null)
            {
                grdUploadedProposal.DataBind();
                updPnlProposals.Update();
            }



        }



        private void LoadMissedProposal()
        {
            grdMissedUploadedProposal.DataSource = null;
            grdMissedUploadedProposal.DataBind();

            string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            ProposalUploadController proposalUploadController = new ProposalUploadController();
            grdMissedUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfUsers(TAKEN_BY_SCRUTINIZING, UserCode);

            if (grdMissedUploadedProposal.DataSource != null)
            {
                grdMissedUploadedProposal.DataBind();
                updPnlMissedProposals.Update();
            }




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
            txtJobTypeName.Text = "";
            txtJobRemarks.Text = "";
            txtJobType.Text = "";
            txtIsValidated.Text = "";
            txtJobNo.Text = "";
            txtQuotationNo.Text = "";
            txtProposalUploadId.Text = "";
            txtProposalUploadUserCode.Text = "";
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
            txtEnteredBranchCode.Text = "";
            txtVehicleChassisNo.Text = "";
            txtScrutinizeRemarks.Text = "";


            txtEndorsementType.Text = "";
            txtCancellationType.Text = "";

            chklPendings.Items.Clear();
            chklRejectReasons.Items.Clear();

            chkIsCertificateConvertion.Checked = false;

            ddlIssueType.SelectedValue = "0";
            ddlPolicyType.SelectedValue = "0";

            grdUploadedDocs.DataSource = null;
            grdUploadedDocs.DataBind();

            grdPreviousDocuments.DataSource = null;
            grdPreviousDocuments.DataBind();


            divQuotationNo.Visible = false;
            divJobNo.Visible = false;
            divPolicyNo.Visible = false;

            divIsCertificateConvertion.Visible = false;
        }


        private void sendRejectionMail(string jobNo)
        {
            if (txtEnteredBranchCode.Text == "")
            {
                return;
            }

            ProposalUploadController proposalUploadController = new ProposalUploadController();



            CommonMail mail = new CommonMail();
            //  mail.From_address = "motor.quotation@hnbgeneral.com";

            mail.From_address = "mnb.workflow@hnbgeneral.com";


            mail.To_address = proposalUploadController.getEmailOfBranchStaff(txtEnteredBranchCode.Text);
            //mail.To_address = "ruchira.ariyarathne@hnbgeneral.com";

            string enteredUserEmail = "";
            enteredUserEmail = proposalUploadController.getEmailOfUser(txtProposalUploadUserCode.Text);


            if (enteredUserEmail != "")
            {
                mail.Cc_address = enteredUserEmail + ",tharindu.dilanka@hnbassurance.com";
            }
            else
            {
                mail.Cc_address = "tharindu.dilanka@hnbassurance.com";
            }





            //string pageURl = "";
            //pageURl = Request.Url.AbsoluteUri;
            //// pageURl = pageURl.Replace("Quotation.aspx", "MRApprove.aspx");

            //int index = pageURl.LastIndexOf("/");
            //if (index > 0)
            //{
            //    pageURl = pageURl.Substring(0, index + 1);
            //}


            //pageURl = pageURl + "ProposalUploadView.aspx" + "?ProposalUploadId=" + txtProposalUploadId.Text;




            mail.Subject = "Uploaded Proposal Details Rejected";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Details Rejected</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Quotation No./Job No. " + jobNo + " Rejected" +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "Reason for Reject - " + txtScrutinizeRemarks.Text +
                      "</td>" +
                       "</tr>" +
                       "</table>" +
                        " </body> " +
                        " </html>";


            try
            {
                mail.Body = BodyText;
                mail.sendMail();

            }
            catch (Exception ee)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

            }
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Complete the current job before taking new job');", true);
                return;

            }
            //if (txtProposalUploadId.Text != "")
            //{
            //    lblMessage.Text = "Complete the current job before taking new job";
            //    return;

            //}
            ClearComponents();

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


            string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();
            //    proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusAndUser(TAKEN_BY_SCRUTINIZING, UserCode);


            proposalUpload = proposalUploadController.GetUploadedProposalOfProposalUploadId(grdMissedUploadedProposal.Rows[0].Cells[3].Text);



            txtJobType.Text = proposalUpload.JobType;
            if (txtJobType.Text == "N")
            {
                txtJobTypeName.Text = "New";

                divQuotationNo.Visible = true;
                divJobNo.Visible = false;
                divPolicyNo.Visible = false;

                divIsCertificateConvertion.Visible = false;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;
                divIsCertificateConvertion.Visible = true;
            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;
                divIsCertificateConvertion.Visible = false;
            }
            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;
                divIsCertificateConvertion.Visible = false;
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
            txtProposalUploadUserCode.Text = proposalUpload.EnteredUser;


            txtPolicyNo.Text = proposalUpload.TCSPolicyNo;

            txtEndorsementType.Text = proposalUpload.EndorsementType;
            txtCancellationType.Text = proposalUpload.CancellationType;

            if (proposalUpload.IsCertificateConvertion == 1)
            {
                chkIsCertificateConvertion.Checked = true;
            }
            else
            {
                chkIsCertificateConvertion.Checked = false;
            }


            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();

            if (txtJobType.Text == "N")
            {

                loadUploadedDocumentsToGrid(proposalUpload.QuotationNo);
            }
            else if (txtJobType.Text == "E")
            {

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

                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                txtJobRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);

                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;


            }



            loadPendings();
            loadRejectReasons();

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
                        if (files.Count > 0)
                        {
                            uploadedFiles.AddRange(files);
                        }
                    }

                }

                grdPreviousDocuments.DataSource = uploadedFiles;
                grdPreviousDocuments.DataBind();

            }
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

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();








            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
            // proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatus(INITIAL);

            string jobType = "";
            jobType = ddlPrefJobType.SelectedValue.ToString();
            if (jobType == "" || jobType == "A")
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatus(INITIAL);
            }
            else if (jobType == "NC")
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusOfNotSpecifiedType(INITIAL, "C");
            }
            else if (jobType == "EC")
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusOfCertificateConvertion(INITIAL, "E");
            }
            else
            {
                proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusOfSpecificType(INITIAL, jobType);
            }


            txtJobType.Text = proposalUpload.JobType;
            if (txtJobType.Text == "N")
            {
                txtJobTypeName.Text = "New";

                divQuotationNo.Visible = true;
                divJobNo.Visible = false;
                divPolicyNo.Visible = false;

                divIsCertificateConvertion.Visible = false;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;

                divIsCertificateConvertion.Visible = true;
            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;

                divIsCertificateConvertion.Visible = false;
            }
            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;

                divIsCertificateConvertion.Visible = false;
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
            txtProposalUploadUserCode.Text = proposalUpload.EnteredUser;


            txtPolicyNo.Text = proposalUpload.TCSPolicyNo;

            txtEnteredBranchCode.Text = proposalUpload.EnteredUserBranchCode;

            txtEndorsementType.Text = proposalUpload.EndorsementType;
            txtCancellationType.Text = proposalUpload.CancellationType;

            if (proposalUpload.IsCertificateConvertion == 1)
            {
                chkIsCertificateConvertion.Checked = true;
            }
            else
            {
                chkIsCertificateConvertion.Checked = false;
            }


            if (txtJobType.Text == "N")
            {

                loadUploadedDocumentsToGrid(proposalUpload.QuotationNo);
            }
            else if (txtJobType.Text == "E")
            {

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

                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);

                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                txtJobRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);

                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;


            }




            loadPendings();
            loadRejectReasons();


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


            //To update the status of the ProposalUpload as TAKEN_BY_SCRUTINIZING

            string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();



            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(proposalUpload.ProposalUploadId, UserCode, TAKEN_BY_SCRUTINIZING, "Taken for Scrutinize");


            if (proposalUploadController.CheckIsPolicyBlacklisted(txtPolicyNo.Text, txtVehicleNo.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('This Policy No or Vehicle No is Blacklisted');", true);

            }
        }

        protected void btnApproveToProcess_Click(object sender, EventArgs e)
        {
            if (txtProposalUploadId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Take a job first');", true);
                return;

            }
            if (txtJobType.Text == "C")
            {

                if (ddlPolicyType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select Policy type');", true);
                    return;
                }
            }
            else
            {
                if (ddlIssueType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select Issue type');", true);
                    return;
                }
                if (ddlPolicyType.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select Policy type');", true);
                    return;
                }

            }
            if (txtJobType.Text == "N")
            {
                if (txtIsValidated.Text != "VALIDATED")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please validate the Vehicle/Chassis No. before approve');", true);
                    return;

                }
            }


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(Convert.ToInt32(txtProposalUploadId.Text), UserCode, APPROVED_BY_SCRUTINIZING, txtScrutinizeRemarks.Text);


            proposalUploadController.UpdateIssueTypeAndPolicyType(Convert.ToInt32(txtProposalUploadId.Text), ddlIssueType.SelectedValue, ddlPolicyType.SelectedValue);





            if (txtIsValidated.Text != "VALIDATED")
            {
                proposalUploadController.UpdateIsValidated(Convert.ToInt32(txtProposalUploadId.Text));
            }



            if (chkIsCertificateConvertion.Checked)
            {
                proposalUploadController.UpdateIsCertificateConvertion(Convert.ToInt32(txtProposalUploadId.Text), 1);
            }
            else
            {
                proposalUploadController.UpdateIsCertificateConvertion(Convert.ToInt32(txtProposalUploadId.Text), 0);
            }


            foreach (ListItem item in chklPendings.Items)
            {
                if (item.Selected)
                {
                    InsertPendings(Convert.ToInt32(txtProposalUploadId.Text), item.Value);
                }

            }



            ClearComponents();
            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());
            LoadMissedProposal();
            // btnTakeJob.Enabled = true;
        }


        public void InsertPendings(int ProposalUploadId, string PendingCode)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleCommand cmd = new OracleCommand("MNBQ_WF_INSERT_PENDS_OF_JOB", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = ProposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_PENDING_CODE", OracleType.Number));
            cmd.Parameters["V_PENDING_CODE"].Value = PendingCode;

            cmd.Parameters.Add(new OracleParameter("V_IS_SELECTED", OracleType.Number));
            cmd.Parameters["V_IS_SELECTED"].Value = 1;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public void InsertRejectReasons(int ProposalUploadId, string ReasonCode)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleCommand cmd = new OracleCommand("MNBQ_WF_INSERT_REJ_REAS_OF_JOB", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = ProposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_REASON_CODE", OracleType.Number));
            cmd.Parameters["V_REASON_CODE"].Value = ReasonCode;

            cmd.Parameters.Add(new OracleParameter("V_IS_SELECTED", OracleType.Number));
            cmd.Parameters["V_IS_SELECTED"].Value = 1;



            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (txtScrutinizeRemarks.Text == "")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter reason for Reject in Remarks');", true);
                return;
            }

            bool isReasonSelected = false;

            foreach (ListItem item in chklRejectReasons.Items)
            {
                if (item.Selected)
                {
                    isReasonSelected = true;
                }

            }

            if (isReasonSelected == false)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select reason for rejection');", true);
                return;
            }


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }

            proposalUploadController.UpdateProposalUploadStatus(Convert.ToInt32(txtProposalUploadId.Text), UserCode, REJECTED_BY_SCRUTINIZING, txtScrutinizeRemarks.Text);

            string jobNo = "";


            if (txtJobType.Text == "N")
            {
                jobNo = txtQuotationNo.Text;

            }
            else if (txtJobType.Text == "E")
            {
                jobNo = txtJobNo.Text;
            }
            else if (txtJobType.Text == "R")
            {
                jobNo = txtJobNo.Text;
            }
            else if (txtJobType.Text == "C")
            {
                jobNo = txtJobNo.Text;
            }


            deletePreviousRejectReasons(Convert.ToInt32(txtProposalUploadId.Text));
            foreach (ListItem item in chklRejectReasons.Items)
            {
                if (item.Selected)
                {
                    InsertRejectReasons(Convert.ToInt32(txtProposalUploadId.Text), item.Value);
                }

            }


            deleteUploadedDocuments(jobNo);//delete files when rejected by scrutinization



            sendRejectionMail(jobNo);



            ClearComponents();
            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());
            LoadMissedProposal();
            //btnTakeJob.Enabled = true;
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

        private void deleteUploadedDocuments(string quotationNo)
        {
            string DOCUMENT_UPLOAD_PATH = "";
            string REJECTED_DOCUMENT_UPLOAD_PATH = "";


            if (txtJobType.Text == "N")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();
                REJECTED_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["NEW_BUSINESS_REJECTED_PATH"].ToString();
            }
            else if (txtJobType.Text == "E")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();
                REJECTED_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_REJECTED_PATH"].ToString();
            }
            else if (txtJobType.Text == "R")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();
                REJECTED_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_REJECTED_PATH"].ToString();
            }
            else if (txtJobType.Text == "C")
            {
                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();
                REJECTED_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_REJECTED_PATH"].ToString();
            }



            string folderPath = @DOCUMENT_UPLOAD_PATH + quotationNo;


            DirectoryInfo moveDestination = new DirectoryInfo(@REJECTED_DOCUMENT_UPLOAD_PATH);
            // string folderPath = @DOCUMENT_UPLOAD_PATH;

            // string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); 


            DirectoryInfo folder = new DirectoryInfo(@folderPath);


            if (Directory.Exists(folder.FullName))
            {
                if (!Directory.Exists(moveDestination.FullName + quotationNo.ToUpper()))
                {
                    System.IO.Directory.CreateDirectory(moveDestination.FullName + quotationNo.ToUpper());
                }

                FileInfo[] Files = folder.GetFiles("*.pdf");

                foreach (FileInfo file in Files)
                {
                    try
                    {
                        if (File.Exists(moveDestination.FullName + quotationNo.ToUpper() + "\\" + file.Name))
                        {
                            File.Move(moveDestination.FullName + quotationNo.ToUpper() + "\\" + file.Name, moveDestination.FullName + quotationNo.ToUpper() + "\\" + file.Name + "_" + (DateTime.Now.ToString().Replace('/', '_').Replace(':', '_')));

                        }

                        File.Move(file.FullName, moveDestination.FullName + quotationNo.ToUpper() + "\\" + file.Name);



                    }
                    catch (Exception exx)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while moving rejected document.');", true);

                    }

                }

            }


            //if (Directory.Exists(folderPath))
            //{
            //    string[] filePaths = Directory.GetFiles(folderPath);
            //    List<ListItem> files = new List<ListItem>();
            //    foreach (string filePath in filePaths)
            //    {
            //        File.SetAttributes(filePath, FileAttributes.Normal);
            //        //File.Delete(filePath);



            //        if (!Directory.Exists(moveDestination.FullName + quotationNo.ToUpper()))
            //        {
            //            System.IO.Directory.CreateDirectory(moveDestination.FullName + quotationNo.ToUpper());
            //        }

            //        try
            //        {
            //            File.Move(filePath, moveDestination.FullName + quotationNo.ToUpper() + "\\" + quotationNo.ToUpper());

            //        }
            //        catch (Exception exx)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while moving rejected document.');", true);

            //        }



            //    }
            //}
        }
        protected void btnOpenQuotationDetails_Click(object sender, EventArgs e)
        {
            if (txtQuotationNo.Text != "")
            {
                string url = "QuotationDetailsView.aspx?quotationNo=" + txtQuotationNo.Text;
                string s = "window.open('" + url + "', 'popup_window', 'width=800,height=580,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }



        }

        protected void btnValidateVehicleChassisNo_Click(object sender, EventArgs e)
        {

            Session.Remove("VehicleChassisNo");
            if (txtVehicleChassisNo.Text != "")
            {

                Session.Remove("VehicleChassisNo");
                Session["VehicleChassisNo"] = txtVehicleChassisNo.Text;
                txtIsValidated.Text = "VALIDATED";
                //string url = "TCSPreviousPolicies.aspx?VehicleChassisNo=" + txtVehicleChassisNo.Text;
                //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=580,left=100,top=100,resizable=yes');";
                //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                //  (this.pnlViewPrevPols.FindControl("iframePrevPols") as HtmlControl).Attributes.Add("src", "TCSPreviousPolicies.aspx?VehicleChassisNo=" + txtVehicleChassisNo.Text);
                //   iframePrevPols.Attributes.Add("src", "TCSPreviousPolicies.aspx?VehicleChassisNo=" + txtVehicleChassisNo.Text);
                string url = "TCSPreviousPolicies.aspx?VehicleChassisNo";
                string s = "window.open('" + url + "', 'popup_window', 'width=600,height=400,left=300,top=200,resizable=yes,toolbar=no,titlebar=no');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }

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
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
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


        protected void grdUploadedDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocPath = e.Row.Cells[4].Text;

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "../Common/DocumentViewerAndEditor.aspx?docPath=" + DocPath);

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

                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "../Common/DocumentViewerAndEditor.aspx?docPath=" + DocPath);

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

        private void loadPendings()
        {
            chklPendings.Items.Clear();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT T.PENDING_CODE,T.PENDING_NAME FROM MNBQ_WF_PENDINGS T ORDER BY T.PENDING_NAME ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ListItem item = new ListItem();
                    item.Text = dr["PENDING_NAME"].ToString();
                    item.Value = dr["PENDING_CODE"].ToString();
                    // item.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                    item.Selected = false;
                    chklPendings.Items.Add(item);


                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void loadRejectReasons()
        {
            chklRejectReasons.Items.Clear();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT T.REASON_CODE,T.REASON_NAME FROM MNBQ_WF_REJECT_REASON T ORDER BY T.ORDER_SEQ ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ListItem item = new ListItem();
                    item.Text = dr["REASON_NAME"].ToString();
                    item.Value = dr["REASON_CODE"].ToString();
                    item.Selected = false;
                    chklRejectReasons.Items.Add(item);


                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void loadIssueTypes()
        {
            ddlIssueType.Items.Clear();
            ddlIssueType.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "select t.ISSUE_TYPE_CODE,t.ISSUE_TYPE_NAME from MNBQ_WF_ISSUE_TYPE t order by t.ISSUE_TYPE_ORDER ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlIssueType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void loadPolicyTypes()
        {
            ddlPolicyType.Items.Clear();
            ddlPolicyType.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "select t.POLICY_TYPE_CODE,t.POLICY_TYPE_NAME from MNBQ_WF_POLICY_TYPE t order by t.POLICY_TYPE_ORDER ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlPolicyType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void deletePreviousRejectReasons(int ProposalUploadId)
        {

            try
            {

                OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                OracleDataAdapter da = new OracleDataAdapter();
                string sql = "";
                sql = "DELETE  FROM MNBQ_WF_REJ_REASONS_OF_JOB WHERE PROPOSAL_UPLOAD_ID=" + ProposalUploadId + "";
                da.DeleteCommand = new OracleCommand(sql, con);
                con.Open();



                da.DeleteCommand.ExecuteNonQuery();


                con.Close();

            }
            catch (Exception ex)
            {

            }


        }

        protected void ddlPrefJobType_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadUploadedProposal(ddlPrefJobType.SelectedValue.ToString());

            HttpCookie userJobTypeCookie = new HttpCookie("userScrutiJobTypeCookie");
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

            try
            {
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



                string[] filePaths = Directory.GetFiles(folderPath);
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }

                return files;
            }
            catch (Exception ex)
            {
                return null;
            }


           

        }

    }
}