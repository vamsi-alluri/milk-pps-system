namespace pps_api.Entities
{
    public class MilkSuppliers
    {
        public int? AgentID { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? MilkTypesAvailable { get; set; }
        public int? OnRoute { get; set; }

        public MilkSuppliers(int? AgentID_, string Name_, string? Location_, string? Address_, string? MilkTypesAvailable_, int? OnRoute_)
        {
            this.AgentID = AgentID_;
            this.Name = Name_;
            this.Location = Location_;
            this.Address = Address_;
            this.MilkTypesAvailable = MilkTypesAvailable_;
            this.OnRoute = OnRoute_;
        }
    }
}
