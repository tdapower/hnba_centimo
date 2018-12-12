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
    public partial class CreditCalculate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadUtilizationPremiumTypes();
            }
        }
        private void loadUtilizationPremiumTypes()
        {
            ddlUtilizationPremium.Items.Clear();
            ddlUtilizationPremium.Items.Add(new ListItem("--- Select One ---", "0"));
            ddlUtilizationPremium.Items.Add(new ListItem("Pro-rata Utilization Premium ", "1"));
            ddlUtilizationPremium.Items.Add(new ListItem("Short Term Utilization Premium ", "2"));



        }
        //protected void btnCalculate_Click(object sender, EventArgs e)
        //{

        //    calculateCRSFForDebit();
        //    calculateAdminFeeForDebit();
        //    calculatePolicyFeeForDebit();
        //    calculateStampDutyForDebit();
        //    calculateNBTForDebit();

        //    calculateTotalForDebit();


        //    string premiumType = "";
        //    if (ddlUtilizationPremium.SelectedValue != "0")
        //    {
        //        premiumType = ddlUtilizationPremium.SelectedValue;

        //        //"Pro-rata Utilization Premium ", "1"
        //        //"Short Term Utilization Premium ", "2"



        //        if (premiumType == "1")
        //        {
        //            calculateProRataBasicForCredit();
        //            calculateProRataSRCCForCredit();
        //            calculateProRataTCForCredit();
        //            calculateProRataCRSFForCredit();
        //            calculateProRataAdminFeeForCredit();

        //            calculateProRataPolicyFeeForCredit();
        //            calculateProRataStampDutyForCredit();
        //            calculateProRataNBTForCredit();



        //            calculateSubTotalForCredit();
        //            calculateProRataVATForCredit();

        //            calculateTotalForCredit();


        //        }
        //        else if (premiumType == "2")
        //        {

        //        }


        //    }


        //}


        protected void btnCalculate_Click(object sender, EventArgs e)
        {



            string premiumType = "";
            if (ddlUtilizationPremium.SelectedValue != "0")
            {
                premiumType = ddlUtilizationPremium.SelectedValue;
                if (premiumType == "1")
                {
                    premiumType = "PRORATA";
                }
                else
                {
                    premiumType = "SHORT";
                }

                calculate(premiumType);
                getCalculatedResults(txtJobNo.Text);

            }


        }



        private void calculate(string premiumType)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());


            cmd.Connection = con;
            cmd.CommandText = "MNB_CREDIT_CALC";
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add("V_JOB_NO", OracleType.VarChar).Value = txtJobNo.Text;
            cmd.Parameters.Add("V_NO_OF_USED_DAYS", OracleType.Number).Value = Convert.ToInt32(txtUsedDays.Text);
            cmd.Parameters.Add("V_NO_OF_UTIL_DAYS", OracleType.Number).Value = Convert.ToInt32(txtUtilizedDays.Text);
            cmd.Parameters.Add("V_BASIC_PREM", OracleType.Number).Value = Convert.ToDouble(txtBasicPremium.Text);
            cmd.Parameters.Add("V_SRCC", OracleType.Number).Value = Convert.ToDouble(txtSRCC.Text);
            cmd.Parameters.Add("V_TC", OracleType.Number).Value = Convert.ToDouble(txtTC.Text);
            cmd.Parameters.Add("V_UTILIZATION_PREMIUM", OracleType.VarChar).Value = premiumType;
            cmd.Parameters.Add("V_VEHICLE_RISK_TYPE_ID", OracleType.Number).Value = Convert.ToInt32(txtVehicleRiskTypeId.Text);
            cmd.Parameters.Add("V_VEHICLE_CLASS_ID", OracleType.Number).Value = Convert.ToInt32(txtVehicleClassTypeId.Text);


            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void getCalculatedResults(string jobNo)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = " SELECT  " +
                    " JOB_NO              ," +//0
                    " NO_OF_USED_DAYS		," +//1
                    " NO_OF_UTIL_DAYS		," +//2
                    " BASIC_PREM					," +//3
                    " SRCC 						," +//4
                    " TC  						," +//5
                    " UTILIZATION_PREMIUM     ," +//6
                    " VEHICLE_RISK_TYPE_ID	," +//7
                    " VEHICLE_CLASS_ID	," +//8
                    " NO_OF_DAYS_FOR_CALC ," +//9
                    " CALC_GROSS_PREM ," +//10
                    " TOTAL_FOR_STAMP_DUTY ," +//11
                    " CALC_DEBIT_CRSF ," +//12
                    " CALC_DEBIT_ADIMIN_FEE ," +//13
                    " CALC_DEBIT_POLICY_FEE ," +//14
                    " CALC_DEBIT_STAMP_DUTY ," +//15
                    " CALC_DEBIT_NBT ," +//16
                    " CALC_DEBIT_TOTAL ," +//17
                    " CALC_DEBIT_VAT_CRSF ," +//18
                    " CALC_DEBIT_VAT_ADIMIN_FEE ," +//19
                    " CALC_DEBIT_VAT_POLICY_FEE ," +//20
                    " CALC_DEBIT_VAT_STAMP_DUTY ," +//21
                    " CALC_DEBIT_VAT_NBT ," +//22
                    " CALC_DEBIT_VAT_TOTAL ," +//23
                    " CALC_CREDIT_BASIC_PREM	 ," +//24
                    " CALC_CREDIT_SRCC ," +//25
                    " CALC_CREDIT_TC	 ," +//26
                    " CALC_CREDIT_ADIMIN_FEE ," +//27
                    " CALC_CREDIT_SUB_TOTAL ," +//28
                    " CALC_CREDIT_STAMP_DUTY ," +//29
                    " CALC_CREDIT_NBT ," +//30
                    " CALC_CREDIT_VAT ," +//31
                    " CALC_CREDIT_TOTAL ," +//32
                    " CALC_CREDIT_VAT_BASIC_PREM	 ," +//33
                    " CALC_CREDIT_VAT_SRCC ," +//34
                    " CALC_CREDIT_VAT_TC	 ," +//35
                    " CALC_CREDIT_VAT_ADIMIN_FEE ," +//36
                    " CALC_CREDIT_VAT_SUB_TOTAL ," +//37
                    " CALC_CREDIT_VAT_STAMP_DUTY ," +//38
                    " CALC_CREDIT_VAT_NBT ," +//39
                    " CALC_CREDIT_VAT_VAT ," +//40
                    " CALC_CREDIT_VAT_TOTAL " +//41
                        " FROM  MNB_CREDIT_VALS WHERE JOB_NO='" + jobNo + "'";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txtCRSF.Text = dr[18].ToString();
                txtAdiministrationFee.Text = dr[13].ToString();
                txtPolicyFee.Text = dr[14].ToString();
                txtStampDuty.Text = dr[15].ToString();
                txtNBT.Text = dr[16].ToString();
                txtTotal.Text = dr[17].ToString();


                txtCreditBasicPremium.Text = dr[24].ToString();
                txtCreditSRCC.Text = dr[25].ToString();
                txtCreditTC.Text = dr[26].ToString();
                txtCreditAdiministrationFee.Text = dr[27].ToString();
                txtCreditSubTotal.Text = dr[28].ToString();
                txtCreditStampDuty.Text = dr[29].ToString();
                txtCreditNBT.Text = dr[30].ToString();
                txtCreditVAT.Text = dr[31].ToString();
                txtCreditTotal.Text = dr[32].ToString();



                txtCreditBasicPremiumWithVAT.Text = dr[33].ToString();
                txtCreditSRCCWithVAT.Text = dr[34].ToString();
                txtCreditTCWithVAT.Text = dr[35].ToString();
                txtCreditAdiministrationFeeWithVAT.Text = dr[36].ToString();
                txtCreditSubTotalWithVAT.Text = dr[37].ToString();
                txtCreditStampDutyWithVAT.Text = dr[38].ToString();
                txtCreditNBTWithVAT.Text = dr[39].ToString();
                txtCreditTotalWithVAT.Text = dr[41].ToString();




            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        //private void calculateProRataBasicForCredit()
        //{
        //    double BasicForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    BasicForCredit = (Convert.ToDouble(txtBasicPremium.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditBasicPremiumWithVAT.Text = BasicForCredit.ToString("#,##0.00");
        //    txtCreditBasicPremium.Text = calculateWithoutVAT(BasicForCredit.ToString("#,##0.00"));



        //}


        //private void calculateProRataSRCCForCredit()
        //{
        //    double SRCCForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    SRCCForCredit = (Convert.ToDouble(txtSRCC.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditSRCCWithVAT.Text = SRCCForCredit.ToString("#,##0.00");
        //    txtCreditSRCC.Text = calculateWithoutVAT(SRCCForCredit.ToString("#,##0.00"));
        //}

        //private void calculateProRataTCForCredit()
        //{
        //    double TCForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    TCForCredit = (Convert.ToDouble(txtTC.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditTCWithVAT.Text = TCForCredit.ToString("#,##0.00");
        //    txtCreditTC.Text = calculateWithoutVAT(TCForCredit.ToString("#,##0.00"));
        //}

        //private void calculateProRataCRSFForCredit()
        //{
        //    double CRSFForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    CRSFForCredit = (Convert.ToDouble(txtCRSF.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditCRSFWithVAT.Text = CRSFForCredit.ToString("#,##0.00");
        //    txtCreditCRSF.Text = calculateWithoutVAT(CRSFForCredit.ToString("#,##0.00"));
        //}

        //private void calculateProRataAdminFeeForCredit()
        //{
        //    double AdminFeeForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    AdminFeeForCredit = (Convert.ToDouble(txtAdiministrationFee.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditAdiministrationFeeWithVAT.Text = AdminFeeForCredit.ToString("#,##0.00");
        //    txtCreditAdiministrationFee.Text = calculateWithoutVAT(AdminFeeForCredit.ToString("#,##0.00"));
        //}

        //private void calculateProRataPolicyFeeForCredit()
        //{
        //    double PolicyFeeForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    PolicyFeeForCredit = (Convert.ToDouble(txtPolicyFee.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditPolicyFeeWithVAT.Text = PolicyFeeForCredit.ToString("#,##0.00");
        //    txtCreditPolicyFee.Text = calculateWithoutVAT(PolicyFeeForCredit.ToString("#,##0.00"));
        //}
        //private void calculateProRataStampDutyForCredit()
        //{
        //    double StampDutyForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    StampDutyForCredit = (Convert.ToDouble(txtStampDuty.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditStampDutyWithVAT.Text = StampDutyForCredit.ToString("#,##0.00");
        //    txtCreditStampDuty.Text = calculateWithoutVAT(StampDutyForCredit.ToString("#,##0.00"));
        //}
        //private void calculateProRataNBTForCredit()
        //{
        //    double NBTForCredit = 0.00;
        //    int NoOfDaysForCalc = 0;

        //    NoOfDaysForCalc = Convert.ToInt32(txtUsedDays.Text) + Convert.ToInt32(txtUtilizedDays.Text);

        //    NBTForCredit = (Convert.ToDouble(txtNBT.Text) * Convert.ToDouble(txtUtilizedDays.Text)) / NoOfDaysForCalc;

        //    txtCreditNBTWithVAT.Text = NBTForCredit.ToString("#,##0.00");
        //    txtCreditNBT.Text = calculateWithoutVAT(NBTForCredit.ToString("#,##0.00"));
        //}

        //private void calculateProRataVATForCredit()
        //{
        //    double VATCredit = 0.00;

        //    VATCredit = Convert.ToDouble(calculateWVAT((Convert.ToDouble(txtCreditSubTotal.Text) + Convert.ToDouble(txtCreditNBT.Text)).ToString()));

        //    txtCreditVAT.Text = VATCredit.ToString("#,##0.00");
        //}
        //private void calculateSubTotalForCredit()
        //{

        //    double total = 0.00;
        //    total = total + Convert.ToDouble(txtCreditBasicPremiumWithVAT.Text);
        //    total = total + Convert.ToDouble(txtCreditSRCCWithVAT.Text);
        //    total = total + Convert.ToDouble(txtCreditTCWithVAT.Text);
        //    // total = total + Convert.ToDouble(txtCreditCRSFWithVAT.Text);
        //    total = total + Convert.ToDouble(txtCreditAdiministrationFeeWithVAT.Text);

        //    txtCreditSubTotalWithVAT.Text = Math.Round(Convert.ToDouble(total), 2).ToString("#,##0.00");



        //    double totalWithoutVAT = 0.00;
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditBasicPremium.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditSRCC.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditTC.Text);
        //    // totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditCRSFWithVAT.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditAdiministrationFee.Text);

        //    txtCreditSubTotal.Text = Math.Round(Convert.ToDouble(totalWithoutVAT), 2).ToString("#,##0.00");

        //}
        //private void calculateTotalForCredit()
        //{

        //    double total = 0.00;
        //    total = total + Convert.ToDouble(txtCreditSubTotalWithVAT.Text);
        //    // total = total + Convert.ToDouble(txtCreditPolicyFeeWithVAT.Text);
        //    total = total + Convert.ToDouble(txtCreditStampDutyWithVAT.Text);
        //    total = total + Convert.ToDouble(txtCreditNBTWithVAT.Text);


        //    txtCreditTotalWithVAT.Text = Math.Round(Convert.ToDouble(total), 2).ToString("#,##0.00");





        //    double totalWithoutVAT = 0.00;
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditSubTotal.Text);
        //    // totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditPolicyFeeWithVAT.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditStampDuty.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditNBT.Text);
        //    totalWithoutVAT = totalWithoutVAT + Convert.ToDouble(txtCreditVAT.Text);

        //    txtCreditTotal.Text = Math.Round(Convert.ToDouble(totalWithoutVAT), 2).ToString("#,##0.00");


        //}








        //private void calculateCRSFForDebit()
        //{

        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_CRSF";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("V_VEHICLE_RISK_TYPE_ID", txtVehicleRiskTypeId.Text);
        //    cmd.Parameters.Add("V_VEHICLE_CLASS_ID", txtVehicleClassTypeId.Text);
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_CRSF", OracleType.Number);
        //    cmd.Parameters["V_CRSF"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    var CRSF = Convert.ToString(cmd.Parameters["V_CRSF"].Value);


        //    txtCRSF.Text = string.Format("{0:N2}", calculateWithVAT(CRSF));

        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //}

        //private void calculateAdminFeeForDebit()
        //{

        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_ADMIN_FEE";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    double grossPremium = 0.00;
        //    grossPremium = grossPremium + Convert.ToDouble(txtBasicPremium.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtSRCC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtTC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtCRSF.Text);




        //    cmd.Parameters.Add("V_GROSS_PREMIUM", grossPremium.ToString());
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_ADMIN_FEE", OracleType.Number);
        //    cmd.Parameters["V_ADMIN_FEE"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    var AdminFee = Convert.ToString(cmd.Parameters["V_ADMIN_FEE"].Value);


        //    txtAdiministrationFee.Text = string.Format("{0:N2}", calculateWithVAT(AdminFee));

        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //}


        //private void calculatePolicyFeeForDebit()
        //{

        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_POLICY_FEE";
        //    cmd.CommandType = CommandType.StoredProcedure;




        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_POLICY_FEE", OracleType.Number);
        //    cmd.Parameters["V_POLICY_FEE"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    var PolicyFee = Convert.ToString(cmd.Parameters["V_POLICY_FEE"].Value);


        //    txtPolicyFee.Text = string.Format("{0:N2}", calculateWithVAT(PolicyFee));

        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //}
        //private void calculateStampDutyForDebit()
        //{


        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);

        //    double grossPremium = 0.00;
        //    grossPremium = grossPremium + Convert.ToDouble(txtBasicPremium.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtSRCC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtTC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtCRSF.Text);


        //    double totalForStampDuty = 0.00;
        //    totalForStampDuty = totalForStampDuty + grossPremium;
        //    totalForStampDuty = totalForStampDuty + Convert.ToDouble(txtAdiministrationFee.Text);
        //    totalForStampDuty = totalForStampDuty + Convert.ToDouble(txtPolicyFee.Text);



        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_STAMP_DUTY";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("V_TOTAL_FOR_STAMP", totalForStampDuty.ToString());
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);
        //    cmd.Parameters.Add("V_VEHICLE_CLASS_ID", txtVehicleClassTypeId.Text);


        //    cmd.Parameters.Add("V_STAMP_DUTY", OracleType.Number);
        //    cmd.Parameters["V_STAMP_DUTY"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    var stampDuty = Convert.ToString(cmd.Parameters["V_STAMP_DUTY"].Value);


        //    txtStampDuty.Text = calculateWithVAT(stampDuty);

        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //}
        //private void calculateNBTForDebit()
        //{


        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);

        //    double grossPremium = 0.00;
        //    grossPremium = grossPremium + Convert.ToDouble(txtBasicPremium.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtSRCC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtTC.Text);
        //    grossPremium = grossPremium + Convert.ToDouble(txtCRSF.Text);


        //    double totalForStampDuty = 0.00;
        //    totalForStampDuty = totalForStampDuty + grossPremium;
        //    totalForStampDuty = totalForStampDuty + Convert.ToDouble(txtAdiministrationFee.Text);
        //    totalForStampDuty = totalForStampDuty + Convert.ToDouble(txtPolicyFee.Text);



        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_NBT";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("V_TOTAL_FOR_STAMP", totalForStampDuty.ToString());
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);



        //    cmd.Parameters.Add("V_NBT", OracleType.Number);
        //    cmd.Parameters["V_NBT"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    var NBT = Convert.ToString(cmd.Parameters["V_NBT"].Value);


        //    txtNBT.Text = Math.Round(Convert.ToDouble(NBT), 2).ToString("#,##0.00");

        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //}


        //private void calculateTotalForDebit()
        //{

        //    double total = 0.00;
        //    total = total + Convert.ToDouble(txtBasicPremium.Text);
        //    total = total + Convert.ToDouble(txtSRCC.Text);
        //    total = total + Convert.ToDouble(txtTC.Text);
        //    total = total + Convert.ToDouble(txtCRSF.Text);
        //    total = total + Convert.ToDouble(txtAdiministrationFee.Text);
        //    total = total + Convert.ToDouble(txtPolicyFee.Text);
        //    total = total + Convert.ToDouble(txtStampDuty.Text);
        //    total = total + Convert.ToDouble(txtNBT.Text);

        //    txtTotal.Text = Math.Round(Convert.ToDouble(total), 2).ToString("#,##0.00");

        //}

        //private string calculateWithVAT(string amount)
        //{
        //    if (amount == "" || amount == "0.00" || amount == "0")
        //    {
        //        return "0.00";
        //    }
        //    amount = amount.Replace(",", "");



        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_VALUE_WITH_VAT";
        //    cmd.CommandType = CommandType.StoredProcedure;



        //    cmd.Parameters.Add("V_AMOUNT_WITHOUT_VAT", amount);
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_AMOUNT_WITH_VAT", OracleType.Number);
        //    cmd.Parameters["V_AMOUNT_WITH_VAT"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    string AmountWithVAT = "";

        //    AmountWithVAT = Math.Round(Convert.ToDouble(cmd.Parameters["V_AMOUNT_WITH_VAT"].Value), 2).ToString("#,##0.00");



        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();


        //    return AmountWithVAT;

        //}

        //private string calculateWithoutVAT(string amount)
        //{

        //    if (amount == "" || amount == "0.00" || amount == "0")
        //    {
        //        return "0.00";
        //    }
        //    amount = amount.Replace(",", "");

        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_VALUE_WITHOUT_VAT";
        //    cmd.CommandType = CommandType.StoredProcedure;



        //    cmd.Parameters.Add("V_AMOUNT_WITH_VAT", amount);
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_AMOUNT_WITHOUT_VAT", OracleType.Number);
        //    cmd.Parameters["V_AMOUNT_WITHOUT_VAT"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    string AmountWithoutVAT = "";

        //    AmountWithoutVAT = Math.Round(Convert.ToDouble(cmd.Parameters["V_AMOUNT_WITHOUT_VAT"].Value), 2).ToString("#,##0.00");



        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();


        //    return AmountWithoutVAT;

        //}
        //private string calculateWVAT(string amount)
        //{

        //    if (amount == "" || amount == "0.00" || amount == "0")
        //    {
        //        return "0.00";
        //    }
        //    amount = amount.Replace(",", "");

        //    OracleCommand cmd = new OracleCommand();
        //    OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

        //    string dbServerDate = "";


        //    ProposalUploadController proposalUploadController = new ProposalUploadController();
        //    dbServerDate = proposalUploadController.getDBServerTime().Remove(10);


        //    cmd.Connection = con;
        //    cmd.CommandText = "MNB_CALC_VAT";
        //    cmd.CommandType = CommandType.StoredProcedure;



        //    cmd.Parameters.Add("V_AMOUNT_WITHOUT_VAT", amount);
        //    cmd.Parameters.Add("V_QUOT_DATE", OracleType.DateTime).Value = Convert.ToDateTime(dbServerDate);

        //    cmd.Parameters.Add("V_VAT", OracleType.Number);
        //    cmd.Parameters["V_VAT"].Direction = ParameterDirection.ReturnValue;

        //    cmd.Connection.Open();
        //    cmd.ExecuteNonQuery();

        //    string VAT = "";

        //    VAT = Math.Round(Convert.ToDouble(cmd.Parameters["V_VAT"].Value), 2).ToString("#,##0.00");



        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();


        //    return VAT;

        //}
    }
}