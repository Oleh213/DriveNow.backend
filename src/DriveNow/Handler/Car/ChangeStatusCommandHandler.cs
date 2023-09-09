using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Enums;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ChangeStatusCommandHandler: IRequestHandler<ChangeStatusCommand, ResultModel>
{
    private readonly ShopContext _context;
    
    private IMapper _mapper;

    public ChangeStatusCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResultModel> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
    {
        var check_user = await _context.users.FirstOrDefaultAsync(user => user.UserId == command._userId);

        if (check_user != null)
        {
            var car_check = await _context.cars.FirstOrDefaultAsync(car => car.CarId == command._carId);

            if (car_check != null)
            {
                car_check.Free = Free.No;

                await _context.SaveChangesAsync();

                return new ResultModel
                {
                    Message = "Successful!"
                };
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null; 
        }
    }
}