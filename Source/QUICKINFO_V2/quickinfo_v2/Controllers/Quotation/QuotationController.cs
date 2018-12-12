using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System.Data.OracleClient;
using quickinfo_v2.Models.Quotation;

namespace quickinfo_v2.Controllers.Quotation
{

    public class QuotationController
    {
        private string connectionString;

        public QuotationController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
        }

        public DataTable GetQuotations(string whereStatement)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "   SELECT MM.QUOTATION_NO AS \"Quotation No\", MM.REQUEST_BY AS \"Requested By\", MM.CLIENT_NAME AS \"Client Name\" " +
            //      " , MM.VEHICLE_CHASIS_NO AS \"Vehicle/Chassi No\", MM.REQUEST_DATE AS \"Requested Date\", MS.STATUS_NAME AS \"Status\" FROM MNBQ_MAIN MM  " +
            //     " INNER JOIN MNBQ_STATUSES MS ON MM.STATUS=MS.STATUS_CODE " +
            //      " WHERE (" + whereStatement + ") ORDER BY MM.JOB_ID ASC";




            sql = "   SELECT QUOTATION_NO AS , MM.REQUEST_BY AS \"Requested By\", MM.CLIENT_NAME AS \"Client Name\" " +
                          " , MM.VEHICLE_CHASIS_NO AS \"Vehicle/Chassi No\", MM.REQUEST_DATE AS \"Requested Date\" FROM MNBQ_MAIN MM  " +
                          " WHERE (" + whereStatement + ") ORDER BY MM.JOB_ID ASC";







            da.SelectCommand = new OracleCommand(sql, con);

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(da.SelectCommand.ExecuteReader());
                return dt;
            }
            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable GetQuotationsTakaful(string whereStatement)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "   SELECT MM.QUOTATION_NO AS \"Quotation No\", MM.REQUEST_BY AS \"Requested By\", MM.CLIENT_NAME AS \"Client Name\" " +
            //      " , MM.VEHICLE_CHASIS_NO AS \"Vehicle/Chassi No\", MM.REQUEST_DATE AS \"Requested Date\", MS.STATUS_NAME AS \"Status\" FROM MNBQ_MAIN MM  " +
            //     " INNER JOIN MNBQ_STATUSES MS ON MM.STATUS=MS.STATUS_CODE " +
            //      " WHERE (" + whereStatement + ") ORDER BY MM.JOB_ID ASC";




            sql = "   SELECT QUOTATION_NO AS , MM.REQUEST_BY AS \"Requested By\", MM.CLIENT_NAME AS \"Client Name\" " +
                          " , MM.VEHICLE_CHASIS_NO AS \"Vehicle/Chassi No\", MM.REQUEST_DATE AS \"Requested Date\" FROM MNBQ_T_MAIN MM  " +
                          " WHERE (" + whereStatement + ") ORDER BY MM.JOB_ID ASC";







            da.SelectCommand = new OracleCommand(sql, con);

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(da.SelectCommand.ExecuteReader());
                return dt;
            }
            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        public QuotationMain GetQuotationMainDetails(string quotationNo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            if (quotationNo.Substring(5, 1) == "T" || quotationNo.Substring(5, 1) == "t")
            {
                sql = "   SELECT " +
                             "MM.JOB_ID             ," +//0
                              "MM.QUOTATION_NO      ," +//1
                              "MM.REQUEST_BY        ," +//2
                              "MM.CLIENT_NAME       ," +//3
                              "MM.VEHICLE_CHASIS_NO ," +//4
                              "MM.RISK_TYPE_ID 		," +//5
                              "MM.VEHICLE_TYPE_ID   ," +//6
                              "MM.VEHICLE_CLASS_ID  ," +//7
                              "MM.SUM_INSURED       ," +//8
                              "MM.PERIOD_TYPE_CODE  ," +//9
                              "MM.PERIOD_CODE		," +//10
                              "MM.AGENT_BROKER      ," +//11
                              "MM.LEASING_TYPE      ," +//12
                              "MM.FUEL_TYPE_CODE	," +//13
                              "MM.PRODUCT_CODE		," +//14
                              "MM.BRANCH_ID         ," +//15
                              "MM.REMARK            ," +//16
                              "MM.REQUEST_DATE      ," +//17
                              "MM.STATUS            ," +//18
                              "MM.USER_ID           ," +//19
                              "MM.REVISION_NO		," +//20
                              "MM.QUOT_YEAR         ," +//21
                              "MM.AGENT_BROKER_CODE         " +//22
                              " FROM MNBQ_T_MAIN MM  " +
                             " WHERE MM.QUOTATION_NO=:V_QUOTATION_NO";
            }
            else
            {
                sql = "   SELECT " +
                                "MM.JOB_ID             ," +//0
                                 "MM.QUOTATION_NO      ," +//1
                                 "MM.REQUEST_BY        ," +//2
                                 "MM.CLIENT_NAME       ," +//3
                                 "MM.VEHICLE_CHASIS_NO ," +//4
                                 "MM.RISK_TYPE_ID 		," +//5
                                 "MM.VEHICLE_TYPE_ID   ," +//6
                                 "MM.VEHICLE_CLASS_ID  ," +//7
                                 "MM.SUM_INSURED       ," +//8
                                 "MM.PERIOD_TYPE_CODE  ," +//9
                                 "MM.PERIOD_CODE		," +//10
                                 "MM.AGENT_BROKER      ," +//11
                                 "MM.LEASING_TYPE      ," +//12
                                 "MM.FUEL_TYPE_CODE	," +//13
                                 "MM.PRODUCT_CODE		," +//14
                                 "MM.BRANCH_ID         ," +//15
                                 "MM.REMARK            ," +//16
                                 "MM.REQUEST_DATE      ," +//17
                                 "MM.STATUS            ," +//18
                                 "MM.USER_ID           ," +//19
                                 "MM.REVISION_NO		," +//20
                                 "MM.QUOT_YEAR         ," +//21
                                 "MM.AGENT_BROKER_CODE         " +//22
                                 " FROM MNBQ_MAIN MM  " +
                                " WHERE MM.QUOTATION_NO=:V_QUOTATION_NO";

            }
            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_QUOTATION_NO", quotationNo));


            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                QuotationMain quotationMain = new QuotationMain(Convert.ToInt32(reader["JOB_ID"].ToString()),
                                        reader["QUOTATION_NO"].ToString(),
                                        reader["REQUEST_BY"].ToString(),
                                        reader["CLIENT_NAME"].ToString(),
                                        reader["VEHICLE_CHASIS_NO"].ToString(),
                                        reader["RISK_TYPE_ID"].ToString(),
                                        reader["VEHICLE_TYPE_ID"].ToString(),
                                        reader["VEHICLE_CLASS_ID"].ToString(),
                                        reader["SUM_INSURED"].ToString(),
                                        reader["PERIOD_TYPE_CODE"].ToString(),
                                        reader["PERIOD_CODE"].ToString(),
                                        reader["AGENT_BROKER"].ToString(),
                                        reader["LEASING_TYPE"].ToString(),
                                        reader["FUEL_TYPE_CODE"].ToString(),
                                        reader["PRODUCT_CODE"].ToString(),
                                        reader["BRANCH_ID"].ToString(),
                                        reader["REMARK"].ToString(),
                                        reader["REQUEST_DATE"].ToString(),
                                        reader["STATUS"].ToString(),
                                        reader["USER_ID"].ToString(),
                                        Convert.ToInt32(reader["REVISION_NO"].ToString()),
                                        reader["QUOT_YEAR"].ToString(),
                                        reader["AGENT_BROKER_CODE"].ToString());



                reader.Close();
                return quotationMain;

            }
            catch (Exception err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


    }
}