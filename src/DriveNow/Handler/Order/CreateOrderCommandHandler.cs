using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, ResultModel>
{
    private readonly ShopContext _context;

    public CreateOrderCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultModel> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var check_user = await _context.users.FirstOrDefaultAsync(UserId => UserId.UserId == command._UserId);

        if (check_user != null)
        {
            var TotalPrice = 0;
            
            string promocodeName = "";

            var promocodeSum = 0;

            Guid OrderId = Guid.NewGuid();

            if (command.InputModel.Promocode != null)
            {
                var promocode_check = await _context.promocodes.FirstOrDefaultAsync(PromocodeName =>
                    PromocodeName.PromocodeName == command.InputModel.Promocode);

                if (promocode_check != null)
                {
                    promocodeName = promocode_check.PromocodeName;

                    promocodeSum = promocode_check.Sum;
                }
            }
            
            TotalPrice -= promocodeSum;

            _context.orders.Add(new Order
            {
                OrderId = OrderId,
                UserId = command._UserId,
                TotalPrice = TotalPrice,
                OrderTime = DateTime.Now.Date,
                Promocode = promocodeName,
                CarId = command.InputModel.CarId
            });
            var car = await _context.cars.FirstOrDefaultAsync(CarId => CarId.CarId == command.InputModel.CarId);

            if (car != null)
            {
                car.Free = Enums.Free.Yes;
                
            }

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
}