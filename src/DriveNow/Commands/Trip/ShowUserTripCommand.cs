using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands.Trip;

public class ShowUserTripCommand: IRequest<TripModel>
{
    public Guid UserId { get; set; }

    public ShowUserTripCommand(Guid userId)
    {
        UserId = userId;
    }
}