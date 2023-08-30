using DriveNow.Context;

namespace DriveNow.Model;

public class ShowUserOrderOutputModel
{
    public Guid OrderId { get; set; }
    
    public int TotalPrice { get; set; }

    public DateTimeOffset OrderTime { get; set; }

    public string Promocode { get; set; }
    
    public string NameCar { get; set; }
    
    public Car Car { get; set; }
}