namespace pps_api.Entities
{
    public class VehicleType
    {
        public int? TypeID { get; set; }
        public string TypeName { get; set; }

        public VehicleType(int? TypeID_, string TypeName_)
        {
            this.TypeID = TypeID_;
            this.TypeName = TypeName_;
        }
    }
}
