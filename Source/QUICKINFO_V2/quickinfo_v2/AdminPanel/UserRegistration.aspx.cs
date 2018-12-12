//******************************************
// Author            :Tharindu Athapattu
// Date              :11/04/2013
// Reviewed By       :
// Description       : User Registration Form
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
using quickinfo_v2.CommonCLS;

public partial class UserRegistration : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            validatePageAuthentication();

            string InterVal = System.Configuration.ConfigurationManager.AppSettings["MessageClearAfter"].ToString();
            Timer1.Interval = Convert.ToInt32(InterVal);

            txtEPF.Attributes.Add("onkeyup", "jsValidateNum(this)");
            txtSearchEPF.Attributes.Add("onkeyup", "jsValidateNum(this)");

            btnResetPassword.Attributes.Add("onClick", "if(confirm('Are you sure to RESET the user password?','User Admin')){}else{return false}");



            ClearComponents();
            initializeValues();

            Session.Remove("UserRegMode");

            pnlUserGrid.Visible = false;
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
        grdUsers.DataSource = null;
        grdUsers.DataBind();

        if ((txtSearchUserCode.Text == "") && (txtSearchUserName.Text == "") && (txtSearchEPF.Text == "") && (ddlSearchUserRole.SelectedValue.ToString() == "0") && (ddlSearchCompany.SelectedValue.ToString() == "0"))
        {
            lblError.Text = "Search text cannot be blank";
            return;
        }

        OracleConnection myOleDbConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        OracleCommand myOleDbCommand = new OracleCommand();

        myOleDbConnection.Open();

        myOleDbCommand.Connection = myOleDbConnection;


        if (txtSearchUserCode.Text != "")
        {

            SQL = "(LOWER(T.USER_CODE) LIKE '%" + txtSearchUserCode.Text.ToLower() + "%') AND";
        }

        if (txtSearchUserName.Text != "")
        {

            SQL = SQL + "(LOWER(T.USER_NAME) LIKE '%" + txtSearchUserName.Text.ToLower() + "%') AND";
        }
        if (txtSearchEPF.Text != "")
        {
            SQL = SQL + "(LOWER(T.EPF_NO) LIKE '%" + txtSearchEPF.Text.ToLower() + "%') AND";
        }
        if (ddlSearchUserRole.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.USER_ROLE_CODE LIKE '%" + ddlSearchUserRole.SelectedValue.ToString() + "%') AND";
        }

        if (ddlSearchCompany.SelectedValue.ToString() != "0")
        {

            SQL = SQL + "(T.COMPANY_CODE LIKE '%" + ddlSearchCompany.SelectedValue.ToString() + "%') AND";
        }



        SQL = SQL.Substring(0, SQL.Length - 3);


        String selectQuery = "";
        selectQuery = "   SELECT T.USER_CODE AS \"User Code\" ," +
            " T.USER_NAME AS \"User Name\"," +
            " T.COMPANY_CODE AS \"Companmy\"," +
         "  T.EPF_NO AS \"EPF NO\"," +
        "   T.USER_ROLE_CODE," +
               " UR.USER_ROLE_NAME AS \"User Role\"," +
         "  (CASE T.STATUS WHEN 1 THEN 'Active' ELSE 'In-Active' END) \"Status\"," +
         "  T.USER_EMAIL AS \"E-Mail\" " +
        "    FROM WF_ADMIN_USERS T  " +
            " INNER JOIN WF_ADMIN_USER_ROLES UR ON T.USER_ROLE_CODE=UR.USER_ROLE_CODE " +
            " WHERE (" + SQL + ") ORDER BY T.USER_CODE ASC";

        myOleDbCommand.CommandText = selectQuery;

        OracleDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
        if (myOleDbDataReader.HasRows == true)
        {
            DataTable dbTable = new DataTable();
            grdUsers.DataSource = myOleDbDataReader;
            grdUsers.DataBind();

            pnlUserGrid.Visible = true;
        }
    }




    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserRegistration.aspx");
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



        if (txtUserCode.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter User Code";
            Timer1.Enabled = true;
            return;
        }


        if (txtUserName.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter User Name";
            Timer1.Enabled = true;
            return;
        }


        if (txtEPF.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter EPF";
            Timer1.Enabled = true;
            return;
        }

        if (txtEmail.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter user E-mail";
            Timer1.Enabled = true;
            return;
        }

        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "0")
        {
            lblMsg.Text = "Please Select User Company";
            Timer1.Enabled = true;
            return;
        }


        if (Session["UserRegMode"].ToString() == "NEW")
        {
            if (CheckUserCodeAlreadyExist(txtUserCode.Text))
            {
                lblMsg.Text = "Enetered User Code Already Exists";
                Timer1.Enabled = true;
                return;
            }
        }



        try
        {
            string newPassword = "";
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            if (Session["UserRegMode"].ToString() == "NEW")
            {
                spProcess = new OracleCommand("INSERT_WF_ADMIN_USERS");
            }
            else if (Session["UserRegMode"].ToString() == "UPDATE")
            {
                spProcess = new OracleCommand("UPDATE_WF_ADMIN_USERS");
            }


            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar, 50).Value = txtUserCode.Text;
            spProcess.Parameters.Add("V_USER_NAME", OracleType.VarChar, 50).Value = txtUserName.Text;
            spProcess.Parameters.Add("V_EPF_NO", OracleType.Number, 6).Value = Convert.ToInt32(txtEPF.Text);
            spProcess.Parameters.Add("V_USER_EMAIL", OracleType.VarChar, 100).Value = txtEmail.Text.Trim();
            spProcess.Parameters.Add("V_USER_ROLE_CODE", OracleType.Number, 5).Value = Convert.ToInt32(ddlUserRole.SelectedValue);


            spProcess.Parameters.Add("V_USER_COMPANY", OracleType.VarChar).Value = ddlCompany.SelectedValue;



            if (rdbtnInActive.Checked)
            {
                spProcess.Parameters.Add("V_STATUS", OracleType.Number, 1).Value = 0;
            }
            else
            {
                spProcess.Parameters.Add("V_STATUS", OracleType.Number, 1).Value = 1;
            }

            if (rbtnTravellingYes.Checked)
            {
                spProcess.Parameters.Add("V_TRV_ENTITLE", OracleType.VarChar).Value = "true";
            }
            else
            {
                spProcess.Parameters.Add("V_TRV_ENTITLE", OracleType.VarChar).Value = "false";
            }


            spProcess.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = ddlBranch.SelectedValue;

            spProcess.Parameters.Add("V_USER_BRANCHES", OracleType.VarChar).Value = txtOtherBranches.Text;



            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_CREATED_USER", OracleType.VarChar).Value = UserCode;

            if (Session["UserRegMode"].ToString() == "NEW")
            {


                spProcess.Parameters.Add("V_NEW_PWD", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
                spProcess.Parameters["V_NEW_PWD"].Direction = ParameterDirection.Output;


            }



            spProcess.ExecuteNonQuery();



            if (Session["UserRegMode"].ToString() == "NEW")
            {


                newPassword = Convert.ToString(spProcess.Parameters["V_NEW_PWD"].Value);




            }



            conProcess.Close();

            deleteSystemRoles(txtUserCode.Text, ddlCompany.SelectedValue);
            saveSystemRoles(txtUserCode.Text, ddlCompany.SelectedValue);




            if (Session["UserRegMode"].ToString() == "NEW")
            {

                sendUserCreationMail(txtUserCode.Text, ddlCompany.SelectedValue, newPassword);


            }


            ClearComponents();
            SearchData();
            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

            //Response.Redirect("UserRegistration.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;
        }

    }
    private void saveSystemRoles(string userCode,string company)
    {
        foreach (GridViewRow row in grdSystems.Rows)
        {
            Label lblSystemCode = (Label)row.FindControl("lblSystemCode");
            CheckBox chkIsSystemAllowed = (CheckBox)row.FindControl("chkIsSystemAllowed");
            DropDownList ddlSystemUserRole = (DropDownList)row.FindControl("ddlSystemUserRole");

            insertSystemRoleRow(userCode,company, lblSystemCode.Text, ddlSystemUserRole.SelectedValue, chkIsSystemAllowed.Checked);



        }
    }



    private void insertSystemRoleRow(string userCode, string company, string systemCode, string userRoleCode, bool isAllowed)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;

            spProcess = new OracleCommand("INSERT_WF_ADMIN_USER_SYSTEM");

            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;

            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = userCode;
            spProcess.Parameters.Add("V_COMPANY_CODE", OracleType.VarChar).Value = company;
            spProcess.Parameters.Add("V_SYSTEM_CODE", OracleType.Number).Value = systemCode;

            spProcess.Parameters.Add("V_USER_ROLE_CODE", OracleType.Number).Value = userRoleCode;

            if (isAllowed)
            {
                spProcess.Parameters.Add("V_IS_ALLOWED", OracleType.Number).Value = 1;
            }
            else
            {
                spProcess.Parameters.Add("V_IS_ALLOWED", OracleType.Number).Value = 0;
            }

            spProcess.ExecuteNonQuery();
            conProcess.Close();

        }
        catch (Exception ex)
        {
        }
    }

    private void deleteSystemRoles(string userCode,string company)
    {
        try
        {
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand cmd = null;

            string strQuery = "";

            strQuery = "DELETE FROM WF_ADMIN_USER_SYSTEM WHERE USER_CODE=:V_USER_CODE AND COMPANY_CODE=:V_COMPANY_CODE";

            cmd = new OracleCommand(strQuery, conProcess);

            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", userCode));
            cmd.Parameters.Add(new OracleParameter("V_COMPANY_CODE", company));

            cmd.ExecuteNonQuery();
            conProcess.Close();

        }
        catch (Exception ex)
        {

        }

    }


    private bool CheckUserCodeAlreadyExist(string UserCode)
    {
        bool returnVal = false;
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT USER_CODE FROM WF_ADMIN_USERS WHERE USER_CODE='" + UserCode + "'";

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
        txtUserCode.Text = "";
        txtUserName.Text = "";
        txtEPF.Text = "";
        txtEmail.Text = "";
        txtOtherBranches.Text = "";
        ddlUserRole.SelectedValue = "0";
        ddlCompany.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";


        txtUserCode.Enabled = false;
        txtUserName.Enabled = false;
        txtEPF.Enabled = false;
        txtEmail.Enabled = false;
        txtOtherBranches.Enabled = false;
        ddlUserRole.Enabled = false;
        ddlCompany.Enabled = false;
        ddlBranch.Enabled = false;

        rdbtnActive.Enabled = false;
        rdbtnInActive.Enabled = false;

        rbtnTravellingYes.Enabled = false;
        rbtnTravellingNo.Enabled = false;

        grdSystems.DataSource = null;
        grdSystems.DataBind();

        grdSystems.Enabled = false;

        btnAddNew.Enabled = true;
        btnAlter.Enabled = false;
        btnSave.Enabled = false;

        btnUnlockUser.Enabled = false;
        btnResetPassword.Enabled = false;
        // btnCancel.Enabled = false;



    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtUserCode.Enabled = true;
        txtUserName.Enabled = true;
        txtEPF.Enabled = true;
        txtEmail.Enabled = true;
        txtOtherBranches.Enabled = true;
        ddlUserRole.Enabled = true;
        ddlCompany.Enabled = true;
        ddlBranch.Enabled = true;

        rdbtnActive.Enabled = true;
        rdbtnInActive.Enabled = true;


        rbtnTravellingYes.Enabled = true;
        rbtnTravellingNo.Enabled = true;

        txtUserCode.Text = "";
        txtUserName.Text = "";
        txtEPF.Text = "";
        txtEmail.Text = "";
        txtOtherBranches.Text = "";
        ddlUserRole.SelectedValue = "0";
        rdbtnActive.Checked = true;


        grdSystems.Enabled = true;

        loadSystems();

        btnSave.Enabled = true;

        Session["UserRegMode"] = "NEW";
    }

    protected void btnAlter_Click(object sender, EventArgs e)
    {
        if (txtUserCode.Text == "")
        {
            lblMsg.Text = "Please Select An User";
            Timer1.Enabled = true;
            return;
        }

        //txtUserCode.Enabled = true;
        txtUserName.Enabled = true;
        txtEPF.Enabled = true;
        txtEmail.Enabled = true;
        ddlUserRole.Enabled = true;
        ddlCompany.Enabled = true;


        ddlBranch.Enabled = true;
        txtOtherBranches.Enabled = true;


        rdbtnActive.Enabled = true;
        rdbtnInActive.Enabled = true;


        rbtnTravellingYes.Enabled = true;
        rbtnTravellingNo.Enabled = true;

        grdSystems.Enabled = true;

        btnSave.Enabled = true;

        Session["UserRegMode"] = "UPDATE";
    }

    protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtUserCode.Text = grdUsers.SelectedRow.Cells[1].Text.Trim();
        txtUserName.Text = grdUsers.SelectedRow.Cells[2].Text.Trim();
        if (grdUsers.SelectedRow.Cells[3].Text.Trim() != "&nbsp;" && grdUsers.SelectedRow.Cells[3].Text.Trim() != "")
        {

            ddlCompany.SelectedValue = grdUsers.SelectedRow.Cells[3].Text.Trim();
        }
        txtEPF.Text = grdUsers.SelectedRow.Cells[4].Text.Trim();
        txtEmail.Text = grdUsers.SelectedRow.Cells[8].Text.Trim();
        ddlUserRole.SelectedValue = grdUsers.SelectedRow.Cells[5].Text.Trim();

        if (grdUsers.SelectedRow.Cells[7].Text.Trim() == "Active")
        {
            rdbtnActive.Checked = true;
            rdbtnInActive.Checked = false;
        }
        else
        {
            rdbtnActive.Checked = false;
            rdbtnInActive.Checked = true;
        }

        loadUserDetails(txtUserCode.Text, ddlCompany.SelectedValue);
        loadSystems();

        btnAlter.Enabled = true;


        btnUnlockUser.Enabled = true;
        btnResetPassword.Enabled = true;
    }


    private void loadUserDetails(string userCode, string companyCode)
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        String selectQuery = "";
        selectQuery = "SELECT 	" +
                    "T.BRANCH_ID       , " +//0
                    " T.TRV_ENTITLE     , " +//1
                    " T.USER_BRANCHES    " +//2
                    " FROM WF_ADMIN_USERS T " +
                    " WHERE T.USER_CODE=:V_USER_CODE AND T.COMPANY_CODE=:V_COMPANY_CODE";


        OracleCommand cmd = new OracleCommand(selectQuery, con);
        cmd.Parameters.Add(new OracleParameter("V_USER_CODE", userCode));
        cmd.Parameters.Add(new OracleParameter("V_COMPANY_CODE", companyCode));

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();
            ddlBranch.SelectedValue = dr[0].ToString();

            if (dr[1].ToString() == "true")
            {
                rbtnTravellingYes.Checked = true;
                rbtnTravellingNo.Checked = false;
            }
            else
            {
                rbtnTravellingYes.Checked = false;
                rbtnTravellingNo.Checked = true;
            }


            txtOtherBranches.Text = dr[2].ToString();





        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[5].Visible = false;
    }


    private void initializeValues()
    {
        loadUserRoles();
        loadCompanies();
        loadBranches();
        loadSystems();
        lblError.Text = "";
        lblMsg.Text = "";
    }

    private void loadCompanies()
    {
        ddlCompany.Items.Clear();

        ddlCompany.Items.Add(new ListItem("--- Select One ---", "0"));
        ddlCompany.Items.Add(new ListItem("Life", "Life"));
        ddlCompany.Items.Add(new ListItem("General", "General"));

        ddlSearchCompany.Items.Add(new ListItem("--- Select One ---", "0"));
        ddlSearchCompany.Items.Add(new ListItem("Life", "Life"));
        ddlSearchCompany.Items.Add(new ListItem("General", "General"));



    }


    private void loadUserRoles()
    {
        ddlUserRole.Items.Clear();
        ddlUserRole.Items.Add(new ListItem("--- Select One ---", "0"));

        ddlSearchUserRole.Items.Clear();
        ddlSearchUserRole.Items.Add(new ListItem("--- Select One ---", "0"));


        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT USER_ROLE_CODE,USER_ROLE_NAME FROM WF_ADMIN_USER_ROLES";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlUserRole.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                ddlSearchUserRole.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

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
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("--- Select One ---", "0"));


        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT branch_code FROM sf_branches ORDER BY branch_code";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                ddlBranch.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));

            }
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }



    public void loadSystems()
    {

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataAdapter da = new OracleDataAdapter();
        string sql = "";
        sql = "select SYSTEM_CODE,SYSTEM_NAME from WF_ADMIN_SYSTEM WHERE IS_ACTIVE=1  " +
          " ORDER BY    SYSTEM_NAME ";


        da.SelectCommand = new OracleCommand(sql, con);

        DataTable dt = new DataTable();

        try
        {
            con.Open();
            dt.Load(da.SelectCommand.ExecuteReader());
            grdSystems.DataSource = dt;
            grdSystems.DataBind();
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

    public DataTable loadSystemsOfUser(string userCode, string company)
    {

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataAdapter da = new OracleDataAdapter();
        string sql = "";
        sql = "select u.user_code,u.SYSTEM_CODE,u.is_allowed,u.user_role_code,s.SYSTEM_NAME from WF_ADMIN_USER_SYSTEM u " +
             " INNER JOIN WF_ADMIN_SYSTEM s ON s.SYSTEM_CODE=u.SYSTEM_CODE " +
            " WHERE u.user_code=:V_USER_CODE AND u.COMPANY_CODE=:V_COMPANY_CODE AND u.is_allowed=1";


        da.SelectCommand = new OracleCommand(sql, con);
        da.SelectCommand.Parameters.Add(new OracleParameter("V_USER_CODE", userCode));
        da.SelectCommand.Parameters.Add(new OracleParameter("V_COMPANY_CODE", company));



        DataTable dt = new DataTable();

        try
        {
            con.Open();
            dt.Load(da.SelectCommand.ExecuteReader());


        }
        catch (OracleException err)
        {
            throw new ApplicationException("Data error.");
        }
        finally
        {
            con.Close();
        }

        return dt;




    }




    protected void grdSystems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            con.Open();
            DropDownList DropDownList = (e.Row.FindControl("ddlSystemUserRole") as DropDownList);

            Label LabelSystemCode = (e.Row.FindControl("lblSystemCode") as Label);

            CheckBox chkIsSystemAllowed = (e.Row.FindControl("chkIsSystemAllowed") as CheckBox);

            OracleCommand cmd = new OracleCommand("select USER_ROLE_CODE,USER_ROLE_NAME from WF_ADMIN_USER_ROLES WHERE SYSTEM_CODE=" + LabelSystemCode.Text + "", con);

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            DropDownList.DataSource = dt;

            DropDownList.DataTextField = "USER_ROLE_NAME";
            DropDownList.DataValueField = "USER_ROLE_CODE";
            DropDownList.DataBind();
            DropDownList.Items.Insert(0, new ListItem("--Select--", "0"));


            if (txtUserCode.Text != "")
            {
                DataTable dtUserRoles = new DataTable();
                dtUserRoles = loadSystemsOfUser(txtUserCode.Text, ddlCompany.SelectedValue);
                DataRow[] rows = dtUserRoles.Select("SYSTEM_CODE = " + LabelSystemCode.Text);

                if (rows.Length > 0)
                {
                    DropDownList.SelectedValue = rows[0][3].ToString();

                    if (rows[0][2].ToString() == "1")
                    {
                        chkIsSystemAllowed.Checked = true;
                    }
                    else
                    {
                        chkIsSystemAllowed.Checked = false;
                    }

                }




            }


        }

    }


    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        try
        {

            string newPassword = "";
            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            spProcess = new OracleCommand("WF_ADMIN_RESET_PASSWORD");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = txtUserCode.Text;
            spProcess.Parameters.Add("V_COMPANY", OracleType.VarChar).Value = ddlCompany.SelectedValue;


            spProcess.Parameters.Add("V_NEW_PWD", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
            spProcess.Parameters["V_NEW_PWD"].Direction = ParameterDirection.Output;

            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_EVENT_USER", OracleType.VarChar).Value = UserCode;


            spProcess.ExecuteNonQuery();


            newPassword = Convert.ToString(spProcess.Parameters["V_NEW_PWD"].Value);
            sendPWDResetNotifyMailToUser(txtUserCode.Text, ddlCompany.SelectedValue, newPassword);

            conProcess.Close();


            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;
        }
    }

    protected void btnUnlockUser_Click(object sender, EventArgs e)
    {
        try
        {

            OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            conProcess.Open();
            OracleCommand spProcess = null;
            spProcess = new OracleCommand("WF_ADMIN_UNLOCK_USER");



            spProcess.CommandType = CommandType.StoredProcedure;
            spProcess.Connection = conProcess;
            spProcess.Parameters.Add("V_USER_CODE", OracleType.VarChar).Value = txtUserCode.Text;
            spProcess.Parameters.Add("V_COMPANY", OracleType.VarChar).Value = ddlCompany.SelectedValue;



            string UserCode = "";
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                UserCode = reqCookies["UserCode"].ToString();
            }


            spProcess.Parameters.Add("V_EVENT_USER", OracleType.VarChar).Value = UserCode;


            spProcess.ExecuteNonQuery();


            sendUserUnlockNotifyMailToUser(txtUserCode.Text, ddlCompany.SelectedValue);

            conProcess.Close();


            lblMsg.Text = "Successfully Saved";
            Timer1.Enabled = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error While Saving";
            Timer1.Enabled = true;
        }
    }

    private void sendUserCreationMail(string userCode, string company, string newPwd)
    {


        AdminMail mail = new AdminMail();

        mail.From_address = System.Configuration.ConfigurationManager.AppSettings["USER_ADMIN_MAIL_SEND_ADDRESS"].ToString();

        mail.To_address = getEmailOfUser(userCode, company);



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";



        mail.Subject = "System User Created";


        string userSystems = "";

        DataTable dtUserRoles = new DataTable();
        dtUserRoles = loadSystemsOfUser(txtUserCode.Text, ddlCompany.SelectedValue);
        foreach (DataRow r in dtUserRoles.Rows)
        {
            userSystems = userSystems + r[4].ToString()+"</br>";
        }

        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>New user account created</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td colspan=\"2\">" +
                   " System user details  " +
                  "</td>" +
                   "</tr>" +

                    "<tr>" +
                          "<td>" +
                   " User Name  " +
                  "</td>" +
                        "<td>" +
                   userCode +
                  "</td>" +
                   "</tr>" +


                          "<tr>" +
                          "<td>" +
                   " Company  " +
                  "</td>" +
                        "<td>" +
                   company +
                  "</td>" +
                   "</tr>" +

                       "<tr>" +
                          "<td>" +
                   " Password  " +
                  "</td>" +
                        "<td>" +
                   newPwd +
                  "</td>" +
                   "</tr>" +

        "<tr>" +
                          "<td>" +
                   " Systems  " +
                  "</td>" +
                        "<td>" +
                   userSystems +
                  "</td>" +
                   "</tr>" +





                     "<tr>" +
                         "<td colspan=\"2\">" +
                     "Note: Above password is single time password and you must change it after your first login" +

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

    private void sendPWDResetNotifyMailToUser(string userCode, string company, string newPwd)
    {


        AdminMail mail = new AdminMail();

        mail.From_address = System.Configuration.ConfigurationManager.AppSettings["USER_ADMIN_MAIL_SEND_ADDRESS"].ToString();

        mail.To_address = getEmailOfUser(userCode, company);



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";



        mail.Subject = "User Password Reset";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>User Password Reset</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   " Your password has been reset successfully! Your new password is " + newPwd +
                  "</td>" +
                   "</tr>" +

                    "<tr>" +
                   "</tr>" +

                     "<tr>" +
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

    private void sendUserUnlockNotifyMailToUser(string userCode, string company)
    {


        AdminMail mail = new AdminMail();

        mail.From_address = System.Configuration.ConfigurationManager.AppSettings["USER_ADMIN_MAIL_SEND_ADDRESS"].ToString();

        mail.To_address = getEmailOfUser(userCode, company);



        mail.Bcc_address = "tharindu.dilanka@hnbassurance.com";



        mail.Subject = "User Account Unlocked";
        String BodyText;

        BodyText = "<html>" +
                    "<head>" +
                    "<title>User Password Reset</title>" +
                   " <body> " +
                     "<table>" +
                   "<tr>" +
                   "<td>" +
                   " Your system account has been unlocked successfully." +
                  "</td>" +
                   "</tr>" +

                    "<tr>" +
                   "</tr>" +

                     "<tr>" +
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

    public string getEmailOfUser(string userCode, string company)
    {
        string userEmail = "";
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        String selectQuery = "";

        selectQuery = "   SELECT " +
                             "WAU.USER_EMAIL  " +
                             " FROM  WF_ADMIN_USERS WAU " +
                             " WHERE WAU.USER_CODE=:V_USER_CODE  AND WAU.COMPANY_CODE=:V_COMPANY_CODE  ";


        OracleCommand cmd = new OracleCommand(selectQuery, con);
        cmd.Parameters.Add(new OracleParameter("V_USER_CODE", userCode));

        cmd.Parameters.Add(new OracleParameter("V_COMPANY_CODE", company));

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

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Timer1.Enabled = false;
    }
}
