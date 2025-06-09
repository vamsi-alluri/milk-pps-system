namespace pps_api.Entities
{
    public class GoodsTypes
    {
        public int? GoodsID { get; set; }
        public string GoodsName { get; set; }

        public GoodsTypes(int? GoodsID_, string GoodsName_)
        {
            this.GoodsID = GoodsID_;
            this.GoodsName = GoodsName_;
        }
    }
}
