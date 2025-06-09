namespace pps_api.Entities
{
    public class TransportVehicles
    {
        public int? VehicleID { get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleTypeID { get; set; }
        public int GoodsID { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? LastLocation { get; set; }
        public decimal? CarryingCapacity { get; set; }
        public decimal? VehicleCapacity { get; set; }
        public decimal? Cost { get; set; }
        public int? DriverID { get; set; }
        public int? RouteID { get; set; }

        public TransportVehicles(int? VehicleID_, string VehicleNumber_, int VehicleTypeID_, int GoodsID_, string? Source_, string? Destination_, string? LastLocation_, decimal? CarryingCapacity_, decimal? VehicleCapacity_, decimal? Cost_, int? DriverID_, int? RouteID_)
        {
            this.VehicleID = VehicleID_;
            this.VehicleNumber = VehicleNumber_;
            this.VehicleTypeID = VehicleTypeID_;
            this.GoodsID = GoodsID_;
            this.Source = Source_;
            this.Destination = Destination_;
            this.LastLocation = LastLocation_;
            this.CarryingCapacity = CarryingCapacity_;
            this.VehicleCapacity = VehicleCapacity_;
            this.Cost = Cost_;
            this.DriverID = DriverID_;
            this.RouteID = RouteID_;
        }
    }
}
