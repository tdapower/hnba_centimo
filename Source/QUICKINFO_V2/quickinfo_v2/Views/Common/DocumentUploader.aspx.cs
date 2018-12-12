using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Common
{
    public partial class DocumentUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["tid"] != null)
                {




                    if (Session["JobType"] != null)
                    {
                        if (Session["JobType"].ToString() == "New")
                        {
                            //string quotationNo = "";

                            //if (Session["QuotationNo"].ToString() != "")
                            //{
                            //    quotationNo = Session["QuotationNo"].ToString();

                            //    string DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["DOCUMENT_UPLOAD_PATH"].ToString();

                            //    string pathToCreate = @DOCUMENT_UPLOAD_PATH + quotationNo;

                            //    if (Request.Files.Count > 0)
                            //    {
                            //        //if (Directory.Exists((pathToCreate)))
                            //        //{
                            //        //    //In here, start looping and modify the path to create to add a number
                            //        //    //until you get the value needed
                            //        //}

                            //        //Now you know it is ok, create it
                            //        Directory.CreateDirectory((pathToCreate));
                            //    }
                            //    foreach (string s in Request.Files)
                            //    {
                            //        HttpPostedFile file = Request.Files[s];
                            //        file.SaveAs(System.IO.Path.Combine(pathToCreate, file.FileName));

                            //    }
                            //}
                        }
                        else if (Session["JobType"].ToString() == "Renewal")
                        {
                            string jobNo = "";

                            if (Session["SessionedJobNo"] != null)
                            {
                                jobNo = Session["SessionedJobNo"].ToString();
                            }

                            if (jobNo != "")
                            {

                                string DOCUMENT_UPLOAD_PATH = "";
                                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["RENEWAL_QUEUED_DOC_UPLOAD_PATH"].ToString();




                                DirectoryInfo dest = new DirectoryInfo(DOCUMENT_UPLOAD_PATH);

                                if (!Directory.Exists(dest.FullName + jobNo.ToUpper()))
                                {

                                    System.IO.Directory.CreateDirectory(dest.FullName + jobNo.ToUpper());

                                }

                                string pathToCreate = @DOCUMENT_UPLOAD_PATH + jobNo.ToUpper();

                                foreach (string s in Request.Files)
                                {
                                    HttpPostedFile file = Request.Files[s];
                                    file.SaveAs(System.IO.Path.Combine(pathToCreate, file.FileName));

                                }
                            }

                        }


                        else if (Session["JobType"].ToString() == "Endorsement")
                        {
                            string jobNo = "";

                            if (Session["SessionedJobNo"] != null)
                            {
                                jobNo = Session["SessionedJobNo"].ToString();
                            }

                            if (jobNo != "")
                            {

                                string DOCUMENT_UPLOAD_PATH = "";
                                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["ENDORSEMENT_DOC_UPLOAD_PATH"].ToString();


                                DirectoryInfo dest = new DirectoryInfo(DOCUMENT_UPLOAD_PATH);

                                if (!Directory.Exists(dest.FullName + jobNo.ToUpper()))
                                {

                                    System.IO.Directory.CreateDirectory(dest.FullName + jobNo.ToUpper());

                                }


                                string pathToCreate = @DOCUMENT_UPLOAD_PATH + jobNo.ToUpper();

                                foreach (string s in Request.Files)
                                {
                                    HttpPostedFile file = Request.Files[s];
                                    file.SaveAs(System.IO.Path.Combine(pathToCreate, file.FileName));

                                }
                            }
                        }

                        else if (Session["JobType"].ToString() == "Cancel")
                        {
                            string jobNo = "";

                            if (Session["SessionedJobNo"] != null)
                            {
                                jobNo = Session["SessionedJobNo"].ToString();
                            }

                            if (jobNo != "")
                            {

                                string DOCUMENT_UPLOAD_PATH = "";
                                DOCUMENT_UPLOAD_PATH = System.Configuration.ConfigurationManager.AppSettings["CANCELLATION_QUEUED_DOC_UPLOAD_PATH"].ToString();



                                DirectoryInfo dest = new DirectoryInfo(DOCUMENT_UPLOAD_PATH);

                                if (!Directory.Exists(dest.FullName + jobNo.ToUpper()))
                                {

                                    System.IO.Directory.CreateDirectory(dest.FullName + jobNo.ToUpper());

                                }


                                string pathToCreate = @DOCUMENT_UPLOAD_PATH + jobNo.ToUpper();

                                foreach (string s in Request.Files)
                                {
                                    HttpPostedFile file = Request.Files[s];
                                    file.SaveAs(System.IO.Path.Combine(pathToCreate, file.FileName));

                                }
                            }
                        }

                    }
                }
            }
        }
    }
}