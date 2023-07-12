using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
    public class SingInUserCommandHandler : IRequestHandler<SingInCommand, ResultUserSingInCommand>
    {
        public ShopContext _context;

        private readonly IOptions<AuthOptions> options;

        public readonly AppSettings _applicationSettings;

        public SingInUserCommandHandler(ShopContext context, IOptions<AuthOptions> options, IOptions<AppSettings> _applicationSettings)
        {
            _context = context;
            this.options = options;
            this._applicationSettings = _applicationSettings.Value;
        }

        public async Task<ResultUserSingInCommand> Handle(SingInCommand command, CancellationToken cancellationToken) {

            var result = new ResultUserSingInCommand();

            if (command.Email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(x => x.Email == command.Email);

                if (user != null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    if (user.Email == command.Email && user.Password == hashedPassword)
                    {
                        var token = GenerateToken(user);

                        result.Message = "Successful!";

                        result.Success = true;

                        result.Token = token;
                    }
                    else
                    {
                        result.Message = "Bad!";

                        result.Success = false;
                    }
                }
                else
                {
                    result.Message = "Bad!";
                    result.Success = false;
                }
            }

            else if (command.Number != null)
            {

                var user = await _context.users.FirstOrDefaultAsync(x => x.Number == command.Number);

                if (user != null)
                {
                    var sha = SHA256.Create();

                    var asByteArray = Encoding.Default.GetBytes(command.Password);

                    var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

                    if (user.Number == command.Number && user.Password == hashedPassword)
                    {

                        var token = GenerateToken(user);

                        result.Message = "Successful!";

                        result.Success = true;

                        result.Token = token;
                    }
                    else
                    {
                        result.Message = "Bad!";

                        result.Success = false;
                    }
                }
                else
                {
                    result.Message = "Bad!";

                    result.Success = false;
                }
            }
            return result;

        }

        public string GenerateToken(User user)
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

