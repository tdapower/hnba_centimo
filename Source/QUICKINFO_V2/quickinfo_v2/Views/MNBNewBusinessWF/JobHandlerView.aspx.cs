//******************************************
// Author            :Tharindu Athapattu
// Date              :02/10/2015
// Reviewed By       :
// Description       : JobHandlerView Form
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

public partial class JobHandlerView : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            Session["QuotationNo"] = "";
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            ClearComponents();

            ManageFormComponents("INITIAL");

            initializeValues();

            Session.Remove("Mode");


            btnRevertToScrutinizing.Attributes.Add("onClick", "if(confirm('Are you sure to Revert the job to Scrutinizing?','Motor New Business Workflow')){}else{return false}");

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
            btnRevertToScrutinizing.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {

            btnRevertToScrutinizing.Enabled = false;

            ClearComponents();
        }
        else if (mode == "EDIT")
        {
            btnRevertToScrutinizing.Enabled = false;

        }
        else if (mode == "LOADED")
        {

            btnRevertToScrutinizing.Enabled = true;

        }
    }



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

        if ((txtSearchQuotationNo.Text == ""))
        {

            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Search text cannot be blank');", true);
            return;
        }


        if (txtSearchQuotationNo.Text != "")
        {

            SQL = "(LOWER(QUOTATION_NO) LIKE '%" + txtSearchQuotationNo.Text.ToLower() + "%') AND";
        }



        SQL = SQL.Substring(0, SQL.Length - 3);


        ProposalUploadController proposalUploadController = new ProposalUploadController();

        grdSearchResults.DataSource = proposalUploadController.GetJobsForManage(txtSearchQuotationNo.Text);

        if (grdSearchResults.DataSource != null)
        {
            grdSearchResults.DataBind();
        }



        pnlSearchGrid.Visible = true;

    }



    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobHandlerView.aspx");
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
        ClearComponents();
    }

    //protected void btnGivePriority_Click(object sender, EventArgs e)
    //{
    //    if (txtJobNo.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Please select Job/Quotation No";
    //        Timer1.Enabled = true;
    //        return;
    //    }

    //    if (txtRemarks.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Please enter remark for prioritization";
    //        Timer1.Enabled = true;
    //        return;
    //    }


    //    try
    //    {

    //        string UserCode = "";
    //        string UserBranch = "";
    //        HttpCookie reqCookies = Request.Cookies["userInfo"];
    //        if (reqCookies != null)
    //        {
    //            UserCode = reqCookies["UserCode"].ToString();
    //            UserBranch = reqCookies["UserBranch"].ToString();
    //        }


    //        ProposalUploadController proposalUploadController = new ProposalUploadController();

    //        proposalUploadController.PrioritizeJob(txtProposalUploadId.Text, txtRemarks.Text, UserCode);


    //        ClearComponents();
    //        //  SearchData();
    //        lblMsg.Text = "Successfully Prioritized";
    //        Timer1.Enabled = true;

    //        ManageFormComponents("INITIAL");


    //        //Response.Redirect("UserRegistration.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Error While Saving";
    //        Timer1.Enabled = true;
    //    }

    //}

    protected void btnRevertToScrutinizing_Click(object sender, EventArgs e)
    {
        if (txtJobNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please select Job/Quotation No";
            Timer1.Enabled = true;
            return;
        }

        if (txtRemarks.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter remark for revert";
            Timer1.Enabled = true;
            return;
        }

        if (!validateAction("REVERT", txtProposalUploadId.Text))
        {
            lblMsg.Text = "This job not allowed to Revert";
            Timer1.Enabled = true;
            return;
        }
        try
        {

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }

            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
            ProposalUploadController proposalUploadController = new ProposalUploadController();

            proposalUploadController.RevertJobToScrutinizing(txtProposalUploadId.Text,INITIAL, txtRemarks.Text, UserCode);


            ClearComponents();
            //  SearchData();
            lblMsg.Text = "Successfully Reverted";
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


    private bool validateAction(string actionName, string proposalUploadId)
    {
        bool isValidationPassed = true;


        ProposalUploadController proposalUploadController = new ProposalUploadController();
        string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
        string TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD"].ToString();
        string RENEWAL_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOCS_ADDED"].ToString();
        string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
        string TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD"].ToString();
        string CANCELLATION_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_DOCS_ADDED"].ToString();
        string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();



        string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
        string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();

        string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
        string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();

        string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();
        string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();

        string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();


        string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();




        if (actionName == "REVERT")
        {
            string currentStatus = "";

            currentStatus = proposalUploadController.getStatusOfJob(proposalUploadId);
            if (currentStatus == APPROVED_BY_VALIDATORS || currentStatus == INITIAL || currentStatus == RENEWAL_ADDED || currentStatus == TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD || currentStatus == RENEWAL_DOCS_ADDED || currentStatus == CANCELLATION_ADDED
             || currentStatus == TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD || currentStatus == CANCELLATION_DOCS_ADDED || currentStatus == ENDORSEMENT_ADDED || currentStatus == TAKEN_BY_SCRUTINIZING || currentStatus == REJECTED_BY_SCRUTINIZING)
            {
                isValidationPassed = false;
            }

        }


        return isValidationPassed;
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

        txtProposalUploadId.Text = "";
        txtJobNo.Text = "";

        txtPolicyID.Text = "";
        txtProposalNo.Text = "";

        txtEnteredBranchCode.Text = "";
        txtRemarks.Text = "";


    }





    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtProposalUploadId.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();
        txtJobNo.Text = grdSearchResults.SelectedRow.Cells[2].Text.Trim();


        if (txtProposalUploadId.Text != "")
        {
            ManageFormComponents("LOADED");
        }




        //ProposalUpload proposalUpload = new ProposalUpload();


        //ProposalUploadController proposalUploadController = new ProposalUploadController();
        //proposalUpload = proposalUploadController.GetUploadedProposal(Convert.ToInt32(txtProposalUploadId.Text));



        //  txtPolicyNo.Text = proposalUpload.QuotationNo;




    }

    protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }


    private void initializeValues()
    {

        lblError.Text = "";
        lblMsg.Text = "";

    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }

}
