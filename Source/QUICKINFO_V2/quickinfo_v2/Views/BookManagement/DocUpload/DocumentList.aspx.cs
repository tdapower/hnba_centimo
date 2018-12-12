using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.BookManagement.DocUpload
{
    public partial class DocumentList : System.Web.UI.Page
    {
        string bookSerialSeqId = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if ((Request.QueryString["BookSerialSeqId"] == null || Request.QueryString["BookSerialSeqId"] == "") && Session["TempId"] == null)
                {
                    return;
                }
                if (Request.QueryString["BookSerialSeqId"] != null)
                {
                    bookSerialSeqId = Request.QueryString["BookSerialSeqId"].ToString();
                    txtProposalNo.Text = bookSerialSeqId;
                }

                if (bookSerialSeqId != "")
                {

                    loadUploadedDocumentsToGrid(bookSerialSeqId);
                }
                if (Session["TempId"] != null)
                {

                    loadUploadedDocumentsToGridFromTempId(Session["TempId"].ToString());
                }

            }
        }
        protected void grdUploadedDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string DocId = e.Row.Cells[2].Text;
                string TempId = e.Row.Cells[4].Text;
                (e.Row.FindControl("irm2") as HtmlControl).Attributes.Add("src", "DocumentViewer.aspx?BookSerialSeqId=" + bookSerialSeqId + "&DocId=" + DocId + "&TempId=" + TempId);

                LinkButton btnDel = e.Row.FindControl("lnkBtnDeleteDocument") as LinkButton;
                btnDel.Attributes.Add("onClick", "if(confirm('Are you sure to Delete this Document?','MNB Workflow')){}else{return false}");

            }

        }

        private void loadUploadedDocumentsToGrid(string bsookSerialSeqId)
        {

            grdUploadedDocs.DataSource = null;
            grdUploadedDocs.DataBind();

            DataTable docList = new DataTable();
            docList = GetDocList(bsookSerialSeqId);
            grdUploadedDocs.DataSource = docList;


            if (grdUploadedDocs.DataSource != null)
            {
                grdUploadedDocs.DataBind();
            }

        }

        private void loadUploadedDocumentsToGridFromTempId(string tempId)
        {

            grdUploadedDocs.DataSource = null;
            grdUploadedDocs.DataBind();

            DataTable docList = new DataTable();
            docList = GetDocListFronTempId(tempId);
            grdUploadedDocs.DataSource = docList;


            if (grdUploadedDocs.DataSource != null)
            {
                grdUploadedDocs.DataBind();
            }

        }


        public DataTable GetDocList(string bsookSerialSeqId)
        {

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = " select t.DOC_SEQ_ID as \"Document Id\",  " +
                    " t.DOC_NAME as \"Document Name\",  " +
                    " t.TEMP_ID   " +
                     " from MNBQ_WF_BOOK_SR_DOCS t  " +
                    " where t.BOOK_SR_SEQ_NO=:V_BOOK_SR_SEQ_NO  " +
                    " order by t.DOC_SEQ_ID asc  ";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_BOOK_SR_SEQ_NO", bsookSerialSeqId));


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



        public DataTable GetDocListFronTempId(string tempId)
        {

            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataAdapter da = new OracleDataAdapter();
            string sql = "";

            sql = " select t.DOC_SEQ_ID as \"Document Id\",  " +
                    " t.DOC_NAME as \"Document Name\",  " +
                    " t.TEMP_ID   " +
                     " from MNBQ_WF_BOOK_SR_DOCS t  " +
                    " where t.TEMP_ID=:V_TEMP_ID  " +
                    " order by t.DOC_SEQ_ID asc  ";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_TEMP_ID", tempId));


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


        protected void lnkBtnDeleteDocument_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;

            string docId = grdUploadedDocs.Rows[rowID].Cells[2].Text;

            deleteDocument(docId);

            loadUploadedDocumentsToGrid(txtProposalNo.Text);
        }


        private void deleteDocument(string docId)
        {
            if (txtProposalNo.Text == "" && docId == null)
            {
                return;
            }
            try
            {

                OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                OracleDataAdapter da = new OracleDataAdapter();
                string sql = "";
                sql = "DELETE  FROM MNBQ_WF_BOOK_SR_DOCS WHERE BOOK_SR_SEQ_NO='" + txtProposalNo.Text + "' AND DOC_SEQ_ID=" + docId;
                da.DeleteCommand = new OracleCommand(sql, con);
                con.Open();



                da.DeleteCommand.ExecuteNonQuery();


                con.Close();

            }
            catch (Exception ex)
            {

            }


        }

    }

}