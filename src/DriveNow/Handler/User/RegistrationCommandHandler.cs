using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Enums;
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
            if (await _context.users.FirstOrDefaultAsync(Number => Number.Number == command.NewUserInputModel.EmailOrNumber) == null && IsPhoneNumber(command.NewUserInputModel.EmailOrNumber))
            {
                var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.Number == command.NewUserInputModel.EmailOrNumber);

                var user = _mapper.Map<User, UserModel>(user_ckeck);

                if (user == null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.NewUserInputModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    _context.users.Add(new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = command.NewUserInputModel.FirstName,
                        SecondName = command.NewUserInputModel.SecondName,
                        Number = command.NewUserInputModel.EmailOrNumber,
                        Password = hashedPassword,
                        Role = Enums.Role.User
                    });

                    await _context.SaveChangesAsync();

                    return ("Successful");

                }
            }

            else if (await _context.users.FirstOrDefaultAsync(Email => Email.Email == command.NewUserInputModel.EmailOrNumber)== null && IsEmail(command.NewUserInputModel.EmailOrNumber))
            {

                var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.Email == command.NewUserInputModel.EmailOrNumber);

                var user = _mapper.Map<User, UserModel>(user_ckeck);

                if (user == null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.NewUserInputModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    _context.users.Add(new User
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = command.NewUserInputModel.FirstName,
                        SecondName = command.NewUserInputModel.SecondName,
                        Email = command.NewUserInputModel.EmailOrNumber,
                        Password = hashedPassword,
                        Role = Enums.Role.User,
                        Status = DocumentStatus.Missing
                    });

                    await _context.SaveChangesAsync();

                    return ("Successful!");
                }              
            }

            return null;
        }
        public bool IsEmail(string data)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(data, emailPattern);
        }
        
        public bool IsPhoneNumber(string data)
        {
            string phonePattern = @"^\+?[\d\s-]+$";
            return Regex.IsMatch(data, phonePattern);
        }
	}
}

