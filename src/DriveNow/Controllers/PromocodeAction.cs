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
	[Route("PromocodeAction")]
	public class PromocodeAction: ControllerBase
	{
        private IMediator _mediator;

        public PromocodeAction(IMediator mediator)
		{
			_mediator = mediator;
		}

        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("38d5f673-5834-4c0b-bc21-5714a4a1fe27");

        [HttpPost("AddPromocode")]
		public async Task<string> AddPromocode(PromocodeModel model, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new PromocodeCommand(model, UserId), cancellationToken);
		}

		[HttpPost("ShowAllPromocode")]
		public async Task<List<Promocode>> ShowAllPromocode(ShowForAdminModel model, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowPromocodesCommand(model), cancellationToken);
		}
	}
}

