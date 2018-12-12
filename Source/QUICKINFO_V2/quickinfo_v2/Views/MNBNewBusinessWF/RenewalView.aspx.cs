//******************************************
// Author            :Tharindu Athapattu
// Date              :24/07/2015
// Reviewed By       :
// Description       : Renewal Form
//******************************************
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Net;
using System.DirectoryServices;
using System.Net.Mail;
using System.IO;
using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System.Collections.Generic;

public partial class RenewalView : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            Session["JobType"] = "Renewal";
            Session["QuotationNo"] = "";
            Session["SessionedJobNo"] = "";
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            ClearComponents();

            ManageFormComponents("INITIAL");

            initializeValues();

            Session.Remove("Mode");

            pnlSearchGrid.Visible = false;


            //if (Request.QueryString["ProposalUploadId"] != null)
            //{

            //    if (Request.QueryString["ProposalUploadId"].ToString() != "")
            //    {
            //        loadProposalUploadDetailsForEdit(Request.QueryString["ProposalUploadId"].ToString());

            //        Session["Mode"] = "EDIT";
            //    }
            //}
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


    private void ManageFormComponents(string mode)
    {
        if (mode == "INITIAL")
        {
            txtPolicyNo.Enabled = false;

            btnSearchPolicyNos.Enabled = false;
            lnkBtnAttachment.Enabled = false;
            btnAddNew.Enabled = true;
            // btnAlter.Enabled = false;
            btnSave.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {
            txtPolicyNo.Enabled = true;


            btnSearchPolicyNos.Enabled = true;

            lnkBtnAttachment.Enabled = true;
            btnAddNew.Enabled = false;
            // btnAlter.Enabled = false;
            btnSave.Enabled = true;
            ClearComponents();
        }
        else if (mode == "EDIT")
        {
            txtPolicyNo.Enabled = true;


            btnSearchPolicyNos.Enabled = true;

            lnkBtnAttachment.Enabled = true;
            btnAddNew.Enabled = false;
            // btnAlter.Enabled = false;
            btnSave.Enabled = true;
        }
        else if (mode == "LOADED")
        {
            txtPolicyNo.Enabled = false;


            btnSearchPolicyNos.Enabled = false;

            lnkBtnAttachment.Enabled = false;
            btnAddNew.Enabled = false;
            //  btnAlter.Enabled = true;
            btnSave.Enabled = false;
        }
    }


    //private void loadProposalUploadDetailsForEdit(string ProposalUploadId)
    //{
    //    ClearComponents();

    //    ProposalUpload proposalUpload = new ProposalUpload();


    //    ProposalUploadController proposalUploadController = new ProposalUploadController();

    //    proposalUpload = proposalUploadController.GetUploadedProposal(Convert.ToInt32(ProposalUploadId));


    //    txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
    //    txtPolicyNo.Text = proposalUpload.QuotationNo;




    //    ManageFormComponents("EDIT");
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData();
        ClearComponents();
    }


    private void SearchData()
    {
        string SQL = "";

        grdSearchResults.DataSource = null;
        grdSearchResults.DataBind();

        if ((txtSearchQuotationNo.Text == "") && (txtSearchVehicleNo.Text == ""))
        {

            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Search text cannot be blank');", true);
            return;
        }


        if (txtSearchQuotationNo.Text != "")
        {

            SQL = "(LOWER(QUOTATION_NO) LIKE '%" + txtSearchQuotationNo.Text.ToLower() + "%') AND";
        }

        if (txtSearchVehicleNo.Text != "")
        {

            SQL = "(LOWER(VEHICLE_NO) LIKE '%" + txtSearchVehicleNo.Text.ToLower() + "%') AND";
        }



        SQL = SQL.Substring(0, SQL.Length - 3);


        ProposalUploadController proposalUploadController = new ProposalUploadController();

        grdSearchResults.DataSource = proposalUploadController.GetUploadedProposals(SQL);

        if (grdSearchResults.DataSource != null)
        {
            grdSearchResults.DataBind();
        }



        pnlSearchGrid.Visible = true;

    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProposalUploadView.aspx");
    }


    public DirectoryEntry GetDirectoryObject()
    {
        DirectoryEntry oDE;
        oDE = new DirectoryEntry("LDAP://192.168.10.251");
        return oDE;
    }


    public DirectoryEntry GetLoginName(string EmployeeID)
    {
        DirectoryEntry de = GetDirectoryObject();
        DirectorySearcher deSearch = new DirectorySearcher();
        deSearch.SearchRoot = de;

        deSearch.Filter = "(&(objectClass=user)(EmployeeID=" + EmployeeID + "))";
        deSearch.SearchScope = SearchScope.Subtree;
        SearchResult results = deSearch.FindOne();


        if (!(results == null))
        {

            de = new DirectoryEntry(results.Path);
            Session["USER"] = de.Properties["SAMAccountName"][0].ToString();
            return de;
        }
        else
        {
            Session["USER"] = "";
            return null;
        }
    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["JobType"] = "";
        Session["QuotationNo"] = "";
        Session["SessionedJobNo"] = "";


        ClearComponents();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtPolicyNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter the TCS Policy No";
            Timer1.Enabled = true;
            return;
        }


        if (chkIsSameBranch.Checked == false)
        {
            if (ddlBranch.SelectedValue == "0")
            {
                lblMsg.Text = "Please select Branch of Policy";
                Timer1.Enabled = true;
                return;
            }

        }
        if (chkIsUrgent.Checked == true)
        {
            if (txtRemarks.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter Reason for urgency in remarks field";
                Timer1.Enabled = true;
                return;
            }

        }


        //if (Session["Mode"].ToString() == "NEW")
        //{
        //    if (CheckUserCodeAlreadyExist(txtUserCode.Text))
        //    {
        //        lblMsg.Text = "Enetered User Code Already Exists";
        //        Timer1.Enabled = true;
        //        return;
        //    }
        //}




        try
        {
            ProposalUpload proposalUpload = new ProposalUpload();


            proposalUpload.ProposalUploadId = Convert.ToInt32(txtProposalUploadId.Text == "" ? "0" : txtProposalUploadId.Text);
            proposalUpload.QuotationNo = txtPolicyNo.Text;



            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }

            proposalUpload.EnteredUser = UserCode;
            proposalUpload.EnteredUserBranchCode = UserBranch;


            proposalUpload.SystemName = txtSystemName.Text;
            proposalUpload.JobType = "R";
            proposalUpload.JobNumber = txtJobNo.Text;

            proposalUpload.TCSPolicyNo = txtPolicyNo.Text;
            proposalUpload.TCSProposalNo = txtProposalNo.Text;
            proposalUpload.TCSPolicyId = txtPolicyID.Text;


            if (chkIsDocumentsAvailable.Checked)
            {
                proposalUpload.IsDocsAvailable = 1;
            }
            else
            {
                proposalUpload.IsDocsAvailable = 0;

            }


            if (chkIsDocsPrintFromHDO.Checked)
            {
                proposalUpload.IsDocsPrintFromHDO = 1;
            }
            else
            {
                proposalUpload.IsDocsPrintFromHDO = 0;

            }

            if (chkIsSameBranch.Checked)
            {
                proposalUpload.IsOwnBranchPolicy = 1;
            }
            else
            {
                proposalUpload.IsOwnBranchPolicy = 0;

            }

            if (chkIsUrgent.Checked)
            {
                proposalUpload.IsUrgent = 1;
            }
            else
            {
                proposalUpload.IsUrgent = 0;
            }



            proposalUpload.BranchOfPolicy = ddlBranch.SelectedValue;
            proposalUpload.Remarks = txtRemarks.Text;



            ProposalUploadController proposalUploadController = new ProposalUploadController();

            if (Session["Mode"].ToString() == "NEW")
            {
                proposalUploadController.InsertRenewal(proposalUpload);
            }
            //else if (Session["Mode"].ToString() == "EDIT")
            //{
            //    proposalUploadController.UpdateProposalUpload(proposalUpload);
            //}


            sendMailToPolicyOwnBranch(ddlBranch.SelectedValue, txtPolicyNo.Text);


            txtJobNo.ForeColor = Color.Black;
            string jobNoText = "";
            jobNoText = "Generated Job Nuber is " + txtJobNo.Text;
            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('" + jobNoText + "');", true);

            ClearComponents();
            //  SearchData();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

            ManageFormComponents("INITIAL");


            //Response.Redirect("UserRegistration.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;
        }

    }


    private void sendMailToPolicyOwnBranch(string branchCode, string policyNo)
    {

        ProposalUploadController proposalUploadController = new ProposalUploadController();

        CommonMail mail = new CommonMail();
        //  mail.From_address = "motor.quotation@hnbgeneral.com";

        mail.From_address = "mnb.workflow@hnbgeneral.com";


        mail.To_address = proposalUploadController.getEmailOfBranchStaff(branchCode);
        //mail.To_address = "ruchira.ariyarathne@hnbgeneral.com";


        mail.Cc_address = "tharindu.dilanka@hnbassurance.com";




        //string pageURl = "";
        //pageURl = Request.Url.AbsoluteUri;
        //// pageURl = pageURl.Replace("Quotation.aspx", "MRApprove.aspx");

        //int index = pageURl.LastIndexOf("/");
        //if (index > 0)
        //{
        //    pageURl = pageURl.Substring(0, index + 1);
        //}


        //pageURl = pageURl + "ProposalUploadView.aspx" + "?ProposalUploadId=" + txtProposalUploadId.Text;




        mail.Subject = "Upload Documents for Policy Renewal";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Upload Documents for Policy Renewal</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "Please Upload Documents for Policy Renewal of Policy No. " + policyNo +
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
            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

        }
    }

    //private bool CheckUserCodeAlreadyExist(string UserCode)
    //{
    //    bool returnVal = false;
    //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
    //    OracleDataReader dr;

    //    con.Open();

    //    OracleCommand cmd = new OracleCommand();
    //    cmd.Connection = con;
    //    String selectQuery = "";
    //    selectQuery = "SELECT USER_CODE FROM WF_ADMIN_USERS WHERE USER_CODE='" + UserCode + "'";

    //    cmd.CommandText = selectQuery;

    //    dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        returnVal = true;
    //    }
    //    dr.Close();
    //    dr.Dispose();
    //    cmd.Dispose();
    //    con.Close();
    //    con.Dispose();

    //    return returnVal;
    //}

    private void ClearComponents()
    {
        txtJobNo.Text = "";

        txtProposalUploadId.Text = "";
        txtPolicyNo.Text = "";

        txtSystemName.Text = "";
        txtPolicyID.Text = "";
        txtProposalNo.Text = "";

        txtEnteredBranchCode.Text = "";
        txtRemarks.Text = "";
        chkIsDocumentsAvailable.Checked = false;
        chkIsDocsPrintFromHDO.Checked = false;
        chkIsUrgent.Checked = false;

        ddlBranch.SelectedValue = "0";

        grdPreviousDocuments.DataSource = null;
        grdPreviousDocuments.DataBind();
    }


    private void loadBranches()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("--- Select One ---", "0"));

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.branch_code,t.branch_name from mis_gi_branches t order by t.branch_name ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlBranch.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }


    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Session["JobType"] = "Renewal";
        Session["QuotationNo"] = "";
        Session["SessionedJobNo"] = "";
        ManageFormComponents("NEW");

        Session["Mode"] = "NEW";

        string UserBranch = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserBranch = reqCookies["UserBranch"].ToString();
            txtEnteredBranchCode.Text = UserBranch;
        }
        ProposalUploadController proposalUploadController = new ProposalUploadController();

        txtJobNo.Text = proposalUploadController.GetNewJobNoForRenewal(UserBranch);
        txtJobNo.ForeColor = Color.White;
        Session["SessionedJobNo"] = txtJobNo.Text;
    }



    //protected void btnAlter_Click(object sender, EventArgs e)
    //{
    //    if (txtUserCode.Text == "")
    //    {
    //        lblMsg.Text = "Please Select An User";
    //        Timer1.Enabled = true;
    //        return;
    //    }

    //    //txtUserCode.Enabled = true;
    //    txtUserName.Enabled = true;
    //    txtEPF.Enabled = true;
    //    ddlUserRole.Enabled = true;

    //    rdbtnActive.Enabled = true;
    //    rdbtnInActive.Enabled = true;

    //    btnSave.Enabled = true;

    //    Session["Mode"] = "UPDATE";
    //}

    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtProposalUploadId.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();
        Session["QuotationNo"] = "";
        Session["QuotationNo"] = grdSearchResults.SelectedRow.Cells[2].Text.Trim();


        ProposalUpload proposalUpload = new ProposalUpload();


        ProposalUploadController proposalUploadController = new ProposalUploadController();
        proposalUpload = proposalUploadController.GetUploadedProposal(Convert.ToInt32(txtProposalUploadId.Text));



        txtPolicyNo.Text = proposalUpload.QuotationNo;



        ManageFormComponents("LOADED");
    }

    protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }


    private void initializeValues()
    {

        lblError.Text = "";
        lblMsg.Text = "";

        loadBranches();

        pnlBranchList.Visible = false;
    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }

    protected void chkIsSameBranch_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsSameBranch.Checked == true)
        {
            pnlBranchList.Visible = false;
        }
        else
        {
            pnlBranchList.Visible = true;
        }
    }


    protected void btnLoadPreviousDocuments_Click(object sender, EventArgs e)
    {
        if (txtPolicyNo.Text != "")
        {

            var previousJobList = new List<ProposalUpload>();

            ProposalUploadController proposalUploadController2 = new ProposalUploadController();
            previousJobList = proposalUploadController2.loadOtherJobsByPolicyNo(txtPolicyNo.Text, txtJobNo.Text, "R");

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


}
