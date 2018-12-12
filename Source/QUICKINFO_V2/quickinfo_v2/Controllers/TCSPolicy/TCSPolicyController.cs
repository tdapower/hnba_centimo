using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

namespace quickinfo_v2.Controllers.TCSPolicy
{
    public class TCSPolicyController
    {
        private string connectionString;
        private string connectionStringTakaful;

        public TCSPolicyController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
            connectionStringTakaful = ConfigurationManager.ConnectionStrings["TAKAFULDB"].ToString();

        }


        public bool checkIsProposalNoAvailable(string proposalNo, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;

            bool returnVal = false;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 "T.POL_PROPOSAL_NUMBER  " +
                                 " FROM  T_POLICY T " +
                                 " WHERE T.POL_PROPOSAL_NUMBER='" + proposalNo + "'    ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //dr.Read();

                returnVal = true;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }



        public bool checkIsPolicyNoAvailable(string policyNo, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;

            bool returnVal = false;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 "T.POL_POLICY_NUMBER  " +
                                 " FROM  T_POLICY T " +
                                 " WHERE T.POL_POLICY_NUMBER='" + policyNo + "'    ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                //dr.Read();

                returnVal = true;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }


        public bool checkIsHNBPolicy(string policyId, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;

            bool returnVal = false;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT PPT.PPA_PTY_PARTY_CODE                            FROM T_POLICY_PARTY PPT " +
                            "  WHERE PPT.PPA_POL_POLICY_ID = '" + policyId + "'" +
                                   "   AND PPT.PPA_SHR_STAKE_HOLDER_FN_CODE IN ('HNB_BANK','HNB_STAFF') " +
                                   "   AND PPT.PPA_EFFECTIVE_END_DATE IS NULL  ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                returnVal = true;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }

        public bool checkIsSpecificBrokerCode(string policyId, string system, string brokerCode)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;

            bool returnVal = false;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT PPT.PPA_PTY_PARTY_CODE                            FROM T_POLICY_PARTY PPT " +
                            "  WHERE PPT.PPA_POL_POLICY_ID = '" + policyId + "'" +
                                   "   AND PPT.PPA_SHR_STAKE_HOLDER_FN_CODE = 'BROKER' " +
                                   "   AND PPT.ppa_pty_party_code = '" + brokerCode + "' " +
                                   "   AND PPT.PPA_EFFECTIVE_END_DATE IS NULL  ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                returnVal = true;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }


        public bool checkIsSpecificFinancialInstitute(string policyId, string system, string FNCode)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;

            bool returnVal = false;

            con.Open();


            String selectQuery = "";

            selectQuery = "   SELECT PPT.PPA_PTY_PARTY_CODE                            FROM T_POLICY_PARTY PPT " +
                            "  WHERE PPT.PPA_POL_POLICY_ID =:V_PPA_POL_POLICY_ID " +
                                   "   AND PPT.PPA_SHR_STAKE_HOLDER_FN_CODE =:V_PPA_SHR_STAKE_HOLDER_FN_CODE  " +
                                   "   AND PPT.PPA_EFFECTIVE_END_DATE IS NULL  ";



            OracleCommand cmd = new OracleCommand(selectQuery, con);
            cmd.Parameters.Add(new OracleParameter("V_PPA_POL_POLICY_ID", policyId));
            cmd.Parameters.Add(new OracleParameter("V_PPA_SHR_STAKE_HOLDER_FN_CODE", FNCode));


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                returnVal = true;
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }


        //public string getTargetBranch(string policyID, string system)
        //{

        //    string conString = "";
        //    if (system == "TCS")
        //    {
        //        conString = connectionString;
        //    }
        //    else if (system == "TAKAFUL")
        //    {
        //        conString = connectionStringTakaful;
        //    }

        //    OracleConnection con = new OracleConnection(conString);
        //    OracleDataReader dr;

        //    string returnVal = "";

        //    con.Open();

        //    OracleCommand cmd = new OracleCommand();
        //    cmd.Connection = con;
        //    String selectQuery = "";

        //    selectQuery = "   SELECT " +
        //                         "T.POL_POLICY_NUMBER  " +
        //                         " FROM  T_POLICY T " +
        //                         " WHERE T.POL_POLICY_NUMBER='" + policyNo + "'    ";

        //    cmd.CommandText = selectQuery;

        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        //dr.Read();

        //        returnVal = true;
        //    }
        //    dr.Close();
        //    dr.Dispose();
        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();

        //    return returnVal;

        //}
        public string getPolicyIdOfProposal(string proposalNo, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataReader dr;
            string returnVal = "";



            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 " TO_CHAR(T.POL_POLICY_ID)  " +
                                 " FROM  T_POLICY T " +
                                 " WHERE T.POL_PROPOSAL_NUMBER='" + proposalNo + "'    ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                returnVal = dr[0].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }

        public string getPolicyIdOfPolicyNo(string policyNo)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 " TO_CHAR(T.POL_POLICY_ID)  " +
                                 " FROM  T_POLICY T " +
                                 " WHERE T.POL_POLICY_NUMBER='" + policyNo + "'    ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                returnVal = dr[0].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }



        public DataTable searchTCSPolicies(string whereStatement, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 " TO_CHAR(T.POL_POLICY_ID) AS \"POL_POLICY_ID\",T.POL_POLICY_NUMBER, T.POL_PROPOSAL_NUMBER  " +
                                 " FROM  T_POLICY T " +
                                 " WHERE  (" + whereStatement + ")  ";


            DataTable dt = new DataTable();
            da.SelectCommand = new OracleCommand(selectQuery, con);

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


        public DataTable searchTCSPoliciesForCancellation(string whereStatement, string system)
        {

            string conString = "";
            if (system == "TCS")
            {
                conString = connectionString;
            }
            else if (system == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }

            OracleConnection con = new OracleConnection(conString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";



            selectQuery = "     SELECT a.POL_ID,a.pol_no,a.PRO_NO,a.pol_client,a.pol_reg_no,COMMON.GETINCEPTIONDATE(a.pol_id)   " +
                                        "as pol_start_date,a.pol_end_date,a.pol_status,a.pol_branch FROM crc_policy a  " +
                      "  inner join t_policy b on a.pol_id = b.pol_policy_id     " +
                      " where " + whereStatement + " AND (b.pol_policy_status = '05'  " +
                     "   or b.pol_policy_status = '06' or b.pol_policy_status = '13' or b.pol_policy_status = '25'  " +
                     "    or b.pol_policy_status = '10')  order by a.pol_no ";


            DataTable dt = new DataTable();
            da.SelectCommand = new OracleCommand(selectQuery, con);

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

        public DataTable getDebitNosOfPolicy(string policyNo)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";



            selectQuery = "   SELECT a.pol_no,a.pol_client,a.pol_reg_no,COMMON.GETINCEPTIONDATE(a.pol_id) as pol_start_date,a.pol_end_date,b.dncn_no, " +
                        " a.pol_status,b.basic,b.srcc,b.tc,b.crsf,b.admin_fee,b.stamp,b.nbt,b.policy_fee FROM crc_policy a  " +
                        " inner join T_REPORT_DNCN_GWP_THU b on a.pol_no = b.pol_no  " +
                         " where a.pol_no= '" + policyNo + "'  order by a.pol_no, b.dncn_no";


            DataTable dt = new DataTable();
            da.SelectCommand = new OracleCommand(selectQuery, con);

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
    }
}