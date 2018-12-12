using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quickinfo_v2.Models.MNBNewBusinessWF
{
    public class ProposalUpload
    {
        public int ProposalUploadId { get; set; }
        public string QuotationNo { get; set; }
        public string VehicleNo { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public int IsCoverNoteAvailable { get; set; }
        public string CoverNotePeriod { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string YearOfMake { get; set; }
        public string FirstRegDate { get; set; }
        public string CubicCapacity { get; set; }
        public string EnteredUser { get; set; }
        public string EnteredUserBranchCode { get; set; }
        public string TCSPolicyNo { get; set; }
        public string TCSProposalNo { get; set; }
        public string TCSPolicyId { get; set; }

        public string JobType { get; set; }
        public string JobNumber { get; set; }
        public int IsDocsAvailable { get; set; }
        public string EndorsementType { get; set; }

        public string CancellationType { get; set; }
        public string Remarks { get; set; }
        public int IsOwnBranchPolicy { get; set; }
        public string BranchOfPolicy { get; set; }
        public string IssueType { get; set; }

        public string SystemName { get; set; }

        public int IsUrgent { get; set; }

        public string PolicyType { get; set; }


        public string JobStatus { get; set; }


        public int IsCertificateConvertion { get; set; }


        public int IsDocsPrintFromHDO { get; set; }

        public ProposalUpload()
        {

        }

        public ProposalUpload(int proposalUploadId, string quotationNo, string vehicleNo, string engineNo, string chassisNo, int isCoverNoteAvailable,
            string coverNotePeriod, string addressLine1, string addressLine2, string addressLine3, string yearOfMake, string firstRegDate,
            string cubicCapacity, string enteredUser, string enteredUserBranchCode, string tCSPolicyNo, string tCSProposalNo, string tCSPolicyId,
            string jobType, string jobNumber, string issueType, string policyType, string endorsementType, string cancellationType, string systemName,
            int isCertificateConvertion)
        {
            ProposalUploadId = proposalUploadId;
            QuotationNo = quotationNo;
            VehicleNo = vehicleNo;
            EngineNo = engineNo;
            ChassisNo = chassisNo;
            IsCoverNoteAvailable = isCoverNoteAvailable;
            CoverNotePeriod = coverNotePeriod;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            YearOfMake = yearOfMake;
            FirstRegDate = firstRegDate;
            CubicCapacity = cubicCapacity;
            EnteredUser = enteredUser;
            EnteredUserBranchCode = enteredUserBranchCode;
            TCSPolicyNo = tCSPolicyNo;
            TCSProposalNo = tCSProposalNo;
            TCSPolicyId = tCSPolicyId;
            JobType = jobType;
            JobNumber = jobNumber;
            IssueType = issueType;
            PolicyType = policyType;
            EndorsementType = endorsementType;
            CancellationType = cancellationType;
            SystemName = systemName;
            IsCertificateConvertion = isCertificateConvertion;
        }

        public ProposalUpload(string quotationNo, string vehicleNo, string engineNo, string chassisNo, int isCoverNoteAvailable, string coverNotePeriod,
            string addressLine1, string addressLine2, string addressLine3, string yearOfMake, string firstRegDate, string cubicCapacity, string enteredUser,
            string enteredUserBranchCode, string tCSPolicyNo, string tCSProposalNo, string jobType, string jobNumber, string issueType, string policyType
            , string endorsementType, string cancellationType, string systemName)
        {
            QuotationNo = quotationNo;
            VehicleNo = vehicleNo;
            EngineNo = engineNo;
            ChassisNo = chassisNo;
            IsCoverNoteAvailable = isCoverNoteAvailable;
            CoverNotePeriod = coverNotePeriod;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            YearOfMake = yearOfMake;
            FirstRegDate = firstRegDate;
            CubicCapacity = cubicCapacity;
            EnteredUser = enteredUser;
            EnteredUserBranchCode = enteredUserBranchCode;
            TCSPolicyNo = tCSPolicyNo;
            TCSProposalNo = tCSProposalNo;
            JobType = jobType;
            JobNumber = jobNumber;
            IssueType = issueType;
            PolicyType = policyType;
            EndorsementType = endorsementType;
            CancellationType = cancellationType;
            SystemName = systemName;
        }




    }
}









