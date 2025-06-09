namespace pps_api.Entities
{
    public class QualityControl
    {
        public int? QualityID { get; set; }
        public int? BatchID { get; set; }
        public decimal? FatPercentage { get; set; }
        public decimal? SNFPercentage { get; set; }
        public decimal? AlcoholPercentage { get; set; }
        public decimal? LactometerReading { get; set; }
        public decimal? MBRT { get; set; }
        public decimal? HeatStability { get; set; }
        public bool? ClotOnBoiling { get; set; }
        public string? AdulterationTests { get; set; }
        public decimal? ProteinPercentage { get; set; }

        public QualityControl(int? QualityID_, int? BatchID_, decimal? FatPercentage_, decimal? SNFPercentage_, decimal? AlcoholPercentage_, decimal? LactometerReading_, decimal? MBRT_, decimal? HeatStability_, bool? ClotOnBoiling_, string? AdulterationTests_, decimal? ProteinPercentage_)
        {
            this.QualityID = QualityID_;
            this.BatchID = BatchID_;
            this.FatPercentage = FatPercentage_;
            this.SNFPercentage = SNFPercentage_;
            this.AlcoholPercentage = AlcoholPercentage_;
            this.LactometerReading = LactometerReading_;
            this.MBRT = MBRT_;
            this.HeatStability = HeatStability_;
            this.ClotOnBoiling = ClotOnBoiling_;
            this.AdulterationTests = AdulterationTests_;
            this.ProteinPercentage = ProteinPercentage_;
        }
    }
}
