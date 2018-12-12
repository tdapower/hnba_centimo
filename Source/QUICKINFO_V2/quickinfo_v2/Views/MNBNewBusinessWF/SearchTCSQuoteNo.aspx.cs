using quickinfo_v2.Controllers.MNBNewBusinessWF;
using quickinfo_v2.Controllers.Quotation;
using quickinfo_v2.Controllers.TCSPolicy;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class SearchTCSQuoteNo : System.Web.UI.Page
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




            if ( (txtSearchProposalNo.Text == ""))
            {

                Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Search text cannot be blank');", true);
                return;
            }


        

            if (txtSearchProposalNo.Text != "")
            {

                SQL = "(LOWER(POL_PROPOSAL_NUMBER) LIKE '%" + txtSearchProposalNo.Text.ToLower() + "%') AND";
            }



            SQL = SQL.Substring(0, SQL.Length - 3);


            TCSPolicyController tCSPolicyController = new TCSPolicyController();


            if (rbtnNonTakaful.Checked)
            {
                grdSearchResults.DataSource = tCSPolicyController.searchTCSPolicies(SQL, "TCS");
            }
            else if (rbtnTakaful.Checked)
            {
                grdSearchResults.DataSource = tCSPolicyController.searchTCSPolicies(SQL, "TAKAFUL");
            }



            if (grdSearchResults.DataSource != null)
            {
                grdSearchResults.DataBind();
            }



            pnlSearchGrid.Visible = true;

        }

        protected void grdSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.Visible = false;
            //  Response.Write("<script>window.close();</script>");
            //  onclick="SetName();"


            //txtSelectedQuoteNo.Text = grdSearchResults.SelectedRow.Cells[1].Text.Trim();

        }


        protected void grdSearchResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            // e.Row.Cells[3].Visible = false;

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


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            Timer1.Enabled = false;
        }

     

    }
}