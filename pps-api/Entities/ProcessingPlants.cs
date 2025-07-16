using Microsoft.EntityFrameworkCore;

public class ProcessingPlants
{
    public int? PlantID { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string? Address { get; set; }
    public string? Capabilities { get; set; }
    [Precision(18, 2)]
    public decimal? Cost { get; set; }

    public ProcessingPlants(int? PlantID_, string Name_, string? Location_, string? Address_, string? Capabilities_, decimal? Cost_)
    {
        this.PlantID = PlantID_;
        this.Name = Name_;
        this.Location = Location_;
        this.Address = Address_;
        this.Capabilities = Capabilities_;
        this.Cost = Cost_;
    }
}