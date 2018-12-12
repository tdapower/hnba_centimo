//******************************************
// Author            :Tharindu Athapattu
// Date              :12/09/2015
// Reviewed By       :
// Description       : CallCenterView Form
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
public partial class CallCenterView : System.Web.UI.Page
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
        loadBranches();



        if (UserBranch != "HDO")
        {

            ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(UserBranch));

            ddlBranch.Enabled = false;
        }





        loadStatuses();

        txtDateFrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        txtDateTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

    }
    private void ClearComponents()
    {
    }
    protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[9].Visible = false;



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell timeCel = e.Row.Cells[8];
                if (timeCel.Text == "&nbsp;")
                {
                    timeCel.Text = "0";
                }

                if (timeCel.Text != "")
                {
                    TimeSpan previousTimeTimespan = TimeSpan.FromSeconds(Convert.ToDouble(timeCel.Text));
                    timeCel.Text = TimeSpan.Parse(previousTimeTimespan.ToString()).Add(new TimeSpan(0, 0, 1)).ToString(); ;


                }

                string ProposalUploadId = e.Row.Cells[9].Text;
                string JobType = e.Row.Cells[2].Text;
                string Status = e.Row.Cells[5].Text;



                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "../Common/JobFollowup.aspx?ProposalUploadId=" + ProposalUploadId + "&JobType=" + JobType + "&Status=" + Status);

            }
        }
    }
    protected void grdSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSearchResults.PageIndex = e.NewPageIndex;
        search();
    }





    public System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = System.Web.UI.WebControls.SortDirection.Ascending;

            return (System.Web.UI.WebControls.SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    protected void grdSearchResults_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }

    }

    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance
        DataTable dt = getSearchResult();

        DataView dv = new DataView(dt);
        dv.Sort = sortExpression + direction;

        grdSearchResults.DataSource = dv;
        grdSearchResults.DataBind();
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        search();
    }

    private DataTable getSearchResult()
    {
        ProposalUploadController proposalUploadController = new ProposalUploadController();
        DataTable completedJobs = new DataTable();

        /////



        string SQL = "";
        if (ddlBranch.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(mpu.TARGET_BRANCH_CODE = '" + ddlBranch.SelectedValue.ToString() + "'  OR mpu.quotation_no like '%" + ddlBranch.SelectedValue.ToString() + "%'   OR mpu.job_number like '%" + ddlBranch.SelectedValue.ToString() + "%') AND";
        }
        if (ddlStatus.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(mpu.status = '" + ddlStatus.SelectedValue.ToString() + "') AND";
        }

        if (txtDateFrom.Text != "")
        {

            SQL = SQL + "(  TO_DATE( mpuf.sys_date  ,'dd/mm/RRRR') >=  TO_DATE('" + txtDateFrom.Text.ToLower() + "','dd/mm/RRRR') ) AND";
        }

        if (txtDateTo.Text != "")
        {


            SQL = SQL + "( TO_DATE(mpuf.sys_date ,'dd/mm/RRRR') <=TO_DATE('" + txtDateTo.Text.ToLower() + "','dd/mm/RRRR') ) AND";
        }


        if (txtPolicyNo.Text != "")
        {

            SQL = SQL + "(LOWER(mpu.tcs_policy_no) LIKE '%" + txtPolicyNo.Text.ToLower() + "%') AND";
        }

        if (txtJobNo.Text != "")
        {

            SQL = SQL + "(LOWER(mpu.QUOTATION_NO ) LIKE '%" + txtJobNo.Text.ToLower() + "%'  OR LOWER(mpu.JOB_NUMBER ) LIKE '%" + txtJobNo.Text.ToLower() + "%'   )    AND";
        }





        SQL = SQL.Substring(0, SQL.Length - 3);


        ////

        completedJobs = proposalUploadController.GetJobs(SQL);
        return completedJobs;
    }
    private void search()
    {

        if (ddlBranch.SelectedValue.ToString() == "0" && ddlStatus.SelectedValue.ToString() == "0" && txtDateFrom.Text == "" && txtDateTo.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Search text cannot be blank');", true);
            return;
        }


        grdSearchResults.DataSource = null;
        grdSearchResults.DataBind();


        DataTable completedJobs = new DataTable();
        completedJobs = getSearchResult();
        grdSearchResults.DataSource = completedJobs;


        if (grdSearchResults.DataSource != null)
        {
            grdSearchResults.DataBind();
        }
    }




    private void loadStatuses()
    {
        ddlStatus.Items.Clear();


        string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
        string TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD"].ToString();
        string RENEWAL_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOCS_ADDED"].ToString();
        string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
        string TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD"].ToString();
        string CANCELLATION_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_DOCS_ADDED"].ToString();
        string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();
        string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
        string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();
        string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();
        string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
        string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();
        string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();
        string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();
        string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();
        string COMPLETED_AND_PRINTED = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_AND_PRINTED"].ToString();
        // string UNKNOWN = System.Configuration.ConfigurationManager.AppSettings["UNKNOWN"].ToString();




        ddlStatus.Items.Add(new ListItem("--- Select ---", "0"));
        ddlStatus.Items.Add(new ListItem(RENEWAL_ADDED, RENEWAL_ADDED));
        ddlStatus.Items.Add(new ListItem(TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD, TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD));
        ddlStatus.Items.Add(new ListItem(RENEWAL_DOCS_ADDED, RENEWAL_DOCS_ADDED));
        ddlStatus.Items.Add(new ListItem(CANCELLATION_ADDED, CANCELLATION_ADDED));
        ddlStatus.Items.Add(new ListItem(TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD, TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD));
        ddlStatus.Items.Add(new ListItem(CANCELLATION_DOCS_ADDED, CANCELLATION_DOCS_ADDED));
        ddlStatus.Items.Add(new ListItem(ENDORSEMENT_ADDED, ENDORSEMENT_ADDED));
        ddlStatus.Items.Add(new ListItem(INITIAL, INITIAL));
        ddlStatus.Items.Add(new ListItem(TAKEN_BY_SCRUTINIZING, TAKEN_BY_SCRUTINIZING));
        ddlStatus.Items.Add(new ListItem(REJECTED_BY_SCRUTINIZING, REJECTED_BY_SCRUTINIZING));
        ddlStatus.Items.Add(new ListItem(APPROVED_BY_SCRUTINIZING, APPROVED_BY_SCRUTINIZING));
        ddlStatus.Items.Add(new ListItem(TAKEN_BY_PROCESSING, TAKEN_BY_PROCESSING));
        ddlStatus.Items.Add(new ListItem(COMPLETED_BY_PROCESSING, COMPLETED_BY_PROCESSING));
        ddlStatus.Items.Add(new ListItem(TAKEN_BY_VALIDATORS, TAKEN_BY_VALIDATORS));
        ddlStatus.Items.Add(new ListItem(APPROVED_BY_VALIDATORS, APPROVED_BY_VALIDATORS));
        ddlStatus.Items.Add(new ListItem(COMPLETED_AND_PRINTED, COMPLETED_AND_PRINTED));
        //   ddlStatus.Items.Add(new ListItem(UNKNOWN, UNKNOWN));


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




}
