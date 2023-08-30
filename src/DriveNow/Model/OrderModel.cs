using DriveNow.Context;

namespace DriveNow.Model;

public class OrderModel
{
    public Guid OrderId { get; set; }
		
    public Guid CarId { get; set; }

    public Guid UserId { get; set; }

    public int TotalPrice { get; set; }

    public DateTimeOffset OrderTime { get; set; }

    public string Promocode { get; set; }

    public User User { get; set; }
		
    public Car Car { get; set; } 
}