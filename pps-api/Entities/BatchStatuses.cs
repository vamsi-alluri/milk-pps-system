namespace pps_api.Entities
{
    public class BatchStatuses
    {
        public int? StatusID { get; set; }
        public string StatusName { get; set; }

        public BatchStatuses(int? StatusID_, string StatusName_)
        {
            this.StatusID = StatusID_;
            this.StatusName = StatusName_;
        }
    }
}
