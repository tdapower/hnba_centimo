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
using quickinfo_v2.Models.ITWorkflow;
using System.Collections.Generic;

namespace quickinfo_v2.Controllers.ITWorkflow
{
    public class UserRequestDB
    {

        private string connectionString;

        public UserRequestDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
        }

        public UserRequestDB(string connectionString)
        {
            // Set the specified connection string.
            this.connectionString = connectionString;
        }

        public void InsertUserRequest(UserRequest usrReq)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_IT_WF_USER_REQ", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_REF_NO", OracleType.VarChar, 50));
            cmd.Parameters["V_REF_NO"].Value = usrReq.RefNo;
            cmd.Parameters.Add(new OracleParameter("V_JOB_REMARKS", OracleType.VarChar, 200));
            cmd.Parameters["V_JOB_REMARKS"].Value = usrReq.JobRemarks;
            cmd.Parameters.Add(new OracleParameter("V_SCREENSHOT", OracleType.Blob));
            cmd.Parameters["V_SCREENSHOT"].Value = usrReq.Screenshot;
            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar, 50));
            cmd.Parameters["V_USER_CODE"].Value = usrReq.Screenshot;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public UserRequest GetUserRequest(int requestId)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("IT_WF_GET_USER_REQ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("V_REQUEST_ID", OracleType.Number, 5));
            cmd.Parameters["V_REQUEST_ID"].Value = requestId;

            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();
                UserRequest usrReq = new UserRequest(
                 (int)reader["REQUEST_ID"], (string)reader["REF_NO"],
                 (string)reader["JOB_REMARKS"], (byte[])reader["SCREENSHOT"], (string)reader["USER_CODE"]);
                reader.Close();
                return usrReq;



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

        public List<UserRequest> GetUserRequests()
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("IT_WF_GET_ALL_USER_REQ", con);
            cmd.CommandType = CommandType.StoredProcedure;

            List<UserRequest> usrRequests = new List<UserRequest>();

            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserRequest usrReq = new UserRequest(
               (int)reader["REQUEST_ID"], (string)reader["REF_NO"],
               (string)reader["JOB_REMARKS"], (byte[])reader["SCREENSHOT"], (string)reader["USER_CODE"]);
                    usrRequests.Add(usrReq);
                }
                reader.Close();
                return usrRequests;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateUserRequestd(int EmployeeID, string firstName, string lastName, string titleOfCourtesy)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 10));
            cmd.Parameters["@FirstName"].Value = firstName;
            cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 20));
            cmd.Parameters["@LastName"].Value = lastName;
            cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar,
              25));
            cmd.Parameters["@TitleOfCourtesy"].Value = titleOfCourtesy;
            cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
            cmd.Parameters["@EmployeeID"].Value = EmployeeID;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

    }
}