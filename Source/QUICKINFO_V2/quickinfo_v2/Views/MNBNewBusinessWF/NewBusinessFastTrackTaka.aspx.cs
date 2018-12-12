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

public partial class NewBusinessFastTrackTaka : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            Session["JobType"] = "Fasttrack";
            Session["QuotationNo"] = "";
            Session["SessionedJobNo"] = "";
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            ClearComponents();

            ManageFormComponents("INITIAL");

            initializeValues();

            Session.Remove("Mode");



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
            txtQuotationNo.Enabled = false;
            txtTCSQuoteNo.Enabled = false;

            btnSearchQuotationNos.Enabled = false;
            btnSearchTCSQuoteNo.Enabled = false;

            btnAddNew.Enabled = true;
            // btnAlter.Enabled = false;
            btnSave.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {
            txtQuotationNo.Enabled = true;
            txtTCSQuoteNo.Enabled = true;


            btnSearchQuotationNos.Enabled = true;
            btnSearchTCSQuoteNo.Enabled = true;

            btnAddNew.Enabled = false;
            // btnAlter.Enabled = false;
            btnSave.Enabled = true;
            ClearComponents();
        }

    }









    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewBusinessFastTrackTaka.aspx");
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
        if (txtQuotationNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter the Quotation No";
            Timer1.Enabled = true;
            return;
        }
        if (txtTCSQuoteNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter the TCS Quote No";
            Timer1.Enabled = true;
            return;
        }




        try
        {
            ProposalUpload proposalUpload = new ProposalUpload();


            proposalUpload.ProposalUploadId = Convert.ToInt32(txtProposalUploadId.Text == "" ? "0" : txtProposalUploadId.Text);
            proposalUpload.QuotationNo = txtQuotationNo.Text;



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
            proposalUpload.JobType = "F";
            proposalUpload.JobNumber = txtJobNo.Text;

            proposalUpload.TCSPolicyNo = "";
            proposalUpload.TCSProposalNo = txtTCSQuoteNo.Text;
            proposalUpload.TCSPolicyId = "";



            proposalUpload.IsDocsAvailable = 1;

            proposalUpload.IsOwnBranchPolicy = 1;


            proposalUpload.IsUrgent = 1;



            proposalUpload.BranchOfPolicy = UserBranch;
            proposalUpload.Remarks = txtRemarks.Text;



            ProposalUploadController proposalUploadController = new ProposalUploadController();

            //if (Session["Mode"].ToString() == "NEW")
            //{
            //    proposalUploadController.InsertFastTrack(proposalUpload);
            //}
            proposalUploadController.InsertFastTrack(proposalUpload);


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



    private void ClearComponents()
    {
        txtJobNo.Text = "";

        txtProposalUploadId.Text = "";
        txtQuotationNo.Text = "";
        txtTCSQuoteNo.Text = "";

        txtSystemName.Text = "";
        txtPolicyID.Text = "";
        txtProposalNo.Text = "";

        txtEnteredBranchCode.Text = "";
        txtRemarks.Text = "";


    }





    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Session["JobType"] = "Fasttrack";
        Session["QuotationNo"] = "";
        Session["SessionedJobNo"] = "";
        ManageFormComponents("NEW");

        //Session["Mode"] = "NEW";

        string UserBranch = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserBranch = reqCookies["UserBranch"].ToString();
            txtEnteredBranchCode.Text = UserBranch;
        }
        ProposalUploadController proposalUploadController = new ProposalUploadController();

        txtJobNo.Text = proposalUploadController.GetNewJobNoForFastTrack(UserBranch);
        txtJobNo.ForeColor = Color.White;
        Session["SessionedJobNo"] = txtJobNo.Text;
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
