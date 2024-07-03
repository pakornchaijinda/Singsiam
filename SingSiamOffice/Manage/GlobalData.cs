namespace SingSiamOffice.Manage
{
    public class GlobalData
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string role { get; set; }    
        public string branch_name { get; set; }
        public int branch_id { get; set; }
        public decimal RemainingPaid { get; set; }  = 0;
        public decimal paymentAmount { get; set; }
    }
    public class Collateral1 
    {
        public string? vehicle_brand { get; set; } = "-";
        public string? vehicle_color { get; set; } = "-";
        public string? chassisNo { get; set; } = "-";
        public string? vehicle_ver { get; set; } = "-";
        public string? plate { get; set; } = "-";
        public string? machineNo { get; set; } = "-";
        public string? vehicle_yr { get; set; } = "-";
        public string? tax_issue { get; set; } = "-";
    }
    public class Collateral2 
    {
        public string? deedType { get; set; }
        public string? deedNo { get; set; }
        public string? landType { get; set; }
        public string? mapsheet { get; set; }
        public string? parcelNo { get; set; }
        public string? volumn { get; set; }
        public string? page { get; set; }
        public string? pageInspect { get; set; }
        public string? landAmount { get; set; }
    }
    public class Collateral3 
    {
        public string? bookNo { get; set; }
        public string? farmerCode { get; set; }
        public string? fregisDate { get; set; }
        public string? docRight { get; set; }
        public string? fHolding { get; set; }
        public string? fActivity { get; set; }
        public string? fArea { get; set; }
        public string? fProduct { get; set; }
        public string? fLocate { get; set; }
    }
}
