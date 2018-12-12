using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Common
{
    public partial class DocumentViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string filePath = "";

                if (Request.QueryString["DocPath"] != null)
                {
                    filePath = Request.QueryString["DocPath"].ToString();
                }


                // Response.ContentType = ContentType;
                //  Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                // Response.WriteFile(filePath);
                // Response.End();

                Response.ContentType = "application/pdf";
                Response.WriteFile(filePath);
                Response.Flush();
                Response.End();

         

            }
        }
    }
}