using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class LiqPayCallbackCommand: IRequest<string>
{
    public LiqPayModel _Model { get; set; }
    
    public Guid _UserId { get; set; }

    public LiqPayCallbackCommand(LiqPayModel model, Guid UserId)
    {
        _Model = model;
        _UserId = UserId;
    }
}