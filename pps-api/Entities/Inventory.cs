namespace pps_api.Entities
{
    public class Inventory
    {
        public int? InventoryID { get; set; }
        public int? ProcessingPlantID { get; set; }
        public int ItemTypeID { get; set; }
        public int? MilkTypeID { get; set; }
        public decimal? FatPercentage { get; set; }
        public decimal? SNFPercentage { get; set; }
        public int? QuantityLitres { get; set; }
        public int? QuantityKg { get; set; }
        public DateTime? LastUpdated { get; set; }

        public Inventory(int? InventoryID_, int? ProcessingPlantID_, int ItemTypeID_, int? MilkTypeID_, decimal? FatPercentage_, decimal? SNFPercentage_, int? QuantityLitres_, int? QuantityKg_, DateTime? LastUpdated_)
        {
            this.InventoryID = InventoryID_;
            this.ProcessingPlantID = ProcessingPlantID_;
            this.ItemTypeID = ItemTypeID_;
            this.MilkTypeID = MilkTypeID_;
            this.FatPercentage = FatPercentage_;
            this.SNFPercentage = SNFPercentage_;
            this.QuantityLitres = QuantityLitres_;
            this.QuantityKg = QuantityKg_;
            this.LastUpdated = LastUpdated_;
        }
    }
}
