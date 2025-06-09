namespace pps_api.Entities
{
    public class CurdBatches
    {
        public int? CurdBatchID { get; set; }
        public int? BatchID { get; set; }
        public string? CurdType { get; set; }
        public decimal? Quantity { get; set; }

        public CurdBatches(int? CurdBatchID_, int? BatchID_, string? CurdType_, decimal? Quantity_)
        {
            this.CurdBatchID = CurdBatchID_;
            this.BatchID = BatchID_;
            this.CurdType = CurdType_;
            this.Quantity = Quantity_;
        }
    }
}
