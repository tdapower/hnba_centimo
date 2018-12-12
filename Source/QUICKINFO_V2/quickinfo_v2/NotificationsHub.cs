using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quickinfo_v2
{
    public class NotificationsHub : Hub
    {
        public void NotifyAllClients(string title, string msg, string type, string timeout, string branch)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
            context.Clients.All.displayNotification(title, msg, type, timeout, branch);
        }

        public void NotifyClientForCoverNoteBookRequests(string title, string msg, string receiverUserCode)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
            context.Clients.All.displayCoverNoteBookRequestNotification(title, msg, receiverUserCode);
        }

    }
}