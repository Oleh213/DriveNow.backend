namespace DriveNow.Model;

public class StartTripModel
{
    public Guid CarId { get; set; }
    
    public DateTimeOffset StartTrip { get; set; }
}