using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands.Trip;

public class PayLinkCommand: IRequest<string>
{
    public PayLinkInputModel _model { get; set; }
    
    public Guid _userId { get; set; }

    public PayLinkCommand(PayLinkInputModel model, Guid UserId)
    {
        _model = model;
        _userId = UserId;
    }
}