using Microsoft.EntityFrameworkCore;

namespace pps_api.Entities
{
    public class ProfitabilityReports
    {
        public int? ReportID { get; set; }
        public DateTime ReportDate { get; set; }
        public int DurationID { get; set; }
        [Precision(18, 2)]
        public decimal TotalSalesRevenue { get; set; }
        [Precision(18, 2)]
        public decimal TotalProcurementCost { get; set; }
        [Precision(18, 2)]
        public decimal TotalProcessingCost { get; set; }
        [Precision(18, 2)]
        public decimal TotalPackagingCost { get; set; }
        [Precision(18, 2)]
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
