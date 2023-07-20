using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
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
	public class SingInWithGoogleCommandHandler: IRequestHandler<SingInWithGoogleCommand, string>
	{
        public ShopContext _context;

        private readonly IOptions<AuthOptions> options;

        public readonly AppSettings _applicationSettings;

        private readonly IMapper _mapper;

        public SingInWithGoogleCommandHandler(ShopContext context, IOptions<AuthOptions> options, IOptions<AppSettings> _applicationSettings, IMapper mapper)
		{
            _context = context;
            this.options = options;
            this._applicationSettings = _applicationSettings.Value;
            _mapper = mapper;
        }

        public async Task<string> Handle(SingInWithGoogleCommand command, CancellationToken cancellationToken) {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { this._applicationSettings.GoogleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(command.SingInModel.Credential, settings);

            var user_entity = await _context.users.Where(x => x.Email == payload.Email).FirstOrDefaultAsync();

            var user = _mapper.Map<User, UserModel>(user_entity);

            if (user != null)
            {

                var token = GenerateToken(user);

                return (token);
            }

            else if (user == null)
            {

                var new_user = new User
                {

                    UserId = Guid.NewGuid(),
                    FirstName = payload.FamilyName,
                    SecondName = payload.Name,
                    Email = payload.Email,
                    Role = Enums.Role.User
                };

                _context.users.Add(new_user);

                await _context.SaveChangesAsync();

                var user_cheack = _mapper.Map<User, UserModel>(new_user);

                var token = GenerateToken(user_cheack);

                return (token);
            }

            return ("");
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

