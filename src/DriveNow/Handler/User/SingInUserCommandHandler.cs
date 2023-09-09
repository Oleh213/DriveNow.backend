using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace DriveNow.Handlier
{
    public class SingInUserCommandHandler : IRequestHandler<SingInCommand, string>
    {
        public ShopContext _context;

        private readonly IOptions<AuthOptions> options;

        public readonly AppSettings _applicationSettings;

        private readonly IMapper _mapper;

        public SingInUserCommandHandler(ShopContext context, IOptions<AuthOptions> options, IOptions<AppSettings> _applicationSettings, IMapper mapper)
        {
            _context = context;
            this.options = options;
            this._applicationSettings = _applicationSettings.Value;
            _mapper = mapper;
        }
        public async Task<string> Handle(SingInCommand command, CancellationToken cancellationToken)
        {
            var Email = await _context.users.FirstOrDefaultAsync(Email =>
                Email.Email == command.SingInInputModel.EmailOrNumber);
            var Number =
                await _context.users.FirstOrDefaultAsync(Number => Number.Number == command.SingInInputModel.EmailOrNumber);
            if (Email != null)
            {
                var user = _mapper.Map<User, UserModel>(Email);

                if (user != null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.SingInInputModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    if (user.Email == command.SingInInputModel.EmailOrNumber && user.Password == hashedPassword)
                    {
                        var token = GenerateToken(user);

                        return (token);
                    }
                }
            }

            else if (Number != null)
            {
                var user = _mapper.Map<User, UserModel>(Number);

                if (user != null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.SingInInputModel.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    if (user.Number == command.SingInInputModel.EmailOrNumber && user.Password == hashedPassword)
                    {
                        var token = GenerateToken(user);

                        return (token) ;
                    }
                }
            }
            return null;
        }

        public string GenerateToken(UserModel user)
        {

            var authParams = options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Name, user.SecondName),
                new Claim (JwtRegisteredClaimNames.Sub, user.UserId.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

