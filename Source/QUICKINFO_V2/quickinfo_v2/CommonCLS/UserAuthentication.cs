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

/// <summary>
/// Common class for UserAuthentication
/// </summary>
public class UserAuthentication
{
    public UserAuthentication()
    {

    }


  



    public bool IsAuthorizeForThisPage(String UserName, String SubMenuCode)
    {

        try
        {
            if (Left(UserName, 4) == "HNBA")
            {
                UserName = Right(UserName, (UserName.Length) - 5);
            }
            else if (Left(UserName, 5) == "HNBGI")
            {
                UserName = Right(UserName, (UserName.Length) - 6);
            }
            else
            {
                UserName = Right(UserName, (UserName.Length) - 7);
            }


            bool returnVal = false;
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";



            int userRoleCode = 0;

            userRoleCode = getUserRoleCodeOfCurrentUser(UserName);
            selectQuery = "SELECT SB.SUB_MENU_CODE FROM WF_ADMIN_SUB_MENU SB " +
                        " INNER JOIN WF_ADMIN_USEROLE_PRIVILEGE T ON SB.MAIN_MENU_CODE=T.MAIN_MENU_CODE AND SB.SUB_MENU_CODE=T.SUB_MENU_CODE " +
                        " WHERE  T.USER_ROLE_CODE='" + userRoleCode + "' AND SB.SUB_MENU_CODE=" + SubMenuCode + " AND T.PRIVILEGE_GIVEN=1 ";


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
        catch (Exception ee)
        {
            return false;
        }
    }

    private int getUserRoleCodeOfCurrentUser(string UserName)
    {
        int userRoleCode = 0;

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();


        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";
        selectQuery = "SELECT USER_ROLE_CODE FROM WF_ADMIN_USERS WHERE STATUS=1 AND USER_CODE='" + UserName + "'";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            dr.Read();

            userRoleCode = Convert.ToInt32(dr[0].ToString());

        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return userRoleCode;

    }




    public bool AuthenticateAdminFunctions(String UserName)
    {
        bool isAuthenticated = false;


        if (Left(UserName, 4) == "HNBA")
        {
            UserName = Right(UserName, (UserName.Length) - 5);
        }
        else
        {
            UserName = Right(UserName, (UserName.Length) - 7);
        }

        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
        OracleDataReader dr;

        con.Open();

        OracleCommand cmd = new OracleCommand();
        cmd.Connection = con;
        String selectQuery = "";

        string AdminUserCodes = System.Configuration.ConfigurationManager.AppSettings["AdminUserCodes"].ToString();


        selectQuery = "   SELECT T.USER_ROLE_CODE FROM WF_ADMIN_USERS T  " +
           " WHERE T.USER_CODE='" + UserName + "' AND  T.USER_ROLE_CODE IN (" + AdminUserCodes + ") ";

        cmd.CommandText = selectQuery;

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            isAuthenticated = true;
        }
        dr.Close();
        dr.Dispose();
        cmd.Dispose();
        con.Close();
        con.Dispose();

        return isAuthenticated;
    }


    public string Left(string text, int length)
    {
        return text.Substring(0, length);
    }

    public string Right(string text, int length)
    {
        return text.Substring(text.Length - length, length);
    }

    public string Mid(string text, int start, int end)
    {
        return text.Substring(start, end);
    }

    public string Mid(string text, int start)
    {
        return text.Substring(start, text.Length - start);
    }
}
