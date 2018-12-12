//******************************************
// Author            :Tharindu Athapattu
// Date              :18/07/2017
// Reviewed By       :
// Description       : CentralizeUserMgt
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

public partial class CentralizeUserMgt : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
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
            //btnGivePriority.Enabled = false;
            ClearComponents();
        }
        else if (mode == "NEW")
        {

            //  btnGivePriority.Enabled = false;

            ClearComponents();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtUserCode.Text.Trim() == "" && txtUserName.Text.Trim() == "")
        {
            lblMsg.Text = "Enter User name and User code";
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

            int status = 1;
            if (rdbtnInActive.Checked)
            {
                status = 0;
            }
            else
            {
                status = 1;
            }




            proposalUploadController.SaveCentralizeUser(txtUserCode.Text, txtUserName.Text, status);

            if (fuPhoto.HasFile)
            {
                fuPhoto.SaveAs(quickinfo_v2.Properties.Settings.Default.UserPhotoSavePath + txtUserCode.Text + fuPhoto.FileName.Substring(fuPhoto.FileName.LastIndexOf("."), 4).ToUpper());
            }



            ClearComponents();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

            ManageFormComponents("INITIAL");


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

        txtUserCode.Text = "";
        txtUserName.Text = "";

        rdbtnActive.Checked = true;
        rdbtnInActive.Checked = false;




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
