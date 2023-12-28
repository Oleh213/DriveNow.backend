namespace DriveNow.Model;

public class UserTripModel
{
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public string CarName { get; set; }
    
    public string CarId { get; set; }
    
    public int Price { get; set; }
    
    public Boolean Status { get; set; }

    public DateTime StartTrip { get; set; }
}