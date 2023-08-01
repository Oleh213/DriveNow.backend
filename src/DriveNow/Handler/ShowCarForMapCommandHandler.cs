using DriveNow.Commands;
using DriveNow.DBContext;
using DriveNow.DTO;
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

            foreach (var VARIABLE in _context.cars)
            {
                var locationService = new GoogleLocationService("AIzaSyB_y7ZhPC9n9GDExv0Lpa35BhGtE_tun5g");

                var point = locationService.GetLatLongFromAddress(VARIABLE.Address);

                var Latitude = point.Latitude;

                var Longitude = point.Longitude;
                
                cars.Add(new ShowCarForMapDTO
                {
                    Category = VARIABLE.Catogories.CategoryName,
                    NameCar = VARIABLE.NameCar,
                    Discount = VARIABLE.Discount,
                    Price = VARIABLE.Price,
                    Longitude = Longitude,
                    Latitude = Latitude
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