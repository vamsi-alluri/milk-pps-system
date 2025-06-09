namespace pps_api.Entities
{
    public class Silos
    {
        public int? SiloID { get; set; }
        public int ProcessingPlantID { get; set; }
        public int CapacityLitres { get; set; }
        public int? CurrentStockLitres { get; set; }
        public DateTime? LastUpdated { get; set; }

        public Silos(int? SiloID_, int ProcessingPlantID_, int CapacityLitres_, int? CurrentStockLitres_, DateTime? LastUpdated_)
        {
            this.SiloID = SiloID_;
            this.ProcessingPlantID = ProcessingPlantID_;
            this.CapacityLitres = CapacityLitres_;
            this.CurrentStockLitres = CurrentStockLitres_;
            this.LastUpdated = LastUpdated_;
        }
    }
}
