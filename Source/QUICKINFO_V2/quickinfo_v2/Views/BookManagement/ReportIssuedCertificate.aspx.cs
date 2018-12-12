
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

public partial class ReportIssuedCertificate : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();


            initializeValues();

            Session.Remove("Mode");

            pnlUserGrid.Visible = false;
        }


    }
    private void initializeValues()
    {
        loadBranches();
        loadProducts();

    }
    private void loadProducts()
    {
        ddlSearchProduct.Items.Clear();
        ddlSearchProduct.Items.Add(new ListItem("--Select One--", "0"));
        ddlSearchProduct.Items.Add(new ListItem("Non-Takaful", "Non-Takaful"));
        ddlSearchProduct.Items.Add(new ListItem("Takaful", "Takaful"));

    }
    private void loadBranches()
    {

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
    }

    private void SearchData()
    {
        string SQL = "";
        grdSearchResults.DataSource = null;
        grdSearchResults.DataBind();

        if ((txtSearchPrintedDateFrom.Text == "") && (txtSearchPrintedDateTo.Text == "") && (ddlSearchBranch.SelectedValue.ToString() == "0") && (ddlSearchProduct.SelectedValue.ToString() == "0"))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Search text cannot be blank');", true);

            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchPrintedDateFrom.Text != "")
        {
            SQL = "(to_date(t.PRINTED_DATE,'DD/MM/RRRR') >=  to_date('" + txtSearchPrintedDateFrom.Text.ToLower() + "','DD/MM/RRRR') ) AND";
        }

        if (txtSearchPrintedDateTo.Text != "")
        {
            SQL = "(to_date(t.PRINTED_DATE,'DD/MM/RRRR') <=  to_date('" + txtSearchPrintedDateTo.Text.ToLower() + "','DD/MM/RRRR') ) AND";
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
              selectQuery = "SELECT " +
                           " B.Branch_Name as \"Branch\"  , " +
                          " T.CERT_SERIAL_NUMBER    AS  \"Certificate Serial Number \" , " +
                           " T.TCS_POLICY_NUMBER    AS  \"Policy Number\" , " +
                          " T.PRINTED_DATE  AS  \"Printed Date\" , " +
                          " T.STATUS   AS  \"Status\"   " +
                           " FROM MNBQ_WF_CERTIFICATE_MGR T " +
                              " INNER JOIN MNBQ_WF_BRANCH B ON T.Branch_Code=B.BRANCH_CODE  " +
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
        Response.Redirect("ReportCoverNoteBookDetails.aspx");
    }




}
