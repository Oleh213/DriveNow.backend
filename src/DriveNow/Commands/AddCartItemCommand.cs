using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class AddCartItemCommand: IRequest<ResultModel>
{
    public Guid _UserId;

    public AddCartItemModel _CartItemModel;

    public AddCartItemCommand(Guid UserId, AddCartItemModel model)
    {
        _UserId = UserId;
        _CartItemModel = model;
    }
}