using MediatR;

namespace DriveNow.Commands;

public class LiqPayCallbackCommand: IRequest<string>
{
    public string Data { get; set; }
    
    public string Signature { get; set; }
    
    public Guid _UserId { get; set; }

    public LiqPayCallbackCommand(string data, string signature, Guid UserId)
    {
        Data = data;
        Signature = signature;
        _UserId = UserId;
    }
}