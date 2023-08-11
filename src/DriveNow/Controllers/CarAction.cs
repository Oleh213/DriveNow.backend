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
	[Route("CarAction")]
	public class CarAction: ControllerBase
	{
        private IMediator _mediator;

        public CarAction(IMediator mediator)
		{
			_mediator = mediator;
		}
        //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public Guid UserId = Guid.Parse("41122ab2-ddcd-4dc1-8860-0492520a58e4");

        [HttpPost("AddCar")]
		public async Task<string> AddCar([FromForm] CarModel model, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new CarCommand(model,UserId), cancellationToken);
		}

		[HttpGet("ShowAllCars")]
		public async Task<List<Car>> ShowAllCars (CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ShowAllCarsCommand(UserId), cancellationToken);
        }

		[HttpGet("ShowCarsForMap")]
		public async Task<List<ShowCarForMapDTO>> ShowCarsForMap(CancellationToken cancellationToken)
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