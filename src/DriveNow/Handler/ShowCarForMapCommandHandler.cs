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

public class ShowCarForMapCommandHandler: IRequestHandler<ShowCarForMapCommand, List<ShowCarForMapDTO>>
{
    private readonly ShopContext _context;

    public ShowCarForMapCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<ShowCarForMapDTO>> Handle(ShowCarForMapCommand command, CancellationToken cancellationToken)
    {
        var user_check = await _context.users.FirstOrDefaultAsync(userId => userId.UserId == command._userId);

        if (user_check != null)
        {
            List<ShowCarForMapDTO> cars = new List<ShowCarForMapDTO>();

            await _context.catogories.LoadAsync();

            foreach (var context_cars in _context.cars)
            {
                cars.Add(new ShowCarForMapDTO
                {
                    Category = context_cars.Catogories.CategoryName,
                    NameCar = context_cars.NameCar,
                    Discount = context_cars.Discount,
                    Price = context_cars.Price,
                    Address = context_cars.Address,
                    Free = context_cars.Free.ToString(),
                    PhotoUrl = context_cars.AccualFileUrl
                });
            }

            return cars;
        }
        else
        {
            return null;
        }
    }
}