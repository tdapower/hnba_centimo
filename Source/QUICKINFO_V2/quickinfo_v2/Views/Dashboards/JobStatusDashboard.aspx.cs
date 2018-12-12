using quickinfo_v2.Controllers.Dashboard;
using quickinfo_v2.Controllers.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Dashboards
{
    public partial class JobStatusDashboard : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string ipAddress = "";
                ipAddress = GetUserIP();
                if (ipAddress != "")
                {
                    ProposalUploadController proposalUploadController = new ProposalUploadController();
                    if (!proposalUploadController.ValidateIpAddressForDashboard(ipAddress))
                    {
                        Response.Redirect("~/Logo.aspx");
                    }
                }


                loadJobDetails();
                loadCancellationJobDetails();
            }

       


        }


        private string GetUserIP()
        {
            HttpRequest request = base.Request;

            // Get UserHostAddress property.
            string address = request.UserHostAddress;
            return address;
        }
        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            loadJobDetails();
            loadCancellationJobDetails();
        }
        private void loadJobDetails()
        {

            String GeneratedText = "";



            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            string SQL_Str = " ";

            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SQL_Str = " select " +
                    " (select max( to_char(f1.sys_date,'DD/MM/RRRR HH24:MI:SS')) from mnbq_proposal_upload_followup f1 where f1.proposal_upload_id=mpu.proposal_upload_id  and f1.status='INITIAL') , " +
                    " CASE WHEN mpu.job_type ='N' THEN 'New' "+
                "WHEN mpu.job_type='E' THEN 'Endorsement' WHEN mpu.job_type='R' THEN 'Renewal'  "+
               " WHEN mpu.job_type='C' THEN 'Cancellation' ELSE '' END   AS \"Job Type\"  , " +
                    " CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO "+
                "WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER  "+
               " WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER        " +
               " WHEN  mpu.JOB_TYPE='F' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" ,            " +
                    " mpu.status, " +
                    " wau.user_name " +
                     " from mnbq_proposal_upload mpu " +
                    " inner join mnbq_proposal_upload_followup fup on fup.proposal_upload_id=mpu.proposal_upload_id and fup.status=mpu.status   and fup.seq_id=(select max(fff.seq_id) from mnbq_proposal_upload_followup fff where fff.proposal_upload_id=mpu.proposal_upload_id and fff.status=mpu.status ) " +
                     " left join wf_admin_users wau on wau.user_code=fup.user_code " +
                     " where  mpu.job_type<>'C' AND " +
                     " mpu.status not in('RENEWAL_ADDED','CANCELLATION_ADDED','ENDORSEMENT_ADDED','TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD','TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD','REJECTED_BY_SCRUTINIZING','REJECTED_BY_VALIDATORS','APPROVED_BY_VALIDATORS','COMPLETED_AND_PRINTED','UNKNOWN','TEMP')  " +
                " ORDER BY (select max( to_char(f1.sys_date,'DD/MM/RRRR HH24:MI:SS'))  from mnbq_proposal_upload_followup f1 where f1.proposal_upload_id=mpu.proposal_upload_id  and f1.status='INITIAL')";


                cmd.CommandText = SQL_Str;

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                GeneratedText =
                            "<table class=\"responstable\">" +
                             "<tr>" +
                                 "<th>Initiated Date</th>" +
                                 "<th>Initiated Time</th>" +
                                  "<th>Branch</th>" +
                                   "<th>Job Type</th>" +
                                    "<th>Job No.</th>" +
                                     "<th>Status</th>" +
                                      "<th>Current User</th>" +
                                       "<th>Elapsed Time</th>" +

                             "</tr>";




                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        DateTime inDateTime = Convert.ToDateTime(dr[0].ToString());
                        DateTime currentDateTime = System.DateTime.Now;

                        var timeDiff = currentDateTime.Subtract(inDateTime);

                        string rowStyle = "";




                        if (Convert.ToInt32(timeDiff.TotalSeconds) > 1800)
                        {
                            rowStyle = "<tr style='background-color: #ff0000; color: white;    font-weight: bolder;'>";
                        }
                        else if (Convert.ToInt32(timeDiff.TotalSeconds) > 1200)
                        {
                            rowStyle = "<tr style='background-color: #ff8000; color: white;    font-weight: bolder;'>";
                        }
                        else if (Convert.ToInt32(timeDiff.TotalSeconds) > 900)
                        {
                            rowStyle = "<tr style='background-color: #ffcc00; color: white;    font-weight: bolder;'>";
                        }
                        else
                        {
                            rowStyle = "<tr>";
                        }






                        GeneratedText = GeneratedText + rowStyle +



                                "<td>" + inDateTime.Date.ToString().Remove(10) + "</td>" +
                                "<td>" + ((dr[0].ToString()=="")?"":dr[0].ToString().Substring(11, 8)) + "</td>" +
                                "<td>" + ((dr[2].ToString()=="")?"":dr[2].ToString().Substring(2, 3)) + "</td>" +
                                  "<td>" + dr[1].ToString() + "</td>" +
                                   "<td>" + dr[2].ToString() + "</td>" +
                                    "<td>" + dr[3].ToString() + "</td>" +
                                    "<td>" + dr[4].ToString() + "</td>" +
                                     "<td>" + "D:" + timeDiff.Days + " H:" + timeDiff.Hours + " M:" + timeDiff.Minutes + " S:" + timeDiff.Seconds + "</td>" +

            "</tr>";



                    }
                }

                GeneratedText = GeneratedText + "  </table>";


                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('" + ex + "');", true);
                 
                // errorLog.saveToErrorLog("proposal_no=" + currentProposalNo + ",Error=" + ex.ToString());
            }



            ltrlGeneratedTable.Text = GeneratedText;


        }
        private void loadCancellationJobDetails()
        {

            String GeneratedText = "";



            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            string SQL_Str = " ";

            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SQL_Str = " select " +
                    " (select max( to_char(f1.sys_date,'DD/MM/RRRR HH24:MI:SS')) from mnbq_proposal_upload_followup f1 where f1.proposal_upload_id=mpu.proposal_upload_id  and f1.status='INITIAL') , " +
                    " CASE WHEN mpu.job_type ='N' THEN 'New' WHEN mpu.job_type='E' THEN 'Endorsement' WHEN mpu.job_type='R' THEN 'Renewal' WHEN mpu.job_type='C' THEN 'Cancellation' ELSE '' END   AS \"Job Type\"  , " +
                    " CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" ,            " +
                    " mpu.status, " +
                    " wau.user_name " +
                     " from mnbq_proposal_upload mpu " +
                    " inner join mnbq_proposal_upload_followup fup on fup.proposal_upload_id=mpu.proposal_upload_id and fup.status=mpu.status   and fup.seq_id=(select max(fff.seq_id) from mnbq_proposal_upload_followup fff where fff.proposal_upload_id=mpu.proposal_upload_id and fff.status=mpu.status ) " +
                     " left join wf_admin_users wau on wau.user_code=fup.user_code " +
                     " where mpu.job_type='C' AND " +
                     " mpu.status not in('RENEWAL_ADDED','CANCELLATION_ADDED','ENDORSEMENT_ADDED','TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD','TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD','REJECTED_BY_SCRUTINIZING','APPROVED_BY_VALIDATORS','COMPLETED_AND_PRINTED','UNKNOWN','TEMP')  " +
                " ORDER BY (select max( to_char(f1.sys_date,'DD/MM/RRRR HH24:MI:SS'))  from mnbq_proposal_upload_followup f1 where f1.proposal_upload_id=mpu.proposal_upload_id  and f1.status='INITIAL')";


                cmd.CommandText = SQL_Str;

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                GeneratedText =
                            "<table class=\"responstable\">" +
                             "<tr>" +
                                 "<th>Initiated Date</th>" +
                                 "<th>Initiated Time</th>" +
                                  "<th>Branch</th>" +
                                   "<th>Job Type</th>" +
                                    "<th>Job No.</th>" +
                                     "<th>Status</th>" +
                                      "<th>Current User</th>" +
                                       "<th>Elapsed Time</th>" +

                             "</tr>";




                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        DateTime inDateTime = Convert.ToDateTime(dr[0].ToString());
                        DateTime currentDateTime = System.DateTime.Now;

                        var timeDiff = currentDateTime.Subtract(inDateTime);

                        string rowStyle = "";



                        if (Convert.ToInt32(timeDiff.TotalSeconds) > 10800)
                        {
                            rowStyle = "<tr style='background-color: #ff0000; color: white;    font-weight: bolder;'>";
                        }
                        else if (Convert.ToInt32(timeDiff.TotalSeconds) > 9000)
                        {
                            rowStyle = "<tr style='background-color: #ff8000; color: white;    font-weight: bolder;'>";
                        }
                        else if (Convert.ToInt32(timeDiff.TotalSeconds) > 7200)
                        {
                            rowStyle = "<tr style='background-color: #ffcc00; color: white;    font-weight: bolder;'>";
                        }
                        else
                        {
                            rowStyle = "<tr>";
                        }






                        GeneratedText = GeneratedText + rowStyle +



                                "<td>" + inDateTime.Date.ToString().Remove(10) + "</td>" +
                                   "<td>" + dr[0].ToString().Substring(11, 8) + "</td>" +
                                 "<td>" + dr[2].ToString().Substring(2, 3) + "</td>" +
                                  "<td>" + dr[1].ToString() + "</td>" +
                                   "<td>" + dr[2].ToString() + "</td>" +
                                    "<td>" + dr[3].ToString() + "</td>" +
                                    "<td>" + dr[4].ToString() + "</td>" +
                                     "<td>" + "D:" + timeDiff.Days + " H:" + timeDiff.Hours + " M:" + timeDiff.Minutes + " S:" + timeDiff.Seconds + "</td>" +

            "</tr>";



                    }
                }

                GeneratedText = GeneratedText + "  </table>";


                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                // errorLog.saveToErrorLog("proposal_no=" + currentProposalNo + ",Error=" + ex.ToString());
            }



            ltrlGeneratedTableForCancellation.Text = GeneratedText;


        }
    }
}