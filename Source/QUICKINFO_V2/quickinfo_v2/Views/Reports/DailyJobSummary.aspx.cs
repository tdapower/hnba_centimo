using quickinfo_v2.Controllers.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Reports
{
    public partial class DailyJobSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Please select date for summary.');", true);


                return;
            }


            viewReport(txtFromDate.Text, txtToDate.Text);


        }


        private void viewReport(string fromDate,string toDate)
        {
            string generatedText = "";
            string connectionString = "";
            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            //selectQuery = "select u.proposal_upload_id,u.job_number,u.quotation_no,u.target_branch_code,u.job_type " +
            //      ", " +
            //      "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING'), " +
            //      "round(24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING')- " +
            //      "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_SCRUTINIZING'))) " +
            //      ", " +
            //      "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING' " +
            //             " and rownum=(select max(rownum) from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where " +
            //   " f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING' )" +
            //            "), " +

            //      "round(24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING')- " +
            //      "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_PROCESSING'))) " +
            //      ", " +
            //      "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS'), " +
            //      "round(24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS')- " +
            //      "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_VALIDATORS'))) " +
            //      "from  mnbq_proposal_upload u  " +
            //      "where  to_date(u.sys_date,'DD/MM/RRRR')=to_date('" + date + "','DD/MM/RRRR')  " +
            //      "and u.status='APPROVED_BY_VALIDATORS' " +
            //      " order by u.sys_date ";

selectQuery =    " select u.proposal_upload_id,u.job_number,u.quotation_no,u.target_branch_code,u.job_type  "+
                  " , "+
                  " (select wau.user_name from mnbq_proposal_upload_followup f  "+
                  " left join wf_admin_users wau on wau.user_code=f.user_code  "+
                  " where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING'        and f.seq_id=(select max(fff.seq_id) from mnbq_proposal_upload_followup fff  where fff.proposal_upload_id=u.proposal_upload_id and fff.status='APPROVED_BY_SCRUTINIZING' ))   as  \"Scrutinize User\"  ,  "+
                  " round(24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING')-  "+
                  " (select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_SCRUTINIZING'))) as \"Time For Scrutinize\" "+
                  " ,  "+
                  " (select wau.user_name  from mnbq_proposal_upload_followup f  "+
                  " left join wf_admin_users wau on wau.user_code=f.user_code  "+
                  " where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING'  "+
                          " and f.seq_id=(select max(fff.seq_id) from mnbq_proposal_upload_followup fff  where fff.proposal_upload_id=u.proposal_upload_id and fff.status='COMPLETED_BY_PROCESSING' ))  as \"Processing User\",  "+
                  " round(24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING')-  "+
                  " (select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_PROCESSING')))  as \"Time for Processing\"  "+
                  " ,  "+
                  " ( "+
                  " select wau.user_name from mnbq_proposal_upload_followup f  "+
                          " left join wf_admin_users wau on wau.user_code=f.user_code  "+
                          " where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS' "+
                          " and f.seq_id=(select max(fff.seq_id) from mnbq_proposal_upload_followup fff  where fff.proposal_upload_id=u.proposal_upload_id and fff.status='APPROVED_BY_VALIDATORS' ))  as \"Process Validate User\" , "+
                            " "+
                  " round(24*60*( "+
                          " (select max(f.sys_date) from mnbq_proposal_upload_followup f  "+
                          " where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS')-  "+
                          " (select max(f.sys_date) from mnbq_proposal_upload_followup f  "+
                          " where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_VALIDATORS'))) as \"Time for Process Validation\" "+
                  " from  mnbq_proposal_upload u   "+
                  "where  (to_date(u.sys_date,'DD/MM/RRRR')>=to_date('" + fromDate + "','DD/MM/RRRR')  and   to_date(u.sys_date,'DD/MM/RRRR')<=to_date('" + toDate + "','DD/MM/RRRR'))  and" +
                  " u.status ='APPROVED_BY_VALIDATORS' "+
                   " order by u.sys_date";
                   





            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();


            generatedText = "<table border=\"2\" style=\"background-color:#FFFFFF;border-collapse:collapse;border:2px solid #0000FF;color:#000000;width:100%\" cellpadding=\"3\" cellspacing=\"3\">";


            generatedText = generatedText + "<tr style=\"background-color: #87CEFA;\">" +
                             "<td>Job No.</td>" +
                             "<td>Quotation No.</td>" +
                             "<td>Branch</td>" +
                             "<td>Job Type</td>" +
                             "<td>Scrutinize User</td>" +
                             "<td>Time For Scrutinize</td>" +
                             "<td>Processing User</td>" +
                             "<td>Time for Processing</td>" +
                             "<td>Process Validate User</td>" +
                             "<td>Time for Process Validation</td>" +
                             "<td>Total Time</td>" +
                              "</tr>";

            string scrutiBackColor = "";
            string procBackColor = "";
            string procValiBackColor = "";

            string jobType = "";

            int totalTime = 0;

            while (dr.Read())
            {
               
                if (dr[4].ToString() == "N")
                {
                    jobType = "New";
                   
                    if (Convert.ToInt32(dr[8].ToString()) > 12)
                    {
                        procBackColor = "style=\"background-color: #DC143C;\"";
                    }
                    else
                    {
                        procBackColor = "style=\"background-color: #00FF00;\"";
                    }
                }
                else if (dr[4].ToString() == "E")
                {
                    jobType = "Endorsement";
                    if (dr[8] == null)
                    {
                        return;
                    }
                    if (Convert.ToInt32(dr[8].ToString()) > 5)
                    {
                        procBackColor = "style=\"background-color: #DC143C;\"";
                    }
                    else
                    {
                        procBackColor = "style=\"background-color: #00FF00;\"";
                    }
                }
                else if (dr[4].ToString() == "R")
                {
                    jobType = "Renewal";
                    if (dr[8] == null)
                    {
                        return;
                    }
                    if (Convert.ToInt32(dr[8].ToString()) > 5)
                    {
                        procBackColor = "style=\"background-color: #DC143C;\"";
                    }
                    else
                    {
                        procBackColor = "style=\"background-color: #00FF00;\"";
                    }
                }
                else if (dr[4].ToString() == "C")
                {
                    jobType = "Cancellation";
                    if (dr[8] == null)
                    {
                        return;
                    }
                    if (Convert.ToInt32(dr[8].ToString()) > 30)
                    {
                        procBackColor = "style=\"background-color: #DC143C;\"";
                    }
                    else
                    {
                        procBackColor = "style=\"background-color: #00FF00;\"";
                    }
                }



                generatedText = generatedText + "<tr>";
                generatedText = generatedText + "<td>" + dr[1].ToString() + "</td>";
                generatedText = generatedText + "<td>" + dr[2].ToString() + "</td>";
                generatedText = generatedText + "<td>" + dr[3].ToString() + "</td>";
                generatedText = generatedText + "<td>" + jobType + "</td>";
                generatedText = generatedText + "<td>" + dr[5].ToString() + "</td>";






                if (Convert.ToInt32(dr[6].ToString()) > 5)
                {
                    scrutiBackColor = "style=\"background-color: #DC143C;\"";
                }
                else
                {
                    scrutiBackColor = "style=\"background-color: #00FF00;\"";
                }


                if (Convert.ToInt32(dr[6].ToString()) > 5)
                {
                    procBackColor = "style=\"background-color: #DC143C;\"";
                }
                else
                {
                    procBackColor = "style=\"background-color: #00FF00;\"";
                }


                if (Convert.ToInt32(dr[10].ToString()) > 5)
                {
                    procValiBackColor = "style=\"background-color: #DC143C;\"";
                }
                else
                {
                    procValiBackColor = "style=\"background-color: #00FF00;\"";
                }


                totalTime = Convert.ToInt32(dr[6].ToString()) + Convert.ToInt32(dr[8].ToString()) + Convert.ToInt32(dr[10].ToString());
                generatedText = generatedText + "<td " + scrutiBackColor + ">" + dr[6].ToString() + "</td>";
                generatedText = generatedText + "<td>" + dr[7].ToString() + "</td>";
                generatedText = generatedText + "<td " + procBackColor + ">" + dr[8].ToString() + "</td>";
                generatedText = generatedText + "<td>" + dr[9].ToString() + "</td>";
                generatedText = generatedText + "<td " + procValiBackColor + ">" + dr[10].ToString() + "</td>";
                generatedText = generatedText + "<td>" + totalTime.ToString() + "</td>";


                generatedText = generatedText + "</tr>";




            }

            generatedText = generatedText + "</table>";


            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            ltrlData.Text = generatedText;
        }
    }
}