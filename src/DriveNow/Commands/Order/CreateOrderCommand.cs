using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class CreateOrderCommand: IRequest<ResultModel>
{
    public Guid _UserId;

    public NewOrderInputModel InputModel;

    public CreateOrderCommand(Guid UserId, NewOrderInputModel inputModel)
    {
        _UserId = UserId;
        InputModel = inputModel;
    }
}