//******************************************
// Author            :Tharindu Athapattu
// Date              :25/05/2015
// Reviewed By       :
// Description       : Proposal Upload Form
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

public partial class ProposalUploadView : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            Session["QuotationNo"] = "";
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            //txtCoverNotePeriod.Attributes.Add("onkeyup", "jsValidateNum(this)");
            txtYearOfMake.Attributes.Add("onkeyup", "jsValidateNum(this)");
            ClearComponents();

            ManageFormComponents("INITIAL");

            initializeValues();

            Session.Remove("Mode");

            pnlSearchGrid.Visible = false;


            if (Request.QueryString["ProposalUploadId"] != null)
            {

                if (Request.QueryString["ProposalUploadId"].ToString() != "")
                {
                    loadProposalUploadDetailsForEdit(Request.QueryString["ProposalUploadId"].ToString());

                    Session["Mode"] = "EDIT";
                }
            }
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

            lnkBtnAttachment.Enabled = false;
            btnSearchQuotationNos.Enabled = false;
            btnAddNew.Enabled = true;
            // btnAlter.Enabled = false;
            btnSave.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {
            txtQuotationNo.Enabled = true;
            txtVehicleNo.Enabled = true;
            txtEngineNo.Enabled = true;
            txtChassisNo.Enabled = true;
            chkIsCoverNoteAvailable.Enabled = true;
            txtCoverNotePeriod.Enabled = true;
            txtAddressLine1.Enabled = true;
            txtAddressLine2.Enabled = true;
            txtAddressLine3.Enabled = true;
            txtYearOfMake.Enabled = true;
            txtFirstRegDate.Enabled = true;
            txtCubicCapacity.Enabled = true;

            lnkBtnAttachment.Enabled = true;
            btnSearchQuotationNos.Enabled = true;
            btnAddNew.Enabled = false;
            // btnAlter.Enabled = false;
            btnSave.Enabled = true;
            ClearComponents();
        }
        else if (mode == "EDIT")
        {
            txtQuotationNo.Enabled = true;
            txtVehicleNo.Enabled = true;
            txtEngineNo.Enabled = true;
            txtChassisNo.Enabled = true;
            chkIsCoverNoteAvailable.Enabled = true;
            txtCoverNotePeriod.Enabled = true;
            txtAddressLine1.Enabled = true;
            txtAddressLine2.Enabled = true;
            txtAddressLine3.Enabled = true;
            txtYearOfMake.Enabled = true;
            txtFirstRegDate.Enabled = true;
            txtCubicCapacity.Enabled = true;

            lnkBtnAttachment.Enabled = true;
            btnSearchQuotationNos.Enabled = true;
            btnAddNew.Enabled = false;
            // btnAlter.Enabled = false;
            btnSave.Enabled = true;
        }
        else if (mode == "LOADED")
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

            lnkBtnAttachment.Enabled = false;
            btnSearchQuotationNos.Enabled = false;
            btnAddNew.Enabled = false;
            //  btnAlter.Enabled = true;
            btnSave.Enabled = false;
        }
    }


    private void loadProposalUploadDetailsForEdit(string ProposalUploadId)
    {
        ClearComponents();

        ProposalUpload proposalUpload = new ProposalUpload();


        ProposalUploadController proposalUploadController = new ProposalUploadController();

        proposalUpload = proposalUploadController.GetUploadedProposal(Convert.ToInt32(ProposalUploadId));


        txtProposalUploadId.Text = proposalUpload.ProposalUploadId.ToString();
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



        ManageFormComponents("EDIT");
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
        ClearComponents();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtVehicleNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter the Vehicle No";
            Timer1.Enabled = true;
            return;
        }


        if (txtChassisNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter the Chassis No";
            Timer1.Enabled = true;
            return;
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
            proposalUpload.QuotationNo = txtQuotationNo.Text;
            proposalUpload.VehicleNo = txtVehicleNo.Text;
            proposalUpload.EngineNo = txtEngineNo.Text;
            proposalUpload.ChassisNo = txtChassisNo.Text;

            if (chkIsCoverNoteAvailable.Checked)
            {
                proposalUpload.IsCoverNoteAvailable = 1;
            }
            else
            {
                proposalUpload.IsCoverNoteAvailable = 0;

            }

            proposalUpload.CoverNotePeriod = txtCoverNotePeriod.Text;
            proposalUpload.AddressLine1 = txtAddressLine1.Text;
            proposalUpload.AddressLine2 = txtAddressLine2.Text;
            proposalUpload.AddressLine3 = txtAddressLine3.Text;
            proposalUpload.YearOfMake = txtYearOfMake.Text;
            proposalUpload.FirstRegDate = txtFirstRegDate.Text;
            proposalUpload.CubicCapacity = txtCubicCapacity.Text;


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


            ProposalUploadController proposalUploadController = new ProposalUploadController();

            if (Session["Mode"].ToString() == "NEW")
            {
                proposalUploadController.InsertProposalUpload(proposalUpload);
            }
            else if (Session["Mode"].ToString() == "EDIT")
            {
                proposalUploadController.UpdateProposalUpload(proposalUpload);
            }

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


    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

        ManageFormComponents("NEW");

        Session["Mode"] = "NEW";
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
    }



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }
}
