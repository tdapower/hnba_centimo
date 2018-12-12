using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2.Views.Common
{
    public partial class TCSDocViewPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string filePath = "";
                string SystemName = "";



                if (Request.QueryString["DocPath"] != null)
                {
                    filePath = Request.QueryString["DocPath"].ToString();
                }
                if (Request.QueryString["SystemName"] != null)
                {
                    SystemName = Request.QueryString["SystemName"].ToString();
                }

                string SYSTEM_NAME_TCS = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TCS"].ToString();
                string SYSTEM_NAME_TAKAFUL = System.Configuration.ConfigurationManager.AppSettings["SYSTEM_NAME_TAKAFUL"].ToString();

                // Response.ContentType = ContentType;
                //  Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                // Response.WriteFile(filePath);
                // Response.End();

                //Response.ContentType = "application/pdf";
                //Response.WriteFile(filePath);
                //Response.End();

                //    \\192.168.10.24\u01\bea\user_projects\domains\LinuxDomain\applications\IIMS\IIMS\document\tempDOWNLOAD\WPGG-2540_POLICY_CERTIFICATE_MOTOR-CAR_2365728.pdf
                try
                {

                    if (SystemName == SYSTEM_NAME_TCS)
                    {
                        filePath = filePath.Replace("Z:", @"\\192.168.10.24\u01\bea\user_projects\domains\LinuxDomain\applications\IIMS\IIMS\document");
                    }
                    else if (SystemName == SYSTEM_NAME_TAKAFUL)
                    {
                        filePath = filePath.Replace("Z:", @"\\192.168.10.58\u01\bea\user_projects\domains\LinuxDomain\applications\IIMS\IIMS\document");
                    }
                    Response.ContentType = "application/pdf";
                    Response.WriteFile(@filePath);
                    Response.End();
                }
                catch (Exception ex)
                {

                    Page.ClientScript.RegisterStartupScript(GetType(), "Message", "alert('Error loading document');", true);

                }

            }
        }
    }
}