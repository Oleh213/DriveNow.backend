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

            var cartItem =  _context.cartItems.Where(UserId => UserId.UserId == command._UserId);
            
            Guid OrderId = Guid.NewGuid();

            if (command._model.Promocode != null)
            {
                var promocode_check = await _context.promocodes.FirstOrDefaultAsync(PromocodeName =>
                    PromocodeName.PromocodeName == command._model.Promocode);

                if (promocode_check != null)
                {
                    promocodeName = promocode_check.PromocodeName;

                    promocodeSum = promocode_check.Sum;
                }
            }

            foreach (var cartItemCheck in cartItem)
            {
                _context.orderItems.Add(new OrderItem
                {
                    OrderId = OrderId,
                    RoomId = cartItemCheck.RoomId,
                    UserId = command._UserId,
                    OrderItemId = Guid.NewGuid(),
                    Count = 1,
                    Price = cartItemCheck.Price
                });
                
                TotalPrice += cartItemCheck.Price;
                
                _context.cartItems.Remove(cartItemCheck);
            }

            TotalPrice -= promocodeSum;

            _context.orders.Add(new Order
            {
                OrderId = OrderId,
                UserId = command._UserId,
                TotalPrice = TotalPrice,
                OrderTime = DateTime.Now.Date.ToString(),
                Promocode = promocodeName
            });
            var car = await _context.cars.FirstOrDefaultAsync(CarId => CarId.CarId.ToString() == command._model.CarId);

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