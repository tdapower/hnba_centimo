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

namespace quickinfo_v2.Views.BookManagement.DocUpload
{
    public partial class DocumentUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               // (this.Panel2.FindControl("Iframe1") as HtmlControl).Attributes.Add("src", "DocUpload/DocumentList.aspx?TempId=" + Session["TempId"].ToString());

                string tempId = "";
                if (Session["TempId"] == null)
                {
                    return;
                }

                tempId = Session["TempId"].ToString();



                if (Request.QueryString["tid"] != null)
                {

                    int MaxImageNo = 0;
                    MaxImageNo = Convert.ToInt32(getMaxImageNo());




                    foreach (string s in Request.Files)
                    {
                        MaxImageNo = MaxImageNo + 1;
                        HttpPostedFile file = Request.Files[s];



                        BinaryReader b = new BinaryReader(file.InputStream);
                        byte[] binData = b.ReadBytes(file.ContentLength);

                        string fileName = new FileInfo(file.FileName).Name.ToString();
                        saveDocument(tempId, MaxImageNo, binData, fileName);


                    }


                }
            }
        }
        private void saveDocument(string tempId, int imageId, byte[] doc, string fileName)
        {


            try
            {


                OracleConnection conProcess = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
                conProcess.Open();
                OracleCommand spProcess = null;
                int AppCode = 0;
                string strQuery = "";



                OracleParameter blobParameterDocument = new OracleParameter();


                strQuery = "INSERT INTO MNBQ_WF_BOOK_SR_DOCS(TEMP_ID,DOC_SEQ_ID,DOC_NAME, DOCUMENT) VALUES (";
                strQuery += "'" + tempId + "', ";
                strQuery += "" + imageId + ", ";
                strQuery += "'" + fileName + "', ";
                strQuery += ":doc)";

                blobParameterDocument.ParameterName = "doc";
                blobParameterDocument.Direction = ParameterDirection.Input;


                blobParameterDocument.Value = doc;



                spProcess = new OracleCommand(strQuery, conProcess);
                spProcess.Parameters.Add(blobParameterDocument);


                spProcess.ExecuteNonQuery();
                conProcess.Close();
                conProcess.Dispose();
            }
            catch (Exception ex)
            {

            }


        }

        private string getMaxImageNo()
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["ORAWF"].ToString());
            OracleDataReader dr;

            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            String selectQuery = "";
            selectQuery = "SELECT 	" +
                        " CASE WHEN MAX(T.DOC_SEQ_ID)  IS NULL THEN 0 ELSE TO_NUMBER((MAX(T.DOC_SEQ_ID))) END " +
                        " FROM MNBQ_WF_BOOK_SR_DOCS T ";

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
    }

}