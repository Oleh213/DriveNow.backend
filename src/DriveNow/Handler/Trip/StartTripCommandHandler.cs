using DriveNow.Commands.Trip;
using DriveNow.DBContext;
using MediatR;

namespace DriveNow.Handler.Trip;

public class StartTripCommandHandler: IRequestHandler<StartTripCommand, string>
{
    public ShopContext _context { get; set; }

    public StartTripCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(StartTripCommand command, CancellationToken cancellationToken)
    {
        _context.trips.Add(new Context.Trip
        {
            TripId = Guid.NewGuid(),
            CarId = command._Model.CarId,
            UserId = command.UserId,
            StartTrip = DateTimeOffset.Now,
            Status = true
        });
        await _context.SaveChangesAsync();
        return "Successful!";
    }
}