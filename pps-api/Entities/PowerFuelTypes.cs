using Microsoft.EntityFrameworkCore;

namespace pps_api.Entities
{
    public class PowerFuelTypes
    {
        public int? PowerFuelTypeID { get; set; }
        public string? PowerFuelTypeName { get; set; }
        [Precision(18, 2)]
        public decimal? Cost { get; set; }

        public PowerFuelTypes(int? PowerFuelTypeID_, string? PowerFuelTypeName_, decimal? Cost_)
        {
            this.PowerFuelTypeID = PowerFuelTypeID_;
            this.PowerFuelTypeName = PowerFuelTypeName_;
            this.Cost = Cost_;
        }
    }
}
