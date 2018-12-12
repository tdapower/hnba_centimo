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

public partial class PrioritizeView : System.Web.UI.Page
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


            btnGivePriority.Attributes.Add("onClick", "if(confirm('Are you sure to Give Priority to this Job?','Motor New Business Workflow')){}else{return false}");

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
            btnGivePriority.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {

            btnGivePriority.Enabled = false;

            ClearComponents();
        }
        else if (mode == "EDIT")
        {
            btnGivePriority.Enabled = false;

        }
        else if (mode == "LOADED")
        {

            btnGivePriority.Enabled = true;

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
        Response.Redirect("PrioritizeView.aspx");
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

    protected void btnGivePriority_Click(object sender, EventArgs e)
    {
        if (txtJobNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please select Job/Quotation No";
            Timer1.Enabled = true;
            return;
        }

        if (txtRemarks.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter remark for prioritization";
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


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            proposalUploadController.PrioritizeJob(txtProposalUploadId.Text, txtRemarks.Text, UserCode);


            ClearComponents();
            //  SearchData();
            lblMsg.Text = "Successfully Prioritized";
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
