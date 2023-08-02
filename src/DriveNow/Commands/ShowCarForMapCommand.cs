using DriveNow.DTO;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class ShowCarForMapCommand: IRequest<List<ShowCarForMapDTO>>
{

    public Guid _userId;

    public ShowCarForMapCommand(Guid UserId)
    {
        _userId = UserId;
    }
}