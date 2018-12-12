using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quickinfo_v2.Models.ITWorkflow
{
    public class UserRequest
    {
       

        public int RequestID { get; set; }
        public string RefNo { get; set; }
        public string JobRemarks { get; set; }
        public byte[] Screenshot { get; set; }
        public string RequestedUser { get; set; }

        public UserRequest(int requestID, string refNo, string jobRemarks, byte[] screenshot,string requestedUser)
        {
            RequestID = requestID;
            RefNo = refNo;
            JobRemarks = jobRemarks;
            Screenshot = screenshot;
            RequestedUser=requestedUser;
        }

        public UserRequest()
        {
        }
    }
}