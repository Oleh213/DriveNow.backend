using System;
using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("CarAction")]
	public class CarAction: ControllerBase
	{
        private IMediator _mediator;

        public CarAction(IMediator mediator)
		{
			_mediator = mediator;
		}
        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("38d5f673-5834-4c0b-bc21-5714a4a1fe27");

        [HttpPost("AddCar")]
		public async Task<string> AddCar([FromForm] CarModel model, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new CarCommand(model,UserId), cancellationToken);
		} 
    }
}