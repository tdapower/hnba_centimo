using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using quickinfo_v2.Models.MNBNewBusinessWF;
using System.Data.OracleClient;
using quickinfo_v2.Controllers.TCSPolicy;

namespace quickinfo_v2.Controllers.MNBNewBusinessWF
{

    public class ProposalUploadController
    {
        private string connectionString;
        private string connectionStringTakaful;

        public ProposalUploadController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
            connectionStringTakaful = ConfigurationManager.ConnectionStrings["TAKAFULDB"].ToString();
        }

        public void InsertProposalUpload(ProposalUpload proposalUpload)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNBQ_PROPOSAL_UPLOAD", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_QUOTATION_NO", OracleType.VarChar));
            cmd.Parameters["V_QUOTATION_NO"].Value = proposalUpload.QuotationNo;

            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_NO", OracleType.VarChar));
            cmd.Parameters["V_VEHICLE_NO"].Value = proposalUpload.VehicleNo;

            cmd.Parameters.Add(new OracleParameter("V_ENGINE_NO", OracleType.VarChar));
            cmd.Parameters["V_ENGINE_NO"].Value = proposalUpload.EngineNo;

            cmd.Parameters.Add(new OracleParameter("V_CHASSIS_NO", OracleType.VarChar));
            cmd.Parameters["V_CHASSIS_NO"].Value = proposalUpload.ChassisNo;

            cmd.Parameters.Add(new OracleParameter("V_IS_COVERNOTE_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_COVERNOTE_AVAILABLE"].Value = proposalUpload.IsCoverNoteAvailable;

            cmd.Parameters.Add(new OracleParameter("V_COVERNOTE_PERIOD", OracleType.VarChar));
            cmd.Parameters["V_COVERNOTE_PERIOD"].Value = proposalUpload.CoverNotePeriod;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_1", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_1"].Value = proposalUpload.AddressLine1;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_2", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_2"].Value = proposalUpload.AddressLine2;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_3", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_3"].Value = proposalUpload.AddressLine3;

            cmd.Parameters.Add(new OracleParameter("V_YEAR_OF_MAKE", OracleType.VarChar));
            cmd.Parameters["V_YEAR_OF_MAKE"].Value = proposalUpload.YearOfMake;

            cmd.Parameters.Add(new OracleParameter("V_FIRST_REG_DATE", OracleType.DateTime));
            cmd.Parameters["V_FIRST_REG_DATE"].Value = proposalUpload.FirstRegDate;

            cmd.Parameters.Add(new OracleParameter("V_CUBIC_CAPACITY", OracleType.VarChar));
            cmd.Parameters["V_CUBIC_CAPACITY"].Value = proposalUpload.CubicCapacity;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_CODE"].Value = proposalUpload.EnteredUser;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = proposalUpload.EnteredUserBranchCode;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


        public void InsertEndorsement(ProposalUpload proposalUpload)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNBQ_ENDORSEMENT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_JOB_NUMBER", OracleType.VarChar));
            cmd.Parameters["V_JOB_NUMBER"].Value = proposalUpload.JobNumber;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_CODE"].Value = proposalUpload.EnteredUser;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = proposalUpload.EnteredUserBranchCode;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_NO"].Value = proposalUpload.TCSPolicyNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_PROPOSAL_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_PROPOSAL_NO"].Value = proposalUpload.TCSProposalNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_ID"].Value = proposalUpload.TCSPolicyId;

            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", OracleType.VarChar));
            cmd.Parameters["V_JOB_TYPE"].Value = proposalUpload.JobType;

            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_AVAILABLE"].Value = proposalUpload.IsDocsAvailable;

            cmd.Parameters.Add(new OracleParameter("V_ENDORSEMENT_TYPE", OracleType.VarChar));
            cmd.Parameters["V_ENDORSEMENT_TYPE"].Value = proposalUpload.EndorsementType;

            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = proposalUpload.Remarks;

            cmd.Parameters.Add(new OracleParameter("V_SYSTEM_NAME", OracleType.VarChar));
            cmd.Parameters["V_SYSTEM_NAME"].Value = proposalUpload.SystemName;


            cmd.Parameters.Add(new OracleParameter("V_IS_URGENT", OracleType.Number));
            cmd.Parameters["V_IS_URGENT"].Value = proposalUpload.IsUrgent;


            cmd.Parameters.Add(new OracleParameter("V_IS_CERTIFICATE_CONVERTION", OracleType.Number));
            cmd.Parameters["V_IS_CERTIFICATE_CONVERTION"].Value = proposalUpload.IsCertificateConvertion;


            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_PRINT_FROM_HDO", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_PRINT_FROM_HDO"].Value = proposalUpload.IsDocsPrintFromHDO;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public void InsertRenewal(ProposalUpload proposalUpload)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNBQ_RENEWAL", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_JOB_NUMBER", OracleType.VarChar));
            cmd.Parameters["V_JOB_NUMBER"].Value = proposalUpload.JobNumber;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_CODE"].Value = proposalUpload.EnteredUser;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = proposalUpload.EnteredUserBranchCode;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_NO"].Value = proposalUpload.TCSPolicyNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_PROPOSAL_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_PROPOSAL_NO"].Value = proposalUpload.TCSProposalNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_ID"].Value = proposalUpload.TCSPolicyId;

            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", OracleType.VarChar));
            cmd.Parameters["V_JOB_TYPE"].Value = proposalUpload.JobType;

            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_AVAILABLE"].Value = proposalUpload.IsDocsAvailable;


            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = proposalUpload.Remarks;

            cmd.Parameters.Add(new OracleParameter("V_IS_OWN_BRANCH_POLICY", OracleType.Number));
            cmd.Parameters["V_IS_OWN_BRANCH_POLICY"].Value = proposalUpload.IsOwnBranchPolicy;

            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", OracleType.VarChar));
            cmd.Parameters["V_BRANCH_CODE_OF_POLICY"].Value = proposalUpload.BranchOfPolicy;


            cmd.Parameters.Add(new OracleParameter("V_SYSTEM_NAME", OracleType.VarChar));
            cmd.Parameters["V_SYSTEM_NAME"].Value = proposalUpload.SystemName;


            cmd.Parameters.Add(new OracleParameter("V_IS_URGENT", OracleType.Number));
            cmd.Parameters["V_IS_URGENT"].Value = proposalUpload.IsUrgent;



            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_PRINT_FROM_HDO", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_PRINT_FROM_HDO"].Value = proposalUpload.IsDocsPrintFromHDO;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public void InsertCancellation(ProposalUpload proposalUpload)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNBQ_CANCELLATION", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_JOB_NUMBER", OracleType.VarChar));
            cmd.Parameters["V_JOB_NUMBER"].Value = proposalUpload.JobNumber;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_CODE"].Value = proposalUpload.EnteredUser;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = proposalUpload.EnteredUserBranchCode;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_NO"].Value = proposalUpload.TCSPolicyNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_PROPOSAL_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_PROPOSAL_NO"].Value = proposalUpload.TCSProposalNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_ID"].Value = proposalUpload.TCSPolicyId;

            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", OracleType.VarChar));
            cmd.Parameters["V_JOB_TYPE"].Value = proposalUpload.JobType;

            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_AVAILABLE"].Value = proposalUpload.IsDocsAvailable;

            cmd.Parameters.Add(new OracleParameter("V_CANCELLATION_TYPE", OracleType.VarChar));
            cmd.Parameters["V_CANCELLATION_TYPE"].Value = proposalUpload.CancellationType;

            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = proposalUpload.Remarks;

            cmd.Parameters.Add(new OracleParameter("V_IS_OWN_BRANCH_POLICY", OracleType.Number));
            cmd.Parameters["V_IS_OWN_BRANCH_POLICY"].Value = proposalUpload.IsOwnBranchPolicy;

            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", OracleType.VarChar));
            cmd.Parameters["V_BRANCH_CODE_OF_POLICY"].Value = proposalUpload.BranchOfPolicy;

            cmd.Parameters.Add(new OracleParameter("V_SYSTEM_NAME", OracleType.VarChar));
            cmd.Parameters["V_SYSTEM_NAME"].Value = proposalUpload.SystemName;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


        public void InsertFastTrack(ProposalUpload proposalUpload)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNBQ_FASTTRACK", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_JOB_NUMBER", OracleType.VarChar));
            cmd.Parameters["V_JOB_NUMBER"].Value = proposalUpload.JobNumber;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_CODE"].Value = proposalUpload.EnteredUser;

            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = proposalUpload.EnteredUserBranchCode;

            //cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            //cmd.Parameters["V_TCS_POLICY_NO"].Value = proposalUpload.TCSPolicyNo;

            cmd.Parameters.Add(new OracleParameter("V_TCS_PROPOSAL_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_PROPOSAL_NO"].Value = proposalUpload.TCSProposalNo;

            //cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));
            //cmd.Parameters["V_TCS_POLICY_ID"].Value = proposalUpload.TCSPolicyId;

            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", OracleType.VarChar));
            cmd.Parameters["V_JOB_TYPE"].Value = proposalUpload.JobType;

            cmd.Parameters.Add(new OracleParameter("V_IS_DOCS_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_DOCS_AVAILABLE"].Value = proposalUpload.IsDocsAvailable;


            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = proposalUpload.Remarks;

            cmd.Parameters.Add(new OracleParameter("V_IS_OWN_BRANCH_POLICY", OracleType.Number));
            cmd.Parameters["V_IS_OWN_BRANCH_POLICY"].Value = proposalUpload.IsOwnBranchPolicy;

            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", OracleType.VarChar));
            cmd.Parameters["V_BRANCH_CODE_OF_POLICY"].Value = proposalUpload.BranchOfPolicy;


            cmd.Parameters.Add(new OracleParameter("V_SYSTEM_NAME", OracleType.VarChar));
            cmd.Parameters["V_SYSTEM_NAME"].Value = proposalUpload.SystemName;


            cmd.Parameters.Add(new OracleParameter("V_IS_URGENT", OracleType.Number));
            cmd.Parameters["V_IS_URGENT"].Value = proposalUpload.IsUrgent;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }



        public void UpdateProposalUpload(ProposalUpload proposalUpload)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_PROPOSAL_UPLOAD", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUpload.ProposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_QUOTATION_NO", OracleType.VarChar));
            cmd.Parameters["V_QUOTATION_NO"].Value = proposalUpload.QuotationNo;

            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_NO", OracleType.VarChar));
            cmd.Parameters["V_VEHICLE_NO"].Value = proposalUpload.VehicleNo;

            cmd.Parameters.Add(new OracleParameter("V_ENGINE_NO", OracleType.VarChar));
            cmd.Parameters["V_ENGINE_NO"].Value = proposalUpload.EngineNo;

            cmd.Parameters.Add(new OracleParameter("V_CHASSIS_NO", OracleType.VarChar));
            cmd.Parameters["V_CHASSIS_NO"].Value = proposalUpload.ChassisNo;

            cmd.Parameters.Add(new OracleParameter("V_IS_COVERNOTE_AVAILABLE", OracleType.Number));
            cmd.Parameters["V_IS_COVERNOTE_AVAILABLE"].Value = proposalUpload.IsCoverNoteAvailable;

            cmd.Parameters.Add(new OracleParameter("V_COVERNOTE_PERIOD", OracleType.VarChar));
            cmd.Parameters["V_COVERNOTE_PERIOD"].Value = proposalUpload.CoverNotePeriod;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_1", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_1"].Value = proposalUpload.AddressLine1;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_2", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_2"].Value = proposalUpload.AddressLine2;

            cmd.Parameters.Add(new OracleParameter("V_ADDRESS_LINE_3", OracleType.VarChar));
            cmd.Parameters["V_ADDRESS_LINE_3"].Value = proposalUpload.AddressLine3;

            cmd.Parameters.Add(new OracleParameter("V_YEAR_OF_MAKE", OracleType.VarChar));
            cmd.Parameters["V_YEAR_OF_MAKE"].Value = proposalUpload.YearOfMake;

            cmd.Parameters.Add(new OracleParameter("V_FIRST_REG_DATE", OracleType.DateTime));
            cmd.Parameters["V_FIRST_REG_DATE"].Value = proposalUpload.FirstRegDate;

            cmd.Parameters.Add(new OracleParameter("V_CUBIC_CAPACITY", OracleType.VarChar));
            cmd.Parameters["V_CUBIC_CAPACITY"].Value = proposalUpload.CubicCapacity;



            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }



        public void PrioritizeJob(string proposalUploadId, string remarks, string userCode)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("MNBQ_PRIORITIZE_JOB", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = remarks;

            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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

        public void BlacklistPolicy(string vehicleNo, string policyNo, string remarks, string userCode)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNB_WF_BLACKLIST_POLICY", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_NO", OracleType.VarChar));
            cmd.Parameters["V_VEHICLE_NO"].Value = vehicleNo;

            cmd.Parameters.Add(new OracleParameter("V_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_POLICY_NO"].Value = policyNo;


            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = remarks;

            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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


        public void SaveCentralizeUser(string userCode, string userName, int status)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_MNB_WF_USER", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            cmd.Parameters.Add(new OracleParameter("V_USER_NAME", OracleType.VarChar));
            cmd.Parameters["V_USER_NAME"].Value = userName;


            cmd.Parameters.Add(new OracleParameter("V_STATUS", OracleType.Number));
            cmd.Parameters["V_STATUS"].Value = status;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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
        public void UpdateCentralizeUser(string userCode, string userName, int status)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNB_WF_USER", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            cmd.Parameters.Add(new OracleParameter("V_USER_NAME", OracleType.VarChar));
            cmd.Parameters["V_USER_NAME"].Value = userName;


            cmd.Parameters.Add(new OracleParameter("V_STATUS", OracleType.Number));
            cmd.Parameters["V_STATUS"].Value = status;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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

        public void ReProcessJob(string jobNo, string jobType, string userCode)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INST_MNBQ_WF_RE_PROC_JOBS_LOG", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_JOB_NO", OracleType.VarChar));
            cmd.Parameters["V_JOB_NO"].Value = jobNo;

            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", OracleType.VarChar));
            cmd.Parameters["V_JOB_TYPE"].Value = jobType;

            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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
        public void RevertJobToScrutinizing(string proposalUploadId, string revertToStatus, string remarks, string userCode)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("MNBQ_REVERT_JOB", con);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_REVERT_TO_STATUS", OracleType.VarChar));
            cmd.Parameters["V_REVERT_TO_STATUS"].Value = revertToStatus;

            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = remarks;

            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_USER_CODE"].Value = userCode;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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



        public ProposalUpload GetUploadedProposal(int proposalUploadId)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "SELECT " +
            //    " PROPOSAL_UPLOAD_ID , " +
            //    " QUOTATION_NO 	 , " +
            //    " VEHICLE_NO 	, " +
            //    " ENGINE_NO 	, " +
            //    " CHASSIS_NO 	 , " +
            //    " IS_COVERNOTE_AVAILABLE 	, " +
            //    " COVERNOTE_PERIOD	 , " +
            //    " ADDRESS_LINE_1 	 , " +
            //    " ADDRESS_LINE_2	 , " +
            //    " ADDRESS_LINE_3	, " +
            //    " YEAR_OF_MAKE , " +
            //    " FIRST_REG_DATE	, " +
            //    " CUBIC_CAPACITY		, " +
            //    " ENTERED_USER_CODE	, " +
            //    " ENTERED_USER_BRANCH_CODE	, " +
            //    " TCS_POLICY_NO	, " +
            //    " TCS_PROPOSAL_NO	, " +
            //      " TCS_POLICY_ID	, " +
            //    " SYS_DATE		,	 " +
            //    " JOB_TYPE	, " +
            //    " JOB_NUMBER			 " +

            //    " FROM MNBQ_PROPOSAL_UPLOAD " +
            //  " WHERE PROPOSAL_UPLOAD_ID = :V_PROPOSAL_UPLOAD_ID ";


            sql = "SELECT " +
                    " MPU.PROPOSAL_UPLOAD_ID , " +
                    " MPU.QUOTATION_NO 	 , " +
                    " MPU.VEHICLE_NO 	, " +
                    " MPU.ENGINE_NO 	, " +
                    " MPU.CHASSIS_NO 	 , " +
                    " MPU.IS_COVERNOTE_AVAILABLE 	, " +
                    " MPU.COVERNOTE_PERIOD	 , " +
                    " MPU.ADDRESS_LINE_1 	 , " +
                    " MPU.ADDRESS_LINE_2	 , " +
                    " MPU.ADDRESS_LINE_3	, " +
                    " MPU.YEAR_OF_MAKE , " +
                    " MPU.FIRST_REG_DATE	, " +
                    " MPU.CUBIC_CAPACITY		, " +
                    " MPU.ENTERED_USER_CODE	, " +
                    " MPU.ENTERED_USER_BRANCH_CODE	, " +
                    " MPU.TCS_POLICY_NO	, " +
                    " MPU.TCS_PROPOSAL_NO	, " +
                    " MPU.TCS_POLICY_ID	, " +
                    " MPU.SYS_DATE		,	 " +
                    " MPU.JOB_TYPE	, " +
                    " MPU.JOB_NUMBER,			 " +
                    " MPU.SYSTEM_NAME,			 " +
                    " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                    " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                    " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " MPU.IS_CERTIFICATE_CONVERTION			 " +
                    " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                    " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                    " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
                    " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
                    " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                    " WHERE MPU.PROPOSAL_UPLOAD_ID = :V_PROPOSAL_UPLOAD_ID ";









            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", proposalUploadId));


            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                          Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));







                reader.Close();
                return proposalUpload;

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


        public ProposalUpload GetEarliestUploadedProposalOfGivenStatus(string status)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "SELECT " +
            //    " PROPOSAL_UPLOAD_ID , " +
            //    " QUOTATION_NO 	 , " +
            //    " VEHICLE_NO 	, " +
            //    " ENGINE_NO 	, " +
            //    " CHASSIS_NO 	 , " +
            //    " IS_COVERNOTE_AVAILABLE 	, " +
            //    " COVERNOTE_PERIOD	 , " +
            //    " ADDRESS_LINE_1 	 , " +
            //    " ADDRESS_LINE_2	 , " +
            //    " ADDRESS_LINE_3	, " +
            //    " YEAR_OF_MAKE , " +
            //    " FIRST_REG_DATE	, " +
            //    " CUBIC_CAPACITY		, " +
            //    " ENTERED_USER_CODE	, " +
            //    " ENTERED_USER_BRANCH_CODE	, " +
            //    " TCS_POLICY_NO	, " +
            //    " TCS_PROPOSAL_NO	, " +
            //    " SYS_DATE			 " +
            //    " FROM MNBQ_PROPOSAL_UPLOAD " +
            //  " WHERE STATUS = :V_STATUS " +
            //  " ORDER BY SYS_DATE ASC";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
             " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
             " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.STATUS = :V_STATUS " +
                 " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));


            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                               Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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

        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusOfSpecificType(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "SELECT " +
            //    " PROPOSAL_UPLOAD_ID , " +
            //    " QUOTATION_NO 	 , " +
            //    " VEHICLE_NO 	, " +
            //    " ENGINE_NO 	, " +
            //    " CHASSIS_NO 	 , " +
            //    " IS_COVERNOTE_AVAILABLE 	, " +
            //    " COVERNOTE_PERIOD	 , " +
            //    " ADDRESS_LINE_1 	 , " +
            //    " ADDRESS_LINE_2	 , " +
            //    " ADDRESS_LINE_3	, " +
            //    " YEAR_OF_MAKE , " +
            //    " FIRST_REG_DATE	, " +
            //    " CUBIC_CAPACITY		, " +
            //    " ENTERED_USER_CODE	, " +
            //    " ENTERED_USER_BRANCH_CODE	, " +
            //    " TCS_POLICY_NO	, " +
            //    " TCS_PROPOSAL_NO	, " +
            //    " SYS_DATE			 " +
            //    " FROM MNBQ_PROPOSAL_UPLOAD " +
            //  " WHERE STATUS = :V_STATUS " +
            //  " ORDER BY SYS_DATE ASC";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
             " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
             " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.STATUS = :V_STATUS  AND MPU.JOB_TYPE= :V_JOB_TYPE  " +
                 " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                                       Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));


                reader.Close();
                return proposalUpload;

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

        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusOfCertificateConvertion(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
             " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
             " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.STATUS = :V_STATUS  AND MPU.JOB_TYPE= :V_JOB_TYPE AND  MPU.IS_CERTIFICATE_CONVERTION=1 " +
                 " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                                       Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));


                reader.Close();
                return proposalUpload;

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


        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusOfNotSpecifiedType(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            //sql = "SELECT " +
            //    " PROPOSAL_UPLOAD_ID , " +
            //    " QUOTATION_NO 	 , " +
            //    " VEHICLE_NO 	, " +
            //    " ENGINE_NO 	, " +
            //    " CHASSIS_NO 	 , " +
            //    " IS_COVERNOTE_AVAILABLE 	, " +
            //    " COVERNOTE_PERIOD	 , " +
            //    " ADDRESS_LINE_1 	 , " +
            //    " ADDRESS_LINE_2	 , " +
            //    " ADDRESS_LINE_3	, " +
            //    " YEAR_OF_MAKE , " +
            //    " FIRST_REG_DATE	, " +
            //    " CUBIC_CAPACITY		, " +
            //    " ENTERED_USER_CODE	, " +
            //    " ENTERED_USER_BRANCH_CODE	, " +
            //    " TCS_POLICY_NO	, " +
            //    " TCS_PROPOSAL_NO	, " +
            //    " SYS_DATE			 " +
            //    " FROM MNBQ_PROPOSAL_UPLOAD " +
            //  " WHERE STATUS = :V_STATUS " +
            //  " ORDER BY SYS_DATE ASC";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
             " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
             " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.STATUS = :V_STATUS  AND MPU.JOB_TYPE <> :V_JOB_TYPE  " +
                 " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                     Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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


        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusAndUser(string status, string userCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";


            //sql = "SELECT " +
            //      " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
            //       " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
            //       " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
            //       " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
            //       " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
            //       " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
            //       " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
            //       " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
            //       " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
            //       " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
            //       " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
            //       " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
            //       " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
            //       " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
            //       " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
            //       " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
            //       " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
            //       " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
            //        " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
            //       " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
            //       " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
            //      " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
            //     " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\"  , " +
            //       " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
            //        " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\" " +
            //       " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
            //       " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
            //    " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
            //          " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MPU.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MPU.STATUS=MPUF.STATUS  " +
            //     " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
            //     " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
            //     " WHERE MPU.STATUS = :V_STATUS  AND MPUF.USER_CODE=:V_USERCODE " +
            //     " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                   " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\"  , " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
                      " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MPU.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MPU.STATUS=MPUF.STATUS  " +
                 " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
                 " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.STATUS = :V_STATUS  AND MPUF.USER_CODE=:V_USERCODE " +
                    " and MPUF.seq_id=(select max(tt.seq_id) from mnbq_proposal_upload_followup tt where tt.proposal_upload_id=MPUF.proposal_upload_id and tt.status=:V_STATUS) " +
                 " ORDER BY  MPU.IS_PRIORITIZED,MPU.SYS_DATE ASC";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_USERCODE", userCode));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                            Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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



        public ProposalUpload GetUploadedProposalOfProposalUploadId(string ProposalUploadId)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                   " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\"  , " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
                      " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MPU.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MPU.STATUS=MPUF.STATUS  " +
                 " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
                 " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE MPU.PROPOSAL_UPLOAD_ID = :V_PROPOSAL_UPLOAD_ID   ";







            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", ProposalUploadId));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                            Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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
        public DataTable GetUploadedProposals(string whereStatement)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            sql = "SELECT  " +
                    " PROPOSAL_UPLOAD_ID,  " +
                    " QUOTATION_NO,  " +
                    " VEHICLE_NO,  " +
                    " ENGINE_NO,  " +
                    " CHASSIS_NO  " +
                    " FROM   " +
                    " MNBQ_PROPOSAL_UPLOAD  " +
                 " WHERE " + whereStatement +
              " ORDER BY    QUOTATION_NO ";


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

        public DataTable GetJobsForManage(string jobOrQuoteNo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";


            OracleCommand cmd = null;


            //sql = "SELECT  " +
            //" PROPOSAL_UPLOAD_ID,  " +
            //    "  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" , " +
            // "   CASE WHEN JOB_TYPE ='N' THEN 'New' WHEN JOB_TYPE='E' THEN 'Endorsement' WHEN JOB_TYPE='R' THEN 'Renewal' WHEN JOB_TYPE='C' THEN 'Cancellation' ELSE '' END   AS \"Job Type\" " +
            //" FROM   " +
            //" MNBQ_PROPOSAL_UPLOAD  " +
            // " WHERE QUOTATION_NO LIKE '%" + jobOrQuoteNo + "%' OR  JOB_NUMBER LIKE '%" + jobOrQuoteNo + "%'  AND status='INITIAL' " +
            //" ORDER BY  SYS_DATE ASC ";


            sql = "SELECT  " +
                " PROPOSAL_UPLOAD_ID,  " +
                    "  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" , " +
                 "   CASE WHEN JOB_TYPE ='N' THEN 'New' WHEN JOB_TYPE='E' THEN 'Endorsement' WHEN JOB_TYPE='R' THEN 'Renewal' WHEN JOB_TYPE='C' THEN 'Cancellation' ELSE '' END   AS \"Job Type\" " +
                " FROM   " +
                " MNBQ_PROPOSAL_UPLOAD  " +
                 " WHERE QUOTATION_NO LIKE '%" + jobOrQuoteNo + "%' OR  JOB_NUMBER LIKE '%" + jobOrQuoteNo + "%'  " +
                " ORDER BY  SYS_DATE ASC ";


            //   cmd = new OracleCommand(sql, con);


            // cmd.Parameters.Add(new OracleParameter("V_QUOTATION_NO", jobOrQuoteNo));

            cmd = new OracleCommand(sql, con);

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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


        public DataTable GetQuotationNosOfStatus(string status)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            //   sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\"," +
            //"JOB_TYPE FROM MNBQ_PROPOSAL_UPLOAD MM  " +
            //          " WHERE STATUS = :V_STATUS AND rownum=(select max(rownum) from MNBQ_PROPOSAL_UPLOAD WHERE STATUS = :V_STATUS)  ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";


            string FST_DOCS_RECEIVED = System.Configuration.ConfigurationManager.AppSettings["FST_DOCS_RECEIVED"].ToString();


            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                " WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS OR STATUS=:V_FST_STATUS ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));

            cmd.Parameters.Add(new OracleParameter("V_FST_STATUS", FST_DOCS_RECEIVED));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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


        public DataTable GetQuotationNosOfStatusWithoutCancellations(string status)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            //   sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\"," +
            //"JOB_TYPE FROM MNBQ_PROPOSAL_UPLOAD MM  " +
            //          " WHERE STATUS = :V_STATUS AND rownum=(select max(rownum) from MNBQ_PROPOSAL_UPLOAD WHERE STATUS = :V_STATUS)  ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";


            string FST_DOCS_RECEIVED = System.Configuration.ConfigurationManager.AppSettings["FST_DOCS_RECEIVED"].ToString();


            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                "  WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE  MM.JOB_TYPE<>'C' AND STATUS = :V_STATUS OR STATUS=:V_FST_STATUS  ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));

            cmd.Parameters.Add(new OracleParameter("V_FST_STATUS", FST_DOCS_RECEIVED));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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

        public DataTable GetQuotationNosOfStatusWithUser(string status)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";


            sql = "   SELECT  CASE WHEN MM.JOB_TYPE ='N' THEN QUOTATION_NO WHEN MM.JOB_TYPE='E' THEN MM.JOB_NUMBER WHEN MM.JOB_TYPE='R' THEN MM.JOB_NUMBER " +
                " WHEN MM.JOB_TYPE='C' THEN MM.JOB_NUMBER WHEN MM.JOB_TYPE='F' THEN MM.JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "MM.JOB_TYPE,w.USER_NAME AS \"User\",MM.IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                " INNER JOIN  mnbq_proposal_upload_followup F ON MM.proposal_upload_id=F.proposal_upload_id AND MM.STATUS=F.STATUS " +
                    " left join wf_admin_users w on F.user_code=w.user_code " +
                          " WHERE MM.STATUS = :V_STATUS " +
                          "    AND f.seq_id=(select max(ff.seq_id) from mnbq_proposal_upload_followup ff where  ff.proposal_upload_id=mm.proposal_upload_id ) " +
                          " ORDER BY  MM.IS_PRIORITIZED,MM.SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));


            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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

        public DataTable GetQuotationNosOfStatusOfSpecificType(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                " WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS AND JOB_TYPE= :V_JOB_TYPE  ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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

        public DataTable GetQuotationNosOfStatusOfCertificateConvertion(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                " WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS AND JOB_TYPE= :V_JOB_TYPE AND IS_CERTIFICATE_CONVERTION=1 ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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


        public DataTable GetQuotationNosOfStatusExceptOfSpecificType(string status, string jobType)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                " WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS AND JOB_TYPE<> :V_JOB_TYPE  ORDER BY  MM.IS_PRIORITIZED,SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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
        public DataTable GetQuotationNosOfStatusOfUsers(string status, string userCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            //sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\"," +
            //    "JOB_TYPE FROM MNBQ_PROPOSAL_UPLOAD MM  " +
            //    " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MM.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MM.STATUS=MPUF.STATUS  " +
            //              " WHERE MM.STATUS = :V_STATUS AND MPUF.USER_CODE=:V_USERCODE ORDER BY  MM.IS_PRIORITIZED,MM.SYS_DATE ASC";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                " WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                         "JOB_TYPE,IS_CERTIFICATE_CONVERTION, MM.PROPOSAL_UPLOAD_ID   "+
                         " FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                         " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MM.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MM.STATUS=MPUF.STATUS  " +
                                   " WHERE MM.STATUS = :V_STATUS AND MPUF.USER_CODE=:V_USERCODE " +
                                   " and MPUF.seq_id=(select max(tt.seq_id) from mnbq_proposal_upload_followup tt where tt.proposal_upload_id=MPUF.proposal_upload_id and tt.status=:V_STATUS) " +
                                   "ORDER BY  MM.IS_PRIORITIZED,MM.SYS_DATE ASC";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));

            cmd.Parameters.Add(new OracleParameter("V_USERCODE", userCode));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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

        public DataTable GetQuotationNosOfStatusOfUsersOfSpecificType(string status, string jobType, string userCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                "WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                         "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                         " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MM.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MM.STATUS=MPUF.STATUS  " +
                                   " WHERE MM.STATUS = :V_STATUS AND JOB_TYPE= :V_JOB_TYPE AND MPUF.USER_CODE=:V_USERCODE " +
                                   " and MPUF.seq_id=(select max(tt.seq_id) from mnbq_proposal_upload_followup tt where tt.proposal_upload_id=MPUF.proposal_upload_id and tt.status=:V_STATUS) " +
                                   "ORDER BY  MM.IS_PRIORITIZED,MM.SYS_DATE ASC";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));
            cmd.Parameters.Add(new OracleParameter("V_USERCODE", userCode));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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
        public DataTable GetQuotationNosOfStatusOfUsersExceptOfSpecificType(string status, string jobType, string userCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";



            sql = "   SELECT  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER " +
                "WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER  ELSE '' END AS \"Job/Quotation No\"," +
                         "JOB_TYPE,IS_CERTIFICATE_CONVERTION FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                         " LEFT JOIN MNBQ_PROPOSAL_UPLOAD_FOLLOWUP MPUF ON MM.PROPOSAL_UPLOAD_ID=MPUF.PROPOSAL_UPLOAD_ID  AND  MM.STATUS=MPUF.STATUS  " +
                                   " WHERE MM.STATUS = :V_STATUS AND JOB_TYPE!= :V_JOB_TYPE AND MPUF.USER_CODE=:V_USERCODE " +
                                   " and MPUF.seq_id=(select max(tt.seq_id) from mnbq_proposal_upload_followup tt where tt.proposal_upload_id=MPUF.proposal_upload_id and tt.status=:V_STATUS) " +
                                   "ORDER BY  MM.IS_PRIORITIZED,MM.SYS_DATE ASC";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_JOB_TYPE", jobType));
            cmd.Parameters.Add(new OracleParameter("V_USERCODE", userCode));

            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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
        public string GetRemarksOfStatus(string docUploadId, string status)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = " select t.remarks from mnbq_proposal_upload_followup t where t.proposal_upload_id=:V_DOC_UPLOAD_ID and t.status = :V_STATUS   ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_DOC_UPLOAD_ID", docUploadId));
            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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

        public string GetJobNoFromProposalUploadId(string docUploadId)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = " select  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER WHEN JOB_TYPE='F' THEN JOB_NUMBER ELSE '' END as \"JOB_NO\" from MNBQ_PROPOSAL_UPLOAD t where t.proposal_upload_id=:V_DOC_UPLOAD_ID  ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_DOC_UPLOAD_ID", docUploadId));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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
        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusForRenewalDocUpload(string status, string ownBranchCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";


            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\", " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\", " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
                 " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
                 " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE STATUS = :V_STATUS AND BRANCH_CODE_OF_POLICY=:V_BRANCH_CODE_OF_POLICY " +
                 " ORDER BY SYS_DATE ASC";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", ownBranchCode));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                         reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                               Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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
        public ProposalUpload GetEarliestUploadedProposalOfGivenStatusForCancellationDocUpload(string status, string ownBranchCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";


            sql = "SELECT " +
                  " (CASE WHEN MPU.PROPOSAL_UPLOAD_ID  IS NULL THEN 0  ELSE MPU.PROPOSAL_UPLOAD_ID END) AS  \"PROPOSAL_UPLOAD_ID\", " +
                   " (CASE WHEN MPU.QUOTATION_NO  IS NULL THEN ''  ELSE MPU.QUOTATION_NO END) AS  \"QUOTATION_NO\", " +
                   " (CASE WHEN MPU.VEHICLE_NO  IS NULL THEN ''  ELSE MPU.VEHICLE_NO END) AS  \"VEHICLE_NO\", " +
                   " (CASE WHEN MPU.ENGINE_NO  IS NULL THEN ''  ELSE MPU.ENGINE_NO END) AS  \"ENGINE_NO\", " +
                   " (CASE WHEN MPU.CHASSIS_NO  IS NULL THEN ''  ELSE MPU.CHASSIS_NO END) AS  \"CHASSIS_NO\", " +
                   " (CASE WHEN MPU.IS_COVERNOTE_AVAILABLE  IS NULL THEN 0  ELSE MPU.IS_COVERNOTE_AVAILABLE END) AS  \"IS_COVERNOTE_AVAILABLE\", " +
                   " (CASE WHEN MPU.COVERNOTE_PERIOD  IS NULL THEN ''  ELSE MPU.COVERNOTE_PERIOD END) AS  \"COVERNOTE_PERIOD\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_1  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_1 END) AS  \"ADDRESS_LINE_1\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_2  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_2 END) AS  \"ADDRESS_LINE_2\", " +
                   " (CASE WHEN MPU.ADDRESS_LINE_3  IS NULL THEN ''  ELSE MPU.ADDRESS_LINE_3 END) AS  \"ADDRESS_LINE_3\", " +
                   " (CASE WHEN MPU.YEAR_OF_MAKE  IS NULL THEN ''  ELSE MPU.YEAR_OF_MAKE END) AS  \"YEAR_OF_MAKE\", " +
                   " (CASE WHEN MPU.FIRST_REG_DATE  IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')   ELSE MPU.FIRST_REG_DATE END) AS  \"FIRST_REG_DATE\", " +
                   " (CASE WHEN MPU.CUBIC_CAPACITY  IS NULL THEN ''  ELSE MPU.CUBIC_CAPACITY END) AS  \"CUBIC_CAPACITY\", " +
                   " (CASE WHEN MPU.ENTERED_USER_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_CODE END) AS  \"ENTERED_USER_CODE\", " +
                   " (CASE WHEN MPU.ENTERED_USER_BRANCH_CODE  IS NULL THEN ''  ELSE MPU.ENTERED_USER_BRANCH_CODE END) AS  \"ENTERED_USER_BRANCH_CODE\", " +
                   " (CASE WHEN MPU.TCS_POLICY_NO  IS NULL THEN ''  ELSE MPU.TCS_POLICY_NO END) AS  \"TCS_POLICY_NO\", " +
                   " (CASE WHEN MPU.TCS_PROPOSAL_NO  IS NULL THEN ''  ELSE MPU.TCS_PROPOSAL_NO END) AS  \"TCS_PROPOSAL_NO\", " +
                   " (CASE WHEN MPU.TCS_POLICY_ID  IS NULL THEN ''  ELSE MPU.TCS_POLICY_ID END) AS  \"TCS_POLICY_ID\", " +
                    " (CASE WHEN MPU.JOB_TYPE  IS NULL THEN ''  ELSE MPU.JOB_TYPE END) AS  \"JOB_TYPE\", " +
                   " (CASE WHEN MPU.JOB_NUMBER  IS NULL THEN ''  ELSE MPU.JOB_NUMBER END) AS  \"JOB_NUMBER\" , " +
                     " (CASE WHEN MPU.SYSTEM_NAME  IS NULL THEN ''  ELSE MPU.SYSTEM_NAME END) AS  \"SYSTEM_NAME\", " +
                  " (CASE WHEN IT.ISSUE_TYPE_NAME  IS NULL THEN ''  ELSE IT.ISSUE_TYPE_NAME END) AS  \"ISSUE_TYPE_NAME\", " +
                 " (CASE WHEN PT.POLICY_TYPE_NAME  IS NULL THEN ''  ELSE PT.POLICY_TYPE_NAME END) AS  \"POLICY_TYPE_NAME\"  , " +
                   " (CASE WHEN MCT.CANCELLATION_TYPE_NAME  IS NULL THEN ''  ELSE MCT.CANCELLATION_TYPE_NAME END) AS  \"CANCELLATION_TYPE_NAME\", " +
                    " (CASE WHEN MET.ENDORSEMENT_TYPE_NAME  IS NULL THEN ''  ELSE MET.ENDORSEMENT_TYPE_NAME END) AS  \"ENDORSEMENT_TYPE_NAME\", " +
                    " (CASE WHEN MPU.IS_CERTIFICATE_CONVERTION  IS NULL THEN 0  ELSE MPU.IS_CERTIFICATE_CONVERTION END) AS  \"CERTIFICATE_CONVERTION\" " +
                   " FROM MNBQ_PROPOSAL_UPLOAD MPU " +
                   " LEFT JOIN MNBQ_WF_ISSUE_TYPE  IT ON MPU.ISSUE_TYPE_CODE=IT.ISSUE_TYPE_CODE " +
                " LEFT JOIN MNBQ_WF_POLICY_TYPE  PT ON MPU.POLICY_TYPE_CODE=PT.POLICY_TYPE_CODE " +
                 " LEFT JOIN MNBQ_CANCELLATION_TYPE  MCT ON MPU.CANCELLATION_TYPE=MCT.CANCELLATION_TYPE_CODE " +
                 " LEFT JOIN MNBQ_ENDORSEMENT_TYPE  MET ON MPU.ENDORSEMENT_TYPE=MET.ENDORSEMENT_TYPE_CODE " +
                 " WHERE STATUS = :V_STATUS AND BRANCH_CODE_OF_POLICY=:V_BRANCH_CODE_OF_POLICY " +
                 " ORDER BY SYS_DATE ASC";





            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", ownBranchCode));

            da.SelectCommand = cmd;


            try
            {
                con.Open();
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // Check if the query returned a record.
                if (!reader.HasRows) return null;

                // Get the first row.
                reader.Read();


                ProposalUpload proposalUpload = new ProposalUpload((Convert.ToInt32(reader["PROPOSAL_UPLOAD_ID"].ToString())),
                         reader["QUOTATION_NO"].ToString(),
                        reader["VEHICLE_NO"].ToString(),
                         reader["ENGINE_NO"].ToString(),
                         reader["CHASSIS_NO"].ToString(),
                         (Convert.ToInt32(reader["IS_COVERNOTE_AVAILABLE"].ToString())),
                         reader["COVERNOTE_PERIOD"].ToString(),
                         reader["ADDRESS_LINE_1"].ToString(),
                         reader["ADDRESS_LINE_2"].ToString(),
                         reader["ADDRESS_LINE_3"].ToString(),
                         reader["YEAR_OF_MAKE"].ToString(),
                         reader["FIRST_REG_DATE"].ToString(),
                         reader["CUBIC_CAPACITY"].ToString(),
                         reader["ENTERED_USER_CODE"].ToString(),
                        reader["ENTERED_USER_BRANCH_CODE"].ToString(),
                         reader["TCS_POLICY_NO"].ToString(),
                         reader["TCS_PROPOSAL_NO"].ToString(),
                         reader["TCS_POLICY_ID"].ToString(),
                          reader["JOB_TYPE"].ToString(),
                         reader["JOB_NUMBER"].ToString(),
                         reader["ISSUE_TYPE_NAME"].ToString(),
                          reader["POLICY_TYPE_NAME"].ToString(),
                      reader["CANCELLATION_TYPE_NAME"].ToString(),
                         reader["ENDORSEMENT_TYPE_NAME"].ToString(),
                          reader["SYSTEM_NAME"].ToString(),
                               Convert.ToInt32(reader["CERTIFICATE_CONVERTION"].ToString()));



                reader.Close();
                return proposalUpload;

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
        public DataTable GetJobsForRenewal(string status, string branchCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = "   SELECT  CASE  WHEN JOB_TYPE='R' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\"," +
                " JOB_TYPE FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS AND BRANCH_CODE_OF_POLICY=:V_BRANCH_CODE_OF_POLICY ORDER BY SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", branchCode));




            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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
        public DataTable GetJobsForCancellation(string status, string branchCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = "   SELECT  CASE  WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END AS \"Job/Quotation No\"," +
                " JOB_TYPE FROM MNBQ_PROPOSAL_UPLOAD MM  " +
                          " WHERE STATUS = :V_STATUS AND BRANCH_CODE_OF_POLICY=:V_BRANCH_CODE_OF_POLICY ORDER BY SYS_DATE ASC";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE_OF_POLICY", branchCode));




            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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
        public DataTable GetScrutinizationSummary()
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            sql = "select u.user_name \"User\", " +
                    " (select count(*) from MNBQ_PROPOSAL_UPLOAD t where to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') ) \"Total\", " +
                    " (select count(t.user_code)  " +
                    " from MNBQ_PROPOSAL_UPLOAD_FOLLOWUP t  where  " +
                     " t.user_code=u.user_code and " +
                     " t.status ='REJECTED_BY_SCRUTINIZING' " +
                    " and to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') " +
                    " group by t.user_code) \"Rejected\" , " +
                    " (select count(t.user_code) from MNBQ_PROPOSAL_UPLOAD_FOLLOWUP t   " +
                    " where  t.user_code=u.user_code and " +
                    " t.status ='APPROVED_BY_SCRUTINIZING' " +
                    " and to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') " +
                    " group by t.user_code) \"Apporved\" " +
                    " from wf_admin_users u where u.user_role_code=45";


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

        public DataTable GetApprovalSummary()
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            sql = "select u.user_name \"User\", " +
                    " (select count(*) from MNBQ_PROPOSAL_UPLOAD t where to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') ) \"Total\", " +
                    " (select count(t.user_code)  " +
                    " from MNBQ_PROPOSAL_UPLOAD_FOLLOWUP t  where  " +
                     " t.user_code=u.user_code and " +
                     " t.status ='TAKEN_BY_VALIDATORS' " +
                    " and to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') " +
                    " group by t.user_code) \"Taken\" , " +
                    " (select count(t.user_code) from MNBQ_PROPOSAL_UPLOAD_FOLLOWUP t   " +
                    " where  t.user_code=u.user_code and " +
                    " t.status ='APPROVED_BY_VALIDATORS' " +
                    " and to_date(t.sys_date,'DD/MM/RRRR')= to_date(sysdate,'DD/MM/RRRR') " +
                    " group by t.user_code) \"Apporved\" " +
                    " from wf_admin_users u where u.user_role_code=47";


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
        public void UpdateProposalUploadStatus(int proposalUploadId, string updatedByUser, string newStatus, string remarks)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_PROPOSAL_STATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_UPDATED_BY_USER_CODE", OracleType.VarChar));
            cmd.Parameters["V_UPDATED_BY_USER_CODE"].Value = updatedByUser;

            cmd.Parameters.Add(new OracleParameter("V_NEW_STATUS", OracleType.VarChar));
            cmd.Parameters["V_NEW_STATUS"].Value = newStatus;

            cmd.Parameters.Add(new OracleParameter("V_REMARKS", OracleType.VarChar));
            cmd.Parameters["V_REMARKS"].Value = remarks;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }




        public void UpdateTargetBranchCode(string systemName, int proposalUploadId, string tcsPolicyId)
        {

            string targetBranchCode = "";
            targetBranchCode = getTargetBranchCode(systemName, tcsPolicyId);



            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_TARGET_BRANCH_CODE", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_TARGET_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_TARGET_BRANCH_CODE"].Value = targetBranchCode;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }



        public string getTargetBranchCode(string systemName, string tcsPolicyId)
        {

            string conString = "";
            if (systemName == "TCS")
            {
                conString = connectionString;
            }
            else if (systemName == "TAKAFUL")
            {
                conString = connectionStringTakaful;
            }


            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = " select  crc_get_assurance_code('" + tcsPolicyId + "') from dual  ";

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
        public void UpdateTCSProposalNoAndPolicyId(int proposalUploadId, string TCSProposalNo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_TCS_PROPOSAL_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_TCS_PROPOSAL_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_PROPOSAL_NO"].Value = TCSProposalNo;

            TCSPolicyController tCSPolicyController = new TCSPolicyController();

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));

            string policyID = "";
            policyID = tCSPolicyController.getPolicyIdOfProposal(TCSProposalNo, "TCS");
            if (policyID != "")
            {
                cmd.Parameters["V_TCS_POLICY_ID"].Value = policyID;
            }
            else if (policyID == "")
            {

                cmd.Parameters["V_TCS_POLICY_ID"].Value = tCSPolicyController.getPolicyIdOfProposal(TCSProposalNo, "TAKAFUL");
            }


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        public void UpdateTCSdPolicyIdAndPolicyNo(int proposalUploadId, string TCSProposalNo, string TCSPolicyNo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_TCS_POL_ID_POL_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_NO"].Value = TCSPolicyNo;

            TCSPolicyController tCSPolicyController = new TCSPolicyController();

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_ID", OracleType.VarChar));

            string policyID = "";
            policyID = tCSPolicyController.getPolicyIdOfProposal(TCSProposalNo, "TCS");
            if (policyID != "")
            {
                cmd.Parameters["V_TCS_POLICY_ID"].Value = policyID;
            }
            else if (policyID == "")
            {

                cmd.Parameters["V_TCS_POLICY_ID"].Value = tCSPolicyController.getPolicyIdOfProposal(TCSProposalNo, "TAKAFUL");
            }


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


        public void UpdateTCSPolicyNo(int proposalUploadId, string TCSPolicyNo)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_TCS_POL_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_TCS_POLICY_NO", OracleType.VarChar));
            cmd.Parameters["V_TCS_POLICY_NO"].Value = TCSPolicyNo;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateIsValidated(int proposalUploadId)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_IS_VALIDATED", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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


        public void UpdateIssueTypeAndPolicyType(int proposalUploadId, string issueTyeCode, string policyTyeCode)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_WF_ISU_POL_TYPE", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_ISSUE_TYPE_CODE", OracleType.Number));
            cmd.Parameters["V_ISSUE_TYPE_CODE"].Value = issueTyeCode;


            cmd.Parameters.Add(new OracleParameter("V_POLICY_TYPE_CODE", OracleType.Number));
            cmd.Parameters["V_POLICY_TYPE_CODE"].Value = policyTyeCode;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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


        public void UpdateIsCertificateConvertion(int proposalUploadId, int isCertificateConvertion)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("UPDATE_MNBQ_WF_IS_CERT_CONV", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_IS_CERTIFICATE_CONVERTION", OracleType.Number));
            cmd.Parameters["V_IS_CERTIFICATE_CONVERTION"].Value = isCertificateConvertion;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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


        public string getEmailOfUser(string userCode)
        {
            string userEmail = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();


            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 "WAU.USER_EMAIL  " +
                                 " FROM  WF_ADMIN_USERS WAU " +
                                 " WHERE WAU.USER_CODE=:V_USER_CODE    ";

            OracleCommand cmd = new OracleCommand(selectQuery, con);
            cmd.Parameters.Add(new OracleParameter("V_USER_CODE", userCode));

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                userEmail = dr[0].ToString();
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return userEmail;

        }
        public string getEmailOfBranchStaff(string branchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

          
            String selectQuery = "";

            selectQuery = "   SELECT " +
                                 "T.STAFF_MAIL  " +
                                 " FROM  mnbq_wf_branch T " +
                                 " WHERE T.BRANCH_CODE=:V_BRANCH_CODE    ";


            OracleCommand cmd = new OracleCommand(selectQuery, con);
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE", branchCode));

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
        public string getBranchNameOfBranchCode(string branchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();



            String selectQuery = "";

            selectQuery = "  select t.branch_name from mis_gi_branches t where t.branch_code=:V_BRANCH_CODE   ";

            OracleCommand cmd = new OracleCommand(selectQuery, con);
            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE", branchCode));

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
        public string getCountOfStatuses()
        {
            string generatedText = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = " SELECT T.STATUS,COUNT(*) FROM MNBQ_PROPOSAL_UPLOAD T  GROUP BY T.STATUS  ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                generatedText = generatedText + "\n { label: \" " + dr[0].ToString() + "\", value:" + dr[1].ToString() + " },";
            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return generatedText;

        }
        public string getCountOfUsersOfUploadedProposals()
        {
            string generatedText = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select w.user_name,aa.no_of_proposals from (select t.entered_user_code,count(*) as no_of_proposals from mnbq_proposal_upload t " +
                         " group by t.entered_user_code) aa " +
                        " left join wf_admin_users w on aa.entered_user_code=w.user_code ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();



            while (dr.Read())
            {

                generatedText = generatedText + "\n { x: \" " + dr[0].ToString() + "\", y:" + dr[1].ToString() + " },";

            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return generatedText;

        }
        public string getCountOfBranchesOfUploadedProposals()
        {
            string generatedText = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select t.entered_user_branch_code,count(*) as no_of_proposals from mnbq_proposal_upload t    group by t.entered_user_branch_code ";

            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();



            while (dr.Read())
            {

                generatedText = generatedText + "\n { x: \" " + dr[0].ToString() + "\", y:" + dr[1].ToString() + " },";

            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return generatedText;

        }

        public string GetNewJobNoForEndorsement(string BranchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("GET_ENDORSEMENT_JOB_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = BranchCode;

            cmd.Parameters.Add("V_NEW_JOB_NO", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters["V_NEW_JOB_NO"].Direction = ParameterDirection.Output;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                returnVal = Convert.ToString(cmd.Parameters["V_NEW_JOB_NO"].Value);


                return returnVal;
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
        public string GetNewJobNoForRenewal(string BranchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("GET_RENEWAL_JOB_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = BranchCode;

            cmd.Parameters.Add("V_NEW_JOB_NO", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters["V_NEW_JOB_NO"].Direction = ParameterDirection.Output;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                returnVal = Convert.ToString(cmd.Parameters["V_NEW_JOB_NO"].Value);


                return returnVal;
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }

        public string GetNewJobNoForCancellation(string BranchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("GET_CANCELLATION_JOB_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = BranchCode;

            cmd.Parameters.Add("V_NEW_JOB_NO", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters["V_NEW_JOB_NO"].Direction = ParameterDirection.Output;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                returnVal = Convert.ToString(cmd.Parameters["V_NEW_JOB_NO"].Value);


                return returnVal;
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }



        public string GetNewJobNoForFastTrack(string BranchCode)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("GET_FASTTRACK_JOB_NO", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_ENTERED_USER_BRANCH_CODE", OracleType.VarChar));
            cmd.Parameters["V_ENTERED_USER_BRANCH_CODE"].Value = BranchCode;

            cmd.Parameters.Add("V_NEW_JOB_NO", OracleType.VarChar, 20).Direction = ParameterDirection.Output;
            cmd.Parameters["V_NEW_JOB_NO"].Direction = ParameterDirection.Output;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                returnVal = Convert.ToString(cmd.Parameters["V_NEW_JOB_NO"].Value);


                return returnVal;
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


        public int[] getJobNoAndRevisionNoOfQuotation(string quotationNo)
        {

            int[] returnVal = new int[2];

            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            if (quotationNo.Substring(5, 1) == "T" || quotationNo.Substring(5, 1) == "t")
            {
                selectQuery = "   SELECT " +
                                               "T.JOB_ID ,T.REVISION_NO " +
                                               " FROM  mnbq_t_main T " +
                                               " WHERE T.QUOTATION_NO='" + quotationNo + "'    ";
            }
            else
            {
                selectQuery = "   SELECT " +
                                               "T.JOB_ID ,T.REVISION_NO " +
                                               " FROM  mnbq_main T " +
                                               " WHERE T.QUOTATION_NO='" + quotationNo + "'    ";
            }


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                returnVal[0] = Convert.ToInt32(dr[0].ToString());
                returnVal[1] = Convert.ToInt32(dr[1].ToString());


            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;

        }


        public bool validateQuotationNoFromDB(string quotationNo)
        {
            bool returnVal = false;
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";





            if (quotationNo.Substring(5, 1) == "T" || quotationNo.Substring(5, 1) == "t")
            {
                selectQuery = "   SELECT " +
                               "T.QUOTATION_NO  " +
                               " FROM  MNBQ_T_MAIN T " +
                               " WHERE T.QUOTATION_NO='" + quotationNo + "'    ";
            }
            else
            {

                selectQuery = "   SELECT " +
                            "T.QUOTATION_NO  " +
                            " FROM  MNBQ_MAIN T " +
                            " WHERE T.QUOTATION_NO='" + quotationNo + "'    ";
            }





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
        public string GetTimeDifferenceOfTwoStatuses(int proposalUploadId, string statusFrom, string statusTo)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = "select " +
                " ROUND(86400 *((select t2.sys_date  from mnbq_proposal_upload_followup t2 where t2.proposal_upload_id=:V_DOC_UPLOAD_ID and t2.status=:STATUS_TO ) -  " +
                " (select t1.sys_date  from mnbq_proposal_upload_followup t1 where t1.proposal_upload_id=:V_DOC_UPLOAD_ID and t1.status=:STATUS_FROM  )), 2) " +
                " from dual    ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_DOC_UPLOAD_ID", proposalUploadId));
            cmd.Parameters.Add(new OracleParameter("STATUS_TO", statusTo));
            cmd.Parameters.Add(new OracleParameter("STATUS_FROM", statusFrom));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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
        public string GetTimeDifferenceFromStatusToNow(int proposalUploadId, string statusFrom)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = "select " +
                " ROUND(86400 *((select sysdate from dual) -  " +
                " (select max(t1.sys_date)  from mnbq_proposal_upload_followup t1 where t1.proposal_upload_id=:V_DOC_UPLOAD_ID and t1.status=:STATUS_FROM  )), 2) " +
                " from dual    ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_DOC_UPLOAD_ID", proposalUploadId));
            cmd.Parameters.Add(new OracleParameter("STATUS_FROM", statusFrom));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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

        public DataTable GetJobs(string whereStatement)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";




            sql = "select   " +
                " mpu.TARGET_BRANCH_CODE as \"Branch\", " +
                  " CASE WHEN mpu.JOB_TYPE ='N' THEN 'New' WHEN mpu.JOB_TYPE='E' THEN 'Endorsement' WHEN mpu.JOB_TYPE='R' THEN 'Renewal' WHEN mpu.JOB_TYPE='C' THEN 'Cancellation'  WHEN mpu.JOB_TYPE='F' THEN 'Fast Track' ELSE '' END   AS \"Job Type\" , " +
                  "  CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER  WHEN  mpu.JOB_TYPE='F' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" , " +
                "  mpu.tcs_policy_no  AS \"Policy No.\" ," +
                  " mpu.status  AS \"Status\", " +
                 " wau.user_name  AS \"User\", " +
               " mpuf.sys_date  AS \"Date and Time\", " +
                  " (ROUND(86400 *((select t1.sys_date  from mnbq_proposal_upload_followup t1 where t1.proposal_upload_id= mpu.proposal_upload_id and t1.status= mpu.status  and rownum =1) -  " +
                " ( select t1.sys_date  from mnbq_proposal_upload_followup t1 where t1.proposal_upload_id= mpu.proposal_upload_id and t1.status= 'INITIAL' and rownum =1 )), 2))  AS \"Time\" " +
               ", mpu.proposal_upload_id " +
                " from mnbq_proposal_upload mpu " +
                      " left join mnbq_proposal_upload_followup mpuf on  mpu.proposal_upload_id=mpuf.proposal_upload_id and mpu.status=mpuf.status " +
              " left join wf_admin_users wau on mpuf.user_code=wau.user_code " +
                 " WHERE " + whereStatement +
              " ORDER BY    mpuf.sys_date ";




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

        public DataTable GetUsers(string whereStatement)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";




            sql = "select   " +
                " u.USER_CODE as \"User Code\", " +
                " u.USER_NAME as \"User Name\" " +
                " from WF_ADMIN_USERS u " +
                 " WHERE " + whereStatement +
              " ORDER BY    u.USER_CODE ";




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
        public DataTable GetJobFollowup(string proposalUploadId)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = " select t.status as \"Status\",t.remarks as \"Remarks\",wau.user_name \"User Name\",t.sys_date as \"Date\"  " +
                     " from mnbq_proposal_upload_followup t  " +
                     " left join wf_admin_users wau on wau.user_code=t.user_code " +
                    " where t.proposal_upload_id=:V_PROPOSAL_UPLOAD_ID  " +
                    " order by t.sys_date asc  ";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", proposalUploadId));


            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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

        public DataTable GetJobsOfStatus(string status)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = " select  CASE WHEN JOB_TYPE ='N' THEN QUOTATION_NO WHEN JOB_TYPE='E' THEN JOB_NUMBER WHEN JOB_TYPE='R' THEN JOB_NUMBER WHEN JOB_TYPE='C' THEN JOB_NUMBER ELSE '' END as \"Job/Quotation No.\"," +
                " t.remarks as \"Remarks\",wau.user_name \"User Name\",t.sys_date as \"Date\"  " +
                     " from mnbq_proposal_upload_followup t  " +
                     " left join wf_admin_users wau on wau.user_code=t.user_code " +
                    " where t.status=:V_STATUS  " +
                    " order by t.sys_date asc  ";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_STATUS", status));


            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
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


        public string getStatusOfJob(string proposalUploadId)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = "select t.status from mnbq_proposal_upload t where t.proposal_upload_id=:V_PROPOSAL_UPLOAD_ID";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", proposalUploadId));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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


        public string getStatusOfJobFromJobNo(string jobNo)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = "select t.status from mnbq_proposal_upload t where   QUOTATION_NO = :V_JOB_NO OR  JOB_NUMBER = :V_JOB_NO ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_NO", jobNo));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = dr[0].ToString();
                    return returnVal;
                }
                else
                {
                    return null;
                }
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



        public void InsertDocumentViewURL(int proposalUploadId, string documentURL)
        {

            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("INSERT_DMS_DOC_URL", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", OracleType.Number));
            cmd.Parameters["V_PROPOSAL_UPLOAD_ID"].Value = proposalUploadId;

            cmd.Parameters.Add(new OracleParameter("V_DMS_DOC_URL", OracleType.VarChar));
            cmd.Parameters["V_DMS_DOC_URL"].Value = documentURL;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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


        public DataTable GetJobSummaryOfDay(string date)
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = "select u.proposal_upload_id,u.target_branch_code,u.job_number,u.quotation_no,u.job_type " +
                    ", " +
                    "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING'), " +
                    "24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_SCRUTINIZING')- " +
                    "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_SCRUTINIZING')) " +
                    ", " +
                    "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING'), " +
                    "24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='COMPLETED_BY_PROCESSING')- " +
                    "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_PROCESSING')) " +
                    ", " +
                    "(select wau.user_name from mnbq_proposal_upload_followup f left join wf_admin_users wau on wau.user_code=f.user_code where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS'), " +
                    "24*60*((select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='APPROVED_BY_VALIDATORS')- " +
                    "(select max(f.sys_date) from mnbq_proposal_upload_followup f where f.proposal_upload_id=u.proposal_upload_id and f.status='TAKEN_BY_VALIDATORS')) " +
                    "from  mnbq_proposal_upload u  " +
                    "where  to_date(u.sys_date,'DD/MM/RRRR')=to_date('" + date + "','DD/MM/RRRR')  " +
                    "and u.status='APPROVED_BY_VALIDATORS' ";




            OracleCommand cmd = new OracleCommand(sql, con);



            DataTable dt = new DataTable();




            DataSet dtset = new DataSet();




            try
            {
                con.Open();
                //dt.Load(cmd.ExecuteReader());
                //return dt;

                OracleDataAdapter adpt = new OracleDataAdapter(cmd);
                adpt.Fill(dtset);
                dt = dtset.Tables[0];
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

        public string getDBServerTime()
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "  select to_date(sysdate,'DD/MM/RRRR') from dual  ";

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



        public List<ProposalUpload> loadOtherJobsByPolicyNo(string policyNo, string jobNo, string jobType)
        {
            var retList = new List<ProposalUpload>();

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;
            con.Open();
            String selectQuery = "";

            if (jobType == "N")
            {
                selectQuery = "select t.quotation_no,t.job_type,t.status from mnbq_proposal_upload t where t.tcs_policy_no=:V_POLICY_NO  and t.job_number<>:V_JOB_NO";

            }
            else
            {
                selectQuery = "select CASE WHEN t.JOB_TYPE ='N' THEN t.QUOTATION_NO ELSE t.JOB_NUMBER  END AS \"job_number\"," +
                    "t.job_type,t.status from mnbq_proposal_upload t " +
                    "where t.tcs_policy_no=:V_POLICY_NO  and " +
                    " (t.job_number is null or t.job_number<>:V_JOB_NO)";

            }



            OracleCommand cmd = new OracleCommand(selectQuery, con);
            cmd.Parameters.Add(new OracleParameter("V_POLICY_NO", policyNo));
            cmd.Parameters.Add(new OracleParameter("V_JOB_NO", jobNo));



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ProposalUpload proposalUpload = new ProposalUpload();
                    proposalUpload.JobNumber = dr[0].ToString();
                    proposalUpload.JobType = dr[1].ToString();
                    proposalUpload.JobStatus = dr[2].ToString();


                    retList.Add(proposalUpload);
                }
            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            return retList;
        }


        public bool CheckIsPolicyBlacklisted(string policyNo, string vehicleNo)
        {
            bool returnVal = false;


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = " select t.* from MNB_WF_BLACKLISTED_POLICY t where t.VEHICLE_NO=:V_VEHICLE_NO OR t.POLICY_NO = :V_POLICY_NO  ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_NO", vehicleNo));
            cmd.Parameters.Add(new OracleParameter("V_POLICY_NO", policyNo));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    returnVal = true;
                }
                else
                {

                    returnVal = false;
                }
            }



            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }

            return returnVal;
        }

        public bool CheckIsDocsPrintFromHDO(string proposalUploadId)
        {
            bool returnVal = false;


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = " select t.* from MNBQ_PROPOSAL_UPLOAD t where t.PROPOSAL_UPLOAD_ID=:V_PROPOSAL_UPLOAD_ID  AND IS_DOCS_PRINT_FROM_HDO=1 ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PROPOSAL_UPLOAD_ID", proposalUploadId));
            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    // dr.Read();

                    returnVal = true;


                }
                else
                {

                    returnVal = false;
                }
            }



            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }

            return returnVal;
        }

        public string GetTimeRangeJobReport(string dateFrom, string dateTo, string timeFrom, string timeTo)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleCommand cmd = new OracleCommand("MNBQ_TIME_RANGE_JOB_REPORT", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new OracleParameter("V_DATE_FROM", OracleType.VarChar));
            cmd.Parameters["V_DATE_FROM"].Value = dateFrom;
            cmd.Parameters.Add(new OracleParameter("V_DATE_TO", OracleType.VarChar));
            cmd.Parameters["V_DATE_TO"].Value = dateTo;
            cmd.Parameters.Add(new OracleParameter("V_TIME_FROM", OracleType.VarChar));
            cmd.Parameters["V_TIME_FROM"].Value = timeFrom;
            cmd.Parameters.Add(new OracleParameter("V_TIME_TO", OracleType.VarChar));
            cmd.Parameters["V_TIME_TO"].Value = timeTo;

            cmd.Parameters.Add("R_TOTAL_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_TOTAL_COUNT"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("R_NEW_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_NEW_COUNT"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("R_ENDO_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_ENDO_COUNT"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("R_RENEWAL_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_RENEWAL_COUNT"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("R_CANCEL_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_CANCEL_COUNT"].Direction = ParameterDirection.Output;
            cmd.Parameters.Add("R_NEW_FST_COUNT", OracleType.Number).Direction = ParameterDirection.Output;
            cmd.Parameters["R_NEW_FST_COUNT"].Direction = ParameterDirection.Output;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                returnVal = "<style type=" + "text/css" + ">" +
                    "table {" +
                          " font-family: 'Arial';" +
                          " margin: 25px auto;" +
                          " border-collapse: collapse;" +
                          " border: 1px solid #eee;" +
                          " border-bottom: 2px solid #00cccc;" +
                          " box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.1), 0px 10px 20px rgba(0, 0, 0, 0.05), 0px 20px 20px rgba(0, 0, 0, 0.05), 0px 30px 20px rgba(0, 0, 0, 0.05);" +
                        " }" +
                        " table tr:hover {" +
                        "   background: #f4f4f4;" +
                        " }" +
                        " table tr:hover td {" +
                        "   color: #555;" +
                        " }" +
                        " table th, table td {" +
                         "  text-align: center;" +
                        "   color: #000;" +
                        "   border: 1px solid #eee;" +
                        "   padding: 10px 0px;" +
                        "   border-collapse: collapse;" +
                        " }" +
                        "</style>" +
                    "<table>" +
                          "<tr>" +
                            "<td></td>" +
                            "<td>Total</td>" +
                            "<td>New</td>" +
                            "<td>Endorsement</td>" +
                            "<td>Renewal</td>" +
                            "<td>Cancellation</td>" +
                            "<td>New-Fast Track</td>" +
                          "</tr>" +
                          "<tr>" +
                            "<td style=\"width:250px;\">Jobs from " + dateFrom + " - " + dateTo + " , Time " + timeFrom + " - " + timeTo + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_TOTAL_COUNT"].Value) + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_NEW_COUNT"].Value) + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_ENDO_COUNT"].Value) + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_RENEWAL_COUNT"].Value) + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_CANCEL_COUNT"].Value) + "</td>" +
                            "<td>" + Convert.ToString(cmd.Parameters["R_NEW_FST_COUNT"].Value) + "</td>" +
                          "</tr>" +
                        "</table>";

                return returnVal;
            }
            catch (OracleException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }


        public bool ValidateIpAddressForDashboard(string ipAddress)
        {
            bool returnVal = false;


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();
            OracleDataReader dr;
            string sql = "";

            sql = " select t.* from mnbq_dash_user_ip t where t.ip_address=:V_IP_ADDRESS  ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_IP_ADDRESS", ipAddress));
            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }

            catch (OracleException err)
            {
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }

            return returnVal;
        }

    }
}