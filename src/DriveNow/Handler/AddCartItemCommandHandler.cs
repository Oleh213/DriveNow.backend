using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class AddCartItemCommandHandler: IRequestHandler<AddCartItemCommand, ResultModel>
{
    private readonly ShopContext _context;

    public AddCartItemCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultModel> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var check_user = await _context.users.FirstOrDefaultAsync(UserId => UserId.UserId == command._UserId);

        if (check_user != null)
        {
            _context.cartItems.Add(new CartItem
            {
                UserId = command._UserId,
                CartItemId = new Guid(),
                Count = 1,
                Price = command._CartItemModel.Price,
                RoomId = Guid.Parse(command._CartItemModel.CarId),
            });

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