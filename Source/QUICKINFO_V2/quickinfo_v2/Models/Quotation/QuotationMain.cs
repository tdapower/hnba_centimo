using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quickinfo_v2.Models.Quotation
{
    public class QuotationMain
    {
        public int JobId { get; set; }
        public string QuotationNo { get; set; }
        public string RequestBy { get; set; }
        public string ClientName { get; set; }
        public string VehicleChasisNo { get; set; }
        public string RiskTypeId { get; set; }
        public string VehicleTypeId { get; set; }
        public string VehicleClassId { get; set; }
        public string SumInsured { get; set; }
        public string PeriodTypeCode { get; set; }
        public string PeriodCode { get; set; }
        public string AgentBroker { get; set; }
        public string LeasingType { get; set; }
        public string FuelTypeCode { get; set; }
        public string ProductCode { get; set; }
        public string BranchId { get; set; }
        public string Remark { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public int RevisionNo { get; set; }
        public string QuotYear { get; set; }
        public string AgentBrokerCode { get; set; }

        public QuotationMain() { }

        public QuotationMain(int jobId, string quotationNo, string requestBy, string clientName, string vehicleChasisNo, string riskTypeId, string vehicleTypeId, string vehicleClassId, string sumInsured, string periodTypeCode, string periodCode, string agentBroker, string leasingType, string fuelTypeCode, string productCode, string branchId, string remark, string requestDate, string status, string userId, int revisionNo, string quotYear, string agentBrokerCode)
        {
            JobId = jobId;
            QuotationNo = quotationNo;
            RequestBy = requestBy;
            ClientName = clientName;
            VehicleChasisNo = vehicleChasisNo;
            RiskTypeId = riskTypeId;
            VehicleTypeId = vehicleTypeId;
            VehicleClassId = vehicleClassId;
            SumInsured = sumInsured;
            PeriodTypeCode = periodTypeCode;
            PeriodCode = periodCode;
            AgentBroker = agentBroker;
            LeasingType = leasingType;
            FuelTypeCode = fuelTypeCode;
            ProductCode = productCode;
            BranchId = branchId;
            Remark = remark;
            RequestDate = requestDate;
            Status = status;
            UserId = userId;
            RevisionNo = revisionNo;
            QuotYear = quotYear;
            AgentBrokerCode = agentBrokerCode;

        }
    }
}