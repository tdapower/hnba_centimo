//******************************************
// Author            :Tharindu Athapattu
// Date              :11/08/2015
// Reviewed By       :
// Description       : PrioritizeView Form
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
using System.Runtime.InteropServices;

public partial class JobFileManager : System.Web.UI.Page
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
            loadJobTypes();

            btnReProcess.Enabled = false;

            //btnGivePriority.Attributes.Add("onClick", "if(confirm('Are you sure to Give Priority to this Job?','Motor New Business Workflow')){}else{return false}");

        }



    }




    private void loadJobTypes()
    {
        ddlJobType.Items.Clear();


        ddlJobType.Items.Add(new ListItem("--- Select ---", "0"));
        ddlJobType.Items.Add(new ListItem("New", "N"));
        ddlJobType.Items.Add(new ListItem("Renewal", "R"));
        ddlJobType.Items.Add(new ListItem("Endorsement", "E"));
        ddlJobType.Items.Add(new ListItem("Cancellation", "C"));
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
            //btnGivePriority.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {

            //  btnGivePriority.Enabled = false;

            ClearComponents();
        }
        else if (mode == "EDIT")
        {
            //  btnGivePriority.Enabled = false;

        }
        else if (mode == "LOADED")
        {

            //   btnGivePriority.Enabled = true;

        }
    }

    protected void btnReProcess_Click(object sender, EventArgs e)
    {
        //        <add key="NEW_UPLOAD_PATH_TEMP" value="\\\\192.168.10.103\\HNBGI\\TEMP\\NEW\\" />
        //<add key="ENDORSEMENT_UPLOAD_PATH_TEMP" value="\\\\192.168.10.103\\HNBGI\\TEMP\\ENDORSEMENT\\" />
        //<add key="RENEWAL_UPLOAD_PATH_TEMP" value="\\\\192.168.10.103\\HNBGI\\TEMP\\RENEWAL\\" />
        //<add key="CANCELLATION_UPLOAD_PATH_TEMP" value="\\\\192.168.10.103\\HNBGI\\TEMP\\CANCELLATION\\" />

        if (ddlJobType.SelectedValue.ToString() == "0")
        {
            lblMsg.Text = "Please select Job type";
            return;

        }
        if (txtSearchQuotationNo.Text == "")
        {
            lblMsg.Text = "Please enter Job Number";
            return;

        }


        if (txtFilePath.Text == "")
        {
            lblMsg.Text = "Please select a Job Number";
            return;

        }




        string jobType = "";
        jobType = ddlJobType.SelectedValue.ToString();

        string DOCUMENT_UPLOAD_PATH_3 = "";
        string TEMP_DOCUMENT_UPLOAD_PATH = "";

        if (jobType == "N")
        {
            TEMP_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["NEW_UPLOAD_PATH_TEMP"].ToString();
        }
        else if (jobType == "E")
        {
            TEMP_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UPLOAD_PATH_TEMP"].ToString();
        }
        else if (jobType == "R")
        {
            TEMP_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UPLOAD_PATH_TEMP"].ToString();
        }
        else if (jobType == "C")
        {
            TEMP_DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UPLOAD_PATH_TEMP"].ToString();
        }

        //<add key="NEW_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\NEW\\" />
        //<add key="ENDORSEMENT_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\ENDORSEMENT\\" />
        //<add key="RENEWAL_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\RENEWAL\\" />
        //<add key="CANCELLATION_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\CANCELLATION\\" />
        if (jobType == "N")
        {
            DOCUMENT_UPLOAD_PATH_3 = System.Configuration.ConfigurationManager.AppSettings["NEW_UPLOAD_PATH_3"].ToString();
        }
        else if (jobType == "E")
        {
            DOCUMENT_UPLOAD_PATH_3 = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UPLOAD_PATH_3"].ToString();
        }
        else if (jobType == "R")
        {
            DOCUMENT_UPLOAD_PATH_3 = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UPLOAD_PATH_3"].ToString();
        }
        else if (jobType == "C")
        {
            DOCUMENT_UPLOAD_PATH_3 = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UPLOAD_PATH_3"].ToString();
        }

        if (isAllowedToReprocess(txtJobNo.Text))
        {

            //Move the file temporarily
            File.Move(txtFilePath.Text, (TEMP_DOCUMENT_UPLOAD_PATH + "\\" + txtJobNo.Text.ToUpper()) + ".pdf");

            //Move the file to process again
            File.Move((TEMP_DOCUMENT_UPLOAD_PATH + "\\" + txtJobNo.Text.ToUpper()) + ".pdf", (DOCUMENT_UPLOAD_PATH_3 + "\\" + txtJobNo.Text.ToUpper()) + ".pdf");




            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();
            }


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            proposalUploadController.ReProcessJob(txtJobNo.Text, jobType, UserCode);

            lblMsg.Text = "Job successfully re-processed";

            txtSearchQuotationNo.Text = "";
            txtFilePath.Text = "";
            txtJobNo.Text = "";
        }
        else
        {
            lblMsg.Text = "Job not allowed to re-process";
        }


        btnReProcess.Enabled = false;

    }

    private bool isAllowedToReprocess(string jobNo)
    {
        bool isAllowedToReprocess = false;


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





        string currentStatus = "";

        currentStatus = proposalUploadController.getStatusOfJobFromJobNo(jobNo);

        if (currentStatus == null || currentStatus == "")
        {
            if (ddlJobType.SelectedValue.ToString() == "N")
            {
                if (proposalUploadController.validateQuotationNoFromDB(txtSearchQuotationNo.Text))
                {

                    return true;
                }
            }
        }


        if (currentStatus == RENEWAL_ADDED || currentStatus == CANCELLATION_ADDED || currentStatus == RENEWAL_ADDED || currentStatus == ENDORSEMENT_ADDED || currentStatus == REJECTED_BY_SCRUTINIZING)
        {
            isAllowedToReprocess = true;
        }




        return isAllowedToReprocess;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtFilePath.Text = "";
        txtJobNo.Text = "";
        lblMsg.Text = "";

        if (ddlJobType.SelectedValue.ToString() == "0")
        {
            lblMsg.Text = "Please select Job type";
            return;

        }
        if (txtSearchQuotationNo.Text == "")
        {
            lblMsg.Text = "Please enter Job Number";
            return;

        }


        SearchInQueuedLocationAndRename(ddlJobType.SelectedValue.ToString(), txtSearchQuotationNo.Text);


        SearchData(ddlJobType.SelectedValue.ToString(), txtSearchQuotationNo.Text);



        // ClearComponents();


        if (txtFilePath.Text == "")
        {

            lblMsg.Text = "File not found";
            return;

        }




        ProposalUploadController proposalUploadController = new ProposalUploadController();
        string currentStatus = "";

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




        currentStatus = proposalUploadController.getStatusOfJobFromJobNo(txtSearchQuotationNo.Text);

        if (currentStatus == null || currentStatus == "")
        {
            if (ddlJobType.SelectedValue.ToString() == "N")
            {
                if (proposalUploadController.validateQuotationNoFromDB(txtSearchQuotationNo.Text))
                {
                    btnReProcess.Enabled = true;

                    lblMsg.Text = "Job file found and ready to re-process";
                    return;
                }
            }
        }

        if (currentStatus == RENEWAL_ADDED || currentStatus == CANCELLATION_ADDED || currentStatus == RENEWAL_ADDED || currentStatus == ENDORSEMENT_ADDED || currentStatus == REJECTED_BY_SCRUTINIZING)
        {
            btnReProcess.Enabled = true;

            lblMsg.Text = "Job file found and ready to re-process";
        }
        else
        {


            if (currentStatus == null || currentStatus == "")
            {

                lblMsg.Text = "Invalid Job";
                return;
            }
            lblMsg.Text = "Current status of the job is " + currentStatus + " and not allowed to re-process";
            return;
        }



    }


    private void SearchData(string jobType, string jobNo)
    {
        //  <add key="NEW_UPLOAD_PATH_1" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS\\NEW\\" />
        //<add key="ENDORSEMENT_UPLOAD_PATH_1" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS\\ENDORSEMENT\\" />
        //<add key="RENEWAL_UPLOAD_PATH_1" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS\\RENEWAL\\" />
        //<add key="CANCELLATION_UPLOAD_PATH_1" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS\\CANCELLATION\\" />


        //<add key="NEW_UPLOAD_PATH_2" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_2\\NEW\\" />
        //<add key="ENDORSEMENT_UPLOAD_PATH_2" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_2\\ENDORSEMENT\\" />
        //<add key="RENEWAL_UPLOAD_PATH_2" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_2\\RENEWAL\\" />
        //<add key="CANCELLATION_UPLOAD_PATH_2" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_2\\CANCELLATION\\" />

        //<add key="NEW_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\NEW\\" />
        //<add key="ENDORSEMENT_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\ENDORSEMENT\\" />
        //<add key="RENEWAL_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\RENEWAL\\" />
        //<add key="CANCELLATION_UPLOAD_PATH_3" value="\\\\192.168.10.103\\HNBGI\\SCN_DOCS_3\\CANCELLATION\\" />

        // string DOCUMENT_UPLOAD_PATH = "";

        string[] DOCUMENT_UPLOAD_PATH = new string[3];



        if (jobType == "N")
        {
            DOCUMENT_UPLOAD_PATH[0] = System.Configuration.ConfigurationManager.AppSettings["NEW_UPLOAD_PATH_1"].ToString();
            DOCUMENT_UPLOAD_PATH[1] = System.Configuration.ConfigurationManager.AppSettings["NEW_UPLOAD_PATH_2"].ToString();
            DOCUMENT_UPLOAD_PATH[2] = System.Configuration.ConfigurationManager.AppSettings["NEW_UPLOAD_PATH_3"].ToString();




        }
        else if (jobType == "E")
        {
            DOCUMENT_UPLOAD_PATH[0] = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UPLOAD_PATH_1"].ToString();
            DOCUMENT_UPLOAD_PATH[1] = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UPLOAD_PATH_2"].ToString();
            DOCUMENT_UPLOAD_PATH[2] = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_UPLOAD_PATH_3"].ToString();

        }
        else if (jobType == "R")
        {
            DOCUMENT_UPLOAD_PATH[0] = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UPLOAD_PATH_1"].ToString();
            DOCUMENT_UPLOAD_PATH[1] = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UPLOAD_PATH_2"].ToString();
            DOCUMENT_UPLOAD_PATH[2] = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_UPLOAD_PATH_3"].ToString();

        }
        else if (jobType == "C")
        {
            DOCUMENT_UPLOAD_PATH[0] = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UPLOAD_PATH_1"].ToString();
            DOCUMENT_UPLOAD_PATH[1] = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UPLOAD_PATH_2"].ToString();
            DOCUMENT_UPLOAD_PATH[2] = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_UPLOAD_PATH_3"].ToString();

        }

        string fileAvailableLocation = "";
        for (int a = 0; a < DOCUMENT_UPLOAD_PATH.Length; a++)
        {
            fileAvailableLocation = checkIsFileExisted(DOCUMENT_UPLOAD_PATH[a], jobNo);

            if (fileAvailableLocation != "")
            {
                if (checkIsFileLocked(fileAvailableLocation) == false)
                {
                    txtFilePath.Text = fileAvailableLocation;
                    txtJobNo.Text = jobNo;
                    return;
                }
            }

        }


    }


    private void SearchInQueuedLocationAndRename(string jobType, string jobNo)
    {

        string DOCUMENT_UPLOAD_PATH = "";
        ProposalUploadController proposalUploadController = new ProposalUploadController();
        string currentStatus = "";
        currentStatus = proposalUploadController.getStatusOfJobFromJobNo(txtSearchQuotationNo.Text);

        string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
        string TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD"].ToString();
        string RENEWAL_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOCS_ADDED"].ToString();
        string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
        string TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD"].ToString();
        string CANCELLATION_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_DOCS_ADDED"].ToString();
        string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();





        if (jobType == "N")
        {
            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();
        }
        else if (jobType == "E")
        {
            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();

        }
        else if (jobType == "R")
        {
            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();

        }
        else if (jobType == "C")
        {
            DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();

        }

        string fileAvailableLocation = "";


        if (!Directory.Exists(DOCUMENT_UPLOAD_PATH + jobNo))
        {
            return;
        }

        fileAvailableLocation = checkIsFileExisted(DOCUMENT_UPLOAD_PATH + jobNo, jobNo);

        if (fileAvailableLocation != "")
        {
            if (checkIsFileLocked(fileAvailableLocation) == false)
            {

                if (currentStatus == null || currentStatus == "")
                {
                    if (ddlJobType.SelectedValue.ToString() == "N")
                    {
                        if (proposalUploadController.validateQuotationNoFromDB(txtSearchQuotationNo.Text))
                        {

                            System.IO.File.Move(fileAvailableLocation, fileAvailableLocation + "_");
                        }
                    }
                }

                if (currentStatus == RENEWAL_ADDED || currentStatus == CANCELLATION_ADDED || currentStatus == RENEWAL_ADDED || currentStatus == ENDORSEMENT_ADDED)
                {
                    System.IO.File.Move(fileAvailableLocation, fileAvailableLocation + "_");
                }

            }
        }



    }

    private string checkIsFileExisted(string folderPath, string jobNo)
    {

      


        string fileLocation = "";

        bool isFound = false;

        
        string[] filePaths = Directory.GetFiles(folderPath);

        foreach (string filePath in filePaths)
        {


            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (jobNo == fileName)
            {
                isFound = true;
                fileLocation = filePath;
            }


        }


        return fileLocation;
    }


    private bool checkIsFileLocked(string filePath)
    {
        try
        {
            using (File.Open(filePath, FileMode.Open)) { }
        }
        catch (IOException e)
        {
            var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

            return errorCode == 32 || errorCode == 33;
        }

        return false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("JobFileManager.aspx");
    }








    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearComponents();
    }






    private void ClearComponents()
    {

        txtFilePath.Text = "";
        txtJobNo.Text = "";



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
