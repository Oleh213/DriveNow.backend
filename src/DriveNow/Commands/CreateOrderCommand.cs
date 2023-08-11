using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class CreateOrderCommand: IRequest<ResultModel>
{
    public Guid _UserId;

    public CreateOrderModel _model;

    public CreateOrderCommand(Guid UserId, CreateOrderModel model)
    {
        _UserId = UserId;
        _model = model;
    }
}