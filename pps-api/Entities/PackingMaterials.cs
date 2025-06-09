namespace pps_api.Entities
{
    public class PackingMaterials
    {
        public int? PackingMaterialID { get; set; }
        public string? PackingMaterialName { get; set; }
        public decimal? Cost { get; set; }
        public string? PurposeOfPackingMaterial { get; set; }

        public PackingMaterials(int? PackingMaterialID_, string? PackingMaterialName_, decimal? Cost_, string? PurposeOfPackingMaterial_)
        {
            this.PackingMaterialID = PackingMaterialID_;
            this.PackingMaterialName = PackingMaterialName_;
            this.Cost = Cost_;
            this.PurposeOfPackingMaterial = PurposeOfPackingMaterial_;
        }
    }
}
