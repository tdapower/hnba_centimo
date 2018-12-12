using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Net.Mail;

namespace quickinfo_v2.Views.Common
{
    public partial class TCSDocumentsViewer : System.Web.UI.Page
    {

        string PolicyNo = "";
        string SystemName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            PolicyNo = "";
            SystemName = "";
            if (Request.QueryString["PolicyNo"] != null)
            {
                PolicyNo = Request.QueryString["PolicyNo"].ToString();
            }


            if (Request.QueryString["SystemName"] != null)
            {
                SystemName = Request.QueryString["SystemName"].ToString();
            }


            //ifrmDoc.Attributes.Add("src", "DocumentViewer.aspx?docPath=" + docPath);

            LoadTCSDocs(PolicyNo);
            //}
        }
        protected void grdTCSDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
        }

        protected void grdTCSDocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDocPath.Text = "";
            string docPath = "";
            docPath = grdTCSDocs.SelectedRow.Cells[2].Text.Trim();
            ifrmDoc.Attributes.Add("src", "TCSDocViewPanel.aspx?docPath=" + docPath + "&SystemName=" + SystemName);


            string SYSTEM_NAME_TCS = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TCS"].ToString();
            string SYSTEM_NAME_TAKAFUL = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TAKAFUL"].ToString();

            if (SystemName == SYSTEM_NAME_TCS)
            {
                docPath = docPath.Replace("Z:", @"\\192.168.10.24\u01\bea\user_projects\domains\LinuxDomain\applications\IIMS\IIMS\document");
            }
            else if (SystemName == SYSTEM_NAME_TAKAFUL)
            {
                docPath = docPath.Replace("Z:", @"\\192.168.10.58\u01\bea\user_projects\domains\LinuxDomain\applications\IIMS\IIMS\document");
            }


            txtDocPath.Text = docPath;
        }

        private void LoadTCSDocs(string polNo)
        {
            if (polNo != "") { }
            grdTCSDocs.DataSource = null;
            grdTCSDocs.DataBind();

            grdTCSDocs.DataSource = GetTCSDocs(polNo);


            if (grdTCSDocs.DataSource != null)
            {
                grdTCSDocs.DataBind();
            }


        }


        protected void btnEmail_Click(object sender, EventArgs e)
        {

            if (txtTo.Text == "")
            {
                lblMsg.Text = "To address cannot be empty";
                return;
            }





            CommonMail mail = new CommonMail();

            mail.From_address = "mnb.workflow@hnbgeneral.com";


            if (txtTo.Text != "")
            {
                mail.To_address = txtTo.Text;
            }

            if (txtCc.Text != "")
            {
                mail.Cc_address = txtCc.Text;
            }



            mail.Subject = "Motor New Bussiness Documents";
            String BodyText;
            BodyText = "Plese find the attachement";

            mail.Attachment = (new Attachment(txtDocPath.Text,"Document.pdf"));

            mail.Body = BodyText;
            mail.sendMail();

            lblMsg.Text = "Document successfully sent";


        }

        public DataTable GetTCSDocs(string policyNo)
        {

            string conString = "";
            string connectionString = "";
            string connectionStringTakaful = "";


            connectionString = ConfigurationManager.ConnectionStrings["ORAWF"].ToString();
            connectionStringTakaful = ConfigurationManager.ConnectionStrings["TAKAFULDB"].ToString();


            string SYSTEM_NAME_TCS = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TCS"].ToString();
            string SYSTEM_NAME_TAKAFUL = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TAKAFUL"].ToString();

            if (SystemName == SYSTEM_NAME_TCS)
            {
                conString = connectionString;
            }
            else if (SystemName == SYSTEM_NAME_TAKAFUL)
            {
                conString = connectionStringTakaful;
            }



            OracleConnection con = new OracleConnection(conString);

            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";
            sql = "  select t.DOCUMENT_NAME as \"Document Name\",(CASE WHEN  t.DOCUMENT_DW_PATH  IS NULL THEN t.DOCUMENT_UP_PATH  ELSE t.DOCUMENT_DW_PATH  END) AS  \"path\" ,t.DCM_CRTD_ON AS \"Created Date\" from MNB_WF_GET_TCS_DOC_PATHS t where t.POL_POLICY_NUMBER =:V_POL_POLICY_NUMBER  order by t.DCM_CRTD_ON desc";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_POL_POLICY_NUMBER", policyNo));

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
    }
}