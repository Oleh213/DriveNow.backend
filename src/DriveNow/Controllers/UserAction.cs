using System;
using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("UserAction")]
	public class UserAction: ControllerBase
	{
        private IMediator _mediator;

        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("38d5f673-5834-4c0b-bc21-5714a4a1fe27");

        public UserAction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("ShowInformationAboutUser")]
		public async Task<UserModel> ShowInformationAboutUser(CancellationToken cancellationToken)
		{
			return await _mediator.Send (new ShowUserCommand(new ShowForAdminModel { UserId = UserId }), cancellationToken);
		}

		[HttpGet("ShowAllUsers")]
		public async Task<List<User>> ShowAllUsers(CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowAllUserCommand(new ShowForAdminModel { UserId = UserId }), cancellationToken);
		}
    }
}