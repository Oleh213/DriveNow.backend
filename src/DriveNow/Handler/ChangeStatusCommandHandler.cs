using DriveNow.Commands;
using DriveNow.DBContext;
using DriveNow.Enums;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ChangeStatusCommandHandler: IRequestHandler<ChangeStatusCommand, ResultModel>
{
    private readonly ShopContext _context;

    public ChangeStatusCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultModel> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
    {
        var check_user = await _context.users.FirstOrDefaultAsync(UserId => UserId.UserId == command._userId);

        if (check_user != null)
        {
            var car = await _context.cars.FirstOrDefaultAsync(CarId => CarId.CarId == command._carId);

            if (car != null)
            {
                car.Free = Enums.Free.No;

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