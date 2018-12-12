using quickinfo_v2.Controllers.Dashboard;
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
    public partial class UserSummaryDashboard2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    loadScrutinizeUsers("45");
            //    loadProcessUsers("46");
            //    loadProcessValidateUsers("47,49");
            //}
        }
        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            //string ScrutinizeUsersRoleCode = System.Configuration.ConfigurationManager.AppSettings["ScrutinizeUsersRoleCode"].ToString();
            //string ProcessUsersRoleCode = System.Configuration.ConfigurationManager.AppSettings["ProcessUsersRoleCode"].ToString();


            //loadScrutinizeUsers("45");
            //loadProcessUsers("46");
            //loadProcessValidateUsers("47,49");

        }


        //private void loadScrutinizeUsers(string userGroup)
        //{
        //    lstvScrutinizers.DataSource = null;
        //    lstvScrutinizers.DataBind();

        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    con.Open();

        //    String selectQuery = "";
        //    selectQuery = "select t.user_code,t.user_name from wf_admin_users t " +
        //        "where t.user_role_code=" + userGroup + " ";

        //    //selectQuery = "select t.user_code,t.user_name, " +
        //    //        " (select count(f.user_code) from mnbq_proposal_upload_followup f " +
        //    //             " where f.user_code=t.user_code and f.status='APPROVED_BY_SCRUTINIZING'  " +
        //    //             " and to_date(f.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') group by f.user_code ) as \"Approved\", " +
        //    //        " (select count(f.user_code) from mnbq_proposal_upload_followup f " +
        //    //             " where f.user_code=t.user_code and f.status='REJECTED_BY_SCRUTINIZING'  " +
        //    //             " and to_date(f.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') group by f.user_code ) as \"Rejected\" " +
        //    //         " from wf_admin_users t " +
        //    //                       " where t.user_role_code=" + userGroup + " ";



        //    //selectQuery = " select t.status,count(t.status) as \"count\" from mnbq_proposal_upload t " +
        //    //    " where t.entered_user_branch_code='" + userBranch + "' and to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
        //    //                " group by t.status ";


        //    OracleDataAdapter da = new OracleDataAdapter(selectQuery, con);
        //    DataTable dt = new DataTable();

        //    da.Fill(dt);
        //    con.Close();







        //    lstvScrutinizers.DataSource = dt;
        //    lstvScrutinizers.DataBind();

        //}


        //protected void lstvScrutinizers_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
        //    string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();

        //    string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();



        //    Literal ltrlSummary;
        //    TextBox txtUserCode = (TextBox)e.Item.FindControl("txtUserCode");



        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {

        //        if (txtUserCode.Text != "")
        //        {
        //            DashboardController dashboardController = new DashboardController();

        //            DataTable dtSummary = dashboardController.getCurrentSummaryOfUser(txtUserCode.Text);


        //            int countOfTakenByScrutinizingAllUsers = 0;
        //            countOfTakenByScrutinizingAllUsers = dashboardController.getCurrentSummaryOfStatus(TAKEN_BY_SCRUTINIZING);


        //            int countOfTakenByScrutinizing = 0;
        //            int countOfCOmpletedByScrutinizing = 0;

        //            int countOfRejected = 0;
        //            int countOfScrutinizing = 0;


        //            DataView dv = new DataView(dtSummary);
        //            dv.Sort = "STATUS";


        //            int index1_2 = dv.Find(TAKEN_BY_SCRUTINIZING);
        //            if (index1_2 == -1)
        //            {
        //                countOfTakenByScrutinizing = 0;
        //            }
        //            else
        //            {
        //                countOfTakenByScrutinizing = Convert.ToInt32(dv[index1_2]["count"].ToString());
        //            }


        //            int index2_1 = dv.Find(APPROVED_BY_SCRUTINIZING);
        //            if (index2_1 == -1)
        //            {
        //                countOfCOmpletedByScrutinizing = 0;
        //            }
        //            else
        //            {
        //                countOfCOmpletedByScrutinizing = Convert.ToInt32(dv[index2_1]["count"].ToString());
        //            }

        //            int index4 = dv.Find(REJECTED_BY_SCRUTINIZING);
        //            if (index4 == -1)
        //            {
        //                countOfRejected = 0;
        //            }
        //            else
        //            {
        //                countOfRejected = Convert.ToInt32(dv[index4]["count"].ToString());
        //            }

        //            countOfScrutinizing = countOfCOmpletedByScrutinizing + countOfRejected;

        //            double percentageOfApprovedByScrutinizing = 0;
        //            double percentageOfRejected = 0;
        //            double percentageOfUserTotalTaken = 0;


        //            percentageOfApprovedByScrutinizing = (int)Math.Round((double)(countOfCOmpletedByScrutinizing * 100) / countOfScrutinizing);
        //            percentageOfRejected = (int)Math.Round((double)(countOfRejected * 100) / countOfScrutinizing);


        //            percentageOfUserTotalTaken = (int)Math.Round((double)(countOfScrutinizing * 100) / countOfTakenByScrutinizingAllUsers);


        //            if (percentageOfApprovedByScrutinizing < 0) percentageOfApprovedByScrutinizing = 0;
        //            if (percentageOfRejected < 0) percentageOfRejected = 0;
        //            if (percentageOfUserTotalTaken < 0) percentageOfUserTotalTaken = 0;

        //            ltrlSummary = (Literal)e.Item.FindControl("ltrlSummary");

        //            ltrlSummary.Text = " <div class=\"progress\">" +
        //                                                       "   <div class=\"progress-bar progress-bar-success progress-bar-striped\" role=\"progressbar\"" +
        //                                                          "    aria-valuenow=\"" + percentageOfApprovedByScrutinizing + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percentageOfApprovedByScrutinizing + "%\">" +
        //                                                          "    " + percentageOfApprovedByScrutinizing + "% (" + countOfCOmpletedByScrutinizing + ")" +
        //                                                        "  </div>" +
        //                                                  "  </div>" +
        //                                                 " <div class=\"progress\">" +
        //                                                      "   <div class=\"progress-bar progress-bar-danger progress-bar-striped\" role=\"progressbar\"" +
        //                                                         "    aria-valuenow=\"" + percentageOfRejected + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percentageOfRejected + "%\">" +
        //                                                         "    " + percentageOfRejected + "% (" + countOfRejected + ")" +
        //                                                       "  </div>" +
        //                                                 "  </div>" +
        //                                             " <div class=\"progress\">" +
        //                                                   "   <div class=\"progress-bar progress-bar-info  progress-bar-striped\" role=\"progressbar\"" +
        //                                                      "    aria-valuenow=\"" + percentageOfUserTotalTaken + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percentageOfUserTotalTaken + "%\">" +
        //                                                      "    " + percentageOfUserTotalTaken + "% (" + countOfScrutinizing + ")" +
        //                                                    "  </div>" +
        //                                              "  </div>";


        //        }

        //    }
        //}

        //protected void lstvProcessers_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();
        //    string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();



        //    Literal ltrlSummary;
        //    TextBox txtUserCode = (TextBox)e.Item.FindControl("txtUserCode");



        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {

        //        if (txtUserCode.Text != "")
        //        {
        //            DashboardController dashboardController = new DashboardController();

        //            DataTable dtSummary = dashboardController.getCurrentSummaryOfUser(txtUserCode.Text);


        //            int countOfTakenByProcessingAllUsers = 0;
        //            countOfTakenByProcessingAllUsers = dashboardController.getCurrentSummaryOfStatus(COMPLETED_BY_PROCESSING);


        //            int countOfCOmpletedByProcessing = 0;


        //            DataView dv = new DataView(dtSummary);
        //            dv.Sort = "STATUS";


        //            int index1_2 = dv.Find(COMPLETED_BY_PROCESSING);
        //            if (index1_2 == -1)
        //            {
        //                countOfCOmpletedByProcessing = 0;
        //            }
        //            else
        //            {
        //                countOfCOmpletedByProcessing = Convert.ToInt32(dv[index1_2]["count"].ToString());
        //            }



        //            double percentageOfCompletedByProcessing = 0;

        //            percentageOfCompletedByProcessing = (int)Math.Round((double)(countOfCOmpletedByProcessing * 100) / countOfTakenByProcessingAllUsers);


        //            if (percentageOfCompletedByProcessing < 0) percentageOfCompletedByProcessing = 0;

        //            ltrlSummary = (Literal)e.Item.FindControl("ltrlSummary");

        //            ltrlSummary.Text = " <div class=\"progress\">" +
        //                                                       "   <div class=\"progress-bar progress-bar-success progress-bar-striped\" role=\"progressbar\"" +
        //                                                          "    aria-valuenow=\"" + percentageOfCompletedByProcessing + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percentageOfCompletedByProcessing + "%\">" +
        //                                                          "    " + percentageOfCompletedByProcessing + "% (" + countOfCOmpletedByProcessing + ")" +
        //                                                        "  </div>" +
        //                                                  "  </div>";

        //        }

        //    }
        //}
        //protected void lstvProessValidators_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();



        //    Literal ltrlSummary;
        //    TextBox txtUserCode = (TextBox)e.Item.FindControl("txtUserCode");



        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {

        //        if (txtUserCode.Text != "")
        //        {
        //            DashboardController dashboardController = new DashboardController();

        //            DataTable dtSummary = dashboardController.getCurrentSummaryOfUser(txtUserCode.Text);


        //            int countOfApprovedByProcessValidatorsAllUsers = 0;
        //            countOfApprovedByProcessValidatorsAllUsers = dashboardController.getCurrentSummaryOfStatus(APPROVED_BY_VALIDATORS);


        //            int countOfCOmpletedByProcessValidators = 0;


        //            DataView dv = new DataView(dtSummary);
        //            dv.Sort = "STATUS";


        //            int index1_2 = dv.Find(APPROVED_BY_VALIDATORS);
        //            if (index1_2 == -1)
        //            {
        //                countOfCOmpletedByProcessValidators = 0;
        //            }
        //            else
        //            {
        //                countOfCOmpletedByProcessValidators = Convert.ToInt32(dv[index1_2]["count"].ToString());
        //            }



        //            double percentageOfCompletedByProcessValidators = 0;

        //            percentageOfCompletedByProcessValidators = (int)Math.Round((double)(countOfCOmpletedByProcessValidators * 100) / countOfApprovedByProcessValidatorsAllUsers);


        //            if (percentageOfCompletedByProcessValidators < 0) percentageOfCompletedByProcessValidators = 0;


        //            ltrlSummary = (Literal)e.Item.FindControl("ltrlSummary");

        //            ltrlSummary.Text = " <div class=\"progress\">" +
        //                                                       "   <div class=\"progress-bar progress-bar-success progress-bar-striped\" role=\"progressbar\"" +
        //                                                          "    aria-valuenow=\"" + percentageOfCompletedByProcessValidators + "\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width: " + percentageOfCompletedByProcessValidators + "%\">" +
        //                                                          "    " + percentageOfCompletedByProcessValidators + "% (" + countOfCOmpletedByProcessValidators + ")" +
        //                                                        "  </div>" +
        //                                                  "  </div>";

        //        }

        //    }
        //}
        //private void loadProcessUsers(string userGroup)
        //{
        //    lstvProcessers.DataSource = null;
        //    lstvProcessers.DataBind();

        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    con.Open();

        //    String selectQuery = "";
        //    selectQuery = "select t.user_code,t.user_name from wf_admin_users t where t.user_role_code=" + userGroup + " ";

        //    OracleDataAdapter da = new OracleDataAdapter(selectQuery, con);
        //    DataTable dt = new DataTable();

        //    da.Fill(dt);
        //    con.Close();

        //    lstvProcessers.DataSource = dt;
        //    lstvProcessers.DataBind();


        //}


        //private void loadProcessValidateUsers(string userGroup)
        //{
        //    lstvProessValidators.DataSource = null;
        //    lstvProessValidators.DataBind();

        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    con.Open();

        //    String selectQuery = "";
        //    selectQuery = "select t.user_code,t.user_name from wf_admin_users t where t.user_role_code in (" + userGroup + ") ";

        //    OracleDataAdapter da = new OracleDataAdapter(selectQuery, con);
        //    DataTable dt = new DataTable();

        //    da.Fill(dt);
        //    con.Close();

        //    lstvProessValidators.DataSource = dt;
        //    lstvProessValidators.DataBind();


        //}



    }
}