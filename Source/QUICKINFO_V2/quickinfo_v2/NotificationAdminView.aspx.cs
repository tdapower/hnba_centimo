using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace quickinfo_v2
{
    public partial class NotificationAdminView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public void SendNotifications()
        {
            if (txtTitle.Text == "" || txtMessage.Text == "" || txtTimeout.Text == "")
            {
                return;
            }
            string title = txtTitle.Text;
            string message = txtMessage.Text;
            string timeout = txtTimeout.Text;
            string branch = txtBranch.Text;

            string type = "";

            if (rdbSuccess.Checked == true)
            {
                type = "Success";
            }
            else if (rdbInfo.Checked == true)
            {
                type = "Info";
            }
            else if (rdbWarning.Checked == true)
            {
                type = "Warning";
            }
            else if (rdbError.Checked == true)
            {
                type = "Error";
            }



            NotificationsHub nHub = new NotificationsHub();
            nHub.NotifyAllClients(title, message, type, timeout, branch);
        }
        protected void btnSendNotification_Click(object sender, EventArgs e)
        {
            if (txtDadPinCode.Text == "tdapower")
            {
                SendNotifications();
            }
            else
            {
                  ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('You are not the DAD to send notifications');", true);
            }

        }
    }
}