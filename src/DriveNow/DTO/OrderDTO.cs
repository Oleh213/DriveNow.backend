using DriveNow.Context;

namespace DriveNow.DTO;

public class OrderDTO
{
    public Guid OrderId { get; set; }
    
    public int TotalPrice { get; set; }

    public DateTime OrderTime { get; set; }

    public string Promocode { get; set; }
    
    public string NameCar { get; set; }
    
    public Car Car { get; set; }
}