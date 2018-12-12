using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;

namespace quickinfo_v2.Controllers.Dashboard
{
    public class DashboardController
    {
        private string connectionString;
        public DashboardController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["STD_ORAWF"].ToString();
        }



        public DataTable getCurrentSummary()
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            selectQuery = " select t.status,count(t.status) as \"count\" from mnbq_proposal_upload t where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                            " group by t.status ";


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

        public int getCurrentSummaryOfStatus(string status)
        {


            int returnVal = 0;
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = " select t.status,count(t.status) as \"count\" from mnbq_proposal_upload_followup t " +
                          "  where t.status='" + status + "' and to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                        " group by t.status ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                dr.Read();

                returnVal = Convert.ToInt32(dr[1].ToString());

            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;
        }


        public int getCurrentSummaryOfUnknown()
        {


            int returnVal = 0;
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = " select count(distinct t.job_no) as \"count\" from MNBQ_UNKNOWN_JOBS t " +
                          "  where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                dr.Read();

                returnVal = Convert.ToInt32(dr[0].ToString());

            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;
        }

        public DataTable getCurrentSummaryOfBranch(string userBranch)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            selectQuery = " select t.status,count(t.status) as \"count\" from mnbq_proposal_upload t " +
                " where t.entered_user_branch_code='" + userBranch + "' and to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                            " group by t.status ";


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
        public DataTable getCurrentSummaryOfUser(string userCode)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            selectQuery = " select t.user_code,t.status,count(t.status) as \"count\" from mnbq_proposal_upload_followup t " +
                " where t.user_code='" + userCode + "' and to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                            " group by t.user_code,t.status  ";


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

        public int getJobCountOfUserForStatus(string userCode, string status)
        {

            int returnVal = 0;
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";


            selectQuery = " select t.user_code,t.status,count(t.status) as \"count\" from mnbq_proposal_upload_followup t " +
                       " where t.user_code='" + userCode + "' and t.status='" + status + "' and to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                                   " group by t.user_code,t.status  ";


            cmd.CommandText = selectQuery;

            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                if (dr[1].ToString() != "")
                {
                    returnVal = Convert.ToInt32(dr[2].ToString());
                }
            }



            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnVal;
        }


        public DataTable loadDTCountOfJobTypes()
        {
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select  CASE WHEN JOB_TYPE ='N' THEN 'New' WHEN JOB_TYPE='E' THEN 'Endorsement' WHEN JOB_TYPE='R' THEN 'Renewal' WHEN JOB_TYPE='C' THEN 'Cancellation'  WHEN JOB_TYPE='F' THEN 'Fast Track' ELSE '' END   AS \"Job Type\", " +
                " count(t.job_type) from mnbq_proposal_upload t where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                " AND T.STATUS='APPROVED_BY_VALIDATORS' " +
                " group by t.job_type";

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

        public string loadCountOfJobTypes()
        {
            string generatedText = "";
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";

            selectQuery = "select  CASE WHEN JOB_TYPE ='N' THEN 'New' WHEN JOB_TYPE='E' THEN 'Endorsement' WHEN JOB_TYPE='R' THEN 'Renewal' WHEN JOB_TYPE='C' THEN 'Cancellation' WHEN JOB_TYPE='F' THEN 'Fast Track' ELSE '' END   AS \"Job Type\", " +
                " count(t.job_type) from mnbq_proposal_upload t where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') " +
                " group by t.job_type";

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

        public DataTable getZonalSummaryForTheMonth()
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            //selectQuery = "select mgz.zonal_name AS \" \", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.entered_user_branch_code where mgb.zonal_code=mgz.zonal_code and mpu.job_type='N'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"New\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.entered_user_branch_code where mgb.zonal_code=mgz.zonal_code and mpu.job_type='R'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Renewal\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.entered_user_branch_code where mgb.zonal_code=mgz.zonal_code and mpu.job_type='E'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Endorsement\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.entered_user_branch_code where mgb.zonal_code=mgz.zonal_code and mpu.job_type='C'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Cancellation\" " +
            //            " from mis_gi_zonal mgz ";


            //selectQuery = "select mgz.zonal_name AS \" \", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.TARGET_BRANCH_CODE where mgb.zonal_code=mgz.zonal_code and mpu.job_type='N'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"New\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.TARGET_BRANCH_CODE where mgb.zonal_code=mgz.zonal_code and mpu.job_type='R'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Renewal\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.TARGET_BRANCH_CODE where mgb.zonal_code=mgz.zonal_code and mpu.job_type='E'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Endorsement\", " +
            //            " (select count(*) from mnbq_proposal_upload mpu left join mis_gi_branches mgb on mgb.branch_code=mpu.TARGET_BRANCH_CODE where mgb.zonal_code=mgz.zonal_code and mpu.job_type='C'  AND MPU.STATUS='APPROVED_BY_VALIDATORS' AND EXTRACT(MONTH FROM to_date(mpu.sys_date,'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate,'RRRR/MM/DD'))) AS \"Cancellation\" " +
            //            " from mis_gi_zonal mgz ";

            selectQuery = " select mgz.zonal_name, " +
                  " count(case  " +
                     " when mpu.job_type = 'N' then 1 " +
                   " end) as new_count, " +
                    " count(case " +
                     " when mpu.job_type = 'R' then 1  " +
                   " end) as Renewal_count, " +
                    " count(case " +
                     " when mpu.job_type = 'E' then 1 " +
                   " end) as Endorsement_count, " +
                   "  count(case " +
                   "   when mpu.job_type = 'C' then 1  " +
                   " end) as Cancellation_count " +
                    " from mnbq_proposal_upload mpu " +
            " left join mis_gi_branches mgb on mgb.branch_code=mpu.TARGET_BRANCH_CODE  " +
             " inner join mis_gi_zonal mgz on mgb.zonal_code=mgz.zonal_code " +
            " where MPU.STATUS='APPROVED_BY_VALIDATORS'  " +
             " AND EXTRACT(MONTH FROM to_date(mpu.sys_date, 'RRRR/MM/DD'))=EXTRACT(MONTH FROM to_date(sysdate, 'RRRR/MM/DD')) " +
            " group by mgz.zonal_name ";



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
        public DataTable getJobSummaryOfBranch(string userBranch)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";


            string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();




            selectQuery = "select  " +
                         " CASE WHEN mpu.JOB_TYPE ='N' THEN 'New' WHEN mpu.JOB_TYPE='E' THEN 'Endorsement' WHEN mpu.JOB_TYPE='R' THEN 'Renewal' WHEN mpu.JOB_TYPE='C' THEN 'Cancellation'  WHEN mpu.JOB_TYPE='F' THEN 'Fast Track' ELSE '' END   AS \"Job Type\" , " +
                         "  CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER  WHEN  mpu.JOB_TYPE='F' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" , " +
                         " mpu.status  AS \"Status\", " +
                              " mpuf.REMARKS  AS \"Remarks\", " +
                           " wau.user_name  AS \"User\", " +
                           " mpuf.sys_date  AS \"Date and Time\" " +
                        " from mnbq_proposal_upload mpu " +
                        " left join mnbq_proposal_upload_followup mpuf on  mpu.proposal_upload_id=mpuf.proposal_upload_id and mpu.status=mpuf.status " +
                        " left join wf_admin_users wau on mpuf.user_code=wau.user_code " +
                        " where mpu.entered_user_branch_code=:V_BRANCH_CODE  and   mpu.status <>:V_STATUS and to_date(mpuf.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD')" +
                        " order by mpuf.sys_date   desc";


            DataTable dt = new DataTable();
            da.SelectCommand = new OracleCommand(selectQuery, con);

            da.SelectCommand.Parameters.Add(new OracleParameter("V_BRANCH_CODE", userBranch));
            da.SelectCommand.Parameters.Add(new OracleParameter("V_STATUS", APPROVED_BY_VALIDATORS));


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
        public DataTable getCompletedJobSummaryOfBranch(string userBranch)
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";
            string APPROVED_BY_VALIDATORS = System.Configuration.ConfigurationManager.AppSettings["APPROVED_BY_VALIDATORS"].ToString();


            selectQuery = "select  mpu.proposal_upload_id, " +
                      "  CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='F' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" , " +
                         "  mpu.tcs_policy_no  AS \"Policy No.\"," +
                         " mpu.SYSTEM_NAME " +
                        " from mnbq_proposal_upload mpu " +
                          " left join mnbq_proposal_upload_followup mpuf on  mpu.proposal_upload_id=mpuf.proposal_upload_id and mpu.status=mpuf.status " +
                      " where mpu.entered_user_branch_code=:V_BRANCH_CODE  and   mpu.status =:V_STATUS and to_date(mpuf.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD')";


            DataTable dt = new DataTable();
            da.SelectCommand = new OracleCommand(selectQuery, con);



            da.SelectCommand.Parameters.Add(new OracleParameter("V_BRANCH_CODE", userBranch));
            da.SelectCommand.Parameters.Add(new OracleParameter("V_STATUS", APPROVED_BY_VALIDATORS));


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



        public DataTable getUnknownSummary()
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";



            selectQuery = " select  t.job_no as \"Job No\",t.job_type as \"Job Type\",t.unknown_type  as \"Reason\" from MNBQ_UNKNOWN_JOBS t " +
                        " where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') ORDER BY t.job_type ";


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

        public DataTable getRejectedSummary()
        {


            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";

            selectQuery = " select CASE WHEN  mpu.JOB_TYPE ='N' THEN  mpu.QUOTATION_NO WHEN  mpu.JOB_TYPE='E' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='R' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='C' THEN  mpu.JOB_NUMBER WHEN  mpu.JOB_TYPE='F' THEN  mpu.JOB_NUMBER ELSE '' END AS \"Job/Quotation No\" ," +
                             " CASE WHEN mpu.job_type ='N' THEN 'New' WHEN mpu.job_type='E' THEN 'Endorsement' WHEN mpu.job_type='R' THEN 'Renewal' WHEN mpu.job_type='C' THEN 'Cancellation'  WHEN mpu.job_type='F' THEN 'Fast Track' ELSE '' END   AS \"Job Type\"," +
                            " mpuf.remarks AS \"Remarks\"," +
                            " (select LISTAGG(rr.reason_name , ', ' )  WITHIN GROUP (ORDER BY rr.reason_name) from MNBQ_WF_REJ_REASONS_OF_JOB rrj inner join  MNBQ_WF_REJECT_REASON rr on rrj.reason_code=rr.reason_code where rrj.proposal_upload_id=mpu.proposal_upload_id) \"Reject Reasons\" " +
                            " from mnbq_proposal_upload mpu" +
                            " inner join mnbq_proposal_upload_followup mpuf on mpu.proposal_upload_id=mpuf.proposal_upload_id and  mpu.status=mpuf.status " +
                            " inner join MNBQ_WF_REJ_REASONS_OF_JOB mrr on mpu.proposal_upload_id=mrr.proposal_upload_id" +
                             " where to_date(mpu.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD')" +
                             " order by mpu.job_type ";


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


        public DataTable getNotScannedYetJobsSummary()
        {
       
            OracleConnection con = new OracleConnection(connectionString);
            OracleDataAdapter da = new OracleDataAdapter();

            String selectQuery = "";



            selectQuery = " select  t.job_number as \"Job No\","+
                  " CASE WHEN t.job_type='E' THEN 'Endorsement' WHEN t.job_type='R' THEN 'Renewal'  " +
               " WHEN t.job_type='C' THEN 'Cancellation' ELSE '' END   AS \"Job Type\"   " +
                " from mnbq_proposal_upload t " +
                        " where to_date(t.sys_date,'RRRR/MM/DD')=to_date(sysdate,'RRRR/MM/DD') "+
                              " and t.status  in('RENEWAL_ADDED','CANCELLATION_ADDED','ENDORSEMENT_ADDED')  " +
                        " ORDER BY t.job_type ";


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