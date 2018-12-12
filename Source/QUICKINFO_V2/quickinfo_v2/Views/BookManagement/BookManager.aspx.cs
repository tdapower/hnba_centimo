
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

public partial class BookManager : System.Web.UI.Page
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
        loadChannels();
        loadProducts();
        loadBookReqNumbers();
    }


    private void loadChannels()
    {
        ddlChannel.Items.Clear();
        ddlChannel.Items.Add(new ListItem("--Select One--", "0"));
        ddlChannel.Items.Add(new ListItem("Branch", "Branch"));
        ddlChannel.Items.Add(new ListItem("Broker", "Broker"));
        ddlChannel.Items.Add(new ListItem("Financial Company", "Financial Company"));

        ddlChannel.ClearSelection();
        ddlChannel.Items.FindByValue("Branch").Selected = true;
        txtChannelCode.Enabled = false;

    }


    private void loadProducts()
    {
        ddlProduct.Items.Clear();
        ddlProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlProduct.Items.Add(new ListItem("Non-Takaful", "Non-Takaful"));
        ddlProduct.Items.Add(new ListItem("Takaful", "Takaful"));



        ddlSearchProduct.Items.Clear();
        ddlSearchProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlSearchProduct.Items.Add(new ListItem("Non-Takaful", "Non-Takaful"));
        ddlSearchProduct.Items.Add(new ListItem("Takaful", "Takaful"));

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

    private void loadBookReqNumbers()
    {
        ddlBookReqNo.Items.Clear();
        ddlBookReqNo.Items.Add(new ListItem("--- Select One ---", "0"));


        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        string BOOK_REQ_APPROVED_BY_HDO = System.Configuration.ConfigurationManager.AppSettings["BOOK_REQ_APPROVED_BY_HDO"].ToString();


        selectQuery = "select t.REQUEST_NO from MNBQ_WF_CVR_NOTE_BOOK_REQ t WHERE t.STATUS='" + BOOK_REQ_APPROVED_BY_HDO + "'  AND t.REQUEST_NO NOT IN (SELECT CVR_NOTE_REQUEST_NO FROM MNBQ_WF_BOOK_MGR)  order by t.REQUEST_NO ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlBookReqNo.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));


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

        if ((txtSearchBookNumber.Text == "") && (txtSearchChannel.Text == "") && (ddlSearchBranch.SelectedValue.ToString() == "0") && (ddlSearchProduct.SelectedValue.ToString() == "0"))
        {
            lblError.Text = "Search text cannot be blank";
            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchBookNumber.Text != "")
        {

            SQL = "(LOWER(T.BOOK_NUMBER) LIKE '%" + txtSearchBookNumber.Text.ToLower() + "%') AND";
        }

        if (txtSearchChannel.Text != "")
        {

            SQL = SQL + "(LOWER(T.CHANNEL_CODE) LIKE '%" + txtSearchChannel.Text.ToLower() + "%') AND";
        }

        if (ddlSearchBranch.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.BRANCH_CODE = '" + ddlSearchBranch.SelectedValue.ToString() + "') AND";
        }

        if (ddlSearchProduct.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.PRODUCT = '" + ddlSearchProduct.SelectedValue.ToString() + "') AND";
        }


        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = "  SELECT    " +
                  " T.BOOK_ID  , " +
                    " T.BOOK_NUMBER   AS \"Book Number\" , " +
                    " B.BRANCH_NAME   AS \"Branch\", " +
                    " T.CHANNEL   AS \"Channel\", " +
                    " T.CHANNEL_CODE  AS \"Channel Code\" , " +
                    " T.ISSUE_DATE   AS \"Issued Date\", " +
                    " T.PRODUCT   AS \"Product\", " +
                    " T.REMARKS  AS \"Remarks\" " +
                " FROM     MNBQ_WF_BOOK_MGR T " +
                " INNER JOIN MNBQ_WF_BRANCH B ON T.Branch_Code=B.BRANCH_CODE " +
            " WHERE (" + SQL + ") ORDER BY T.BOOK_ID ASC";

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
        Response.Redirect("BookManager.aspx");
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
        if (txtBookNumber.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Book Number";
            Timer1.Enabled = true;
            return;
        }
        else
        {
            if (Session["Mode"].ToString() == "NEW")
            {
                if (CheckBookNumberlreadyExist(txtBookNumber.Text.Trim()))
                {
                    lblMsg.Text = "Entered Book Number already available";
                    Timer1.Enabled = true;
                    return;
                }
            }
        }

        if (txtIssuedDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Issued Date";
            Timer1.Enabled = true;
            return;
        }
        if (ddlBookReqNo.SelectedValue == "" || ddlBookReqNo.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Book Request No";
            Timer1.Enabled = true;
            return;
        }


        if (ddlBranch.SelectedValue == "" || ddlBranch.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Branch";
            Timer1.Enabled = true;
            return;
        }


        if (ddlProduct.SelectedValue == "" || ddlProduct.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Product";
            Timer1.Enabled = true;
            return;
        }




        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            if (Session["Mode"].ToString() == "NEW")
            {
                spProcess = new OracleCommand("INSERT_MNBQ_WF_BOOK_MGR");
            }
            else if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess = new OracleCommand("UPDATE_MNBQ_WF_BOOK_MGR");
            }


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess.Parameters.Add("V_BOOK_ID", OracleType.Number).Value = Convert.ToInt32(txtBookMGRId.Text);
            }

            spProcess.Parameters.Add("V_CVR_NOTE_REQUEST_NO", OracleType.VarChar).Value = ddlBookReqNo.SelectedValue;
            spProcess.Parameters.Add("V_BOOK_NUMBER", OracleType.VarChar).Value = txtBookNumber.Text.Trim();


            spProcess.Parameters.Add("V_CVR_NOTE_RSERIAL_NO_START", OracleType.VarChar).Value = txtCoverNoteSerialNumberStart.Text.Trim();
            spProcess.Parameters.Add("V_CVR_NOTE_RSERIAL_NO_END", OracleType.VarChar).Value = txtCoverNoteSerialNumberEnd.Text.Trim();

            spProcess.Parameters.Add("V_BRANCH_CODE", OracleType.VarChar).Value = ddlBranch.SelectedValue;
            spProcess.Parameters.Add("V_CHANNEL", OracleType.VarChar).Value = ddlChannel.SelectedValue;
            spProcess.Parameters.Add("V_CHANNEL_CODE", OracleType.VarChar).Value = txtChannelCode.Text.Trim();
            spProcess.Parameters.Add("V_ISSUE_DATE", OracleType.DateTime).Value = txtIssuedDate.Text;
            spProcess.Parameters.Add("V_PRODUCT", OracleType.VarChar).Value = ddlProduct.SelectedValue;
            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtRemarks.Text;

            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;




            spProcess.ExecuteNonQuery();
            conProcess.Close();


            sendNotifyMailToBookRequester(ddlBookReqNo.SelectedValue, txtBookNumber.Text.Trim());


            ClearComponents();
            SearchData();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Book Successfully Issued');", true);

            initializeValues();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;



            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Error While Issueing Book');", true);
        }

    }


    private void loadBookDetails(string bookId)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT 	" +
                      " T.BOOK_ID  , " +//0
                       " T.CVR_NOTE_REQUEST_NO  , " +//1
                      " T.BOOK_NUMBER   , " +//2
                      " T.CVR_NOTE_RSERIAL_NO_START  , " +//3
                      " T.CVR_NOTE_RSERIAL_NO_END   , " +//4
                      " T.BRANCH_CODE   , " +//5
                      " T.CHANNEL   , " +//6
                      " T.CHANNEL_CODE  , " +//7
                      " T.ISSUE_DATE  , " +//8
                      " T.PRODUCT  , " +//9
                      " T.REMARKS   " +//10
                    " FROM MNBQ_WF_BOOK_MGR T " +
                    " WHERE T.BOOK_ID=" + bookId + "";






        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            txtBookMGRId.Text = dr[0].ToString();



            ddlBookReqNo.Items.Add(new ListItem(dr[1].ToString(), dr[1].ToString()));
            ddlBookReqNo.SelectedValue = dr[1].ToString();




            txtBookNumber.Text = dr[2].ToString();

            txtCoverNoteSerialNumberStart.Text = dr[3].ToString();
            txtCoverNoteSerialNumberEnd.Text = dr[4].ToString();

            ddlBranch.SelectedValue = dr[5].ToString();

            ddlChannel.SelectedValue = dr[6].ToString();
            txtChannelCode.Text = dr[7].ToString();
            txtIssuedDate.Text = dr[8].ToString().Remove(10);
            ddlProduct.SelectedValue = dr[9].ToString();
            txtRemarks.Text = dr[10].ToString();


            if (txtCoverNoteSerialNumberStart.Text != "" && txtCoverNoteSerialNumberEnd.Text != "")
            {
                int diff = (Convert.ToInt32(txtCoverNoteSerialNumberEnd.Text) - Convert.ToInt32(txtCoverNoteSerialNumberStart.Text)) + 1;

                txtNoOfCopies.Text = diff.ToString();
            }



            string channel = "";

            channel = ddlChannel.SelectedValue.ToString();
            if (channel != "Branch")
            {
                txtChannelCode.Enabled = true;

            }
            else
            {
                txtChannelCode.Enabled = false;

            }


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

    private void ClearComponents()
    {
        txtBookMGRId.Text = "";
        ddlBookReqNo.SelectedValue = "0";
        txtBookNumber.Text = "";
        txtCoverNoteSerialNumberStart.Text = "";
        txtCoverNoteSerialNumberEnd.Text = "";
        txtNoOfCopies.Text = "";
        ddlBranch.SelectedValue = "0";
        ddlChannel.SelectedValue = "0";
        txtChannelCode.Text = "";
        txtIssuedDate.Text = "";
        ddlProduct.SelectedValue = "0";
        txtRemarks.Text = "";

        txtBookMGRId.Enabled = false;
        ddlBookReqNo.Enabled = false;
        txtBookNumber.Enabled = false;
        txtCoverNoteSerialNumberStart.Enabled = false;
        txtCoverNoteSerialNumberEnd.Enabled = false;
        txtNoOfCopies.Enabled = false;
        ddlBranch.Enabled = false;
        ddlChannel.Enabled = false;
        txtChannelCode.Enabled = false;
        txtIssuedDate.Enabled = false;
        ddlProduct.Enabled = false;
        txtRemarks.Enabled = false;

        btnAddNew.Enabled = true;
        btnAlter.Enabled = false;
        btnSave.Enabled = false;
        // btnCancel.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtBookMGRId.Enabled = true;
        ddlBookReqNo.Enabled = true;
        txtBookNumber.Enabled = true;
        txtCoverNoteSerialNumberStart.Enabled = true;
        txtCoverNoteSerialNumberEnd.Enabled = true;
        txtNoOfCopies.Enabled = true;
        ddlBranch.Enabled = false;
        ddlChannel.Enabled = true;
        txtChannelCode.Enabled = true;
        txtIssuedDate.Enabled = true;
        ddlProduct.Enabled = true;
        txtRemarks.Enabled = true;


        txtBookMGRId.Text = "";
        ddlBookReqNo.SelectedValue = "0";
        txtBookNumber.Text = "";
        txtCoverNoteSerialNumberStart.Text = "";
        txtCoverNoteSerialNumberEnd.Text = "";
        txtNoOfCopies.Text = "";
        ddlBranch.SelectedValue = "0";
        ddlChannel.SelectedValue = "0";
        txtChannelCode.Text = "";
        txtIssuedDate.Text = "";
        ddlProduct.SelectedValue = "0";
        txtRemarks.Text = "";

        btnSave.Enabled = true;

        ddlChannel.ClearSelection();
        ddlChannel.Items.FindByValue("Branch").Selected = true;

        txtChannelCode.Enabled = false;



        Session["Mode"] = "NEW";
    }

    protected void btnAlter_Click(object sender, EventArgs e)
    {
        if (txtBookMGRId.Text == "")
        {
            lblMsg.Text = "Please Select a Book";
            Timer1.Enabled = true;
            return;
        }

        txtBookMGRId.Enabled = true;
        ddlBookReqNo.Enabled = true;
        txtBookNumber.Enabled = true;
        txtCoverNoteSerialNumberStart.Enabled = true;
        txtCoverNoteSerialNumberEnd.Enabled = true;
        txtNoOfCopies.Enabled = true;
        ddlBranch.Enabled = false;
        ddlChannel.Enabled = true;
        txtChannelCode.Enabled = true;
        txtIssuedDate.Enabled = true;
        ddlProduct.Enabled = true;
        txtRemarks.Enabled = true;

        string channel = "";

        channel = ddlChannel.SelectedValue.ToString();
        if (channel != "Branch")
        {
            txtChannelCode.Enabled = true;

        }
        else
        {
            txtChannelCode.Enabled = false;

        }

        btnSave.Enabled = true;

        Session["Mode"] = "UPDATE";
    }

    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearComponents();
        txtBookMGRId.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();


        loadBookDetails(grdSearchResults.SelectedRow.Cells[1].Text.Trim());

        btnAlter.Enabled = true;
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

    protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string channel = "";

        channel = ddlChannel.SelectedValue.ToString();
        if (channel != "Branch")
        {
            txtChannelCode.Enabled = true;

        }
        else
        {
            txtChannelCode.Enabled = false;

        }

    }

    protected void ddlBookReqNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBookReqNo.SelectedValue.ToString() == "0")
        {
            return;
        }


        string selectedBookReqNo = "";

        selectedBookReqNo = ddlBookReqNo.SelectedValue.ToString();

        string requestedBranch = "";
        requestedBranch = loadRequestedBranch(selectedBookReqNo);

        ddlBranch.ClearSelection();
        ddlBranch.Items.FindByValue(requestedBranch).Selected = true;
    }

    private string loadRequestedBranch(string requestNo)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT BRANCH_CODE FROM MNBQ_WF_CVR_NOTE_BOOK_REQ WHERE REQUEST_NO='" + requestNo + "'";

    


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


    private void sendNotifyMailToBookRequester(string requestNo,string coverNoteBookNo)
    {


        CommonMail mail = new CommonMail();

        mail.From_address = "mnb.workflow@hnbgeneral.com";

        string requestedUser = "";
        requestedUser = getRequestedUser(requestNo);
        mail.To_address = getEmailOfUser(requestedUser);




        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";



        mail.Subject = "Cover Note Book Issued";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>Cover Note Book Requested has been issued</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   "The manual cover note book request made under request number " + requestNo + " is completed and the cover note book number, " + coverNoteBookNo + " has been issued now." +
                  "</td>" +
                   "</tr>" +

                    "<tr>" +
                   "</tr>" +

                     "<tr>" +
                   "<td>" +
                   "Please confirm upon receiving." +
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

}
