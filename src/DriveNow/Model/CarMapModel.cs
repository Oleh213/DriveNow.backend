using DriveNow.Context;

namespace DriveNow.Model;

public class CarMapModel
{
    public Guid CarId { get; set; }
    
    public string NameCar { get; set; }
    
    public int Price { get; set; }
    
    public int? Discount { get; set; }
    
    public string Address { get; set; }
    
    public Catogory Category { get; set; }
    
    public string Free { get; set; }
    
    public string PhotoUrl { get; set; }
}