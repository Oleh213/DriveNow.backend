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
	[Route("PromocodeController")]
	public class PromocodeAction: ControllerBase
	{
        private IMediator _mediator;

        public PromocodeAction(IMediator mediator)
		{
			_mediator = mediator;
		}

        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");

        [HttpPost("AddPromocode")]
		public async Task<string> AddPromocode(NewPromocodeInputModel inputModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new PromocodeCommand(inputModel, UserId), cancellationToken);
		}

		[HttpGet("ShowAllPromocode")]
		public async Task<List<PromocodeModel>> ShowAllPromocode(CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowPromocodesCommand(UserId), cancellationToken);
		}
	}
}

