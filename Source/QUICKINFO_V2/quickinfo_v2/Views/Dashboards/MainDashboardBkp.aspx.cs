using quickinfo_v2.Controllers.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Dashboards
{
    public partial class MainDashboardBkp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tmrUpdateTimer_Tick(object sender, EventArgs e)
        {
            loadStatusSummary();
            loadCountOfJobTypes();
            loadZonalSummaryForTheMonth();
        }
        private void loadStatusSummary()
        {


            string RENEWAL_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_ADDED"].ToString();
            string TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD"].ToString();
            string RENEWAL_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_DOCS_ADDED"].ToString();
            string CANCELLATION_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_ADDED"].ToString();
            string TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD"].ToString();
            string CANCELLATION_DOCS_ADDED = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_DOCS_ADDED"].ToString();
            string ENDORSEMENT_ADDED = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_ADDED"].ToString();



            string INITIAL = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();
            string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();
            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();
            string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();

            string REJECTED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["REJECTED_BY_SCRUTINIZING"].ToString();


            string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();


            string COMPLETED_AND_PRINTED = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_AND_PRINTED"].ToString();





            DashboardController dashboardController = new DashboardController();

            DataTable dtSummary = dashboardController.getCurrentSummary();



            int countOfRenewalAdded = 0;
            int countOfTakenByBranchRenewalDocUpld = 0;
            int countOfRenewalDocsAdded = 0;
            int countOfCancellationAdded = 0;
            int countOfTakenByBranchCancellationDocUpld = 0;
            int countOfCancellationDocsAdded = 0;
            int countOfEndorsementAdded = 0;
            int countOfNotInitialised = 0;



            int countOfInitial = 0;
            int countOfTakenByScrutinizing = 0;
            int countOfScrutinizing = 0;


            int countOfCOmpletedByScrutinizing = 0;
            int countOfTakenByProcessing = 0;
            int countOfProcessing = 0;

            int countOfCompletedByProcessing = 0;
            int countOfTakenByValidators = 0;
            int countOfPrinted = 0;
            int countOfValidators = 0;


            int countOfRejected = 0;
            int countOfCompleted = 0;
            int countOfUnknown = 0;

            DataView dv = new DataView(dtSummary);
            dv.Sort = "STATUS";

            //////////////


            int index0_1 = dv.Find(RENEWAL_ADDED);
            if (index0_1 == -1)
            {
                countOfRenewalAdded = 0;
            }
            else
            {
                countOfRenewalAdded = Convert.ToInt32(dv[index0_1]["count"].ToString());
            }
            int index0_2 = dv.Find(TAKEN_BY_BRANCH_RENEWAL_DOC_UPLD);
            if (index0_2 == -1)
            {
                countOfTakenByBranchRenewalDocUpld = 0;
            }
            else
            {
                countOfTakenByBranchRenewalDocUpld = Convert.ToInt32(dv[index0_2]["count"].ToString());
            }
            int index0_3 = dv.Find(RENEWAL_DOCS_ADDED);
            if (index0_3 == -1)
            {
                countOfRenewalDocsAdded = 0;
            }
            else
            {
                countOfRenewalDocsAdded = Convert.ToInt32(dv[index0_3]["count"].ToString());
            }
            int index0_4 = dv.Find(CANCELLATION_ADDED);
            if (index0_4 == -1)
            {
                countOfCancellationAdded = 0;
            }
            else
            {
                countOfCancellationAdded = Convert.ToInt32(dv[index0_4]["count"].ToString());
            }
            int index0_5 = dv.Find(TAKEN_BY_BRANCH_CANCELLATION_DOC_UPLD);
            if (index0_5 == -1)
            {
                countOfTakenByBranchCancellationDocUpld = 0;
            }
            else
            {
                countOfTakenByBranchCancellationDocUpld = Convert.ToInt32(dv[index0_5]["count"].ToString());
            }
            int index0_6 = dv.Find(CANCELLATION_DOCS_ADDED);
            if (index0_6 == -1)
            {
                countOfCancellationDocsAdded = 0;
            }
            else
            {
                countOfCancellationDocsAdded = Convert.ToInt32(dv[index0_6]["count"].ToString());
            }
            int index0_7 = dv.Find(ENDORSEMENT_ADDED);
            if (index0_7 == -1)
            {
                countOfEndorsementAdded = 0;
            }
            else
            {
                countOfEndorsementAdded = Convert.ToInt32(dv[index0_7]["count"].ToString());
            }

            countOfUnknown = dashboardController.getCurrentSummaryOfUnknown();

            countOfNotInitialised = countOfRenewalAdded + countOfTakenByBranchRenewalDocUpld +
                countOfRenewalDocsAdded + countOfCancellationAdded + countOfTakenByBranchCancellationDocUpld + countOfCancellationDocsAdded + countOfEndorsementAdded;




            ////////
            int index1_1 = dv.Find(INITIAL);
            if (index1_1 == -1)
            {
                countOfInitial = 0;
            }
            else
            {
                countOfInitial = Convert.ToInt32(dv[index1_1]["count"].ToString());
            }


            int index1_2 = dv.Find(TAKEN_BY_SCRUTINIZING);
            if (index1_2 == -1)
            {
                countOfTakenByScrutinizing = 0;
            }
            else
            {
                countOfTakenByScrutinizing = Convert.ToInt32(dv[index1_2]["count"].ToString());
            }


            countOfScrutinizing = countOfInitial + countOfTakenByScrutinizing;



            //////////

            int index2_1 = dv.Find(APPROVED_BY_SCRUTINIZING);
            if (index2_1 == -1)
            {
                countOfCOmpletedByScrutinizing = 0;
            }
            else
            {
                countOfCOmpletedByScrutinizing = Convert.ToInt32(dv[index2_1]["count"].ToString());
            }


            int index2_2 = dv.Find(TAKEN_BY_PROCESSING);
            if (index2_2 == -1)
            {
                countOfTakenByProcessing = 0;
            }
            else
            {
                countOfTakenByProcessing = Convert.ToInt32(dv[index2_2]["count"].ToString());
            }
            countOfProcessing = countOfCOmpletedByScrutinizing + countOfTakenByProcessing;



            //////////
            int index3_1 = dv.Find(COMPLETED_BY_PROCESSING);
            if (index3_1 == -1)
            {
                countOfCompletedByProcessing = 0;
            }
            else
            {
                countOfCompletedByProcessing = Convert.ToInt32(dv[index3_1]["count"].ToString());
            }

            int index3_2 = dv.Find(TAKEN_BY_VALIDATORS);
            if (index3_2 == -1)
            {
                countOfTakenByValidators = 0;
            }
            else
            {
                countOfTakenByValidators = Convert.ToInt32(dv[index3_2]["count"].ToString());
            }

            countOfValidators = countOfCompletedByProcessing + countOfTakenByValidators;



            //////////
            int index4 = dv.Find(REJECTED_BY_SCRUTINIZING);
            if (index4 == -1)
            {
                countOfRejected = 0;
            }
            else
            {
                countOfRejected = Convert.ToInt32(dv[index4]["count"].ToString());
            }
            ////////////////
            int index5 = dv.Find(APPROVED_BY_VALIDATORS);
            if (index5 == -1)
            {
                countOfCompleted = 0;
            }
            else
            {
                countOfCompleted = Convert.ToInt32(dv[index5]["count"].ToString());
            }
            ////////////////
            int index6 = dv.Find(COMPLETED_AND_PRINTED);
            if (index6 == -1)
            {
                countOfPrinted = 0;
            }
            else
            {
                countOfPrinted = Convert.ToInt32(dv[index6]["count"].ToString());
            }

            countOfCompleted = countOfCompleted + countOfPrinted;//completed and printed



            ltrlNotInitialised.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfNotInitialised.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfNotInitialised.ToString() + "</div>";

            ltrlScrutinize.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfScrutinizing.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfScrutinizing.ToString() + "</div>";
            ltrlProcess.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfProcessing.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfProcessing.ToString() + " </div>";
            ltrlProcessValidate.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfValidators.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfValidators.ToString() + "</div>";
            ltrlReject.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfRejected.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfRejected.ToString() + "</div>";
            ltrlComplete.Text = " <div class=\"num\" data-start=\"0\" data-end=" + countOfCompleted.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfCompleted.ToString() + " </div>";
            ltrlUnknown.Text = " <div  class=\"num\"  data-start=\"0\"   data-end=" + countOfUnknown.ToString() + " data-postfix=\"\" data-duration=\"1500\" data-delay=\"1800\">" + countOfUnknown.ToString() + "</div>";




            int totalOfStages = 0;
            totalOfStages = countOfScrutinizing + countOfProcessing + countOfValidators + countOfRejected + countOfCompleted + countOfUnknown;


            double averageOfScrutinizing = 0;
            double averageOfProcessing = 0;
            double averageOfValidators = 0;
            double averageOfRejected = 0;
            double averageOfCompleted = 0;
            double averageOfUnknown = 0;


            averageOfScrutinizing = (int)Math.Round((double)(countOfScrutinizing * 100) / totalOfStages);
            averageOfProcessing = (int)Math.Round((double)(countOfProcessing * 100) / totalOfStages);
            averageOfValidators = (int)Math.Round((double)(countOfValidators * 100) / totalOfStages);
            averageOfRejected = (int)Math.Round((double)(countOfRejected * 100) / totalOfStages);
            averageOfCompleted = (int)Math.Round((double)(countOfCompleted * 100) / totalOfStages);
            averageOfUnknown = (int)Math.Round((double)(countOfUnknown * 100) / totalOfStages);

            if (averageOfScrutinizing < 0) averageOfScrutinizing = 0;
            if (averageOfProcessing < 0) averageOfProcessing = 0;
            if (averageOfValidators < 0) averageOfValidators = 0;
            if (averageOfRejected < 0) averageOfRejected = 0;
            if (averageOfCompleted < 0) averageOfCompleted = 0;
            if (averageOfUnknown < 0) averageOfUnknown = 0;



            //For summery chart
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Stage");
            dt.Columns.Add("Count");
            dt.Rows.Add(new object[] { "Scrutinizing", averageOfScrutinizing });
            dt.Rows.Add(new object[] { "Processing", averageOfProcessing });
            dt.Rows.Add(new object[] { "Validators", averageOfValidators });
            dt.Rows.Add(new object[] { "Completed", averageOfCompleted });
            dt.Rows.Add(new object[] { "Rejected", averageOfRejected });
            dt.Rows.Add(new object[] { "Unknown", averageOfUnknown });

            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            chartJobTypeCountSummery.Series[0].Points.DataBindXY(x, y);
            chartJobTypeCountSummery.Series[0].ChartType = SeriesChartType.Pie;
            chartJobTypeCountSummery.ChartAreas["ChartAreachartJobTypeCountSummery"].Area3DStyle.Enable3D = true;

            //chartJobTypeCountSummery.Series[0].IsValueShownAsLabel = true;
            chartJobTypeCountSummery.Series[0].Font = new Font("Verdana", 15f, FontStyle.Regular);


            for (int i = 0; i < chartJobTypeCountSummery.Series[0].Points.Count; i++)
            {
                if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Scrutinizing")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Purple;
                }
                else if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Processing")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Orange;
                }
                else if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Validators")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Aqua;
                }
                else if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Completed")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Green;
                }
                else if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Rejected")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Red;
                }
                else if (chartJobTypeCountSummery.Series[0].Points[i].AxisLabel == "Unknown")
                {
                    chartJobTypeCountSummery.Series[0].Points[i].Color = Color.Brown;
                }


            }



            chartJobTypeCountSummery.Series[0]["PieLabelStyle"] = "Outside";

            // Set border width so that labels are shown on the outside
            chartJobTypeCountSummery.Series[0].BorderWidth = 1;
            chartJobTypeCountSummery.Series[0].BorderColor = System.Drawing.Color.FromArgb(255, 255, 255);



            // Add a legend to the chart and dock it to the bottom-center
            chartJobTypeCountSummery.Legends.Add("Legend1");
            chartJobTypeCountSummery.Legends[0].Enabled = true;
            chartJobTypeCountSummery.Legends[0].Docking = Docking.Bottom;
            chartJobTypeCountSummery.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            chartJobTypeCountSummery.Legends[0].Font = new Font("Verdana", 15f, FontStyle.Regular);

            // Set the legend to display pie chart values as percentages
            // Again, the P2 indicates a precision of 2 decimals
            chartJobTypeCountSummery.Series[0].LegendText = "#VALX";
            chartJobTypeCountSummery.Series[0].Label = "#PERCENT{P2}";

            // By sorting the data points, they show up in proper ascending order in the legend
            chartJobTypeCountSummery.DataManipulator.Sort(PointSortOrder.Descending, chartJobTypeCountSummery.Series[0]);
            ////////////////
        }

        private void loadZonalSummaryForTheMonth()
        {
            grdZonalSummaryForTheMonth.DataSource = null;
            grdZonalSummaryForTheMonth.DataBind();

            DashboardController dashboardController = new DashboardController();

            grdZonalSummaryForTheMonth.DataSource = dashboardController.getZonalSummaryForTheMonth();


            if (grdZonalSummaryForTheMonth.DataSource != null)
            {
                grdZonalSummaryForTheMonth.DataBind();
            }

        }
        private void loadCountOfJobTypes()
        {
            //string generatedText = "";


            //DashboardController dashboardController = new DashboardController();

            //generatedText = dashboardController.loadCountOfJobTypes();

            //string scriptText = "";

            //  scriptText = "alert('aaaa')";



            //scriptText = "\n" +
            //   "\n$(document).ready(function () {" +
            //                     "\n         // Bar Charts" +
            //    "\n Morris.Bar({" +
            //    "\n     element: 'chart4'," +
            //    "\n     axes: true," +
            //    "\n     data: [" +
            //   generatedText +
            //   "\n      ]," +
            //    "\n     xkey: 'x'," +
            //     "\n    ykeys: ['y']," +
            //     "\n    labels: ['']," +
            //     "\n    barColors: ['#702070']" +
            //   "\n  });" +
            //     " \n });" +
            //    " \n function data(offset) {" +
            //     " \n    var ret = [];" +
            //     " \n    for (var x = 0; x <= 360; x += 10) {" +
            //     "  \n       var v = (offset + x) % 360;" +
            //      " \n       ret.push({" +
            //      "  \n          x: x," +
            //       " \n          y: Math.sin(Math.PI * v / 180).toFixed(4)," +
            //       " \n          z: Math.cos(Math.PI * v / 180).toFixed(4)," +
            //      " \n       });" +
            //      " \n   }" +
            //      " \n   return ret;" +
            //     "\n }";


            //scriptText = "\n " +
            //    "\n document.getElementById('form1').reset();" +
            //   "\nSys.Application.add_load(function () {" +
            //                     "\n         // Bar Charts" +
            //    "\n Morris.Bar({" +
            //    "\n     element: 'chart4'," +
            //    "\n     axes: true," +
            //    "\n     data: [" +
            //   generatedText +
            //   "\n      ]," +
            //    "\n     xkey: 'x'," +
            //     "\n    ykeys: ['y']," +
            //     "\n    labels: ['']," +
            //     "\n    barColors: ['#702070']" +
            //   "\n  });" +
            //     " \n });" +
            //    " \n function data(offset) {" +
            //     " \n    var ret = [];" +
            //     " \n    for (var x = 0; x <= 360; x += 10) {" +
            //     "  \n       var v = (offset + x) % 360;" +
            //      " \n       ret.push({" +
            //      "  \n          x: x," +
            //       " \n          y: Math.sin(Math.PI * v / 180).toFixed(4)," +
            //       " \n          z: Math.cos(Math.PI * v / 180).toFixed(4)," +
            //      " \n       });" +
            //      " \n   }" +
            //      " \n   return ret;" +
            //     "\n }";



            //   //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegisterClientScriptBlock3", scriptText, false);

            ////   ScriptManager.RegisterClientScriptBlock(ScriptManager2, this.GetType(), "RegisterClientScriptBlock3", scriptText, true);
            //   ScriptManager.RegisterClientScriptBlock(this,typeof(Page),"ToggleScript",scriptText,true);




            DashboardController dashboardController = new DashboardController();



            DataTable dt = dashboardController.loadDTCountOfJobTypes();
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            chartJobTypeCount.Series[0].Points.DataBindXY(x, y);
            chartJobTypeCount.Series[0].ChartType = SeriesChartType.Column;
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].Area3DStyle.Enable3D = false;
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisX.LabelStyle.Font = new Font("Verdana", 13f, FontStyle.Regular);
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisX.LabelStyle.ForeColor = Color.White;
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisX.LabelStyle.Angle = -60;


            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisY.LabelStyle.Enabled = false;
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisY.LabelStyle.Font = new Font("Verdana", 15f, FontStyle.Regular);
            chartJobTypeCount.ChartAreas["ChartAreachartJobTypeCount"].AxisY.LabelStyle.ForeColor = Color.White;

            chartJobTypeCount.Series[0].IsValueShownAsLabel = true;
            //chartJobTypeCount.Series[0].Font = new Font("Verdana", 15f, FontStyle.Regular);

            chartJobTypeCount.Series[0].BorderWidth = 3;
            if (chartJobTypeCount.Series[0].Points.Count > 0) chartJobTypeCount.Series[0].Points[0].BorderColor = Color.Red;
            if (chartJobTypeCount.Series[0].Points.Count > 1) chartJobTypeCount.Series[0].Points[1].BorderColor = Color.Yellow;
            if (chartJobTypeCount.Series[0].Points.Count > 2) chartJobTypeCount.Series[0].Points[2].BorderColor = Color.Green;
            if (chartJobTypeCount.Series[0].Points.Count > 3) chartJobTypeCount.Series[0].Points[3].BorderColor = Color.Blue;


            chartJobTypeCount.Series[0].Font = new Font("Verdana", 15f, FontStyle.Regular);

            // By sorting the data points, they show up in proper ascending order in the legend
            chartJobTypeCount.DataManipulator.Sort(PointSortOrder.Descending, chartJobTypeCount.Series[0]);
            ////////////////


            chartJobTypeCount.Series[0].Color = Color.Transparent;

        }

        protected void grdZonalSummaryForTheMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                double total = 0.00;

                //  double.Parse(id) + double.Parse(id2);
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "")
                    {
                        total = total + double.Parse(e.Row.Cells[i].Text);
                    }
                }


                e.Row.Cells[e.Row.Cells.Count - 1].Text = total.ToString();
            }
        }

        protected void grdZonalSummaryForTheMonth_RowCreated(object sender, GridViewRowEventArgs e)
        {
            TableCell tc = new TableCell();
            if (e.Row.RowIndex == -1)
            {
                tc.Text = "Total";
                //  tc.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
                tc.Style.Add(HtmlTextWriterStyle.Color, "white");
                tc.Style.Add(HtmlTextWriterStyle.FontSize, "large");
                tc.Style.Add(HtmlTextWriterStyle.Padding, "8px");
            }
            e.Row.Cells.Add(tc);
        }



    }
}