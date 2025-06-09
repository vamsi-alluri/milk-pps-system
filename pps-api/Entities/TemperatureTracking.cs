namespace pps_api.Entities
{
    public class TemperatureTracking
    {
        public int? TempID { get; set; }
        public int? BatchID { get; set; }
        public string? Stage { get; set; }
        public decimal? Temperature { get; set; }

        public TemperatureTracking(int? TempID_, int? BatchID_, string? Stage_, decimal? Temperature_)
        {
            this.TempID = TempID_;
            this.BatchID = BatchID_;
            this.Stage = Stage_;
            this.Temperature = Temperature_;
        }
    }
}
