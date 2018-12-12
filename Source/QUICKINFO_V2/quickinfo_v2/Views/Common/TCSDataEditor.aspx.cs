using quickinfo_v2.Controllers.MNBNewBusinessWF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Common
{
    public partial class TCSDataEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                loadYesNoValues();
                loadPolicyDurationUnits();
                loadCheckedStampDuty();
                loadIsStampDuty();
                loadIsVATCustomer();
                loadPremiumCalculationBasis();
                loadAssuranceCodes();
                loadBusinessParties();

                string partyId = "";
                partyId = "11331920807012015";
                getPartyCode(partyId);
                getNames(partyId);
                getContactDetails(partyId);
                getMemberDetails(partyId);



                string policyId = "";
                policyId = "39213543707012015";
                getPolicyDetails(policyId);
                getPolicyDurationDetails(policyId);
                getPolicyAdditionalDetails(policyId);

                getPolicyPartyDetails(policyId);
                getPolicyRiskPropertyDetails(policyId);


            }
        }
        private void loadPolicyDurationUnits()
        {
            ddlPolicyDurationUnit.Items.Clear();
            ddlPolicyDurationUnit.Items.Add(new ListItem("Year", "G"));
            ddlPolicyDurationUnit.Items.Add(new ListItem("Month", "F"));
            ddlPolicyDurationUnit.Items.Add(new ListItem("Day", "D"));


        }


        private void loadYesNoValues()
        {
            ddlIsPendingRegistration.Items.Clear();
            ddlIsPendingRegistration.Items.Add(new ListItem("Yes", "Y"));
            ddlIsPendingRegistration.Items.Add(new ListItem("No", "N"));

            ddlIsPendingInspection.Items.Clear();
            ddlIsPendingInspection.Items.Add(new ListItem("Yes", "Y"));
            ddlIsPendingInspection.Items.Add(new ListItem("No", "N"));

            ddlIsPendingVIC.Items.Clear();
            ddlIsPendingVIC.Items.Add(new ListItem("Yes", "Y"));
            ddlIsPendingVIC.Items.Add(new ListItem("No", "N"));

            ddlIsPendingPayment.Items.Clear();
            ddlIsPendingPayment.Items.Add(new ListItem("Yes", "Y"));
            ddlIsPendingPayment.Items.Add(new ListItem("No", "N"));

            ddlIsTransferOfOwnership.Items.Clear();
            ddlIsTransferOfOwnership.Items.Add(new ListItem("Yes", "Y"));
            ddlIsTransferOfOwnership.Items.Add(new ListItem("No", "N"));

            ddlIsParticularsOfVehicle.Items.Clear();
            ddlIsParticularsOfVehicle.Items.Add(new ListItem("Yes", "Y"));
            ddlIsParticularsOfVehicle.Items.Add(new ListItem("No", "N"));

            ddlIsLuxurySemi.Items.Clear();
            ddlIsLuxurySemi.Items.Add(new ListItem("Yes", "Y"));
            ddlIsLuxurySemi.Items.Add(new ListItem("No", "N"));

            ddlIsInsSign.Items.Clear();
            ddlIsInsSign.Items.Add(new ListItem("Yes", "Y"));
            ddlIsInsSign.Items.Add(new ListItem("No", "N"));

            ddlIsDulyCompletedProposalForm.Items.Clear();
            ddlIsDulyCompletedProposalForm.Items.Add(new ListItem("Yes", "Y"));
            ddlIsDulyCompletedProposalForm.Items.Add(new ListItem("No", "N"));

            ddlIsCoverNoteIndicator.Items.Clear();
            ddlIsCoverNoteIndicator.Items.Add(new ListItem("Yes", "Y"));
            ddlIsCoverNoteIndicator.Items.Add(new ListItem("No", "N"));

            ddlIsIssueCertificateWithPendingRequirements.Items.Clear();
            ddlIsIssueCertificateWithPendingRequirements.Items.Add(new ListItem("Yes", "Y"));
            ddlIsIssueCertificateWithPendingRequirements.Items.Add(new ListItem("No", "N"));

            ddlIsSpecialApprovalRequired.Items.Clear();
            ddlIsSpecialApprovalRequired.Items.Add(new ListItem("Yes", "Y"));
            ddlIsSpecialApprovalRequired.Items.Add(new ListItem("No", "N"));

            ddlIsCoverNoteExtendingPrivilege.Items.Clear();
            ddlIsCoverNoteExtendingPrivilege.Items.Add(new ListItem("Yes", "Y"));
            ddlIsCoverNoteExtendingPrivilege.Items.Add(new ListItem("No", "N"));


            ddlWhetherTrailerSttached.Items.Clear();
            ddlWhetherTrailerSttached.Items.Add(new ListItem("Yes", "Y"));
            ddlWhetherTrailerSttached.Items.Add(new ListItem("No", "N"));

            ddlExtraFittings.Items.Clear();
            ddlExtraFittings.Items.Add(new ListItem("Yes", "Y"));
            ddlExtraFittings.Items.Add(new ListItem("No", "N"));

            ddlIsRegisteredOwnerOfVehicleYN.Items.Clear();
            ddlIsRegisteredOwnerOfVehicleYN.Items.Add(new ListItem("Yes", "Y"));
            ddlIsRegisteredOwnerOfVehicleYN.Items.Add(new ListItem("No", "N"));
        }


        private void loadCheckedStampDuty()
        {


            ddlCheckedStampDuty.Items.Clear();
            // ddlCheckedStampDuty.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=380195713122013 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlCheckedStampDuty.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();



        }

        private void loadIsStampDuty()
        {

            ddlIsStampDuty.Items.Clear();
            // ddlIsStampDuty.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=214606809022009 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlIsStampDuty.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();

        }


        private void loadIsVATCustomer()
        {



            ddlIsVATCustomer.Items.Clear();
            // ddlIsVATCustomer.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=153166506122007 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlIsVATCustomer.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }

        private void loadPremiumCalculationBasis()
        {


            ddlPremiumCalculationBasis.Items.Clear();
            // ddlPremiumCalculationBasis.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=153196006122007 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlPremiumCalculationBasis.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void loadBusinessParties()
        {
            ddlBusinessParty.Items.Clear();
            // ddlBusinessParty.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=153168206122007 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlBusinessParty.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void loadAssuranceCodes()
        {
            ddlAssuranceCode.Items.Clear();
            // ddlAssuranceCode.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT p.alphanumeric_code_value,p.value_description FROM T_PARAMETER_VALUE P where p.parameter_id=290904623042009 order by p.value_description ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlAssuranceCode.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void getPolicyDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " select t.pol_policy_number,r.rv_meaning  from t_policy t, cg_ref_codes r where t.pol_policy_id=" + policyId + "  and r.rv_low_value=t.pol_policy_status and r.rv_domain='POLICY STATUS' ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtPolicyNo.Text = dr[0].ToString();
                txtCurrentStatus.Text = dr[1].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void getPartyCode(string partyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " select t.pty_party_code from t_party t where t.pty_party_id=" + partyId + "";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtPartyCode.Text = dr[0].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void getNames(string partyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " SELECT  pty_ver.pvr_initial,pty_ver.pvr_first_name,pty_ver.pvr_surname,pty_ver.pvr_occupation,pty_ver.PVR_CITIZENSHIP_NUMBER, " +
                "( CASE WHEN pty_ver.pvr_type_of_organization  IS NULL THEN 'Individual'  ELSE 'Organization' END) AS  PARTY_TYPE " +
                        " FROM   t_party_version pty_ver WHERE pty_ver.pvr_pty_party_id=" + partyId + " and pty_ver.pvr_effective_end_date is null ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtTitle.Text = dr[0].ToString();
                txtFirstName.Text = dr[1].ToString();
                txtLastName.Text = dr[2].ToString();
                txtOccupation.Text = dr[3].ToString();
                txtNICNo.Text = dr[4].ToString();
                txtPartyType.Text = dr[5].ToString();

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void getContactDetails(string partyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " select t.con_address_line_1,t.con_address_line_2,t.con_address_line_3,t.con_telephone_1 from t_contact t " +
                        "  WHERE t.con_pty_party_id=" + partyId + " and t.con_type_of_contact='Mailing/Communication' and   t.con_effective_end_date is null";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtCommunicationAddressLine1.Text = dr[0].ToString();
                txtCommunicationAddressLine2.Text = dr[1].ToString();
                txtCommunicationAddressLine3.Text = dr[2].ToString();
                txtPhoneWork.Text = dr[3].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void getMemberDetails(string partId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " select t.con_address_line_1,t.con_address_line_2,t.con_address_line_3 from t_contact t " +
                        "  WHERE t.con_pty_party_id=" + partId + " and t.con_type_of_contact='Mailing' and   t.con_effective_end_date is null";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtCommunicationAddressLine1.Text = dr[0].ToString();
                txtCommunicationAddressLine2.Text = dr[1].ToString();
                txtCommunicationAddressLine3.Text = dr[2].ToString();

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void getPolicyDurationDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = "  select pd.pod_effective_start_date,pd.pod_policy_expiry_date,pd.pod_term,pd.pod_term_unit from t_policy_detail pd  " +
                        " where pd.pod_pol_policy_id=" + policyId + "  and pd.pod_effective_end_date is null";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                txtPolicyStartDate.Text = dr[0].ToString();
                txtExpirationEndDate.Text = dr[1].ToString();
                txtPolicyDuration.Text = dr[2].ToString();


                ddlPolicyDurationUnit.ClearSelection();
                ddlPolicyDurationUnit.Items.FindByValue(dr[3].ToString()).Selected = true;

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void getPolicyAdditionalDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = "    SELECT	x.pop_pol_policy_id " +
                         " ,MAX(DECODE(x.pop_par_parameter_name, 'Premium calculation basis', x.pop_value_alpha_code)) BASIS " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Business Party', x.pop_value_alpha_code)) BUSI_PARTY " +
                         " ,MAX(DECODE(x.pop_par_parameter_name, 'VAT Customer', x.pop_value_alpha_code)) VAT_CUS " +
                         " ,MAX(DECODE(x.pop_par_parameter_name, 'Stamp Duty', x.pop_value_alpha_code)) STAMP_DUTY " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Assurance Code', x.pop_value_alpha_code)) ASS_CODE " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Have you checked Stamp Duty applicability against Chart ?', x.pop_value_alpha_code)) CHKD_STAMP_DUTY   " +
                         " ,MAX(DECODE(x.pop_par_parameter_name, 'Pending Registration', x.pop_value_alpha_code)) Pending_Registration " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Pending Inspection', x.pop_value_alpha_code)) Pending_Inspection " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Pending VIC / Copy of registration', x.pop_value_alpha_code)) PendingVIC_CopyOfRegistration " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Pending Payment', x.pop_value_alpha_code)) Pending_Payment " +
                        " ,MAX(DECODE(x.pop_par_parameter_name, 'Transfer of Ownership', x.pop_value_alpha_code)) Transfer_of_Ownership " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Particulars of Vehicle', x.pop_value_alpha_code)) Particulars_of_Vehicle " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Luxury / Semi Luxury / Semi Luxury Dual Purpose Tax', x.pop_value_alpha_code)) Lux_Semi_Lux_Dual_Purpose_Tax " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Ins. Sign. / Rubber Stamp Documents', x.pop_value_alpha_code)) Ins_Sign_Rub_Stamp_Docs " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Duly Completed Proposal Form', x.pop_value_alpha_code)) Duly_Completed_Proposal_Form " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Cover Note Indicator', x.pop_value_alpha_code)) Cover_Note_Indicator " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Issue Certificate with Pending Requirements', x.pop_value_alpha_code)) Issue_Cert_with_Pend_Requs " +
                       " ,MAX(DECODE(x.pop_par_parameter_name,'Special Approval Required', x.pop_value_alpha_code)) Special_Approval_Required  " +
                       " ,MAX(DECODE(x.pop_par_parameter_name,'Cover Note Expiry Date', x.pop_value_alpha_code)) Cover_Note_Expiry_Date  " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Cover Note No', x.pop_value_alpha_code)) Cover_Note_No  " +
                        " ,MAX(DECODE(x.pop_par_parameter_name,'Cover Note Extending Privilege', x.pop_value_alpha_code)) Cover_Note_Ext_Privilege  " +


                        "  FROM	( " +
                               "  SELECT	p.pop_pol_policy_id,p.pop_par_parameter_name,p.pop_value_alpha_code  " +
                             "  FROM	t_policy_property  p " +
                             " where p.pop_pol_policy_id=" + policyId + "  " +
                             " and   p.pop_effective_end_date is null " +
                        " ) x " +
                  " GROUP BY x.pop_pol_policy_id";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ddlPremiumCalculationBasis.ClearSelection();
                if (dr[1] != DBNull.Value && dr[1].ToString() != "") ddlPremiumCalculationBasis.Items.FindByValue(dr[1].ToString()).Selected = true;
                ddlBusinessParty.ClearSelection();
                if (dr[2] != DBNull.Value && dr[2].ToString() != "") ddlBusinessParty.Items.FindByValue(dr[2].ToString()).Selected = true;
                ddlIsVATCustomer.ClearSelection();
                if (dr[3] != DBNull.Value && dr[3].ToString() != "") ddlIsVATCustomer.Items.FindByValue(dr[3].ToString()).Selected = true;
                ddlIsStampDuty.ClearSelection();
                if (dr[4] != DBNull.Value && dr[4].ToString() != "") ddlIsStampDuty.Items.FindByValue(dr[4].ToString()).Selected = true;
                ddlAssuranceCode.ClearSelection();
                if (dr[5] != DBNull.Value && dr[5].ToString() != "") ddlAssuranceCode.Items.FindByValue(dr[5].ToString()).Selected = true;
                ddlCheckedStampDuty.ClearSelection();
                if (dr[6] != DBNull.Value && dr[6].ToString() != "") ddlCheckedStampDuty.Items.FindByValue(dr[6].ToString()).Selected = true;

                ddlIsPendingRegistration.ClearSelection();
                if (dr[7] != DBNull.Value && dr[7].ToString() != "") ddlIsPendingRegistration.Items.FindByValue(dr[7].ToString()).Selected = true;
                ddlIsPendingInspection.ClearSelection();
                if (dr[8] != DBNull.Value && dr[8].ToString() != "") ddlIsPendingInspection.Items.FindByValue(dr[8].ToString()).Selected = true;
                ddlIsPendingVIC.ClearSelection();
                if (dr[9] != DBNull.Value && dr[9].ToString() != "") ddlIsPendingVIC.Items.FindByValue(dr[9].ToString()).Selected = true;
                ddlIsPendingPayment.ClearSelection();
                if (dr[10] != DBNull.Value && dr[10].ToString() != "") ddlIsPendingPayment.Items.FindByValue(dr[10].ToString()).Selected = true;
                ddlIsTransferOfOwnership.ClearSelection();
                if (dr[11] != DBNull.Value && dr[11].ToString() != "") ddlIsTransferOfOwnership.Items.FindByValue(dr[11].ToString()).Selected = true;
                ddlIsParticularsOfVehicle.ClearSelection();
                if (dr[12] != DBNull.Value && dr[12].ToString() != "") ddlIsParticularsOfVehicle.Items.FindByValue(dr[12].ToString()).Selected = true;
                ddlIsLuxurySemi.ClearSelection();
                if (dr[13] != DBNull.Value && dr[13].ToString() != "") ddlIsLuxurySemi.Items.FindByValue(dr[13].ToString()).Selected = true;
                ddlIsInsSign.ClearSelection();
                if (dr[14] != DBNull.Value && dr[14].ToString() != "") ddlIsInsSign.Items.FindByValue(dr[14].ToString()).Selected = true;
                ddlIsDulyCompletedProposalForm.ClearSelection();
                if (dr[15] != DBNull.Value && dr[15].ToString() != "") ddlIsDulyCompletedProposalForm.Items.FindByValue(dr[15].ToString()).Selected = true;
                ddlIsCoverNoteIndicator.ClearSelection();
                if (dr[16] != DBNull.Value && dr[16].ToString() != "") ddlIsCoverNoteIndicator.Items.FindByValue(dr[16].ToString()).Selected = true;
                ddlIsIssueCertificateWithPendingRequirements.ClearSelection();
                if (dr[17] != DBNull.Value && dr[17].ToString() != "") ddlIsIssueCertificateWithPendingRequirements.Items.FindByValue(dr[17].ToString()).Selected = true;
                ddlIsSpecialApprovalRequired.ClearSelection();
                if (dr[18] != DBNull.Value && dr[18].ToString() != "") ddlIsSpecialApprovalRequired.Items.FindByValue(dr[18].ToString()).Selected = true;

                if (dr[19] != DBNull.Value && dr[19].ToString() != "") txtCoverNoteExpiryDate.Text = dr[19].ToString();

                if (dr[20] != DBNull.Value && dr[20].ToString() != "") txtCoverNoteNo.Text = dr[20].ToString();

                ddlIsCoverNoteExtendingPrivilege.ClearSelection();
                if (dr[21] != DBNull.Value && dr[21].ToString() != "") ddlIsCoverNoteExtendingPrivilege.Items.FindByValue(dr[21].ToString()).Selected = true;

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void getPolicyRiskPropertyDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = "     SELECT	x.prp_prk_pol_policy_id " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Age of driver', x.prp_value_integer)) age " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Sample Vehicle Number', x.prp_value_alpha_code)) vehi_no " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Registration Number', x.prp_value_alpha_code)) reg_no " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Make & Model', x.prp_value_alpha_code)) make " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Engine No.', x.prp_value_alpha_code)) eng_no " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Chassis_No', x.prp_value_alpha_code)) chasis_no " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Date of First Registration', x.prp_value_alpha_code)) first_reg_date " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Fuel Used', x.prp_value_alpha_code)) fuel " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Condition', x.prp_value_alpha_code)) condition " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Vehicle Mfg. Mth/Year', x.prp_value_alpha_code)) mfg_year " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Whether trailer attached', x.prp_value_alpha_code)) is_trailer_atchd " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Extra Fittings YN', x.prp_value_alpha_code)) has_extra_fits " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Cubic Capacity in cc', x.prp_value_alpha_code)) cubic_capacity " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Inst. with financial interest', x.prp_value_alpha_code)) inst " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Registered Owner of Vehicle Y/N', x.prp_value_alpha_code)) is_reg_owner " +
                    " ,MAX(DECODE(x.prp_par_parameter_name, 'Name of the registered owner', x.prp_value_alpha_code)) name_of_reg_owner " +
                         "    FROM	( " +
                                        " SELECT	rp.prp_prk_pol_policy_id,rp.prp_par_parameter_name,rp.prp_value_alpha_code,rp.prp_value_integer ,rp.prp_value_description " +
                                      " FROM	t_policy_risk_property  rp " +
                                     " where rp.prp_prk_pol_policy_id=" + policyId + "   " +
                                     " and  rp.prp_effective_end_date is null " +
                                " ) x " +
                           " GROUP BY x.prp_prk_pol_policy_id";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[1] != DBNull.Value && dr[1].ToString() != "") txtAgeofDriver.Text = dr[1].ToString();
                if (dr[2] != DBNull.Value && dr[2].ToString() != "") txtSampleVehicleNumber.Text = dr[2].ToString();
                if (dr[3] != DBNull.Value && dr[3].ToString() != "") txtRegistrationNumber.Text = dr[3].ToString();
                if (dr[4] != DBNull.Value && dr[4].ToString() != "") txtMakeModel.Text = dr[4].ToString();
                if (dr[5] != DBNull.Value && dr[5].ToString() != "") txtEngineNo.Text = dr[5].ToString();
                if (dr[6] != DBNull.Value && dr[6].ToString() != "") txtChassisNo.Text = dr[6].ToString();
                if (dr[7] != DBNull.Value && dr[7].ToString() != "") txtDateOfFirstRegistration.Text = dr[7].ToString();
                if (dr[8] != DBNull.Value && dr[8].ToString() != "") txtFuelUsed.Text = dr[8].ToString();
                if (dr[9] != DBNull.Value && dr[9].ToString() != "") txtCondition.Text = dr[9].ToString();
                if (dr[10] != DBNull.Value && dr[10].ToString() != "") txtVehicleMfgMthYea.Text = dr[10].ToString();

                ddlWhetherTrailerSttached.ClearSelection();
                if (dr[11] != DBNull.Value && dr[11].ToString() != "") ddlWhetherTrailerSttached.Items.FindByValue(dr[11].ToString()).Selected = true;

                ddlExtraFittings.ClearSelection();
                if (dr[12] != DBNull.Value && dr[12].ToString() != "") ddlExtraFittings.Items.FindByValue(dr[12].ToString()).Selected = true;



                if (dr[13] != DBNull.Value && dr[13].ToString() != "") txtCubicCapacityinCC.Text = dr[13].ToString();


                if (dr[14] != DBNull.Value && dr[14].ToString() != "") txtInstWithFinancialInterest.Text = dr[14].ToString();



              //  ddlIsRegisteredOwnerOfVehicleYN.ClearSelection();
              //  if (dr[15] != DBNull.Value && dr[15].ToString() != "") ddlIsRegisteredOwnerOfVehicleYN.Items.FindByValue(dr[15].ToString()).Selected = true;


                if (dr[16] != DBNull.Value && dr[16].ToString() != "") txtNameOfTheRegisteredOwner.Text = dr[16].ToString();




            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void getPolicyPartyDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = "    SELECT  x.ppa_pol_policy_id " +
                       " ,MAX(DECODE(x.ppa_shr_stake_holder_fn_code, 'BROKER', x.ppa_pty_party_code)) BROKER_CODE " +
                        " ,MAX(DECODE(x.ppa_shr_stake_holder_fn_code, 'AGENT', x.ppa_pty_party_code)) AGENT_CODE " +
                       "  ,MAX(DECODE(x.ppa_shr_stake_holder_fn_code, 'FINAN-INST', x.ppa_pty_party_name)) FIN_INST " +
                        " ,MAX(DECODE(x.ppa_shr_stake_holder_fn_code, 'POLICY-HOL', x.ppa_pty_party_name)) POL_HOLDER " +
                    " FROM  ( " +
                               "  SELECT  p.ppa_pol_policy_id,p.ppa_shr_stake_holder_fn_code,p.ppa_pty_party_code,p.ppa_pty_party_name " +
                              " FROM	t_policy_party  p " +
                             " where p.ppa_pol_policy_id=" + policyId + "  " +
                             " and   p.ppa_effective_end_date is null " +
                        " ) x " +
                   " GROUP BY x.ppa_pol_policy_id ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();



                txtAgentOrBrokerCode.Text = dr[0].ToString() + dr[1].ToString();//Agent or Broker Code 2n 1i 1 para enne
                txtFinanINST.Text = dr[2].ToString();





            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void getClausenDetails(string policyId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = "  select pd.pod_effective_start_date,pd.pod_policy_expiry_date,pd.pod_term,pd.pod_term_unit from t_policy_detail pd  " +
                        " where pd.pod_pol_policy_id=" + policyId + "  and pd.pod_effective_end_date is null";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                txtPolicyStartDate.Text = dr[0].ToString();
                txtExpirationEndDate.Text = dr[1].ToString();
                txtPolicyDuration.Text = dr[2].ToString();


                ddlPolicyDurationUnit.ClearSelection();
                ddlPolicyDurationUnit.Items.FindByValue(dr[3].ToString()).Selected = true;

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


    }
}