using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class ChangePasswordCommand: IRequest<string>
{
    public Guid _userId { get; set; }
    
    public ChangePasswordInputModel InputModel { get; set; }

    public ChangePasswordCommand(Guid UserId, ChangePasswordInputModel inputModel)
    {
        _userId = UserId;
        InputModel = inputModel;
    }
}