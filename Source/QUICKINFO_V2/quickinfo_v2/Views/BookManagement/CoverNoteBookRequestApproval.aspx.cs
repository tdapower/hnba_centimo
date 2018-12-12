
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
using quickinfo_v2;

public partial class CoverNoteBookRequestApproval : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {


            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);
            btnRejectBM.Attributes.Add("onClick", "if(confirm('Are you sure to Reject this Request?','Cover Note Book Request')){}else{return false}");
            //btnRejectZM.Attributes.Add("onClick", "if(confirm('Are you sure to Reject this Request?','Cover Note Book Request')){}else{return false}");
            btnRejectHDO.Attributes.Add("onClick", "if(confirm('Are you sure to Reject this Request?','Cover Note Book Request')){}else{return false}");




            ClearComponents();
            initializeValues();

            Session.Remove("Mode");

            pnlUserGrid.Visible = false;


            if (Request.Params["RequestSeqNo"] != null)
            {
                if (Request.Params["RequestSeqNo"] != "")
                {
                    ClearComponents();
                    txtBookReqSeqNo.Text = Request.Params["RequestSeqNo"].ToString();


                    loadRequestDetails(Request.Params["RequestSeqNo"].ToString());
                    txtAppRejRemarks.Enabled = true;
                }
            }

        }


    }
    private void initializeValues()
    {
        lblError.Text = "";
        lblMsg.Text = "";

        loadBranches();
        loadendingRequests();
    }




    private void loadBranches()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("--- Select One ---", "0"));

        ddlSearchBranch.Items.Clear();
        ddlSearchBranch.Items.Add(new ListItem("--- Select One ---", "0"));

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

                ddlSearchBranch.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
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


    private void loadendingRequests()
    {
        //grdPendingApprovals
        grdPendingApprovals.DataSource = null;
        grdPendingApprovals.DataBind();


        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        string UserCode = "";
        string UserBranch = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserCode = reqCookies["UserCode"].ToString();
            UserBranch = reqCookies["UserBranch"].ToString();

        }


        //string userBranchType = "";
        //userBranchType = getUserBranchType(UserBranch);

        string BOOK_REQ_PEND_APPR_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_BM"].ToString();
        string BOOK_REQ_PEND_APPR_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_ZM"].ToString();
        string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();


        //string selectStatus = "";


        //if (userBranchType == "BRANCH")
        //{
        //    selectStatus = BOOK_REQ_PEND_APPR_BY_BM;
        //}
        //else if (userBranchType == "ZONAL")
        //{
        //    selectStatus = BOOK_REQ_PEND_APPR_BY_ZM;
        //}
        //else if (userBranchType == "HDO")
        //{
        //    selectStatus = BOOK_REQ_PEND_APPR_BY_HDO;
        //}




        String selectQuery = "";
        //selectQuery = " SELECT  " +
        //          " T.SEQ_NO     ," +
        //          " T.REQUEST_NO  AS \"Request No.\"  " +
        //          " FROM MNBQ_WF_CVR_NOTE_BOOK_REQ T " +
        //          "  INNER JOIN mnbq_wf_cvr_note_book_req_fup f on t.seq_no=f.book_req_seq_no and t.status=f.status " +
        //    " WHERE f.USER_CODE='" + UserCode + "' ORDER BY T.SEQ_NO ASC";


        selectQuery = " SELECT  " +
                  " T.SEQ_NO     ," +
                  " T.REQUEST_NO  AS \"Request No.\"  " +
                  " FROM MNBQ_WF_CVR_NOTE_BOOK_REQ T " +
                  "  INNER JOIN mnbq_wf_cvr_note_book_req_fup f on t.seq_no=f.book_req_seq_no and t.status=f.status " +
            " WHERE  T.STATUS in ('" + BOOK_REQ_PEND_APPR_BY_BM + "','" + BOOK_REQ_PEND_APPR_BY_ZM + "','" + BOOK_REQ_PEND_APPR_BY_HDO + "') AND  f.USER_CODE='" + UserCode + "' ORDER BY T.SEQ_NO ASC";






        myOleDbCommand.CommandText = selectQuery;

        OracleDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
        if (myOleDbDataReader.HasRows == true)
        {
            DataTable dbTable = new DataTable();
            grdPendingApprovals.DataSource = myOleDbDataReader;
            grdPendingApprovals.DataBind();

        }

    }


    protected void grdPendingApprovals_SelectedIndexChanged(object sender, EventArgs e)
    {

        ClearComponents();
        txtBookReqSeqNo.Text = grdPendingApprovals.SelectedRow.Cells[1].Text.Trim();


        loadRequestDetails(grdPendingApprovals.SelectedRow.Cells[1].Text.Trim());
        txtAppRejRemarks.Enabled = true;

    }

    protected void grdPendingApprovals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchData();
        ClearComponents();
    }

    private void SearchData()
    {
        string SQL = "";
        lblError.Text = "";
        grdSearchResults.DataSource = null;
        grdSearchResults.DataBind();

        if ((txtSearchRequestedDate.Text == "") && (txtSearchRequestedNo.Text == "") && (ddlSearchBranch.SelectedValue.ToString() == "0"))
        {
            lblError.Text = "Search text cannot be blank";
            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchRequestedDate.Text != "")
        {

            SQL = "(TO_DATE(T.REQUEST_DATE,'dd/mm/RRRR') = TO_DATE('" + txtSearchRequestedDate.Text.ToLower() + "','dd/mm/RRRR') ) AND";
        }


        if (txtSearchRequestedNo.Text != "")
        {

            SQL = "(LOWER(T.REQUEST_NO) LIKE '%" + txtSearchRequestedNo.Text.ToLower() + "%') AND";
        }
        if (ddlSearchBranch.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.BRANCH_CODE = '" + ddlSearchBranch.SelectedValue.ToString() + "') AND";
        }




        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = " SELECT  " +
                  " T.SEQ_NO     ," +
                  " T.REQUEST_NO  AS \"Request No.\"  ," +
                  " T.REQUEST_DATE  AS \"Request Date\"," +
                  " B.BRANCH_NAME   AS \"Branch\", " +
                  " T.STATUS  AS \"Status\" " +
                  " FROM MNBQ_WF_CVR_NOTE_BOOK_REQ T " +
                  " INNER JOIN MNBQ_WF_BRANCH B ON T.Branch_Code=B.BRANCH_CODE " +
            " WHERE (" + SQL + ") ORDER BY T.SEQ_NO ASC";

        myOleDbCommand.CommandText = selectQuery;

        OracleDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
        if (myOleDbDataReader.HasRows == true)
        {
            DataTable dbTable = new DataTable();
            grdSearchResults.DataSource = myOleDbDataReader;
            grdSearchResults.DataBind();

            pnlUserGrid.Visible = true;
        }
    }




    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CoverNoteBookRequestApproval.aspx");
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearComponents();
    }
    private void showHideButtons(string status)
    {
        string BOOK_REQ_PEND_APPR_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_BM"].ToString();
        string BOOK_REQ_PEND_APPR_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_ZM"].ToString();
        string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();


        if (status == BOOK_REQ_PEND_APPR_BY_BM)
        {
            btnApproveBM.Visible = true;
            btnRejectBM.Visible = true;

            //btnApproveZM.Visible = false;
            //btnRejectZM.Visible = false;

            btnApproveHDO.Visible = false;
            btnRejectHDO.Visible = false;
        }
        else if (status == BOOK_REQ_PEND_APPR_BY_ZM)
        {
            btnApproveBM.Visible = false;
            btnRejectBM.Visible = false;

            //btnApproveZM.Visible = true;
            //btnRejectZM.Visible = true;

            btnApproveHDO.Visible = false;
            btnRejectHDO.Visible = false;
        }
        else if (status == BOOK_REQ_PEND_APPR_BY_HDO)
        {
            btnApproveBM.Visible = false;
            btnRejectBM.Visible = false;

            //btnApproveZM.Visible = false;
            //btnRejectZM.Visible = false;

            btnApproveHDO.Visible = true;
            btnRejectHDO.Visible = true;
        }
    }

    protected void btnApproveBM_Click(object sender, EventArgs e)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string APPROVED_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_APPROVED_BY_BM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = APPROVED_BY_BM;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;



            spProcess.ExecuteNonQuery();
            conProcess.Close();


            //string BOOK_REQ_PEND_APPR_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_ZM"].ToString();
            //updateNextApprovePerson(txtBookReqSeqNo.Text, ddlBranch.SelectedValue, BOOK_REQ_PEND_APPR_BY_ZM);


            string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();
            updateNextApprovePerson(txtBookReqSeqNo.Text, ddlBranch.SelectedValue, BOOK_REQ_PEND_APPR_BY_HDO);

            sendApproveMail(txtReqNo.Text, "Branch");



            ClearComponents();
            SearchData();
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Approved');", true);


            loadendingRequests();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Approving');", true);

        }

    }



    protected void btnRejectBM_Click(object sender, EventArgs e)
    {
        if (txtAppRejRemarks.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter reason for reject the request');", true);
            return;
        }


        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_REJECTED_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_REJECTED_BY_BM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_REJECTED_BY_BM;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;



            spProcess.ExecuteNonQuery();
            conProcess.Close();

            sendRejectMail(txtReqNo.Text, txtAppRejRemarks.Text, "Branch");


            ClearComponents();
            SearchData();


            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Rejected');", true);
            loadendingRequests();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while Rejecting');", true);
        }
    }


    protected void btnApproveZM_Click(object sender, EventArgs e)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_APPROVED_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_APPROVED_BY_ZM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_APPROVED_BY_ZM;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;



            spProcess.ExecuteNonQuery();
            conProcess.Close();

            string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();
            updateNextApprovePerson(txtBookReqSeqNo.Text, ddlBranch.SelectedValue, BOOK_REQ_PEND_APPR_BY_HDO);


            sendApproveMail(txtReqNo.Text, "Zonal");

            ClearComponents();
            SearchData();

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Approved');", true);

            loadendingRequests();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Approving');", true);
        }

    }



    protected void btnRejectZM_Click(object sender, EventArgs e)
    {
        if (txtAppRejRemarks.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter reason for reject the request');", true);
            return;
        }


        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_REJECTED_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_REJECTED_BY_ZM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_REJECTED_BY_ZM;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;



            spProcess.ExecuteNonQuery();
            conProcess.Close();


            sendRejectMail(txtReqNo.Text, txtAppRejRemarks.Text, "Zonal");

            ClearComponents();
            SearchData();
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Rejected');", true);

            loadendingRequests();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Rejecting');", true);
        }
    }

    protected void btnApproveHDO_Click(object sender, EventArgs e)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_APPROVED_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_APPROVED_BY_HDO"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_APPROVED_BY_HDO;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;


            sendApproveMail(txtReqNo.Text, "Head Office");
            sendNotifyMailToBookIssuers(txtReqNo.Text);
            spProcess.ExecuteNonQuery();
            conProcess.Close();

            ClearComponents();
            SearchData();

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Approved');", true);


            loadendingRequests();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Approving');", true);
        }

    }



    protected void btnRejectHDO_Click(object sender, EventArgs e)
    {
        if (txtAppRejRemarks.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter reason for reject the request');", true);
            return;
        }


        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_CVR_NOTE_BK_REQ");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = txtBookReqSeqNo.Text.Trim();




            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_REJECTED_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_REJECTED_BY_HDO"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_REJECTED_BY_HDO;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtAppRejRemarks.Text;



            spProcess.ExecuteNonQuery();
            conProcess.Close();


            sendRejectMail(txtReqNo.Text, txtAppRejRemarks.Text, "Head Office");

            ClearComponents();
            SearchData();

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully Rejected');", true);

            loadendingRequests();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Rejecting');", true);
        }
    }




    private void loadRequestDetails(string requestSeqNo)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT 	" +
                    "T.SEQ_NO       , " +//0
                    " T.REQUEST_NO     , " +//1
                    " T.REQUEST_DATE   , " +//2
                    " T.BRANCH_CODE     , " +//3
                    " T.EXT_CVR_NOTE_BK_NO     , " +//4
                    " T.EXT_CVR_NOTE_BK_NO_START     , " +//5
                    " T.EXT_CVR_NOTE_BK_NO_END     , " +//6
                    " T.REASON_TO_REQUEST_BOOK   , " +//7
                    " T.IS_BOOKS_IN_HAND       , " +//8
                    " T.IN_HAND_CVR_NOTE_BK_NO     , " +//9
                    " T.IN_HAND_BK_NO_START     , " +//10
                    " T.IN_HAND_BK_NO_END     , " +//11
                    " T.STATUS     ,  " +//12
                    " T.USER_CODE    , " +//13
                    " T.SYS_DATE    " +//14
                    " FROM MNBQ_WF_CVR_NOTE_BOOK_REQ T " +
                    " WHERE T.SEQ_NO=" + requestSeqNo + "";






        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            txtBookReqSeqNo.Text = dr[0].ToString();
            txtReqNo.Text = dr[1].ToString();
            txtReqDate.Text = dr[2].ToString().Remove(10);
            ddlBranch.SelectedValue = dr[3].ToString();
            txtExistingCoverNoteBookNumber.Text = dr[4].ToString();
            txtExistingCoverNoteBookNumberStart.Text = dr[5].ToString();
            txtExistingCoverNoteBookNumberEnd.Text = dr[6].ToString();
            txtReason.Text = dr[7].ToString();

            if (dr[8].ToString() == "1")
            {
                rbtnYes.Checked = true;
                rbtnNo.Checked = false;
            }
            else
            {
                rbtnYes.Checked = false;
                rbtnNo.Checked = true;
            }


            txtInHandCoverNoteBookNumber.Text = dr[9].ToString();
            txtInHandCoverNoteBookNumberStart.Text = dr[10].ToString();
            txtInHandCoverNoteBookNumberEnd.Text = dr[11].ToString();


            showHideButtons(dr[12].ToString());
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }


    private void ClearComponents()
    {
        txtBookReqSeqNo.Text = "";
        txtReqNo.Text = "";
        txtReqDate.Text = "";
        ddlBranch.SelectedValue = "0";
        txtExistingCoverNoteBookNumber.Text = "";
        txtExistingCoverNoteBookNumberStart.Text = "";
        txtExistingCoverNoteBookNumberEnd.Text = "";
        txtReason.Text = "";
        rbtnYes.Checked = false;
        rbtnNo.Checked = false;
        txtInHandCoverNoteBookNumber.Text = "";
        txtInHandCoverNoteBookNumberStart.Text = "";
        txtInHandCoverNoteBookNumberEnd.Text = "";
        txtAppRejRemarks.Text = "";


        txtBookReqSeqNo.Enabled = false;
        txtReqNo.Enabled = false;
        txtReqDate.Enabled = false;
        ddlBranch.Enabled = false;
        txtExistingCoverNoteBookNumber.Enabled = false;
        txtExistingCoverNoteBookNumberStart.Enabled = false;
        txtExistingCoverNoteBookNumberEnd.Enabled = false;
        txtReason.Enabled = false;
        rbtnYes.Enabled = false;
        rbtnNo.Enabled = false;
        txtInHandCoverNoteBookNumber.Enabled = false;
        txtInHandCoverNoteBookNumberStart.Enabled = false;
        txtInHandCoverNoteBookNumberEnd.Enabled = false;
        txtAppRejRemarks.Enabled = false;

        btnApproveBM.Visible = false;
        btnRejectBM.Visible = false;

        //btnApproveZM.Visible = false;
        //btnRejectZM.Visible = false;

        btnApproveHDO.Visible = false;
        btnRejectHDO.Visible = false;

        // btnCancel.Enabled = false;
    }



    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {

        ClearComponents();
        txtBookReqSeqNo.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();


        loadRequestDetails(grdSearchResults.SelectedRow.Cells[1].Text.Trim());

    }

    protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }

    private string getUserBranchType(string branchCode)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.branch_type from mis_gi_branches t where t.branch_code='" + branchCode + "'";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;
    }



    private string getNextApprovePersonName(string branchCode)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.manager_name_code from mnbq_wf_branch t where t.branch_code='" + branchCode + "'";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;
    }


    private string getZonalApprovePersonName(string branchCode)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        selectQuery = "select z.manager_name_code " +
                    "from mnbq_wf_zone z  " +
                    "where z.ZONE_CODE=(select t.zonal_code from mis_gi_branches t where t.branch_code='" + branchCode + "')";


        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;
    }


    private string getZonalBranchOfBranch(string branchCode)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.branch_code from mis_gi_branches t " +
             " where  " +
             " t.zonal_code=(select t.zonal_code from mis_gi_branches t where t.branch_code='" + branchCode + "') " +
             " and t.branch_type='ZONAL'";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;
    }
    private void updateNextApprovePerson(string requestSeqNo, string branchCode, string newStatus)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("MNBQ_WF_CVR_REQ_ADD_NXT_USER");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_SEQ_NO", OracleType.VarChar).Value = requestSeqNo;


            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = newStatus;


            //string BOOK_REQ_PEND_APPR_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_ZM"].ToString();
            string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();


            string zonalBranchOfBranch = "";
            //if (newStatus == BOOK_REQ_PEND_APPR_BY_ZM)
            //{

            //    zonalBranchOfBranch = getZonalBranchOfBranch(branchCode);

            //    if (zonalBranchOfBranch == "")
            //    {
            //        zonalBranchOfBranch = "HDO";
            //    }


            //    spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = getZonalApprovePersonName(zonalBranchOfBranch);


            //}
            //else if (newStatus == BOOK_REQ_PEND_APPR_BY_HDO)
            //{
            //    spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = getNextApprovePersonName("HDO");
            //}

            if (newStatus == BOOK_REQ_PEND_APPR_BY_HDO)
            {
                spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = getNextApprovePersonName("HDO");
            }




            spProcess.ExecuteNonQuery();
            conProcess.Close();

            //if (newStatus == BOOK_REQ_PEND_APPR_BY_ZM)
            //{
            //    sendRequestApprovalMail(txtBookReqSeqNo.Text, txtReqNo.Text, zonalBranchOfBranch, newStatus);
            //}
            //else
            if (newStatus == BOOK_REQ_PEND_APPR_BY_HDO)
            {

                sendRequestApprovalMail(txtBookReqSeqNo.Text, txtReqNo.Text, "HDO", newStatus);
            }

        }
        catch (Exception ex)
        {
        }

    }

    private void sendRequestApprovalMail(string newRequestSeqNo, string requestNo, string branchCode, string newStatus)
    {
        if (newRequestSeqNo == "")
        {
            return;
        }

        string BOOK_REQ_PEND_APPR_BY_ZM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_ZM"].ToString();
        string BOOK_REQ_PEND_APPR_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_HDO"].ToString();



        string nextApprovePerson = "";


        if (newStatus == BOOK_REQ_PEND_APPR_BY_ZM)
        {
            nextApprovePerson = getZonalApprovePersonName(branchCode);
        }
        else
        {
            nextApprovePerson = getNextApprovePersonName(branchCode);

        }



        string UserCode = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserCode = reqCookies["UserCode"].ToString();
        }


        ProposalUploadController proposalUploadController = new ProposalUploadController();



        CommonMail mail = new CommonMail();

        mail.From_address = "mnb.workflow@hnbgeneral.com";


        mail.To_address = proposalUploadController.getEmailOfUser(nextApprovePerson);
        // mail.To_address = proposalUploadController.getEmailOfUser(UserCode);

        //string enteredUserEmail = "";
        //enteredUserEmail = proposalUploadController.getEmailOfUser(UserCode);


        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";




        string pageURl = "";

        pageURl = "http://192.168.10.103:8045/Views/BookManagement/";

        pageURl = pageURl + "CoverNoteBookRequestApproval.aspx" + "?RequestSeqNo=" + newRequestSeqNo;



        mail.Subject = "Approval need for Cover Note Book Request";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Approval need for Cover Note Book Request</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "Approval need for Cover Note Book Request under request no.  " + requestNo + " ," +
                  "</td>" +
                   "</tr>" +
                "<tr>" +
                  "<td>" +
                   "Click <a href=\"" + pageURl + "\">here</a> to Approve/Reject the request." +
                  "</td>" +
                   "</tr>" +
                   "</table>" +
                    " </body> " +
                    " </html>";


        try
        {
            mail.Body = BodyText;
            mail.sendMail();


            string notificationMsg = "";
            notificationMsg = "Approval need for Cover Note Book Request under request no.  " + requestNo + "";

            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyClientForCoverNoteBookRequests("Approval need for Cover Note Book Request", notificationMsg, nextApprovePerson);


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

        }
    }


    private void sendApproveMail(string requestNo, string approvedLevel)
    {


        CommonMail mail = new CommonMail();

        mail.From_address = "mnb.workflow@hnbgeneral.com";

        string requestedUser = "";
        requestedUser = getRequestedUser(requestNo);
        mail.To_address = getEmailOfUser(requestedUser);



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";






        mail.Subject = "Cover Note Book Requested has been Approved";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Cover Note Book Requested has been Approved</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "Cover Note Book request under request no. " + requestNo + " has been approved by " + approvedLevel + " level" +
                  "</td>" +
                   "</tr>" +
                   "</table>" +
                    " </body> " +
                    " </html>";


        try
        {
            mail.Body = BodyText;
            mail.sendMail();


            string notificationMsg = "";
            notificationMsg = "Cover Note Book request under request no. " + requestNo + " has been approved by " + approvedLevel + " level";

            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyClientForCoverNoteBookRequests("Cover Note Book Requested has been Approved", notificationMsg, requestedUser);


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

        }
    }

    private void sendRejectMail(string requestNo, string reason, string rejectLevel)
    {


        CommonMail mail = new CommonMail();

        mail.From_address = "mnb.workflow@hnbgeneral.com";

        string requestedUser = "";
        requestedUser = getRequestedUser(requestNo);
        mail.To_address = getEmailOfUser(requestedUser);



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";






        mail.Subject = "Cover Note Book Requested has been Rejected";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Cover Note Book Requested has been Rejected</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "Cover Note Book request under request no. " + requestNo + " has been rejected from " + rejectLevel + " level." +
                  "</td>" +
                   "</tr>" +
                "<tr>" +
                  "<td>" +
                   "Reason for Reject - " + reason +
                  "</td>" +
                   "</tr>" +
                   "</table>" +
                    " </body> " +
                    " </html>";


        try
        {
            mail.Body = BodyText;
            mail.sendMail();



            string notificationMsg = "";
            notificationMsg = "Cover Note Book request under request no. " + requestNo + " has been rejected from " + rejectLevel + " level.";

            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyClientForCoverNoteBookRequests("Cover Note Book Requested has been Rejected", notificationMsg, requestedUser);

        }
        catch (Exception ee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

        }
    }

    private void sendNotifyMailToBookIssuers(string requestNo)
    {


        CommonMail mail = new CommonMail();

        mail.From_address = "mnb.workflow@hnbgeneral.com";


        mail.To_address = getEmailOfBookIssueUser(System.Configuration.ConfigurationManager.AppSettings["CVR_NOTE_BOOK_ISSUER_MAIL_GROUP_NAME"].ToString());



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";



        string pageURl = "";

        pageURl = "http://192.168.10.103:8045/Views/BookManagement/BookManager.aspx?pagecode=200";


        mail.Subject = "Cover Note Book Ready to Issue";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Cover Note Book Requested has been Approved and ready to issue</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "Cover Note Book request under request no. " + requestNo + " has been approved and ready to issue" +
                  "</td>" +
                   "</tr>" +
                    "<tr>" +
                  "<td>" +
                   "Click <a href=\"" + pageURl + "\">here</a> to issue the book." +
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
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

        }
    }

    public string getEmailOfUser(string userCode)
    {
        string userEmail = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        selectQuery = "   SELECT " +
                             "WAU.USER_EMAIL  " +
                             " FROM  WF_ADMIN_USERS WAU " +
                             " WHERE WAU.USER_CODE='" + userCode + "'    ";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            userEmail = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return userEmail;

    }
    public string getRequestedUser(string requestNo)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        selectQuery = "select t.user_code from MNBQ_WF_CVR_NOTE_BOOK_REQ t where t.request_no='" + requestNo + "'    ";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;

    }

    public string getEmailOfBookIssueUser(string mailGroupName)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        selectQuery = "select t.MAILS from mnbq_wf_mail_groups t where t.GROUP_NAME='" + mailGroupName + "'    ";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            returnVal = dr[0].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return returnVal;

    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }


}
