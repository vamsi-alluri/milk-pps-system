namespace pps_api.Entities
{
    public class MilkMixtureComposition
    {
        public int? CompositionID { get; set; }
        public int? BatchID { get; set; }
        public string? MilkType { get; set; }
        public decimal? Percentage { get; set; }

        public MilkMixtureComposition(int? CompositionID_, int? BatchID_, string? MilkType_, decimal? Percentage_)
        {
            this.CompositionID = CompositionID_;
            this.BatchID = BatchID_;
            this.MilkType = MilkType_;
            this.Percentage = Percentage_;
        }
    }
}
