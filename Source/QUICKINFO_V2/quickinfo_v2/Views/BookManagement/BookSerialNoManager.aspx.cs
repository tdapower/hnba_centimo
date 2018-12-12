
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

public partial class BookSerialNoManager : System.Web.UI.Page
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

        loadBookNumbers();


       // loadSerialNumbers();

        loadStatuses();
    }


    private void loadSerialNumbers(int serialNoStartVal)
    {
        ddlSerialNumber.Items.Clear();
        ddlSerialNumber.Items.Add(new ListItem("-- Select One --", "0"));

        //ddlSearchSerialNumber.Items.Clear();
        //ddlSearchSerialNumber.Items.Add(new ListItem("-- Select One --", "0"));

        int serialNoEndVal = serialNoStartVal + 49;

        for (int i = serialNoStartVal; i <= serialNoEndVal; i++)
        {
            ddlSerialNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //ddlSearchSerialNumber.Items.Add(new ListItem(i.ToString(), i.ToString()));

        }



    }


    private void loadStatuses()
    {
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("-- Select One --", "0"));
        ddlStatus.Items.Add(new ListItem("Cancelled", "Cancelled"));
        ddlStatus.Items.Add(new ListItem("Other", "Other"));

        ddlSearchStatus.Items.Clear();
        ddlSearchStatus.Items.Add(new ListItem("-- Select One --", "0"));
        ddlSearchStatus.Items.Add(new ListItem("Cancelled", "Cancelled"));
        ddlSearchStatus.Items.Add(new ListItem("Other", "Other"));



    }


    private void loadBookNumbers()
    {
        ddlBookNumbers.Items.Clear();
        ddlBookNumbers.Items.Add(new ListItem("--- Select One ---", "0"));



        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "select t.BOOK_ID,t.BOOK_NUMBER from MNBQ_WF_BOOK_MGR t order by t.BOOK_NUMBER ";
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlBookNumbers.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));



            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }


    private void loadSerialNumbersOfBookId(string bookId)
    {
 
        ddlSerialNumber.Items.Clear();
        ddlSerialNumber.Items.Add(new ListItem("-- Select One --", "0"));


        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        String selectQuery = "";
        selectQuery = "select t.SERIAL_NUMBER,t.SEQ_NO from MNBQ_WF_BOOK_SR_NO_MGR t WHERE BOOK_ID=:V_BOOK_ID  order by t.SERIAL_NUMBER";



        OracleCommand cmd = new OracleCommand(selectQuery, con);
        cmd.Parameters.Add(new OracleParameter("V_BOOK_ID", bookId));

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlSerialNumber.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));



            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }


    private void loadBranchOfSelectedBook(string bookId)
    {

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT   " +
                    " B.BRANCH_NAME " +
                    " FROM     MNBQ_WF_BOOK_MGR T " +
                    " INNER JOIN MNBQ_WF_BRANCH B ON T.Branch_Code=B.BRANCH_CODE " +
                    " WHERE T.BOOK_ID=" + bookId;
        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {

                txtBranch.Text = dr[0].ToString();

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }

    private string loadSerialNumbersOfSelectedBook(string bookId)
    {
        string returnVal = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;
        con.Open();
        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT   " +
                    " T.CVR_NOTE_RSERIAL_NO_START " +
                    " FROM     MNBQ_WF_BOOK_MGR T " +
                    " WHERE T.BOOK_ID=" + bookId;
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

        if ((txtSearchBookNumber.Text == "") && (ddlSearchSerialNumber.SelectedValue.ToString() == "0") && (ddlSearchStatus.SelectedValue.ToString() == "0"))
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

            SQL = "(LOWER(B.BOOK_NUMBER) LIKE '%" + txtSearchBookNumber.Text.ToLower() + "%') AND";
        }



        if (ddlSearchSerialNumber.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.SERIAL_NUMBER = '" + ddlSearchSerialNumber.SelectedValue.ToString() + "') AND";
        }

        if (ddlSearchStatus.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.STATUS = '" + ddlSearchStatus.SelectedValue.ToString() + "') AND";
        }


        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = "  SELECT   " +
                     " T.SEQ_NO     , " +
                    " B.BOOK_NUMBER AS \"Book Number\", " +
                    " T.SERIAL_NUMBER  AS \"Serial Number\"  , " +
                    " T.STATUS  AS \"Status\"  " +
                    " FROM MNBQ_WF_BOOK_SR_NO_MGR T " +
                    " INNER JOIN MNBQ_WF_BOOK_MGR B ON T.BOOK_ID=B.BOOK_ID " +
                            " WHERE (" + SQL + ") ORDER BY B.BOOK_NUMBER ASC";

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
        Response.Redirect("BookSerialNoManager.aspx");
    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearComponents();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBookNumbers.SelectedValue.ToString() == "0")
        {
            lblError.Text = "Please select Book Number";
            return;
        }

        if (ddlSerialNumber.SelectedValue.ToString() == "0")
        {
            lblError.Text = "Please select Serial Number";
            return;
        }

        if (ddlStatus.SelectedValue.ToString() == "0")
        {
            lblError.Text = "Please select Status";
            return;
        }





        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            //if (Session["Mode"].ToString() == "NEW")
            //{
            //    spProcess = new OracleCommand("INSERT_MNBQ_WF_BOOK_SR_NO_MGR");
            //}
            //else if (Session["Mode"].ToString() == "UPDATE")
            //{
            //    spProcess = new OracleCommand("UPDATE_MNBQ_WF_BOOK_SR_NO_MGR");
            //}

            spProcess = new OracleCommand("UPDATE_MNBQ_WF_BOOK_SR_NO_MGR");


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            //if (Session["Mode"].ToString() == "UPDATE")
            //{
            //    spProcess.Parameters.Add("V_SEQ_NO", OracleType.Number).Value = Convert.ToInt32(txtSeqId.Text);
            //}
            spProcess.Parameters.Add("V_SEQ_NO", OracleType.Number).Value = Convert.ToInt32(txtSeqId.Text);

            spProcess.Parameters.Add("V_BOOK_ID", OracleType.Number).Value = ddlBookNumbers.SelectedValue;
            spProcess.Parameters.Add("V_SERIAL_NUMBER", OracleType.VarChar).Value = ddlSerialNumber.SelectedItem.ToString();
            spProcess.Parameters.Add("V_STATUS", OracleType.VarChar).Value = ddlStatus.SelectedValue;
            spProcess.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = txtRemarks.Text;

            //if (Session["Mode"].ToString() == "NEW")
            //{

            //    spProcess.Parameters.Add("V_NEW_SEQ_NO", OracleType.Number).Direction = ParameterDirection.Output;
            //    spProcess.Parameters["V_NEW_SEQ_NO"].Direction = ParameterDirection.Output;
            //}


            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = UserCode;



            spProcess.ExecuteNonQuery();

            //string newBookSeqId = "";
            //if (Session["Mode"].ToString() == "NEW")
            //{

            //    newBookSeqId = Convert.ToString(spProcess.Parameters["V_NEW_SEQ_NO"].Value);
            //}
            //else
            //{
            //    newBookSeqId = txtSeqId.Text;
            //}

            conProcess.Close();


            updateBookSerialIdOfUploadedDocs(Session["TempId"].ToString(), (txtSeqId.Text));


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


    private void ClearComponents()
    {
        txtSeqId.Text = "";
        ddlBookNumbers.SelectedValue = "0";
        txtBranch.Text = "";
        ddlSerialNumber.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        txtRemarks.Text = "";

        txtSeqId.Enabled = false;
        ddlBookNumbers.Enabled = false;
        ddlSerialNumber.Enabled = false;
        ddlStatus.Enabled = false;
        txtRemarks.Enabled = false;

        btnAddNew.Enabled = true;
        btnAlter.Enabled = false;
        btnSave.Enabled = false;
        // btnCancel.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtSeqId.Enabled = true;
        ddlBookNumbers.Enabled = true;
        txtBranch.Enabled = false;
        ddlSerialNumber.Enabled = true;
        ddlStatus.Enabled = true;
        txtRemarks.Enabled = true;


        txtSeqId.Text = "";
        ddlBookNumbers.SelectedValue = "0";
        txtBranch.Text = "";
        ddlSerialNumber.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        txtRemarks.Text = "";

        btnSave.Enabled = true;

        Session["Mode"] = "NEW";


        Guid tempId = Guid.NewGuid();
        Session["TempId"] = tempId;

        (this.Panel2.FindControl("Iframe1") as HtmlControl).Attributes.Add("src", "DocUpload/DocumentList.aspx?TempId=" + Session["TempId"].ToString());

    }


    private void updateBookSerialIdOfUploadedDocs(string tempId, string SerialId)
    {


        try
        {


            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            string strQuery = "";



            strQuery = "UPDATE  MNBQ_WF_BOOK_SR_DOCS SET BOOK_SR_SEQ_NO=" + SerialId + " ";
            strQuery += "WHERE TEMP_ID='" + tempId + "'";



            spProcess = new OracleCommand(strQuery, conProcess);


            spProcess.ExecuteNonQuery();
            conProcess.Close();
            conProcess.Dispose();
        }
        catch (Exception ex)
        {

        }


    }


    protected void btnAlter_Click(object sender, EventArgs e)
    {
        if (txtSeqId.Text == "")
        {
            lblMsg.Text = "Please Select a Book Serial Number";
            Timer1.Enabled = true;
            return;
        }

        // txtSeqId.Enabled = true;
        ddlBookNumbers.Enabled = true;
        txtBranch.Enabled = false;
        ddlSerialNumber.Enabled = true;
        ddlStatus.Enabled = true;
        txtRemarks.Enabled = true;



        btnSave.Enabled = true;

        Session["Mode"] = "UPDATE";

        Guid tempId = Guid.NewGuid();
        Session["TempId"] = tempId;
    }

    protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
    {

        ClearComponents();
        txtSeqId.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();

        loadBookDetails(grdSearchResults.SelectedRow.Cells[1].Text.Trim());
        (this.Panel2.FindControl("Iframe1") as HtmlControl).Attributes.Add("src", "DocUpload/DocumentList.aspx?BookSerialSeqId=" + grdSearchResults.SelectedRow.Cells[1].Text.Trim());


        btnAlter.Enabled = true;
    }

    protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
    }


    private void loadBookDetails(string seqId)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT 	" +
                            " T.SEQ_NO ,  " +//0
                          " T.BOOK_ID ,    " +//1
                          " T.SERIAL_NUMBER,     " +//2
                          " T.STATUS ,  " +//3
                          " T.REMARKS    " +//4
                    " FROM MNBQ_WF_BOOK_SR_NO_MGR T " +
                    " WHERE T.SEQ_NO=" + seqId + "";






        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();



            txtSeqId.Text = dr[0].ToString();
            ddlBookNumbers.SelectedValue = dr[1].ToString();
            loadBranchOfSelectedBook(dr[1].ToString());
            ddlSerialNumber.SelectedValue = dr[2].ToString();
            ddlStatus.SelectedValue = dr[3].ToString();
            txtRemarks.Text = dr[4].ToString();
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }





    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }

    protected void ddlBookNumbers_SelectedIndexChanged(object sender, EventArgs e)
    {

        string bookId = "";

        bookId = ddlBookNumbers.SelectedValue.ToString();

        loadBranchOfSelectedBook(bookId);


        //string serialNoStart = "";
        //serialNoStart = loadSerialNumbersOfSelectedBook(bookId);
        //loadSerialNumbers(Convert.ToInt32(serialNoStart));

        loadSerialNumbersOfBookId(bookId);


    }

    protected void ddlSerialNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        string seqNo = "";

        seqNo = ddlSerialNumber.SelectedValue.ToString();

        txtSeqId.Text = seqNo;

    }
}
