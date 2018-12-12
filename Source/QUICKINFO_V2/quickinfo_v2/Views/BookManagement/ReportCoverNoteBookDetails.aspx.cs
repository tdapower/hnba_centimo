
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

public partial class ReportCoverNoteBookDetails : System.Web.UI.Page
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

        loadBookNumbers();
        loadSerialNumbers();
        loadBranches();
        loadChannels();

    }
    private void loadBookNumbers()
    {

        ddlSearchBookNumber.Items.Clear();
        ddlSearchBookNumber.Items.Add(new ListItem("--- Select One ---", "0"));

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.BOOK_NUMBER from MNBQ_WF_BOOK_MGR t order by t.BOOK_NUMBER ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                ddlSearchBookNumber.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }

    private void loadSerialNumbers()
    {

        ddlSearchSerialNumber.Items.Clear();
        ddlSearchSerialNumber.Items.Add(new ListItem("--- Select One ---", "0"));

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.serial_number from MNBQ_WF_BOOK_SR_NO_MGR t order by t.serial_number ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                ddlSearchSerialNumber.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
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

    private void loadChannels()
    {

        ddlSearchChannel.Items.Clear();
        ddlSearchChannel.Items.Add(new ListItem("--- Select One ---", "0"));

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.CHANNEL_CODE from MNBQ_WF_BOOK_MGR t order by t.CHANNEL_CODE ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                ddlSearchChannel.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));

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

        if ((txtSearchIssuedDateFrom.Text == "") && (txtSearchIssuedDateTo.Text == "") && (ddlSearchBookNumber.SelectedValue.ToString() == "0") && (ddlSearchBranch.SelectedValue.ToString() == "0") && (ddlSearchChannel.SelectedValue.ToString() == "0"))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Search text cannot be blank');", true);

            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;

        if (txtSearchIssuedDateFrom.Text != "")
        {
            SQL = "(to_date(c.pol_start_date,'DD/MM/RRRR') >=  to_date('" + txtSearchIssuedDateFrom.Text.ToLower() + "','DD/MM/RRRR') ) AND";
        }

        if (txtSearchIssuedDateTo.Text != "")
        {
            SQL = "(to_date(c.pol_start_date,'DD/MM/RRRR') <=  to_date('" + txtSearchIssuedDateTo.Text.ToLower() + "','DD/MM/RRRR') ) AND";
        }






        if (ddlSearchSerialNumber.SelectedValue.ToString() != "0")
        {

            SQL = "(LOWER(bm.serial_number ) = '" + ddlSearchSerialNumber.SelectedValue.ToString() + "') AND";
        }

   

        if (ddlSearchBookNumber.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(b.BOOK_NUMBER = '" + ddlSearchBookNumber.SelectedValue.ToString() + "') AND";
        }

        if (ddlSearchBranch.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(b.BRANCH_CODE = '" + ddlSearchBranch.SelectedValue.ToString() + "') AND";
        }

        if (ddlSearchChannel.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(b.CHANNEL_CODE = '" + ddlSearchChannel.SelectedValue.ToString() + "') AND";
        }




        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = " select " +
                    " bb.Branch_Name as \"Branch\"  ,  " +
                    " b.book_number as \"Book No.\", " +
                    " b.issue_date AS \"Book Issued Date\", " +
                    " bm.serial_number as \"Serial No.\", " +
                    " c.pol_start_date as \"Cover Note Issued Date\", " +
                    " f.pfp_last_updated_date as \"Policy Issued Date\", " +
                    " c.pol_no as \"Policy No.\", " +
                    " c.pol_status as \"Status\" " +
                     " from MNBQ_WF_BOOK_MGR b " +
                     " inner join MNBQ_WF_BOOK_SR_NO_MGR bm on b.book_id=bm.book_id " +
                     " inner join t_policy_property t on t.pop_value_description=b.book_number " +
                    " inner join crc_policy c on to_char(t.pop_pol_policy_id)=c.pol_id " +
                    " INNER JOIN MNBQ_WF_BRANCH bb ON b.Branch_Code=bb.BRANCH_CODE   " +
                    " inner join t_policy_event_followup f on  to_char(f.pfp_pol_policy_id)=to_char(t.pop_pol_policy_id)  and f.pfp_event_code='ISSUE-POL' " +
            " WHERE (" + SQL + ") ";

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
