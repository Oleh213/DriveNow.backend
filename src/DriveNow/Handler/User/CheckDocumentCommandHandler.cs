using DriveNow.Commands;
using DriveNow.DBContext;
using DriveNow.DTO;
using DriveNow.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class CheckDocumentCommandHandler: IRequestHandler<CheckDocumentCommand, string>
{
    private ShopContext _context;

    public CheckDocumentCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CheckDocumentCommand command, CancellationToken cancellationToken)
    {
        var admin_check = await _context.users.FirstOrDefaultAsync(User_Id => User_Id.UserId == command.DocumentInputModel.AdminId);

        if (admin_check!= null && admin_check.Role == Role.Admin)
        {
            var user_ckeck =
                await _context.users.FirstOrDefaultAsync(UserId => UserId.UserId == command.DocumentInputModel.UserId);

            if (user_ckeck != null)
            {
                user_ckeck.Status = command.DocumentInputModel.Status;

                await _context.SaveChangesAsync();

                return "Successful!";

            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}