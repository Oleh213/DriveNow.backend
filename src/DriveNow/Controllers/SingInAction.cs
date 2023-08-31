using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("SingIn")]
	public class SingInAction: ControllerBase
	{
		public ShopContext _context;

		private readonly IOptions<AuthOptions> options;
		private readonly ILogger<StripeException> _logger;

		
		public SingInAction(ShopContext context, IOptions<AuthOptions> options, ILogger<StripeException> logger)
		{
			_context = context;
			this.options = options;
			_logger = logger;
		}

		[HttpPost("SingInUser")]
		public async Task<IActionResult> SingIn(SingInModel singInModel){

			if (singInModel.Email != null)
			{

				var user = await _context.users.FirstOrDefaultAsync(x => x.Email == singInModel.Email);

				if (user != null)
				{


					var sha = SHA256.Create();

					var asByteArray = Encoding.Default.GetBytes(singInModel.Password);

					var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

					if (user.Email == singInModel.Email && user.Password == hashedPassword)
					{
						var token = GenerateToken(user);

						return Ok(new {
							access_token = token
						}) ;
					}
				}
				else {
					return BadRequest("Bad!");
				}
			}

			else if (singInModel.Number != null)
			{

				var user = await _context.users.FirstOrDefaultAsync(x => x.Number == singInModel.Number);

				if (user != null)
				{
					var sha = SHA256.Create();

					var asByteArray = Encoding.Default.GetBytes(singInModel.Password);

					var hashedPassword = Convert.ToBase64String(sha.ComputeHash(asByteArray));

					if (user.Number == singInModel.Number && user.Password == hashedPassword)
					{

                        var token = GenerateToken(user);

                        return Ok(new
                        {
                            access_token = token
                        });
                    }
				}
				else {
					return BadRequest("Bad!");
				}
			}
			return Ok("Finished!");
		}
		[HttpPost("Token")]
		public string GenerateToken(User user) {

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

		[HttpGet("ShowAllUsers")]
		public async Task<IActionResult> ShowAllUsers() {

			return Ok(_context.users);
		}

		[HttpPost("SingInWithGoogle")]

		public async Task<IActionResult> SingInGoogle(GoogleSingInModel googleSingInModel) {

			var user = await _context.users.FirstOrDefaultAsync(x => x.Email == googleSingInModel.Email);

			if (user != null) {

				var token = GenerateToken(user);

				return Ok(new
				{

					access_token = token
				});
			}

			else if (user == null) {

                _context.users.Add(new User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = googleSingInModel.FirstName,
                    SecondName = googleSingInModel.SecondName,
                    Email = googleSingInModel.Email
                });

				await _context.SaveChangesAsync();

				return Ok("Successful!");
            }
			return BadRequest("Finished");
		}
		
		const string endpointSecret = "whsec_436a4f330179fd38f5d7b6d374499a12eea08afc1cfc1c881407f4a4a4040791";

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
	            var stripeEvent = EventUtility.ConstructEvent(json,
		            Request.Headers["Stripe-Signature"], endpointSecret);

	            // Handle the event
	            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
	            {
		            Console.WriteLine(stripeEvent);

		            return Ok();
	            }
	            // ... handle other event types
	            else
	            {
		            Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);

		            return NoContent();
	            }
            }
            catch (StripeException e)
            {
	            _logger.LogInformation("Error: " + e);
                return Unauthorized();
            }
        }
		
	}
}
