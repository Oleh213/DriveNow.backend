using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("SingIn")]
	public class SingInAction: ControllerBase
	{
		private readonly IMediator _mediator;
		 
		public SingInAction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("SingInUser")]
		public async Task<string> SingIn(SingInModel singInModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new SingInCommand(singInModel),cancellationToken);

		}
		/*
		[HttpGet("ShowAllUsers")]
		public async Task<IActionResult> ShowAllUsers() {

			return Ok(_context.users);
		}
		*/
		[HttpPost("SingInWithGoogle")]
		public async Task<string> SingInGoogle([FromBody] GoogleSingInModel test, CancellationToken cancellationToken) {

			return await _mediator.Send(new SingInWithGoogleCommand(test),cancellationToken);
		}
	}
}
