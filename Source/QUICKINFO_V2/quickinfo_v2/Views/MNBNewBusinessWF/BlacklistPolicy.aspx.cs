//******************************************
// Author            :Tharindu Athapattu
// Date              :11/07/2017
// Reviewed By       :
// Description       : Blacklist Form
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

public partial class BlacklistPolicy : System.Web.UI.Page
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



            btnBlacklistPolicy.Attributes.Add("onClick", "if(confirm('Are you sure to blacklist this Policy/Vehicle No?','Motor New Business Workflow')){}else{return false}");

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
            //btnGivePriority.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {

            //  btnGivePriority.Enabled = false;

            ClearComponents();
        }

    }

    protected void btnBlacklistPolicy_Click(object sender, EventArgs e)
    {

        if (txtVehicleNo.Text.Trim() == "" && txtPolicyNo.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Vehicle No or Policy No.";
            Timer1.Enabled = true;
            return;
        }

        if (txtRemarks.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter remark for Blacklisting";
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

            proposalUploadController.BlacklistPolicy(txtVehicleNo.Text, txtPolicyNo.Text, txtRemarks.Text, UserCode);


            ClearComponents();
            //  SearchData();
            lblMsg.Text = "Successfully Blacklisted";
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

        txtVehicleNo.Text = "";
        txtPolicyNo.Text = "";
        txtRemarks.Text = "";


     


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
