using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.BookManagement.DocUpload
{
    public partial class DocumentViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string proposalNo = "";
                string docId = "";
                string tempId = "";


                if ((Request.QueryString["BookSerialSeqId"] == null || Request.QueryString["BookSerialSeqId"] == "") && Request.QueryString["TempId"] == null)
                {
                    return;
                }


                if (Request.QueryString["BookSerialSeqId"] != null)
                {
                    proposalNo = Request.QueryString["BookSerialSeqId"].ToString();
                }

                if (Request.QueryString["DocId"] != null)
                {
                    docId = Request.QueryString["DocId"].ToString();
                }

                if (Request.QueryString["TempId"] != null)
                {
                    tempId = Request.QueryString["TempId"].ToString();
                }

                if (proposalNo != "" && docId != "")
                {
                    loadDocument(proposalNo, docId);
                    return;
                }

                if (tempId != "" && docId != "")
                {
                    loadDocumentFromTempId(tempId, docId);
                }


            }
        }
        private void loadDocument(string bookSerialSeqId, string docId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

            try
            {

                con.Open();
                OracleDataReader dr;


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                String selectQuery = "";
                selectQuery = "SELECT DOCUMENT,DOC_NAME FROM MNBQ_WF_BOOK_SR_DOCS  " +
                          " WHERE BOOK_SR_SEQ_NO='" + bookSerialSeqId + "' AND DOC_SEQ_ID=" + docId;

                cmd.CommandText = selectQuery;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["DOCUMENT"] != System.DBNull.Value)
                    {
                        //  OracleBlob blob = dr.GetOracleBlob(0);
                        byte[] blob = (byte[])dr["DOCUMENT"];
                        Response.AddHeader("content-disposition", "inline;filename=" + dr[1].ToString() + "");
                        Response.AddHeader("content-length", blob.Length.ToString());


                        //Response.ContentType = "application/pdf";
                        Response.BinaryWrite(blob);
                        Response.Flush();
                        // Response.End();
                    }
                }

                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
        private void loadDocumentFromTempId(string tempId, string docId)
        {
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());

            try
            {

                con.Open();
                OracleDataReader dr;


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                String selectQuery = "";
                selectQuery = "SELECT DOCUMENT,DOC_NAME FROM MNBQ_WF_BOOK_SR_DOCS  " +
                          " WHERE TEMP_ID='" + tempId + "' AND DOC_SEQ_ID=" + docId;

                cmd.CommandText = selectQuery;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["DOCUMENT"] != System.DBNull.Value)
                    {
                        //  OracleBlob blob = dr.GetOracleBlob(0);
                        byte[] blob = (byte[])dr["DOCUMENT"];
                        Response.AddHeader("content-disposition", "inline;filename=" + dr[1].ToString() + "");
                        Response.AddHeader("content-length", blob.Length.ToString());


                       // Response.ContentType = "application/pdf";
                        Response.BinaryWrite(blob);
                        Response.Flush();
                        // Response.End();
                    }
                }

                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
        }
    }
}