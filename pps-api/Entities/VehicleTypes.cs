namespace pps_api.Entities
{
    public class VehicleTypes
    {
        public int? TypeID { get; set; }
        public string TypeName { get; set; }

        public VehicleTypes(int? TypeID_, string TypeName_)
        {
            this.TypeID = TypeID_;
            this.TypeName = TypeName_;
        }
    }
}
