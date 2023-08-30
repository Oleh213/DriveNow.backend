using System;
using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DTO;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("CarController")]
	public class CarAction: ControllerBase
	{
        private IMediator _mediator;

        public CarAction(IMediator mediator)
		{
			_mediator = mediator;
		}
        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");

        [HttpPost("AddCar")]
		public async Task<string> AddCar([FromForm] NewCarInputModel inputModel, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new CarCommand(inputModel,UserId), cancellationToken);
		}

		[HttpGet("ShowAllCars")]
		public async Task<List<CarModel>> ShowAllCars (CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ShowAllCarsCommand(UserId), cancellationToken);
        }

		[HttpGet("ShowCarsForMap")]
		public async Task<List<CarMapModel>> ShowCarsForMap(CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowCarForMapCommand(UserId), cancellationToken);
		}

		[HttpPost("{carId}/changeStatusById")]

		public async Task<ResultModel> ChangeCarStatus([FromRoute] Guid carId, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ChangeStatusCommand(UserId, carId), cancellationToken);
		}
    }
}