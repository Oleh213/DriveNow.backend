using AutoMapper;
using DriveNow.Commands.Trip;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler.Trip;

public class ShowUserTripCommandHandler: IRequestHandler<ShowUserTripCommand,TripModel>
{
    private IMapper _mapper;
    public ShopContext _context { get; set; }

    public ShowUserTripCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TripModel> Handle(ShowUserTripCommand command, CancellationToken cancellationToken)
    {
        var check_ride = await _context.trips
            .Where(user => (user.UserId == command.UserId) && (user.Status == true))
            .FirstOrDefaultAsync();
        
        var result = _mapper.Map<TripModel>(check_ride);
        
        return result;
    }
}