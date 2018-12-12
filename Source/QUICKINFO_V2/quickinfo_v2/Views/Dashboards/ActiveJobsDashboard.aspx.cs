using quickinfo_v2.Controllers.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Dashboards
{
    public partial class ActiveJobsDashboard : System.Web.UI.Page
    {
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
            }



            LoadNotTakenScrutinizing();
            LoadNotTakenProcessing();
            LoadNotTakenValidating();


            LoadTakenScrutinizing();
            LoadTakenProcessing();
            LoadTakenValidating();


           // Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('" + GetUserIP() + "');", true);
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
            //LoadNotTakenScrutinizing();
            //LoadNotTakenProcessing();
            //LoadNotTakenValidating();

            //LoadTakenScrutinizing();
            //LoadTakenProcessing();
            //LoadTakenValidating();
        }



        private void LoadNotTakenScrutinizing()
        {
            grdNotTakenScrutinizing.DataSource = null;
            grdNotTakenScrutinizing.DataBind();

            string INITIALStatusName = System.Configuration.ConfigurationManager.AppSettings["INITIAL"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdNotTakenScrutinizing.DataSource = proposalUploadController.GetQuotationNosOfStatusWithoutCancellations(INITIALStatusName);


            if (grdNotTakenScrutinizing.DataSource != null)
            {
                grdNotTakenScrutinizing.DataBind();
                updPnlNotTakenScrutinizing.Update();
            }



        }
        protected void grdNotTakenScrutinizing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Scrutinizing";
            }

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }
        private void LoadNotTakenProcessing()
        {
            grdNotTakenProcessing.DataSource = null;
            grdNotTakenProcessing.DataBind();

            string APPROVED_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_SCRUTINIZING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdNotTakenProcessing.DataSource = proposalUploadController.GetQuotationNosOfStatusWithoutCancellations(APPROVED_BY_SCRUTINIZING);


            if (grdNotTakenProcessing.DataSource != null)
            {
                grdNotTakenProcessing.DataBind();
                updPnlNotTakenProcessing.Update();
            }



        }
        protected void grdNotTakenProcessing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Processing";
            }


            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }
        private void LoadNotTakenValidating()
        {
            grdNotTakenValidating.DataSource = null;
            grdNotTakenValidating.DataBind();

            string COMPLETED_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["COMPLETED_BY_PROCESSING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdNotTakenValidating.DataSource = proposalUploadController.GetQuotationNosOfStatusWithoutCancellations(COMPLETED_BY_PROCESSING);


            if (grdNotTakenValidating.DataSource != null)
            {
                grdNotTakenValidating.DataBind();
                updPnlNotTakenValidating.Update();
            }



        }
        protected void grdNotTakenValidating_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Validating";
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "F")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#5cff23");
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }


        private void LoadTakenScrutinizing()
        {
            grdTakenScrutinizing.DataSource = null;
            grdTakenScrutinizing.DataBind();

            string TAKEN_BY_SCRUTINIZING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_SCRUTINIZING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdTakenScrutinizing.DataSource = proposalUploadController.GetQuotationNosOfStatusWithUser(TAKEN_BY_SCRUTINIZING);


            if (grdTakenScrutinizing.DataSource != null)
            {
                grdTakenScrutinizing.DataBind();
                updPnlTakenScrutinizing.Update();
            }



        }
        protected void grdTakenScrutinizing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Scrutinizing";
            }

            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }
        private void LoadTakenProcessing()
        {
            grdTakenProcessing.DataSource = null;
            grdTakenProcessing.DataBind();

            string TAKEN_BY_PROCESSING = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_PROCESSING"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdTakenProcessing.DataSource = proposalUploadController.GetQuotationNosOfStatusWithUser(TAKEN_BY_PROCESSING);


            if (grdTakenProcessing.DataSource != null)
            {
                grdTakenProcessing.DataBind();
                updPnlTakenProcessing.Update();
            }



        }
        protected void grdTakenProcessing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Processing";
            }


            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }
        private void LoadTakenValidating()
        {
            grdTakenValidating.DataSource = null;
            grdTakenValidating.DataBind();

            string TAKEN_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["TAKEN_BY_VALIDATORS"].ToString();

            ProposalUploadController proposalUploadController = new ProposalUploadController();

            grdTakenValidating.DataSource = proposalUploadController.GetQuotationNosOfStatusWithUser(TAKEN_BY_VALIDATORS);


            if (grdTakenValidating.DataSource != null)
            {
                grdTakenValidating.DataBind();
                updPnlTakenValidating.Update();
            }



        }
        protected void grdTakenValidating_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Validating";
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "N")
                {
                    e.Row.BackColor = System.Drawing.Color.Aqua;
                }
                else if (e.Row.Cells[1].Text == "F")
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#5cff23");
                }
                else if (e.Row.Cells[1].Text == "E")
                {
                    if (e.Row.Cells[2].Text == "1")
                    {
                        e.Row.BackColor = System.Drawing.Color.Pink;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else if (e.Row.Cells[1].Text == "R")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;
                }
                else if (e.Row.Cells[1].Text == "C")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }

            }
        }
    }
}