using Microsoft.EntityFrameworkCore;

namespace pps_api.Entities
{
    public class Batches
    {
        public int? BatchID { get; set; }
        public int? PlantID { get; set; }
        public int SiloID { get; set; }
        public string? BatchName { get; set; }
        public string MilkType { get; set; }
        [Precision(18, 2)]
        public decimal? FatPercentage { get; set; }
        public decimal? SNFPercentage { get; set; }
        [Precision(18, 2)]
        public decimal? SMPPercentage { get; set; }
        [Precision(18, 2)]
        public int MilkMixtureComposition { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int StatusID { get; set; }
        public int? ForRoute { get; set; }

        public Batches(int? BatchID_, int? PlantID_, int SiloID_, string? BatchName_, string MilkType_, decimal? FatPercentage_, decimal? SNFPercentage_, decimal? SMPPercentage_, int MilkMixtureComposition_, DateTime? CreatedAt_, int StatusID_, int? ForRoute_)
        {
            this.BatchID = BatchID_;
            this.PlantID = PlantID_;
            this.SiloID = SiloID_;
            this.BatchName = BatchName_;
            this.MilkType = MilkType_;
            this.FatPercentage = FatPercentage_;
            this.SNFPercentage = SNFPercentage_;
            this.SMPPercentage = SMPPercentage_;
            this.MilkMixtureComposition = MilkMixtureComposition_;
            this.CreatedAt = CreatedAt_;
            this.StatusID = StatusID_;
            this.ForRoute = ForRoute_;
        }
    }
}
