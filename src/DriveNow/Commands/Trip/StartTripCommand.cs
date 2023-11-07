using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands.Trip;

public class StartTripCommand: IRequest<UserTripModel>
{
    
    public Guid UserId { get; set; }
    public StartTripModel _Model
    {
        get;
        set;
    }

    public StartTripCommand(StartTripModel model,Guid userId)
    {
        _Model = model;
        UserId = userId;
    }
}