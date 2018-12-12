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
    public partial class QuotationDetailsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["quotationNo"] != null)
            {
                loadPeriodTypes();
                loadAgentTypes();
                loadVehicleRiskTypes();
                loadLeasingTypes();
                loadFuelTypes();
                // loadProducts(); //will load according to vehicle type and usage
                loadGoodsInTransits();
                loadPABToDriver();
                loadPABToPassenger();
                loadLegalLiability();
                loadWCI();
                loadLegalLiabilityValues();
                loadTPPD();


                loadQuotaionDetails(Request.QueryString["quotationNo"].ToString());

            }
        }


        private void loadQuotaionDetails(string quotationNo)
        {

            QuotationController quotationController = new QuotationController();

            QuotationMain quotationMain = new QuotationMain();
            quotationMain = quotationController.GetQuotationMainDetails(quotationNo);



            hdnJobID.Value = quotationMain.JobId.ToString();

            lblQuotationNo.Text = quotationMain.QuotationNo;
            txtRequestedBy.Text = quotationMain.RequestBy;
            txtClientName.Text = quotationMain.ClientName;
            txtVehicleNo.Text = quotationMain.VehicleChasisNo;
            ddlVehicleRiskType.SelectedValue = quotationMain.RiskTypeId;
            loadVoluntary(ddlVehicleRiskType.SelectedItem.ToString());

            string riskType = "0";
            riskType = quotationMain.RiskTypeId;
            if (riskType != "0")
            {
                loadVehicleTypes(riskType);
                ddlVehicleType.SelectedValue = quotationMain.VehicleTypeId;


            }


            string vehicleType = "0";
            vehicleType = quotationMain.VehicleTypeId;


            if (riskType != "0" && vehicleType != "0")
            {
                loadVehicleUsages(riskType, vehicleType);
                ddlUsage.SelectedValue = quotationMain.VehicleClassId;
            }



            txtSumInsured.Text = quotationMain.SumInsured;
            ddlPeriodTypes.SelectedValue = quotationMain.PeriodTypeCode;


            string periodType = quotationMain.PeriodTypeCode;

            if (periodType != "0")
            {
                loadPeriods(periodType);
                ddlPeriodOfCover.SelectedValue = quotationMain.PeriodCode;
            }



            ddlAgentOrBroker.SelectedValue = quotationMain.AgentBroker;
            ddlLeasingType.SelectedValue = quotationMain.LeasingType;
            ddlFuelType.SelectedValue = quotationMain.FuelTypeCode;

            loadProducts();
            ddlProduct.SelectedValue = quotationMain.ProductCode;


            hdnBranch.Value = quotationMain.BranchId;
            txtRemarks.Text = quotationMain.Remark;

            hdnRevisionID.Value = quotationMain.RevisionNo.ToString();

            txtAgentOrBrokerCode.Text = quotationMain.AgentBrokerCode;


            loadSavedQuotationCovers(quotationMain.JobId.ToString(), quotationMain.RevisionNo.ToString());
            getCalculatedTotalPremium(quotationMain.JobId.ToString(), quotationMain.RevisionNo.ToString());
        }

        private void getCalculatedTotalPremium(string jobId, string revisionNo)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "  SELECT T.AMOUNT " +
                             " FROM MNB_QUOT_TEMP_RESULT T  " +
                              " WHERE JOB_ID=" + jobId + " AND  REVISION_NO=" + revisionNo + " AND POLICY_COVER_CODE='PREMIUM'";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                //  string aa = Convert.ToDecimal(dr[0].ToString()).ToString();

                // lblTotalPremium.Text =Convert.ToDouble(dr[0].ToString()).ToString();



                double formatToMoney;
                string num = dr[0].ToString();
                if (double.TryParse(num, out formatToMoney))
                {
                    lblTotalPremium.Text = formatToMoney.ToString("#,###.00");

                }

            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void loadSavedQuotationCovers(string jobId, string revisionNo)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "SELECT  A.POLICY_COVER_CODE,A.COVER_TYPE,A.COVER_AMOUNT,A.JOB_ID,A.REVISION_NO FROM MNB_POLICY_COVER_LIST A  " +
                         " WHERE A.JOB_ID=" + jobId + " AND a.revision_no=" + revisionNo + "" +
            " ORDER BY A.JOB_ID";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    hdnRevisionID.Value = dr["revision_no"].ToString();
                    if (dr["policy_cover_code"].ToString() == "multiple rebate")
                    {
                        txtMultipleRebate.Text = dr["cover_amount"].ToString();
                    }
                    else
                    {
                        CheckBox chk = ((CheckBox)pnl_policy_cover.FindControl(dr["policy_cover_code"].ToString()));
                        chk.Checked = true;
                        chk.ForeColor = Color.Red;

                        if (chk.ID == "chk4")
                        {
                            ddlAddPABToDriver.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk5")
                        {
                            ddlPABToPassenger.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk6")
                        {
                            ddlGoodsInTransit.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk7")
                        {
                            ddlLegalLiability.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk12")
                        {
                            ddlTPPD.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk13")
                        {
                            ddlWCI.SelectedValue = dr["cover_type"].ToString();
                        }

                        if (chk.ID == "chk1")
                        {
                            txtHirePurchase.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk2")
                        {
                            loadVoluntary(ddlVehicleRiskType.SelectedItem.ToString());
                            ddlVoluntary.SelectedValue = dr["cover_type"].ToString();
                        }
                        if (chk.ID == "chk3")
                        {
                            txtAACMembership.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk4")
                        {
                            txtAddPABToDriver.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk5")
                        {
                            txtAddPABToPassenger.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk6")
                        {
                            txtAddGoodInTransit.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk7")
                        {
                            ddlLegalLiabilityVal.SelectedValue = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk8")
                        {
                            txtAddTowingCharges.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk9")
                        {
                            loadEarnedNCB(ddlVehicleRiskType.SelectedItem.ToString());
                            ddlEarnedNCB.SelectedValue = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk10")
                        {
                            loaUpFrontNCB(ddlVehicleRiskType.SelectedItem.ToString());
                            ddlUpFrontNCB.SelectedValue = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk11")
                        {
                            txtWindscreen.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk21")
                        {
                            txtLessPointsEarned.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk28")
                        {
                            txtSRCCForPAB.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk29")
                        {
                            txtTCforPAB.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk30")
                        {
                            txtSRCCforWCI.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk31")
                        {
                            txtTCforWCI.Text = dr["cover_amount"].ToString();
                        }
                        if (chk.ID == "chk32")
                        {
                            txtCompulsaryExcess.Text = dr["cover_amount"].ToString();
                        }

                    }
                }

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void loadVehicleTypes(string riskType)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "select distinct mbr.vehicle_type_id,mvt.vehicle_type from mnb_basic_rate mbr " +
                " inner join MNB_VEHICLE_TYPE mvt on mvt.vehicle_type_id=mbr.vehicle_type_id " +
                " where mbr.risk_type_id=" + riskType + " " +
                " ORDER BY mvt.vehicle_type ASC";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlVehicleType.Items.Clear();
                ddlVehicleType.Items.Add(new ListItem("--- Select One ---", "0"));

                while (dr.Read())
                {
                    ddlVehicleType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void loadVehicleUsages(string riskType, string vehicleType)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "select distinct mbr.VEHICLE_CLASS_ID,mvc.VEHICLE_CLASS from mnb_basic_rate mbr " +
                " inner join Mnb_Vehicle_Class mvc on mvc.VEHICLE_CLASS_ID=mbr.VEHICLE_CLASS_ID " +
                " where mbr.risk_type_id=" + riskType + " and  mbr.vehicle_type_id=" + vehicleType + "" +
                " ORDER BY mvc.VEHICLE_CLASS ASC";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlUsage.Items.Clear();
                ddlUsage.Items.Add(new ListItem("--- Select One ---", "0"));

                while (dr.Read())
                {
                    ddlUsage.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void loadVoluntary(string riskType)
        {
            string MotorCycleRiskType = System.Configuration.ConfigurationManager.AppSettings["MNBQMotorCycleRiskType"].ToString();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            if (riskType.ToLower() == MotorCycleRiskType.ToLower())
            {
                selectQuery = "SELECT t.name FROM MNB_COVER_AMOUNTS_AND_RATES T " +
                            " WHERE T.type='VOLUNTARY_EXCESS_MOTOR_CYCLE' AND  TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')  ";
            }
            else
            {
                selectQuery = "SELECT t.name FROM MNB_COVER_AMOUNTS_AND_RATES T " +
                                 " WHERE T.type='VOLUNTARY_EXCESS_OTHER' AND  TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')  ";

            }
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlVoluntary.Items.Clear();
                ddlVoluntary.Items.Add(new ListItem("Select One", "0"));

                while (dr.Read())
                {
                    ddlVoluntary.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));

                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void loadPeriods(string periodType)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "select mp.period_code,mp.period from mnbq_period mp where mp.period_type_code=" + periodType + " and mp.is_active=1";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlPeriodOfCover.Items.Clear();
                ddlPeriodOfCover.Items.Add(new ListItem("0", "0"));

                while (dr.Read())
                {
                    ddlPeriodOfCover.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));

                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }

        private void loadProducts()
        {

            string usageType = "";
            string vehicleType = "";

            usageType = ddlUsage.SelectedValue.ToString();
            vehicleType = ddlVehicleType.SelectedValue.ToString();


            if (vehicleType == "" || usageType == "")
            {
                return;
            }


            ddlProduct.Items.Clear();
            ddlProduct.Items.Add(new ListItem("--- Select One ---", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT PRODUCT_CODE,PRODUCT,ALLOWED_VEHICLE_TYPES,ALLOWED_VEHICLE_USAGES FROM MNBQ_PRODUCT  WHERE    IS_ACTIVE=1 ORDER BY PRODUCT ASC ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {


                    string[] allowedVehicleTypes = dr[2].ToString().Split(',');
                    foreach (string allowedVehicleType in allowedVehicleTypes)
                    {
                        if (allowedVehicleType == vehicleType || allowedVehicleType == "ALL")
                        {
                            string[] allowedVehicleUsages = dr[3].ToString().Split(',');
                            foreach (string allowedVehicleUsage in allowedVehicleUsages)
                            {
                                if (allowedVehicleUsage == usageType || allowedVehicleUsage == "ALL")
                                {
                                    ddlProduct.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                                }
                            }

                        }
                    }
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }
        private void loadLeasingTypes()
        {
            ddlLeasingType.Items.Clear();
            ddlLeasingType.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT LEASE_TYPE_CODE,LEASE_TYPE_NAME FROM MNBQ_LEASE_TYPE   ORDER BY LEASE_TYPE_NAME ASC ";
            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlLeasingType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void loadFuelTypes()
        {
            ddlFuelType.Items.Clear();
            ddlFuelType.Items.Add(new ListItem("--- Select One ---", "0"));

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT FUEL_TYPE_CODE,FUEL_TYPE_NAME FROM MNBQ_FUEL_TYPE   ORDER BY  FUEL_TYPE_ORDER ASC ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlFuelType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        private void loadVehicleRiskTypes()
        {
            ddlVehicleRiskType.Items.Clear();
            ddlVehicleRiskType.Items.Add(new ListItem("--- Select One ---", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT RISK_TYPE_ID,RISK_TYPE FROM MNB_RISK_TYPE   ORDER BY RISK_TYPE ASC ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlVehicleRiskType.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void loadPeriodTypes()
        {
            ddlPeriodTypes.Items.Clear();
            ddlPeriodTypes.Items.Add(new ListItem("Select", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT PERIOD_TYPE_CODE,PERIOD_TYPE_NAME FROM MNBQ_PERIOD_TYPE  WHERE IS_ACTIVE=1 ORDER BY PERIOD_TYPE_NAME ASC ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlPeriodTypes.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }


      


        private void loadGoodsInTransits()
        {
            ddlGoodsInTransit.Items.Clear();
            ddlGoodsInTransit.Items.Add(new ListItem("--- Select One ---", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT GIT_TYPE_CODE,GIT_TYPE FROM MNBQ_GIT_TYPE  WHERE IS_ACTIVE=1 ORDER BY GIT_TYPE_CODE ASC ";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlGoodsInTransit.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

        }

        private void loadLegalLiabilityValues()
        {
            ddlLegalLiabilityVal.Items.Clear();
            ddlLegalLiabilityVal.Items.Add(new ListItem("Select One", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "  SELECT T.NAME " +
                             " FROM MNB_COVER_AMOUNTS_AND_RATES T  " +
                              " WHERE  T.TYPE='Legal Liability'  " +
                              " AND   TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlLegalLiabilityVal.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void loadTPPD()
        {
            ddlTPPD.Items.Clear();
            ddlTPPD.Items.Add(new ListItem("Select One", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "  SELECT T.NAME " +
                             " FROM MNB_COVER_AMOUNTS_AND_RATES T  " +
                              " WHERE  T.TYPE='TPPD'  " +
                              " AND   TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')";
            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlTPPD.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        private void loadEarnedNCB(string riskType)
        {

            string MotorCycleRiskType = System.Configuration.ConfigurationManager.AppSettings["MNBQMotorCycleRiskType"].ToString();
            ddlEarnedNCB.Items.Clear();
            ddlEarnedNCB.Items.Add(new ListItem("Select One", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            if (riskType.ToLower() == MotorCycleRiskType.ToLower())
            {
                selectQuery = "SELECT T.NAME,T.VALUE  FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-MC'  AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY T.ID  ";
            }
            else
            {
                selectQuery = "SELECT T.NAME,T.VALUE FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-OTHER' AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY  T.ID  ";
            }

            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlEarnedNCB.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }



        private void loaUpFrontNCB(string riskType)
        {
            string MotorCycleRiskType = System.Configuration.ConfigurationManager.AppSettings["MNBQMotorCycleRiskType"].ToString();
            ddlUpFrontNCB.Items.Clear();
            ddlUpFrontNCB.Items.Add(new ListItem("Select One", "0"));
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            if (riskType.ToLower() == MotorCycleRiskType.ToLower())
            {
                selectQuery = "SELECT T.NAME,T.VALUE  FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-UP-MC'  AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY T.ID  ";
            }
            else
            {
                selectQuery = "SELECT T.NAME,T.VALUE FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-UP-OTHER' AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY  T.ID  ";
            }

            cmd.CommandText = selectQuery;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlUpFrontNCB.Items.Add(new ListItem(dr[0].ToString(), dr[1].ToString()));
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }


        private void loadAgentTypes()
        {
            ddlAgentOrBroker.Items.Clear();

            string MNBQAgent = System.Configuration.ConfigurationManager.AppSettings["MNBQAgent"].ToString();
            string MNBQBroker = System.Configuration.ConfigurationManager.AppSettings["MNBQBroker"].ToString();
            string MNBQDirect = System.Configuration.ConfigurationManager.AppSettings["MNBQDirect"].ToString();
            string MNBQHNB = System.Configuration.ConfigurationManager.AppSettings["MNBQHNB"].ToString();
            string MNBQDirectSpecial = System.Configuration.ConfigurationManager.AppSettings["MNBQDirectSpecial"].ToString();

            ddlAgentOrBroker.Items.Add(new ListItem("--- Select ---", "0"));
            ddlAgentOrBroker.Items.Add(new ListItem(MNBQAgent, MNBQAgent));
            ddlAgentOrBroker.Items.Add(new ListItem(MNBQBroker, MNBQBroker));
            ddlAgentOrBroker.Items.Add(new ListItem(MNBQDirect, MNBQDirect));
            ddlAgentOrBroker.Items.Add(new ListItem(MNBQHNB, MNBQHNB));
            ddlAgentOrBroker.Items.Add(new ListItem(MNBQDirectSpecial, MNBQDirectSpecial));

        }
        private void loadPABToDriver()
        {
            ddlAddPABToDriver.Items.Clear();
            ddlAddPABToDriver.Items.Add(new ListItem("", "0"));
            for (int i = 1; i <= 3; i++)
            {
                ddlAddPABToDriver.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
        private void loadPABToPassenger()
        {
            ddlPABToPassenger.Items.Clear();
            ddlPABToPassenger.Items.Add(new ListItem("", "0"));
            for (int i = 1; i <= 7; i++)
            {
                ddlPABToPassenger.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void loadLegalLiability()
        {
            ddlLegalLiability.Items.Clear();
            ddlLegalLiability.Items.Add(new ListItem("", "0"));
            for (int i = 1; i <= 40; i++)
            {
                ddlLegalLiability.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        private void loadWCI()
        {
            ddlWCI.Items.Clear();
            ddlWCI.Items.Add(new ListItem("", "0"));
            for (int i = 1; i <= 3; i++)
            {
                ddlWCI.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

    }
}