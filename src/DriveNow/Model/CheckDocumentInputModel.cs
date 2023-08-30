using DriveNow.Enums;

namespace DriveNow.Model;

public class CheckDocumentInputModel
{
    public DocumentStatus Status { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid AdminId { get; set; }
}