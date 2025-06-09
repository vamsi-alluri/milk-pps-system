namespace pps_api.Entities
{
    public class ProfitabilityReports
    {
        public int? ReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public int DurationID { get; set; }
        public decimal TotalSalesRevenue { get; set; }
        public decimal TotalProcurementCost { get; set; }
        public decimal TotalProcessingCost { get; set; }
        public decimal TotalPackagingCost { get; set; }
        public decimal TotalTransportationCost { get; set; }

        public ProfitabilityReports(int? ReportID_, DateTime ReportDate_, int DurationID_, decimal TotalSalesRevenue_, decimal TotalProcurementCost_, decimal TotalProcessingCost_, decimal TotalPackagingCost_, decimal TotalTransportationCost_)
        {
            this.ReportID = ReportID_;
            this.ReportDate = ReportDate_;
            this.DurationID = DurationID_;
            this.TotalSalesRevenue = TotalSalesRevenue_;
            this.TotalProcurementCost = TotalProcurementCost_;
            this.TotalProcessingCost = TotalProcessingCost_;
            this.TotalPackagingCost = TotalPackagingCost_;
            this.TotalTransportationCost = TotalTransportationCost_;
        }
    }
}
