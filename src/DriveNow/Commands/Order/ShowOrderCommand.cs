using DriveNow.DTO;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class ShowOrderCommand: IRequest<List<OrderModel>>
{
    public Guid _userId { get; set; }

    public ShowOrderCommand(Guid userId)
    {
        _userId = userId;
    }
}