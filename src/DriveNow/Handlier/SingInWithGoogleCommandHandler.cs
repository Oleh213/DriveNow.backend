using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using Google.Apis.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace DriveNow.Handlier
{
	public class SingInWithGoogleCommandHandler: IRequestHandler<SingInWithGoogleCommand, ResultUserSingInCommand>
	{
        public ShopContext _context;

        private readonly IOptions<AuthOptions> options;

        public readonly AppSettings _applicationSettings;

        public SingInWithGoogleCommandHandler(ShopContext context, IOptions<AuthOptions> options, IOptions<AppSettings> _applicationSettings)
		{
            _context = context;
            this.options = options;
            this._applicationSettings = _applicationSettings.Value;
        }

        public async Task<ResultUserSingInCommand> Handle(SingInWithGoogleCommand command, CancellationToken cancellationToken) {

            var result = new ResultUserSingInCommand();

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { this._applicationSettings.GoogleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(command.Credential, settings);

            var user = await _context.users.Where(x => x.Email == payload.Email).FirstOrDefaultAsync();

            if (user != null)
            {

                var token = GenerateToken(user);

                result.Message = "Successful!";

                result.Token = token;

                result.Success = true;
            }

            else if (user == null)
            {

                var new_user = new User
                {

                    UserId = Guid.NewGuid(),
                    FirstName = payload.FamilyName,
                    SecondName = payload.Name,
                    Email = payload.Email
                };

                await _context.SaveChangesAsync();

                _context.users.Add(new_user);

                var token = GenerateToken(new_user);

                result.Message = "Successful!";

                result.Token = token;

                result.Success = true;

                
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

