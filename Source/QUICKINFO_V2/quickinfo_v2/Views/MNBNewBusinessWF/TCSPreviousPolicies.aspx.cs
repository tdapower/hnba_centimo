using quickinfo_v2.Controllers.Quotation;
using quickinfo_v2.Models.Quotation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.MNBNewBusinessWF
{
    public partial class TCSPreviousPolicies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["VehicleChassisNo"] != null)
            {



                loadPreviousPolicyDetails(Session["VehicleChassisNo"].ToString());

            }
        }



        private void loadPreviousPolicyDetails(string VehicleChassisNo)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "  select  t.pol_no,t.pol_status,t.pol_client,t.pol_start_date,t.pol_end_date from crc_policy t  where t.pol_reg_no like '" + VehicleChassisNo + "' or t.POL_CHASSIS_NO like '" + VehicleChassisNo + "'   ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ltrlData.Text = "<table><thead><tr>";
                ltrlData.Text = ltrlData.Text + "<th>Policy No</th>";
                ltrlData.Text = ltrlData.Text + "<th>Status</th>";
                ltrlData.Text = ltrlData.Text + "<th>Customer Name</th>";
                ltrlData.Text = ltrlData.Text + "<th>Start Date</th>";
                ltrlData.Text = ltrlData.Text + "<th>End Date</th>";
                ltrlData.Text = ltrlData.Text + "</tr>";
                ltrlData.Text = ltrlData.Text + "</thead>";
                ltrlData.Text = ltrlData.Text + "<tbody>";



                while (dr.Read())
                {
                    ltrlData.Text = ltrlData.Text + "<tr>";
                    ltrlData.Text = ltrlData.Text + "<td>" + dr[0].ToString() + "</td>";
                    ltrlData.Text = ltrlData.Text + "<td>" + dr[1].ToString() + "</td>";
                    ltrlData.Text = ltrlData.Text + "<td>" + dr[2].ToString() + "</td>";
                    ltrlData.Text = ltrlData.Text + "<td>" + dr[3].ToString().Remove(10) + "</td>";
                    ltrlData.Text = ltrlData.Text + "<td>" + dr[4].ToString().Remove(10) + "</td>";
                    ltrlData.Text = ltrlData.Text + "</tr>";
                }

                ltrlData.Text = ltrlData.Text + "</tbody>";
                ltrlData.Text = ltrlData.Text + "</table>";



            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }





    }
}