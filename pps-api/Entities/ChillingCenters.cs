public class ChillingCenters
{
    public int? ChillingCenterID { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string? Address { get; set; }
    public string? Capabilities { get; set; }
    public decimal? Cost { get; set; }
    public int? OnRoute { get; set; }

    public ChillingCenters(int? ChillingCenterID_, string Name_, string? Location_, string? Address_, string? Capabilities_, decimal? Cost_, int? OnRoute_)
    {
        this.ChillingCenterID = ChillingCenterID_;
        this.Name = Name_;
        this.Location = Location_;
        this.Address = Address_;
        this.Capabilities = Capabilities_;
        this.Cost = Cost_;
        this.OnRoute = OnRoute_;
    }
}