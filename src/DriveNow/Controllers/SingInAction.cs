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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Stripe;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("SingIn")]
	public class SingInAction: ControllerBase
	{
		public ShopContext _context;

		private readonly IOptions<AuthOptions> options;
		private readonly ILogger<SingInAction> _logger;

		
		public SingInAction(ShopContext context, IOptions<AuthOptions> options, ILogger<SingInAction> logger)
		{
			_context = context;
			this.options = options;
			_logger = logger;
		}
		private readonly IMediator _mediator;
		 
		public SingInAction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("SingInUser")]
		public async Task<string> SingIn(SingInInputModel singInInputModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new SingInCommand(singInInputModel),cancellationToken);

		}
		/*
		[HttpGet("ShowAllUsers")]
		public async Task<IActionResult> ShowAllUsers() {

			return Ok(_context.users);
		}
		*/
		[HttpPost("SingInWithGoogle")]
		public async Task<string> SingInGoogle([FromBody] GoogleSingInInputModel test, CancellationToken cancellationToken) {

			return await _mediator.Send(new SingInWithGoogleCommand(test),cancellationToken);
		}
	}
}
