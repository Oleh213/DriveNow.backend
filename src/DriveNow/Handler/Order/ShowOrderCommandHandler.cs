using AutoMapper;
using DriveNow.Commands;
using DriveNow.DBContext;
using DriveNow.DTO;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ShowOrderCommandHandler : IRequestHandler<ShowOrderCommand, List<OrderModel>>
{
    private ShopContext _context;

    private readonly IMapper _mapper;


    public ShowOrderCommandHandler(ShopContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderModel>> Handle(ShowOrderCommand command,
        CancellationToken cancellationToken)
    {
        var check_user = await _context.users.SingleOrDefaultAsync(user => user.UserId == command._userId);

        if (check_user != null)
        {
            var userOrders = await _context.orders
                .Where(order => order.UserId == command._userId)
                .Include(order => order.Car)
                .ToListAsync();

            var result = userOrders.Select(order => _mapper.Map<OrderModel>(order)).ToList();

            return result;
        }
        else
        {
            return null;
        }
    }
}