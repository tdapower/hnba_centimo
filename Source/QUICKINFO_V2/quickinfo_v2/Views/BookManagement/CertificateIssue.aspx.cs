
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

public partial class CertificateIssue : System.Web.UI.Page
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
        loadProducts();
    }



    private void loadProducts()
    {
        ddlProduct.Items.Clear();
        ddlProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlProduct.Items.Add(new ListItem("Motor Guard", "Motor Guard"));
        ddlProduct.Items.Add(new ListItem("Motor Guard - Extra", "Motor Guard - Extra"));
        ddlProduct.Items.Add(new ListItem("Takaful", "Takaful"));



        ddlSearchProduct.Items.Clear();
        ddlSearchProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlSearchProduct.Items.Add(new ListItem("Motor Guard", "Motor Guard"));
        ddlSearchProduct.Items.Add(new ListItem("Motor Guard - Extra", "Motor Guard - Extra"));
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

        if ((txtSearchCertificateNumber.Text == "") && (ddlSearchBranch.SelectedValue.ToString() == "0") && (ddlSearchProduct.SelectedValue.ToString() == "0"))
        {
            lblError.Text = "Search text cannot be blank";
            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchCertificateNumber.Text != "")
        {

            SQL = "(LOWER(T.SERIAL_NUMBER_START) LIKE '%" + txtSearchCertificateNumber.Text.ToLower() + "%') AND";
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
        selectQuery = "  SELECT  " +
                          " SEQ_NO     , " +
                          " SERIAL_NUMBER_START  as \"Start\"  , " +
                          " SERIAL_NUMBER_END   as \"End\", " +
                          " B.Branch_Name as \"Branch\"  , " +
                          " ISSUE_DATE  as \"Issued Date\" , " +
                          " PRODUCT   as \"Product\" " +
                    " FROM MNBQ_WF_CERT_ISSUE T " +
                     " INNER JOIN MNBQ_WF_BRANCH B ON T.Branch_Code=B.BRANCH_CODE  " +
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
        if (txtCertificateSerialNumberStart.Text.Trim() == "" || txtCertificateSerialNumberEnd.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Certificate Serial Number Start & End";
            Timer1.Enabled = true;
            return;
        }


        if (txtIssuedDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Issued Date";
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
                spProcess = new OracleCommand("INSERT_MNBQ_WF_CERT_ISSUE");
            }
            else if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess = new OracleCommand("UPDATE_MNBQ_WF_CERT_ISSUE");
            }


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess.Parameters.Add("V_SEQ_NO", OracleType.Number).Value = Convert.ToInt32(txtCertificateIssueSeqNo.Text);
            }


            spProcess.Parameters.Add("V_SERIAL_NUMBER_START", OracleType.VarChar).Value = txtCertificateSerialNumberStart.Text.Trim();
            spProcess.Parameters.Add("V_SERIAL_NUMBER_END", OracleType.VarChar).Value = txtCertificateSerialNumberEnd.Text.Trim();
            spProcess.Parameters.Add("V_BRANCH_CODE", OracleType.VarChar).Value = ddlBranch.SelectedValue;
            spProcess.Parameters.Add("V_ISSUE_DATE", OracleType.DateTime).Value = txtIssuedDate.Text;
            spProcess.Parameters.Add("V_PRODUCT", OracleType.VarChar).Value = ddlProduct.SelectedValue;

            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;








            spProcess.ExecuteNonQuery();
            conProcess.Close();

            ClearComponents();
            SearchData();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;
        }

    }


    private void loadCertificateIssueDetails(string seqId)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT 	" +
                      " T.SEQ_NO  , " +//0
                      " T.SERIAL_NUMBER_START   , " +//1
                      " T.SERIAL_NUMBER_END   , " +//2
                      " T.BRANCH_CODE   , " +//3
                      " T.ISSUE_DATE  , " +//4
                      " T.PRODUCT  " +//5
                    " FROM MNBQ_WF_CERT_ISSUE T " +
                    " WHERE T.SEQ_NO=" + seqId + "";






        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            txtCertificateIssueSeqNo.Text = dr[0].ToString();
            txtCertificateSerialNumberStart.Text = dr[1].ToString();
            txtCertificateSerialNumberEnd.Text = dr[2].ToString();
            ddlBranch.SelectedValue = dr[3].ToString();
            txtIssuedDate.Text = dr[4].ToString().Remove(10);
            ddlProduct.SelectedValue = dr[5].ToString();

            if (txtCertificateSerialNumberStart.Text != "" && txtCertificateSerialNumberEnd.Text != "")
            {
                int diff = (Convert.ToInt32(txtCertificateSerialNumberEnd.Text) - Convert.ToInt32(txtCertificateSerialNumberStart.Text)) + 1;

                txtNoOfCopies.Text = diff.ToString();
            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }


    private void ClearComponents()
    {
        txtCertificateIssueSeqNo.Text = "";
        txtCertificateSerialNumberStart.Text = "";
        txtCertificateSerialNumberEnd.Text = "";
        ddlBranch.SelectedValue = "0";
        txtIssuedDate.Text = "";
        ddlProduct.SelectedValue = "0";

        txtCertificateIssueSeqNo.Enabled = false;
        txtCertificateSerialNumberStart.Enabled = false;
        txtCertificateSerialNumberEnd.Enabled = false;
        ddlBranch.Enabled = false;
        txtIssuedDate.Enabled = false;
        ddlProduct.Enabled = false;

        btnAddNew.Enabled = true;
        btnAlter.Enabled = false;
        btnSave.Enabled = false;
        // btnCancel.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtCertificateIssueSeqNo.Enabled = true;
        txtCertificateSerialNumberStart.Enabled = true;
        txtCertificateSerialNumberEnd.Enabled = true;
        ddlBranch.Enabled = true;
        txtIssuedDate.Enabled = true;
        ddlProduct.Enabled = true;



        txtCertificateIssueSeqNo.Text = "";
        txtCertificateSerialNumberStart.Text = "";
        txtCertificateSerialNumberEnd.Text = "";
        ddlBranch.SelectedValue = "0";
        txtIssuedDate.Text = "";
        ddlProduct.SelectedValue = "0";

        btnSave.Enabled = true;


        Session["Mode"] = "NEW";
    }

    protected void btnAlter_Click(object sender, EventArgs e)
    {
        if (txtCertificateIssueSeqNo.Text == "")
        {
            lblMsg.Text = "Please Select a Certificate Issue Record";
            Timer1.Enabled = true;
            return;
        }
        txtCertificateIssueSeqNo.Enabled = true;
        txtCertificateSerialNumberStart.Enabled = true;
        txtCertificateSerialNumberEnd.Enabled = true;
        ddlBranch.Enabled = true;
        txtIssuedDate.Enabled = true;
        ddlProduct.Enabled = true;


        btnSave.Enabled = true;

        Session["Mode"] = "UPDATE";
    }

    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearComponents();
        txtCertificateIssueSeqNo.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();


        loadCertificateIssueDetails(grdSearchResults.SelectedRow.Cells[1].Text.Trim());

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


}
