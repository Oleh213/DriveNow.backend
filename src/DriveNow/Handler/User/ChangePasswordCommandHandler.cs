using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class ChangePasswordCommandHandler: IRequestHandler<ChangePasswordCommand,string>
{
    private ShopContext _context;

    public ChangePasswordCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user_check = await _context.users.FirstOrDefaultAsync(user => user.UserId == command._userId);

        if (user_check != null)
        {
            var sha = SHA256.Create();

            var asByteArray = Encoding.Default.GetBytes(command.InputModel.Password);

            var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

            if (user_check.Password == hashedPassword)
            {
                var asByteArray2 = Encoding.Default.GetBytes(command.InputModel.NewPassword);
                
                var hashedPassword2 = Convert.ToBase64String(sha.ComputeHash(asByteArray2));

                user_check.Password = hashedPassword2;

                await _context.SaveChangesAsync();

                return "Successful";
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