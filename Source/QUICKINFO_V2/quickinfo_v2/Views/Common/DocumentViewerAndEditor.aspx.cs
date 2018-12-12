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

namespace quickinfo_v2.Views.Common
{
    public partial class DocumentViewerAndEditor : System.Web.UI.Page
    {

        string docPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["docPath"] = "";
                btnDeletePages.Attributes.Add("onClick", "if(confirm('Are you sure to Delete selected pages?','Motor New Business Workflow')){}else{return false}");


                string filePath = "";

                if (Request.QueryString["DocPath"] != null)
                {
                    filePath = Request.QueryString["DocPath"].ToString();
                    docPath = filePath;
                    Session["docPath"] = filePath;
                }


                // Response.ContentType = ContentType;
                //  Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                // Response.WriteFile(filePath);
                // Response.End();

                //Response.ContentType = "application/pdf";
                //Response.WriteFile(filePath);
                //Response.End();

                // ifrmDoc.Attributes.Add("src", filePath);
                //  tttt.Attributes.Add("href", filePath);

                //Response.ContentType = "application/pdf";d
                //Response.WriteFile(filePath);
                //Response.End();


                ifrmDoc.Attributes.Add("src", "DocumentViewer.aspx?docPath=" + docPath);


            }
        }

        protected void btnDeletePages_Click(object sender, EventArgs e)
        {
            if (txtPagesToDelete.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Please enter page numbers to delete');", true);
                return;
            }

            if (Session["docPath"].ToString() != "")
            {
                DeletePages(txtPagesToDelete.Text, Session["docPath"].ToString(), Session["docPath"].ToString());
            }
        }

        public void DeletePages(string pageRange, string SourcePdfPath, string OutputPdfPath, string Password = "")
        {
            List<int> pagesToDelete = new List<int>();
            // check for non-consecutive ranges
            if (pageRange.IndexOf(",") != -1)
            {
                string[] tmpHold = pageRange.Split(',');
                foreach (string nonconseq in tmpHold)
                {
                    // check for ranges
                    if (nonconseq.IndexOf("-") != -1)
                    {
                        string[] rangeHold = nonconseq.Split('-');
                        for (int i = Convert.ToInt32(rangeHold[0]); i <= Convert.ToInt32(rangeHold[1]); i++)
                        {
                            pagesToDelete.Add(i);
                        }
                    }
                    else
                    {
                        pagesToDelete.Add(Convert.ToInt32(nonconseq));
                    }
                }
            }
            else
            {
                // check for ranges
                if (pageRange.IndexOf("-") != -1)
                {
                    string[] rangeHold = pageRange.Split('-');
                    for (int i = Convert.ToInt32(rangeHold[0]); i <= Convert.ToInt32(rangeHold[1]); i++)
                    {
                        pagesToDelete.Add(i);
                    }
                }
                else
                {
                    pagesToDelete.Add(Convert.ToInt32(pageRange));
                }
            }

            Document doc = new Document();
            PdfReader reader = new PdfReader(SourcePdfPath, new System.Text.ASCIIEncoding().GetBytes(Password));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();
                doc.AddDocListener(writer);
                for (int p = 1; p <= reader.NumberOfPages; p++)
                {
                    if (pagesToDelete.FindIndex(s => s == p) != -1)
                    {
                        continue;
                    }
                    doc.SetPageSize(reader.GetPageSize(p));
                    //doc.SetPageSize(PageSize.A4);

                    doc.NewPage();
                    PdfContentByte cb = writer.DirectContent;
                    PdfImportedPage pageImport = writer.GetImportedPage(reader, p);
                    //int rot = reader.GetPageRotation(p);
                    //if (rot == 90 || rot == 270)
                    //    cb.AddTemplate(pageImport, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(p).Height);
                    //else
                    //    cb.AddTemplate(pageImport, 1.0F, 0, 0, 1.0F, 0, 0);



                    cb.AddTemplate(pageImport, 1.0F, 0, 0, 1.0F, 0, 0);

                }
                reader.Close();
                doc.Close();
                File.WriteAllBytes(OutputPdfPath, memoryStream.ToArray());
            }
        }
    }
}