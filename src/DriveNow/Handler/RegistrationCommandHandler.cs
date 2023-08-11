using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler
{
	public class RegistrationCommandHandler: IRequestHandler<RegistrationCommand, string>
	{
        public ShopContext _context;

        private IMapper _mapper;

        public RegistrationCommandHandler( ShopContext context, IMapper mapper)
		{
			_context = context;
            _mapper = mapper;
		}

		public async Task<string> Handle(RegistrationCommand command, CancellationToken cancellationToken)
		{
            if (command._registrationModel.Number != null)
            {
                var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.Number == command._registrationModel.Number);

                var user = _mapper.Map<User, UserModel>(user_ckeck);

                if (user == null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command._registrationModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    _context.users.Add(new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = command._registrationModel.FirstName,
                        SecondName = command._registrationModel.SecondName,
                        Number = command._registrationModel.Number,
                        Password = hashedPassword,
                        Role = Enums.Role.User
                    });

                    await _context.SaveChangesAsync();

                    return ("Successful");

                }
            }

            else if (command._registrationModel.Email != null)
            {

                var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.Email == command._registrationModel.Email);

                var user = _mapper.Map<User, UserModel>(user_ckeck);

                if (user == null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command._registrationModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    _context.users.Add(new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = command._registrationModel.FirstName,
                        SecondName = command._registrationModel.SecondName,
                        Email = command._registrationModel.Email,
                        Password = hashedPassword,
                        Role = Enums.Role.User
                    });

                    await _context.SaveChangesAsync();

                    return ("Successful!");
                }              
            }

            return null;
        }
	}
}

