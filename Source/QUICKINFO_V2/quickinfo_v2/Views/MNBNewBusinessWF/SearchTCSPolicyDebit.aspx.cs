using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Controllers.Quotation;
using quickinfo_v2.Controllers.TCSPolicy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class SearchTCSPolicyDebit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        private void ClearComponents()
        {
            // grdSearchResults.DataSource = null;
            // grdSearchResults.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            ClearComponents();
        }


        private void SearchData()
        {
            string SQL = "";

            grdSearchResults.DataSource = null;
            grdSearchResults.DataBind();




            if ((txtSearchPolicyNo.Text == "") && (txtSearchProposalNo.Text == ""))
            {

                Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Search text cannot be blank');", true);
                return;
            }


            if (txtSearchPolicyNo.Text != "")
            {

                SQL = "(LOWER(pol_no) LIKE '%" + txtSearchPolicyNo.Text.ToLower() + "%') AND";
            }

            if (txtSearchProposalNo.Text != "")
            {

                SQL = "(LOWER(PRO_NO) LIKE '%" + txtSearchProposalNo.Text.ToLower() + "%') AND";
            }


            if (txtSearchVehicleNo.Text != "")
            {

                SQL = "(LOWER(pol_reg_no) LIKE '%" + txtSearchVehicleNo.Text.ToLower() + "%') AND";
            }



            SQL = SQL.Substring(0, SQL.Length - 3);


            TCSPolicyController tCSPolicyController = new TCSPolicyController();



            if (rbtnNonTakaful.Checked)
            {
                grdSearchResults.DataSource = tCSPolicyController.searchTCSPoliciesForCancellation(SQL, "TCS");
            }
            else if (rbtnTakaful.Checked)
            {
                grdSearchResults.DataSource = tCSPolicyController.searchTCSPoliciesForCancellation(SQL, "TAKAFUL");
            }


            if (grdSearchResults.DataSource != null)
            {
                grdSearchResults.DataBind();
            }



            pnlSearchGrid.Visible = true;

        }

        protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            TCSPolicyController tCSPolicyController = new TCSPolicyController();

            txtSelectedPolicyeNo.Text = grdSearchResults.SelectedRow.Cells[2].Text.Trim();

            DataTable dtDebit = tCSPolicyController.getDebitNosOfPolicy(grdSearchResults.SelectedRow.Cells[2].Text.Trim());

            if (dtDebit.Rows.Count > 0)
            {
                chklDebitNumbers.DataSource = dtDebit;
                chklDebitNumbers.DataTextField = "dncn_no";
                chklDebitNumbers.DataValueField = "dncn_no";
                chklDebitNumbers.DataBind();

                chklDebitNumbers.Visible = true;
            }
        }


        protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //  e.Row.Cells[2].Visible = false;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //((Label)e.Row.FindControl("lblName")).Attributes.Add("onclick", "SetName()");

            //    e.Row.Attributes.Add("onclick", "CloseMe(" + e.Row.Cells[2].Text + ")");

            //     //    e.Row.Attributes.Add("onclick", "CloseMe("+  e.Row.RowIndex.ToString()+")");

            //}

            //  if (e.Row.RowType == DataControlRowType.DataRow)
            // {

            //Add onclick attribute to select row.
            // e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference("CloseMe()", "Select$" + e.Row.RowIndex.ToString()));
            //  }
        }
        protected void chklDebitNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedDebitNo"] = chklDebitNumbers.SelectedItem.Value;
            Session["SelectedPolicyeNo"] = txtSelectedPolicyeNo.Text.Trim();

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Timer1.Enabled = false;
        }



    }
}