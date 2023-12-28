using AutoMapper;
using DriveNow.Commands;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ShowCarDetailsCommandHandler: IRequestHandler<ShowCarDetailsCommand, CarModel>
{
    private ShopContext _context;
    private IMapper _mapper;

    public ShowCarDetailsCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CarModel> Handle(ShowCarDetailsCommand command, CancellationToken cancellationToken)
    {
        var cars = await _context.cars.FirstOrDefaultAsync(car => car.CarId == command.Car_Id);
				
        var result = _mapper.Map<CarModel>(cars);

        return result;
    }
}