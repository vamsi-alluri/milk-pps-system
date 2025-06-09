namespace pps_api.Entities
{
    public class MilkTypes
    {
        public int? MilkID { get; set; }
        public string MilkName { get; set; }
        public decimal CostPerLitre { get; set; }
        public string BuffaloOrCow { get; set; }

        public MilkTypes(int? MilkID_, string MilkName_, decimal CostPerLitre_, string BuffaloOrCow_)
        {
            this.MilkID = MilkID_;
            this.MilkName = MilkName_;
            this.CostPerLitre = CostPerLitre_;
            this.BuffaloOrCow = BuffaloOrCow_;
        }
    }
}
