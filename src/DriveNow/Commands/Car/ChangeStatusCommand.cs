using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class ChangeStatusCommand:IRequest<ResultModel>
{
    public Guid _userId;

    public Guid _carId;

    public ChangeStatusCommand(Guid UserId,Guid CarId)
    {
        _userId = UserId;
        _carId = CarId;
    }
}