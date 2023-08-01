namespace DriveNow.DTO;

public class ShowCarForMapDTO
{
    public string NameCar { get; set; }
    
    public int Price { get; set; }

    public int? Discount { get; set; }
    
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Category { get; set; }

    public string PhotoUrl { get; set; }
}