
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

public partial class CoverNoteBookRequest : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            ClearComponents();
            initializeValues();

            Session.Remove("Mode");

            pnlUserGrid.Visible = false;
        }


    }
    private void initializeValues()
    {
        lblError.Text = "";
        lblMsg.Text = "";

        loadBranches();
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

        string UserCode = "";
        string UserBranch = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserCode = reqCookies["UserCode"].ToString();
            UserBranch = reqCookies["UserBranch"].ToString();

        }
        

        ddlBranch.ClearSelection();
        ddlBranch.Items.FindByValue(UserBranch).Selected = true;

    }
    public string GetNewJobNoForRenewal(string BranchCode)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleCommand cmd = new OracleCommand("GET_MNBQ_WF_CVR_NOTE_REQ_NO", con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
        cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = BranchCode;

        cmd.Parameters.Add("V_NEW_REQ_NO", OracleType.VarChar, 20).Direction = ParameterDirection.Output;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();

            returnVal = Convert.ToString(cmd.Parameters["V_NEW_REQ_NO"].Value);


            return returnVal;
        }
        catch (OracleException err)
        {
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
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
            " WHERE (" + SQL + ") ORDER BY T.USER_CODE ASC";

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
        Response.Redirect("CoverNoteBookRequest.aspx");
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearComponents();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ddlBranch.SelectedValue == "" || ddlBranch.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Branch";
            Timer1.Enabled = true;


            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please Select Branch');", true);
            return;
        }

        if (rbtnYes.Checked == true)
        {
            if (txtInHandCoverNoteBookNumber.Text.Trim() == "" || txtInHandCoverNoteBookNumberStart.Text.Trim() == "" || txtInHandCoverNoteBookNumberEnd.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please enter details of Cover Note Book(s) In Hand');", true);

                return;
            }
        }

        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            if (Session["Mode"].ToString() == "NEW")
            {
                spProcess = new OracleCommand("INSERT_MNBQ_WF_CVR_NOTE_BK_REQ");
            }


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;


            spProcess.Parameters.Add("V_REQUEST_NO", OracleType.VarChar).Value = txtReqNo.Text.Trim();

            spProcess.Parameters.Add("V_REQUEST_DATE", OracleType.DateTime).Value = txtReqDate.Text;


            spProcess.Parameters.Add("V_BRANCH_CODE", OracleType.VarChar).Value = ddlBranch.SelectedValue;
            spProcess.Parameters.Add("V_EXT_CVR_NOTE_BK_NO", OracleType.VarChar).Value = txtExistingCoverNoteBookNumber.Text.Trim();
            spProcess.Parameters.Add("V_EXT_CVR_NOTE_BK_NO_START", OracleType.VarChar).Value = txtExistingCoverNoteBookNumberStart.Text.Trim();
            spProcess.Parameters.Add("V_EXT_CVR_NOTE_BK_NO_END", OracleType.VarChar).Value = txtExistingCoverNoteBookNumberEnd.Text.Trim();

            spProcess.Parameters.Add("V_REASON_TO_REQUEST_BOOK", OracleType.VarChar).Value = txtReason.Text;


            if (rbtnYes.Checked)
            {

                spProcess.Parameters.Add("V_IS_BOOKS_IN_HAND", OracleType.Number).Value = 1;
            }
            else
            {
                spProcess.Parameters.Add("V_IS_BOOKS_IN_HAND", OracleType.Number).Value = 0;

            }


            spProcess.Parameters.Add("V_IN_HAND_CVR_NOTE_BK_NO", OracleType.VarChar).Value = txtInHandCoverNoteBookNumber.Text;
            spProcess.Parameters.Add("V_IN_HAND_BK_NO_START", OracleType.VarChar).Value = txtInHandCoverNoteBookNumberStart.Text;
            spProcess.Parameters.Add("V_IN_HAND_BK_NO_END", OracleType.VarChar).Value = txtInHandCoverNoteBookNumberEnd.Text;

            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }

            string BOOK_REQ_PEND_APPR_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_BM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_PEND_APPR_BY_BM;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;


            spProcess.Parameters.Add("V_NEW_REQ_SEQ_NO", OracleType.Number).Direction = ParameterDirection.Output;



            spProcess.ExecuteNonQuery();
            string newRequestSeqNo = "";
            newRequestSeqNo = Convert.ToString(spProcess.Parameters["V_NEW_REQ_SEQ_NO"].Value);



            conProcess.Close();


            updateNextApprovePerson(newRequestSeqNo, ddlBranch.SelectedValue);

            sendRequestApprovalMail(newRequestSeqNo, txtReqNo.Text, ddlBranch.SelectedValue);


            ClearComponents();
            SearchData();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Successfully saved and sent for approval');", true);


        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Saving');", true);

        }

    }

    private void sendRequestApprovalMail(string newRequestSeqNo, string requestNo, string branchCode)
    {
        if (newRequestSeqNo == "")
        {
            return;
        }

        string nextApprovePerson = "";

        nextApprovePerson = getNextApprovePersonName(branchCode);

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
            notificationMsg = "Approval need for Cover Note Book Request under request no.  " + requestNo + " ";


            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyClientForCoverNoteBookRequests("Approval need for Cover Note Book Request", notificationMsg, nextApprovePerson);


        }
        catch (Exception ee)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error while sending notification e-mail.');", true);

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
                rbtnYes.Enabled = true;
            }
            else
            {
                rbtnYes.Checked = false;
                rbtnNo.Checked = true;

                rbtnNo.Enabled = true;
            }



            txtInHandCoverNoteBookNumber.Text = dr[9].ToString();
            txtInHandCoverNoteBookNumberStart.Text = dr[10].ToString();
            txtInHandCoverNoteBookNumberEnd.Text = dr[11].ToString();


        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }

    private bool CheckBookNumberlreadyExist(string BookNumber)
    {
        bool returnVal = false;
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT BOOK_NUMBER FROM MNBQ_WF_BOOK_MGR WHERE BOOK_NUMBER='" + BookNumber + "'";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            returnVal = true;
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
    private void updateNextApprovePerson(string requestSeqNo, string branchCode)
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


            string BOOK_REQ_PEND_APPR_BY_BM = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_PEND_APPR_BY_BM"].ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = BOOK_REQ_PEND_APPR_BY_BM;



            //string UserCode = "";
            //string UserBranch = "";
            //HttpCookie reqCookies = Request.Cookies["userInfo"];
            //if (reqCookies != null)
            //{
            //    UserCode = reqCookies["UserCode"].ToString();
            //    UserBranch = reqCookies["UserBranch"].ToString();

            //}

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = getNextApprovePersonName(branchCode);




            spProcess.ExecuteNonQuery();
            conProcess.Close();


        }
        catch (Exception ex)
        {
        }

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


        btnAddNew.Enabled = true;
        btnSave.Enabled = false;
        // btnCancel.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtBookReqSeqNo.Enabled = false;
        txtReqNo.Enabled = false;
        txtReqDate.Enabled = false;
        ddlBranch.Enabled = false;
        txtExistingCoverNoteBookNumber.Enabled = true;
        txtExistingCoverNoteBookNumberStart.Enabled = true;
        txtExistingCoverNoteBookNumberEnd.Enabled = true;
        txtReason.Enabled = true;
        rbtnYes.Enabled = true;
        rbtnNo.Enabled = true;
        txtInHandCoverNoteBookNumber.Enabled = true;
        txtInHandCoverNoteBookNumberStart.Enabled = true;
        txtInHandCoverNoteBookNumberEnd.Enabled = true;


        txtBookReqSeqNo.Text = "";
        txtReqNo.Text = "";
        txtReqDate.Text = "";
        ddlBranch.SelectedValue = "0";
        txtExistingCoverNoteBookNumber.Text = "";
        txtExistingCoverNoteBookNumberStart.Text = "";
        txtExistingCoverNoteBookNumberEnd.Text = "";
        txtReason.Text = "";
  
        txtInHandCoverNoteBookNumber.Text = "";
        txtInHandCoverNoteBookNumberStart.Text = "";
        txtInHandCoverNoteBookNumberEnd.Text = "";
        btnSave.Enabled = true;

        rbtnYes.Checked = true;
        rbtnNo.Checked = false;

        Session["Mode"] = "NEW";

        string UserCode = "";
        string UserBranch = "";
        HttpCookie reqCookies = Request.Cookies["userInfo"];
        if (reqCookies != null)
        {
            UserCode = reqCookies["UserCode"].ToString();
            UserBranch = reqCookies["UserBranch"].ToString();

        }

        ddlBranch.ClearSelection();
        ddlBranch.Items.FindByValue(UserBranch).Selected = true;


        txtReqNo.Text = GetNewJobNoForRenewal(UserBranch);
        txtReqDate.Text = System.DateTime.Now.ToShortDateString();

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



   



    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }


}
