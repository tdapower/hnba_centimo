using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quickinfo_v2.Models.MNBNewBusinessWF
{
    public class Quotation
    {
        public string QuotationNo { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleRiskType { get; set; }
        public string VehicleType { get; set; }
        public string Usage { get; set; }
        public string FuelType { get; set; }

        
        public Quotation()
        {

        }

        public Quotation(string quotationNo, string vehicleNo, string vehicleRiskType, string vehicleType, string usage, string fuelType)
        {
    
            QuotationNo = quotationNo;
            VehicleNo = vehicleNo;
            VehicleRiskType = vehicleRiskType;
            VehicleType = vehicleType;
            Usage = usage;
            FuelType = fuelType;
        }

  


    }
}









