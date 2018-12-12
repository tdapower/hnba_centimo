using quickinfo_v2.Controllers.Dashboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Dashboards
{
    public partial class UserSummaryDashboardNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadScrutinizingUsersSummary();
            loadProcessUsersSummary();
            loadProcessValidationUsersSummary();
        }

        private void loadScrutinizingUsersSummary()
        {
            string generatedText = "";
            string userGroup = "45";
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select t.user_code,t.user_name,t.EPF_NO from wf_admin_users t where t.user_role_code=" + userGroup + " ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();

            ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                          "<img u=\"image\" src=\"photos/scruti_unit.jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                       " </div>";

            while (dr.Read())
            {

                loadScrutiDataOfUser(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();



        }



        private void loadScrutiDataOfUser(string UserCode, string userName, string epf)
        {

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
            string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();






            if (UserCode != "")
            {
                DashboardController dashboardController = new DashboardController();

                int countOfApprovedByScrutigByUser = dashboardController.getJobCountOfUserForStatus(UserCode, APPROVED_BY_SCRUTINIZING);
                int countOfRejectedByScrutigByUser = dashboardController.getJobCountOfUserForStatus(UserCode, REJECTED_BY_SCRUTINIZING);

                int totalScrutiByUser = 0;
                totalScrutiByUser = countOfApprovedByScrutigByUser + countOfRejectedByScrutigByUser;


                int countOfApprovedByScrutigBAllUsers = 0;
                countOfApprovedByScrutigBAllUsers = dashboardController.getCurrentSummaryOfStatus(APPROVED_BY_SCRUTINIZING);
                int countOfRejectedByScrutigAllUsers = 0;
                countOfRejectedByScrutigAllUsers = dashboardController.getCurrentSummaryOfStatus(REJECTED_BY_SCRUTINIZING);

                int totalScruti = 0;
                totalScruti = countOfApprovedByScrutigBAllUsers + countOfRejectedByScrutigAllUsers;



                double percentageOfCompletedByScrutiUser = 0;

                percentageOfCompletedByScrutiUser = (int)Math.Round((double)(totalScrutiByUser * 100) / totalScruti);


                if (percentageOfCompletedByScrutiUser < 0) percentageOfCompletedByScrutiUser = 0;

                //onerror=\"if (this.src != 'photos/0.jpg' && this.attribute('src') != 'photos/0.jpg') this.src = 'photos/0.jpg';\"

                ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                            "<img u=\"image\" src=\"photos/" + epf + ".jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                            "<div u=\"caption\" t=\"CLIP|LR\" t2=\"B\" du=\"2000\" class=\"captionOrange\" style=\"position: absolute; left: 20px; top: 500px; width: 500px; height: 30px;\">" +
                                            "    " + userName + "" +
                                            "</div>" +
                                            "<div u=\"caption\" t=\"MCLIP|T\" t2=\"T\" d=\"-450\" style=\"position: absolute; left: 605px; top: 40px; width: 400px; height: 30px; font-size: 18px; color: #fff; line-height: 30px; text-align: center;\">Task &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;Value</div>" +
                                            " <div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 90px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Approved Count By User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfApprovedByScrutigByUser + "</div>" +
                                            "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 140px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Rejected Count By  User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfRejectedByScrutigByUser + "</div>" +
                                            " <div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 190px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + totalScrutiByUser + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 240px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By All Users&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + totalScruti + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 290px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Percentage of User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + percentageOfCompletedByScrutiUser.ToString() + "%</div>" +
                                        " </div>";

            }


        }
        ///
        private void loadProcessUsersSummary()
        {
            string generatedText = "";
            string userGroup = "46";
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select t.user_code,t.user_name,t.EPF_NO from wf_admin_users t where t.user_role_code=" + userGroup + " ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();

            ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                          "<img u=\"image\" src=\"photos/proc_unit.jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                       " </div>";

            while (dr.Read())
            {

                loadPrcessDataOfUser(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }



        private void loadPrcessDataOfUser(string UserCode, string userName, string epf)
        {
            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();
            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();


            if (UserCode != "")
            {
                DashboardController dashboardController = new DashboardController();

                int countOfCOmpletedByProcessingByUser = dashboardController.getJobCountOfUserForStatus(UserCode, COMPLETED_BY_PROCESSING);

                int countOfComppletedProcessingAllUsers = 0;
                countOfComppletedProcessingAllUsers = dashboardController.getCurrentSummaryOfStatus(COMPLETED_BY_PROCESSING);

                double percentageOfCompletedByProcessing = 0;

                percentageOfCompletedByProcessing = (int)Math.Round((double)(countOfCOmpletedByProcessingByUser * 100) / countOfComppletedProcessingAllUsers);


                if (percentageOfCompletedByProcessing < 0) percentageOfCompletedByProcessing = 0;

                //onerror=\"if (this.src != 'photos/0.jpg' && this.attribute('src') != 'photos/0.jpg') this.src = 'photos/0.jpg';\"

                ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                            "<img u=\"image\" src=\"photos/" + epf + ".jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                            "<div u=\"caption\" t=\"CLIP|LR\" t2=\"B\" du=\"2000\" class=\"captionOrange\" style=\"position: absolute; left: 20px; top: 500px; width: 500px; height: 30px;\">" +
                                            "    " + userName + "" +
                                            "</div>" +
                                            "<div u=\"caption\" t=\"MCLIP|T\" t2=\"T\" d=\"-450\" style=\"position: absolute; left: 605px; top: 40px; width: 400px; height: 30px; font-size: 18px; color: #fff; line-height: 30px; text-align: center;\">Task &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Value</div>" +
                                           " <div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 90px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfCOmpletedByProcessingByUser + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 140px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By All Users&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfComppletedProcessingAllUsers + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 190px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Percentage of User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + percentageOfCompletedByProcessing.ToString() + "%</div>" +
                                        " </div>";

            }


        }


        /////////////


        private void loadProcessValidationUsersSummary()
        {
            string generatedText = "";
            string userGroup = "47,49";
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select t.user_code,t.user_name,t.EPF_NO from wf_admin_users t where t.user_role_code in ( " + userGroup + ") ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();

            ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                          "<img u=\"image\" src=\"photos/valid_unit.jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                       " </div>";

            while (dr.Read())
            {

                loadPrcessValidateDataOfUser(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }



        private void loadPrcessValidateDataOfUser(string UserCode, string userName, string epf)
        {
            string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();


            if (UserCode != "")
            {
                DashboardController dashboardController = new DashboardController();

                int countOfCOmpletedByProcessValidateByUser = dashboardController.getJobCountOfUserForStatus(UserCode, APPROVED_BY_VALIDATORS);

                int countOfComppletedProcessValidateAllUsers = 0;
                countOfComppletedProcessValidateAllUsers = dashboardController.getCurrentSummaryOfStatus(APPROVED_BY_VALIDATORS);

                double percentageOfCompletedByProcessing = 0;

                percentageOfCompletedByProcessing = (int)Math.Round((double)(countOfCOmpletedByProcessValidateByUser * 100) / countOfComppletedProcessValidateAllUsers);


                if (percentageOfCompletedByProcessing < 0) percentageOfCompletedByProcessing = 0;

                //onerror=\"if (this.src != 'photos/0.jpg' && this.attribute('src') != 'photos/0.jpg') this.src = 'photos/0.jpg';\"

                ltrlSummary.Text = ltrlSummary.Text + "<div>" +
                                            "<img u=\"image\" src=\"photos/" + epf + ".jpg\" onerror=\"if (this.src != 'photos/0.jpg') this.src = 'photos/0.jpg';\" />" +
                                            "<div u=\"caption\" t=\"CLIP|LR\" t2=\"B\" du=\"2000\" class=\"captionOrange\" style=\"position: absolute; left: 20px; top: 500px; width: 500px; height: 30px;\">" +
                                            "    " + userName + "" +
                                            "</div>" +
                                            "<div u=\"caption\" t=\"MCLIP|T\" t2=\"T\" d=\"-450\" style=\"position: absolute; left: 605px; top: 40px; width: 400px; height: 30px; font-size: 18px; color: #fff; line-height: 30px; text-align: center;\">Task &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Value</div>" +
                                           " <div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 90px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfCOmpletedByProcessValidateByUser + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 140px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Count By All Users&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + countOfComppletedProcessValidateAllUsers + "</div>" +
                                           "<div u=\"caption\" t=\"MCLIP|R\" d=\"-300\" style=\"position: absolute; left: 525px; top: 190px; width: 450px; height: 30px; font-size: 18px; color: #fff; line-height: 30px;\">Completed Percentage of User&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + percentageOfCompletedByProcessing.ToString() + "%</div>" +
                                        " </div>";

            }


        }

    }
}