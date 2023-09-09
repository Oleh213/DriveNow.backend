using DriveNow.Enums;

namespace DriveNow.Context;

public class Trip
{
    public Guid TripId { get; set; }
    public Guid UserId { get; set; }
    
    public Guid CarId { get; set; }
    
    public DateTimeOffset StartTrip { get; set; }
    
    public Boolean Status { get; set; }
}