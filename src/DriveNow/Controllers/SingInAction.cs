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
		public async Task<IActionResult> SingIn(SingInModel singInModel)
		{
			var command = new SingInCommand
            {
				Email = singInModel.Email,
				Password = singInModel.Password,
				Number = singInModel.Number
			};

			var result = await _mediator.Send(command);

			if (result.Success) {

				return Ok(new
				{
					access_token = result.Token
				});

			}

			return BadRequest(result.Message);

		}
		/*
		[HttpGet("ShowAllUsers")]
		public async Task<IActionResult> ShowAllUsers() {

			return Ok(_context.users);
		}
		*/
		[HttpPost("SingInWithGoogle")]
		public async Task<IActionResult> SingInGoogle([FromBody] CredentialModel test) {

			var command = new SingInWithGoogleCommand
			{

				Credential = test.Credential
			};

			var result = await _mediator.Send(command);

			if (result.Success) {

				return Ok(new
				{

					access_token = result.Token
				});
			}

			return BadRequest(result.Message);
		}
	}
}
