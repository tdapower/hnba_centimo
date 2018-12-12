
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
using quickinfo_v2.Controllers.TCSPolicy;

public partial class CertificateManager : System.Web.UI.Page
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

        loadProducts();
        loadStatuses();
    }




    private void loadProducts()
    {
        ddlProduct.Items.Clear();
        ddlProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlProduct.Items.Add(new ListItem("Motor Guard", "Motor Guard"));
        ddlProduct.Items.Add(new ListItem("Motor Guard - Extra", "Motor Guard - Extra"));
        ddlProduct.Items.Add(new ListItem("Takaful", "Takaful"));

    }

    private void loadStatuses()
    {
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("-- Select One --", "0"));
        ddlStatus.Items.Add(new ListItem("Issued", "Issued"));
        ddlStatus.Items.Add(new ListItem("Cancelled", "Cancelled"));

        ddlSearchStatus.Items.Clear();
        ddlSearchStatus.Items.Add(new ListItem("-- Select One --", "0"));
        ddlSearchStatus.Items.Add(new ListItem("Issued", "Issued"));
        ddlSearchStatus.Items.Add(new ListItem("Cancelled", "Cancelled"));

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

        if ((txtSearchSerialNumber.Text == "") && (txtSearchPolicyNumber.Text == "") && (ddlSearchStatus.SelectedValue.ToString() == "0"))
        {
            lblError.Text = "Search text cannot be blank";
            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchSerialNumber.Text != "")
        {

            SQL = "(LOWER(T.CERT_SERIAL_NUMBER) LIKE '%" + txtSearchSerialNumber.Text.ToLower() + "%') AND";
        }

        if (txtSearchPolicyNumber.Text != "")
        {

            SQL = SQL + "(LOWER(T.TCS_POLICY_NUMBER) LIKE '%" + txtSearchPolicyNumber.Text.ToLower() + "%') AND";
        }

        if (ddlSearchStatus.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.STATUS = '" + ddlSearchStatus.SelectedValue.ToString() + "') AND";
        }


        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = "SELECT " +
                          " T.SEQ_NO     , " +
                          " T.CERT_SERIAL_NUMBER    AS  \"Certificate Serial Number \" , " +
                          " T.STATUS   AS  \"Status\"  , " +
                          " T.TCS_POLICY_NUMBER    AS  \"Policy Number\" , " +
                          " T.PRODUCT   AS  \"Product\", " +
                          " T.PRINTED_DATE  AS  \"Printed Date\"  " +
                " FROM MNBQ_WF_CERTIFICATE_MGR T " +
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
        Response.Redirect("CertificateManager.aspx");
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearComponents();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCertificateSerialNumber.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Certificate Serial Number";
            Timer1.Enabled = true;
            return;
        }
        else
        {
            if (Session["Mode"].ToString() == "NEW")
            {
                if (CheckCertificateSerialNumberlreadyExist(txtCertificateSerialNumber.Text.Trim()))
                {
                    lblMsg.Text = "Entered Certificate Serial Number already available";
                    Timer1.Enabled = true;
                    return;
                }
            }
        }


        if (ddlStatus.SelectedValue == "" || ddlStatus.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Status";
            Timer1.Enabled = true;
            return;
        }
        else
        {
            if (ddlStatus.SelectedValue == "Cancelled")
            {
                if (txtRemarks.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter reason for cancellation in remarks field";
                    Timer1.Enabled = true;
                    return;
                }
            }
        }


        if (ddlProduct.SelectedValue == "" || ddlProduct.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select Product";
            Timer1.Enabled = true;
            return;
        }


        if (ddlStatus.SelectedValue == "Issued")
        {
            if (txtPolicyNo.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter Policy Number";
                Timer1.Enabled = true;
                return;
            }
            else
            {
                if (!ValidatePolicyNumber((txtPolicyNo.Text)))
                {
                    lblMsg.Text = "Invalid Policy Number";
                    Timer1.Enabled = true;
                    return;
                }
            }
        }



        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            if (Session["Mode"].ToString() == "NEW")
            {
                spProcess = new OracleCommand("INSERT_MNBQ_WF_CERT_MGR");
            }
            else if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess = new OracleCommand("UPDATE_MNBQ_WF_CERT_MGR");
            }


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            if (Session["Mode"].ToString() == "UPDATE")
            {
                spProcess.Parameters.Add("V_SEQ_NO", OracleType.Number).Value = Convert.ToInt32(txtSeqNo.Text);
            }
            spProcess.Parameters.Add("V_CERT_SERIAL_NUMBER", OracleType.VarChar).Value = txtCertificateSerialNumber.Text.Trim();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = ddlStatus.SelectedValue;
            spProcess.Parameters.Add("V_TCS_POLICY_NUMBER", OracleType.VarChar).Value = txtPolicyNo.Text.Trim();
            spProcess.Parameters.Add("V_PRODUCT", OracleType.VarChar).Value = ddlProduct.SelectedValue;
            spProcess.Parameters.Add("V_PRINTED_DATE", OracleType.DateTime).Value = txtPrintedDate.Text;

            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtRemarks.Text;

            string UserCode = "";
            string UserBranch = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
                UserBranch = reqCookies["UserBranch"].ToString();

            }


            spProcess.Parameters.Add("V_BRANCH_CODE", OracleType.VarChar).Value = UserBranch;
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


    private bool ValidatePolicyNumber(string policyNumber)
    {
        bool returnVal = false;

        TCSPolicyController tCSPolicyController = new TCSPolicyController();




        if (tCSPolicyController.checkIsPolicyNoAvailable(policyNumber, "TCS"))
        {
            returnVal = true;
        }



        return returnVal;
    }

    private void loadCertificateDetails(string seqNo)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT  " +
                          " T.SEQ_NO    , " +//0
                          " T.CERT_SERIAL_NUMBER    , " +//1
                          " T.STATUS    , " +//2
                          " T.TCS_POLICY_NUMBER    , " +//3
                          " T.PRODUCT  , " +//4
                          " T.PRINTED_DATE  , " +//5
                          " T.REMARKS  " +//6
                " FROM MNBQ_WF_CERTIFICATE_MGR T " +
                    " WHERE T.SEQ_NO=" + seqNo + "";


        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            txtSeqNo.Text = dr[0].ToString();
            txtCertificateSerialNumber.Text = dr[1].ToString();
            ddlStatus.SelectedValue = dr[2].ToString();
            txtPolicyNo.Text = dr[3].ToString();
            ddlProduct.SelectedValue = dr[4].ToString();
            txtPrintedDate.Text = dr[5].ToString().Remove(10);
            txtRemarks.Text = dr[6].ToString();


        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }

    private bool CheckCertificateSerialNumberlreadyExist(string serialNumber)
    {
        bool returnVal = false;
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT CERT_SERIAL_NUMBER FROM MNBQ_WF_CERTIFICATE_MGR WHERE CERT_SERIAL_NUMBER='" + serialNumber + "'";

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
        txtSeqNo.Text = "";
        txtCertificateSerialNumber.Text = "";
        ddlStatus.SelectedValue = "0";
        txtPolicyNo.Text = "";
        ddlProduct.SelectedValue = "0";
        txtPrintedDate.Text = "";
        txtRemarks.Text = "";


        txtSeqNo.Enabled = false;
        txtCertificateSerialNumber.Enabled = false;
        ddlStatus.Enabled = false;
        txtPolicyNo.Enabled = false;
        ddlProduct.Enabled = false;
        txtPrintedDate.Enabled = false;
        txtRemarks.Enabled = false;


        btnAddNew.Enabled = true;
        btnAlter.Enabled = false;
        btnSave.Enabled = false;
        // btnCancel.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtSeqNo.Enabled = true;
        txtCertificateSerialNumber.Enabled = true;
        ddlStatus.Enabled = true;
        txtPolicyNo.Enabled = true;
        ddlProduct.Enabled = true;
        txtPrintedDate.Enabled = true;
        txtRemarks.Enabled = true;



        txtSeqNo.Text = "";
        txtCertificateSerialNumber.Text = "";
        ddlStatus.SelectedValue = "0";
        txtPolicyNo.Text = "";
        ddlProduct.SelectedValue = "0";
        txtPrintedDate.Text = "";
        txtRemarks.Text = "";


        btnSave.Enabled = true;


        Session["Mode"] = "NEW";
    }

    protected void btnAlter_Click(object sender, EventArgs e)
    {
        if (txtSeqNo.Text == "")
        {
            lblMsg.Text = "Please Select a Certificate";
            Timer1.Enabled = true;
            return;
        }

        txtSeqNo.Enabled = true;
        txtCertificateSerialNumber.Enabled = true;
        ddlStatus.Enabled = true;
        txtPolicyNo.Enabled = true;
        ddlProduct.Enabled = true;
        txtPrintedDate.Enabled = true;
        txtRemarks.Enabled = true;



        btnSave.Enabled = true;

        Session["Mode"] = "UPDATE";
    }

    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearComponents();
        txtSeqNo.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();


        loadCertificateDetails(grdSearchResults.SelectedRow.Cells[1].Text.Trim());


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



    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "Issued")
        {
            txtPolicyNo.Enabled = true;
        }
        else
        {
            txtPolicyNo.Enabled = false;
        }

    }
}
