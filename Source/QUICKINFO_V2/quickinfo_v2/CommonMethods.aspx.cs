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
using System.Web.Services;
using SignalR;
using SignalR.Infrastructure;
using SignalR.Hosting.AspNet;
using quickinfo_v2.CommonCLS;

namespace quickinfo_v2
{
    public partial class CommonMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [WebMethod]
        public static string INSERT_RECORD(string from, string to, string msg)
        {
            try
            {
                OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                conProcess.Open();
                OracleCommand spProcess = null;

                spProcess = new OracleCommand("INSERT_QUICKINFO_CHAT");


                spProcess.CommandType = System.Data.CommandType.StoredProcedure;
                spProcess.Connection = conProcess;

                spProcess.Parameters.Add("V_MSG_FROM", OracleType.VarChar).Value = from;
                spProcess.Parameters.Add("V_MSG_TO", OracleType.VarChar).Value = to;
                spProcess.Parameters.Add("V_MESSAGE", OracleType.VarChar).Value = msg;
                spProcess.Parameters.Add("V_IS_READ", OracleType.Number).Value = 0;


                spProcess.ExecuteNonQuery();
                conProcess.Close();


                return "Success";
            }
            catch (Exception ex)
            {
                return "failure";
            }


        }

        [WebMethod]
        public static string UPDATE_CHAT(string from, string to)
        {
            try
            {
                OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                conProcess.Open();
                OracleCommand spProcess = null;

                spProcess = new OracleCommand("UPDATE_QUICKINFO_CHAT");


                spProcess.CommandType = System.Data.CommandType.StoredProcedure;
                spProcess.Connection = conProcess;

                spProcess.Parameters.Add("V_MSG_FROM", OracleType.VarChar).Value = from;
                spProcess.Parameters.Add("V_MSG_TO", OracleType.VarChar).Value = to;
                spProcess.Parameters.Add("V_IS_READ", OracleType.Number).Value = 1;


                spProcess.ExecuteNonQuery();
                conProcess.Close();


                return "Success";
            }
            catch (Exception ex)
            {
                return "failure";
            }


        }


        [WebMethod]
        public static string POP_CHAT(string from, string to, string msg, string id)
        {
            try
            {


                //var clients = GetClients();
                //clients.showMessage(msg, from, to, id);
                return "Success";
            }
            catch (Exception ex)
            {
                return "failure";
            }
        }


        
    }
}
