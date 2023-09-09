using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.DTO;
using DriveNow.Enums;
using DriveNow.Model;
using GoogleMaps.LocationServices;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ShowCarForMapCommandHandler: IRequestHandler<ShowCarForMapCommand, List<CarMapModel>>
{
    private readonly ShopContext _context;
    
    private IMapper _mapper;

    public ShowCarForMapCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CarMapModel>> Handle(ShowCarForMapCommand command, CancellationToken cancellationToken)
    {
        var user_check = await _context.users.FirstOrDefaultAsync(userId => userId.UserId == command._userId);

        if (user_check != null)
        {
            await _context.cars.LoadAsync();
            
            var result = await _context.cars.Select(car => _mapper.Map<CarMapModel>(car)).ToListAsync();

            return result;
        }
        else
        {
            return null;
        }
    }
}