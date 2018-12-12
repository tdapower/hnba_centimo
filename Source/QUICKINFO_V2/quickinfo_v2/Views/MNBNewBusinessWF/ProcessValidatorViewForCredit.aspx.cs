using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Controllers.TCSPolicy;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class ProcessValidatorViewForCredit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                validatePageAuthentication();
                LockComponents();
                LoadUploadedProposal();
                LoadMissedProposal();

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

        private void LoadUploadedProposal()
        {
            grdUploadedProposal.DataSource = null;
            grdUploadedProposal.DataBind();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();
            grdUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfSpecificType(COMPLETED_BY_PROCESSING, "C");

            if (grdUploadedProposal.DataSource != null)
            {
                grdUploadedProposal.DataBind();
            }


            //  pnlSearchGrid.Visible = true;

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

            string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();


            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            ProposalUploadController proposalUploadController = new ProposalUploadController();
            grdMissedUploadedProposal.DataSource = proposalUploadController.GetQuotationNosOfStatusOfUsersOfSpecificType(TAKEN_BY_VALIDATORS, "C", UserCode);

            if (grdMissedUploadedProposal.DataSource != null)
            {
                grdMissedUploadedProposal.DataBind();
                updPnlMissedProposals.Update();
            }




        }



        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadUploadedProposal();
            LoadMissedProposal();
        }
        private void LockComponents()
        {

            txtQuotationNo.Enabled = false;
            txtVehicleNo.Enabled = false;
            txtEngineNo.Enabled = false;
            txtChassisNo.Enabled = false;
            chkIsCoverNoteAvailable.Enabled = false;
            txtCoverNotePeriod.Enabled = false;
            txtAddressLine1.Enabled = false;
            txtAddressLine2.Enabled = false;
            txtAddressLine3.Enabled = false;
            txtYearOfMake.Enabled = false;
            txtFirstRegDate.Enabled = false;
            txtCubicCapacity.Enabled = false;
            txtProposalNo.Enabled = false;
            // txtTCSPolicyNo.Enabled = false;

            ClearComponents();
        }
        private void ClearComponents()
        {
            txtProposalUploadId.Text = "";

            txtJobTypeName.Text = "";
            txtJobType.Text = "";
            txtJobNo.Text = "";

            txtSystemName.Text = "";
            lblMessage.Text = "";
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
            txtTCSPolicyNo.Text = "";
            txtEnteredBranchCode.Text = "";
            txtRemarks.Text = "";
            txtProposalNo.Text = "";
            txtJobRemarks.Text = "";
            txtScrutinizationRemarks.Text = "";
            txtProcessingRemarks.Text = "";
            txtProposalUploadUserCode.Text = "";

            txtIssueType.Text = "";
            txtPolicyType.Text = "";

            txtEndorsementType.Text = "";
            txtCancellationType.Text = "";

            chklPendings.Items.Clear();

            grdUploadedDocs.DataSource = null;
            grdUploadedDocs.DataBind();



            divQuotationNo.Visible = false;
            divJobNo.Visible = false;
            divPolicyNo.Visible = false;
        }



        protected void btnOpenCRCPage_Click(object sender, EventArgs e)
        {
            if (txtTCSPolicyID.Text != "")
            {

                string url = "http://192.168.10.212:82/crcapptest/Centralize.aspx?polid=" + txtTCSPolicyID.Text;
                string s = "window.open('" + url + "', 'popup_window', 'width=600,height=500,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }



        }


        protected void btnOpenQuotationDetails_Click(object sender, EventArgs e)
        {
            if (txtQuotationNo.Text != "")
            {
                string url = "QuotationDetailsView.aspx?quotationNo=" + txtQuotationNo.Text;
                string s = "window.open('" + url + "', 'popup_window', 'width=600,height=500,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }



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


            ClearComponents();



            // btnTakeJob.Enabled = false;

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
            proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatus(COMPLETED_BY_PROCESSING);

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

                txtTCSPolicyNo.Enabled = true;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;

                txtTCSPolicyNo.Enabled = false;

            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;

                txtTCSPolicyNo.Enabled = false;
            }

            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSProposalNo;

                txtTCSPolicyNo.Enabled = false;
            }

            txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
            txtJobNo.Text = proposalUpload.JobNumber;

            txtSystemName.Text = proposalUpload.SystemName;

            txtTCSPolicyID.Text = proposalUpload.TCSPolicyId.ToString();
            txtProposalNo.Text = proposalUpload.TCSProposalNo;
            txtQuotationNo.Text = proposalUpload.QuotationNo;
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
            txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
            txtEnteredBranchCode.Text = proposalUpload.EnteredUserBranchCode;

            txtProposalUploadUserCode.Text = proposalUpload.EnteredUser;

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
                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
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
                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
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

                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);


                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;
            }




            txtScrutinizationRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, APPROVED_BY_SCRUTINIZING);

            txtProcessingRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, COMPLETED_BY_PROCESSING);
            txtIssueType.Text = proposalUpload.IssueType;
            txtPolicyType.Text = proposalUpload.PolicyType;

            loadPendingsOfJob(Convert.ToInt32(txtProposalUploadId.Text));


            //To update the status of the ProposalUpload as TAKEN_BY_SCRUTINIZING

            string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();



            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(proposalUpload.ProposalUploadId, UserCode, TAKEN_BY_VALIDATORS, "Taken for Process Validation");

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


            ClearComponents();



            // btnTakeJob.Enabled = false;

            ProposalUpload proposalUpload = new ProposalUpload();


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();




            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();
            proposalUpload = proposalUploadController.GetEarliestUploadedProposalOfGivenStatusAndUser(TAKEN_BY_VALIDATORS, UserCode);

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

                txtTCSPolicyNo.Enabled = true;
            }
            else if (txtJobType.Text == "E")
            {
                txtJobTypeName.Text = "Endorsement";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;

                txtTCSPolicyNo.Enabled = false;

            }
            else if (txtJobType.Text == "R")
            {
                txtJobTypeName.Text = "Renewal";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;

                txtTCSPolicyNo.Enabled = false;
            }

            else if (txtJobType.Text == "C")
            {
                txtJobTypeName.Text = "Cancellation";

                divQuotationNo.Visible = false;
                divJobNo.Visible = true;
                divPolicyNo.Visible = true;


                txtTCSPolicyNo.Text = proposalUpload.TCSProposalNo;

                txtTCSPolicyNo.Enabled = false;
            }

            txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
            txtJobNo.Text = proposalUpload.JobNumber;


            txtSystemName.Text = proposalUpload.SystemName;

            txtTCSPolicyID.Text = proposalUpload.TCSPolicyId.ToString();
            txtProposalNo.Text = proposalUpload.TCSProposalNo;
            txtQuotationNo.Text = proposalUpload.QuotationNo;
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
            txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
            txtEnteredBranchCode.Text = proposalUpload.EnteredUserBranchCode;

            txtProposalUploadUserCode.Text = proposalUpload.EnteredUser;

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
                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
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
                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
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
                txtTCSPolicyNo.Text = proposalUpload.TCSPolicyNo;
                loadUploadedDocumentsToGrid(proposalUpload.JobNumber);


                string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
                string jobRemark = "";
                jobRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, CANCELLATION_ADDED);


                string initialRemark = "";
                initialRemark = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, INITIAL);

                txtJobRemarks.Text = jobRemark + "   " + initialRemark;
            }


            txtScrutinizationRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, APPROVED_BY_SCRUTINIZING);

            txtProcessingRemarks.Text = proposalUploadController.GetRemarksOfStatus(txtProposalUploadId.Text, COMPLETED_BY_PROCESSING);
            txtIssueType.Text = proposalUpload.IssueType;
            txtPolicyType.Text = proposalUpload.PolicyType;


            loadPendingsOfJob(Convert.ToInt32(txtProposalUploadId.Text));



        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (txtProposalUploadId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Take a job first');", true);
                return;

            }



            TCSPolicyController tCSPolicyController = new TCSPolicyController();


            if (txtJobType.Text == "N")
            {

                if (txtTCSPolicyNo.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter TCS Policy Number');", true);
                    return;
                }
                if (!tCSPolicyController.checkIsPolicyNoAvailable(txtTCSPolicyNo.Text.Trim(), "TCS"))
                {
                    if (!tCSPolicyController.checkIsPolicyNoAvailable(txtTCSPolicyNo.Text.Trim(), "TAKAFUL"))
                    {

                        lblMessage.Text = "Invalid Policy Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Invalid Policy Number');", true);
                        return;
                    }
                }


            }







            ProposalUploadController proposalUploadController = new ProposalUploadController();

            string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            proposalUploadController.UpdateProposalUploadStatus(Convert.ToInt32(txtProposalUploadId.Text), UserCode, APPROVED_BY_VALIDATORS, txtRemarks.Text);



            if (txtSystemName.Text == "TCS")
            {
                proposalUploadController.UpdateTargetBranchCode("TCS", Convert.ToInt32(txtProposalUploadId.Text), txtTCSPolicyID.Text);

            }
            else if (txtSystemName.Text == "TAKAFUL")
            {
                proposalUploadController.UpdateTargetBranchCode("TAKAFUL", Convert.ToInt32(txtProposalUploadId.Text), txtTCSPolicyID.Text);

            }

            if (txtJobType.Text == "N")
            {
                proposalUploadController.UpdateTCSPolicyNo(Convert.ToInt32(txtProposalUploadId.Text), txtTCSPolicyNo.Text.Trim());

                sendApprovalNotificationMailForNewBusiness();
            }
            else if (txtJobType.Text == "E")
            {
                sendApprovalNotificationMailForEndorsement();
            }
            else if (txtJobType.Text == "R")
            {
                sendApprovalNotificationMailForRenewal();
            }
            else if (txtJobType.Text == "C")
            {
                sendApprovalNotificationMailForCancellation();
            }



            if (tCSPolicyController.checkIsHNBPolicy(txtTCSPolicyID.Text, "TCS"))
            {
                sendApprovalNotificationMailOfHNBToHDO();
            }
            else if (tCSPolicyController.checkIsHNBPolicy(txtTCSPolicyID.Text, "TAKAFUL"))
            {
                sendApprovalNotificationMailOfHNBToHDO();
            }


            //     passDocumentToDMS(); //Temporarily commented

            ClearComponents();
            LoadUploadedProposal();
            LoadMissedProposal();

            //btnTakeJob.Enabled = true;
        }



        private void sendApprovalNotificationMailOfHNBToHDO()
        {

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            CommonMail mail = new CommonMail();

            mail.From_address = "mnb.workflow@hnbgeneral.com";

            mail.To_address = proposalUploadController.getEmailOfBranchStaff("TDA");//HDO FOR HNB

            mail.Cc_address = "tharindu.dilanka@hnbassurance.com";

            mail.Subject = "Uploaded HNB Proposal Processed and Ready to Print";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Processed and Ready to Print</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Quotation No. " + txtQuotationNo.Text + " Processed and Ready to Print." +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "TCS Policy No. - " + txtTCSPolicyNo.Text +
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


        private void sendApprovalNotificationMailForNewBusiness()
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
            // mail.To_address = "tharindu.dilanka@hnbassurance.com";


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




            mail.Subject = "Uploaded Proposal Processed and Ready to Print";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Processed and Ready to Print</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Quotation No. " + txtQuotationNo.Text + " Processed and Ready to Print." +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "TCS Policy No. - " + txtTCSPolicyNo.Text +
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

        private void sendApprovalNotificationMailForEndorsement()
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
            // mail.To_address = "ruchira.ariyarathne@hnbgeneral.com";


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





            mail.Subject = "Uploaded Proposal Processed and Ready to Print";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Processed and Ready to Print</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Job No. " + txtJobNo.Text + " Processed and Ready to Print." +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "TCS Policy No. - " + txtPolicyNo.Text +
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
        private void sendApprovalNotificationMailForRenewal()
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





            mail.Subject = "Uploaded Proposal Renewed and Ready to Print";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Processed and Ready to Print</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Job No. " + txtJobNo.Text + " Renewed and Ready to Print." +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "TCS Policy No. - " + txtPolicyNo.Text +
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
        private void sendApprovalNotificationMailForCancellation()
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




            mail.Subject = "Uploaded Proposal Cancelled and Ready to Print";
            String BodyText;

            BodyText = "<html>" +
                        "<head>" +
                        "<title>Uploaded Proposal Cancelled and Ready to Print</title>" +
                       " <body> " +
                         "<table>" +
                       "<tr>" +
                       "<td>" +
                       "Uploaded Proposal of Job No. " + txtJobNo.Text + " Cancelled and Ready to Print." +
                      "</td>" +
                       "</tr>" +
                    "<tr>" +
                      "<td>" +
                       "TCS Policy No. - " + txtPolicyNo.Text +
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
            //string folderPath = @DOCUMENT_UPLOAD_PATH;


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
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }
            }
        }
        protected void grdMissedUploadedProposal_RowDataBound(object sender, GridViewRowEventArgs e)
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


        private void passDocumentToDMS()
        {
            foreach (GridViewRow row in grdUploadedDocs.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    string DocPath = row.Cells[4].Text;

                    if (DocPath != "" && txtProposalNo.Text != "" && txtPolicyNo.Text != "" && txtJobNo.Text != "")
                    {
                        passDocumentToDMSByService(DocPath, txtProposalNo.Text, txtTCSPolicyNo.Text, txtJobNo.Text);
                    }
                }
            }

        }
        private void passDocumentToDMSByService(string DocPath, string ProposalNo, string PolicyNo, string QuotNo)
        {
            try
            {


                DMSServiceReference.IntegrationServiceSoapClient dmsServiceReference = new DMSServiceReference.IntegrationServiceSoapClient();

                string requestToken = "";
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.SendTimeout = TimeSpan.FromHours(5);
                //  DocPath = @"\\192.168.10.103\hnbgi\15HDO99005.pdf";
                string DocumentPath = "";
                string FileName = "";

                DocumentPath = @DocPath;

                FileInfo file = new FileInfo(DocumentPath);

                FileName = file.Name;


                byte[] documentStream = File.ReadAllBytes(DocumentPath);

                requestToken = dmsServiceReference.GetRequestToken(@"XsQ+Cf7Si3pqQ2KQZKb0s3SubwY=", @"/re3E6RMsZcIt8FMKSWJmOY44+KR0uH7E5reA7aWl+I=", "admin");



                string[] items = requestToken.Split(':');
                requestToken = Regex.Replace(items[1], "[{|}|\"]", "");
                StringBuilder xmltext = new StringBuilder();
                xmltext.Append("<enadoc>");
                xmltext.Append("<document>");
                xmltext.Append("<document name=\"" + FileName + "\" />");
                xmltext.Append("</document>");
                xmltext.Append("<index>");
                xmltext.Append("<index caption=\"LOB Code\" value=\"Motor\"/>");
                xmltext.Append("<index caption=\"Document ID\" value=\"" + FileName.Substring(0, FileName.LastIndexOf(".")).ToUpper() + "\"/>");
                xmltext.Append("<index caption=\"Proposal Number\" value=\"" + ProposalNo + "\"/>");
                xmltext.Append("<index caption=\"Policy Number\" value=\"" + PolicyNo + "\"/>");
                xmltext.Append("<index caption=\"NIC\" value=\"00000000V\"/>");
                xmltext.Append("<index caption=\"Motor QT No\" value=\"" + QuotNo + "\"/>");
                xmltext.Append("</index>");
                xmltext.Append("</enadoc>");


                // xmltext.Append("<index caption=\"Vehicle No\" value=\"" + VehicleNo + "\"/>");


                var response = dmsServiceReference.UploadDocument(requestToken, 1, 1, xmltext.ToString(), documentStream);

                ProposalUploadController proposalUploadController = new ProposalUploadController();
                string docURL = "";

                int pFrom = response.IndexOf("{\"status\":\"Uploaded successfully\",\"DocumentURL\":\"") + "{\"status\":\"Uploaded successfully\",\"DocumentURL\":\"".Length;
                int pTo = response.LastIndexOf("\"}");

                docURL = response.Substring(pFrom, pTo - pFrom);



                proposalUploadController.InsertDocumentViewURL(Convert.ToInt32(txtProposalUploadId.Text), docURL);
                // Label1.Text = response;
            }
            catch (Exception ex)
            {
                // Label1.Text = ex.ToString();
            }
        }
    }
}