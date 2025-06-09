namespace pps_api.Entities
{
    public class CorrectionBatches
    {
        public int CorrectionBatchID { get; set; }
        public int BatchID { get; set; }
        public string Reason { get; set; }
        public DateTime CorrectionTime { get; set; }
        public string Correction { get; set; }

        public CorrectionBatches(int CorrectionBatchID_, int BatchID_, string Reason_, DateTime CorrectionTime_, string Correction_)
        {
            this.CorrectionBatchID = CorrectionBatchID_;
            this.BatchID = BatchID_;
            this.Reason = Reason_;
            this.CorrectionTime = CorrectionTime_;
            this.Correction = Correction_;
        }
    }
}
