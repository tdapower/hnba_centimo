//******************************************
// Author            :Tharindu Athapattu
// Date              :19/07/2017
// Reviewed By       :
// Description       : TimeRangeJobReport Form
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
using quickinfo_v2.Controllers.Dashboard;
using System.Web.UI.DataVisualization.Charting;
using System.ComponentModel;
using System.Web.UI.WebControls;
public partial class TimeRangeJobReport : System.Web.UI.Page
{
    string UserCode = "";
    string UserBranch = "";

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserCode = reqCookies["UserCode"].ToString();
            UserBranch = reqCookies["UserBranch"].ToString();
        }



        if (!IsPostBack)
        {

            validatePageAuthentication();

            ClearComponents();


            initializeValues();

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


    private void initializeValues()
    {

        txtDateFrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtDateTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

    }
    private void ClearComponents()
    {
    }







    protected void btnSearch_Click(object sender, EventArgs e)
    {
        search();
    }

    private void search()
    {
        if (txtDateFrom.Text.Trim() == "" || txtDateTo.Text.Trim() == "" || txtTimeFrom.Value == "" || txtTimeTo.Value == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('All the parameters must fill');", true);

            return;
        }



        ProposalUploadController proposalUploadController = new ProposalUploadController();
        ltrlSummary.Text = proposalUploadController.GetTimeRangeJobReport(txtDateFrom.Text, txtDateTo.Text, txtTimeFrom.Value, txtTimeTo.Value);



    }










}
