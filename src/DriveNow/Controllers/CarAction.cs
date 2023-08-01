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

		[HttpPost("ShowAllCars")]
		public async Task<List<Car>> ShowAllCars (ShowForAdminModel model, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new ShowAllCarsCommand(model), cancellationToken);
        }

		[HttpPost("ShowCarsForMap")]
		public async Task<List<ShowCarForMapDTO>> ShowCarsForMap(ShowForAdminModel model,
			CancellationToken cancellationToken)
		{
			return await _mediator.Send(new ShowCarForMapCommand(UserId), cancellationToken);
		}
    }
}