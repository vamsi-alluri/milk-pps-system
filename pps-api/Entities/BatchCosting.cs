namespace pps_api.Entities
{
    public class BatchCosting
    {
        public int BatchID { get; set; }
        public decimal ProcurementCost { get; set; }
        public decimal ProcessingCost { get; set; }
        public decimal PackagingCost { get; set; }
        public decimal TransportationCost { get; set; }

        public BatchCosting(int BatchID_, decimal ProcurementCost_, decimal ProcessingCost_, decimal PackagingCost_, decimal TransportationCost_)
        {
            this.BatchID = BatchID_;
            this.ProcurementCost = ProcurementCost_;
            this.ProcessingCost = ProcessingCost_;
            this.PackagingCost = PackagingCost_;
            this.TransportationCost = TransportationCost_;
        }
    }
}
