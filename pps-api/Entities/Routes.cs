namespace pps_api.Entities
{
    public class Routes
    {
        public int? RouteID { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public int? AssociatedProcessingPlant { get; set; }

        public Routes(int? RouteID_, string RouteCode_, string RouteName_, int? AssociatedProcessingPlant_)
        {
            this.RouteID = RouteID_;
            this.RouteCode = RouteCode_;
            this.RouteName = RouteName_;
            this.AssociatedProcessingPlant = AssociatedProcessingPlant_;
        }
    }
}
