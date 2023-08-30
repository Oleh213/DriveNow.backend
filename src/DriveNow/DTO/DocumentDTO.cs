using DriveNow.Enums;

namespace DriveNow.DTO;

public class DocumentDTO
{
    public string DocumentUrl { get; set; }
    
    public DocumentStatus Status { get; set; }
    
    public Guid UserId { get; set; }
    
    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
}