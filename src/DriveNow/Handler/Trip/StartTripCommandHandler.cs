using AutoMapper;
using DriveNow.Commands.Trip;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler.Trip;

public class StartTripCommandHandler: IRequestHandler<StartTripCommand, UserTripModel>
{
    private IMapper _mapper;
    public ShopContext _context { get; set; }

    public StartTripCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserTripModel> Handle(StartTripCommand command, CancellationToken cancellationToken)
    {
        var new_trip = new Context.Trip
        {
            TripId = Guid.NewGuid(),
            CarId = command._Model.CarId,
            UserId = command.UserId,
            StartTrip = DateTimeOffset.Now,
            Status = true,
            PaymentUrl = ""
        };
        
        _context.trips.Add(new_trip);

        var car = await _context.cars.FirstOrDefaultAsync(car => car.CarId == command._Model.CarId);
        
        UserTripModel result = new UserTripModel
        {
            Longitude = car.Longitude,
            Latitude = car.Latitude,
            CarName = car.NameCar,
            Status = true,
            StartTrip = DateTimeOffset.Now
        };
        
        await _context.SaveChangesAsync();
        
        return result;
    }
}