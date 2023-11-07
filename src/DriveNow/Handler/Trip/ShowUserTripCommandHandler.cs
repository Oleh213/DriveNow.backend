using AutoMapper;
using DriveNow.Commands.Trip;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler.Trip;

public class ShowUserTripCommandHandler: IRequestHandler<ShowUserTripCommand,UserTripModel>
{
    private IMapper _mapper;
    public ShopContext _context { get; set; }

    public ShowUserTripCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserTripModel> Handle(ShowUserTripCommand command, CancellationToken cancellationToken)
    {
        var check_ride = await _context.trips
            .Where(user => (user.UserId == command.UserId) && (user.Status == true))
            .FirstOrDefaultAsync();
        
        var tripModel = _mapper.Map<TripModel>(check_ride);

        var car_ride = await _context.cars.FirstOrDefaultAsync(car => car.CarId == tripModel.CarId);

        if (car_ride != null && check_ride.Status)
        {
            UserTripModel result = new UserTripModel
                    {
                        Longitude = car_ride.Longitude,
                        Latitude = car_ride.Latitude,
                        CarName = car_ride.NameCar,
                        Status = check_ride.Status,
                        StartTrip = check_ride.StartTrip,
                        CarId = car_ride.CarId.ToString()
                    };
            
            return result;
        }
        return null;
    }
}