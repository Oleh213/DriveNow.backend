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
	[Route("UserController")]
	public class UserAction: ControllerBase
	{
        private IMediator _mediator;

        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");

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
		public async Task<List<UserModel>> ShowAllUsers(CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowAllUserCommand(new ShowForAdminModel { UserId = UserId }), cancellationToken);
		}

		[HttpPost("CheckDocument")]

		public async Task<string> CheckDocument(CheckDocumentInputModel inputModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new CheckDocumentCommand(new CheckDocumentInputModel{AdminId = UserId,Status = inputModel.Status,UserId = inputModel.UserId}),cancellationToken);
		}

		[HttpPost("ChangePassword")]

		public async Task<string> ChangePassword(ChangePasswordInputModel inputModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ChangePasswordCommand(UserId = UserId, inputModel = inputModel));
		}
	}
}