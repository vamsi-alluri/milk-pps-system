namespace pps_api.Entities
{
    public class BatchStatusTracking
    {
        public int? BatchID { get; set; }
        public int StatusID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Remarks { get; set; }

        public BatchStatusTracking(int? BatchID_, int StatusID_, DateTime? UpdatedAt_, string? Remarks_)
        {
            this.BatchID = BatchID_;
            this.StatusID = StatusID_;
            this.UpdatedAt = UpdatedAt_;
            this.Remarks = Remarks_;
        }
    }
}
