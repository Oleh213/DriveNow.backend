using AutoMapper;
using DriveNow.Commands.Trip;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers;
[ApiController]
[Route("TripController")]
public class TripAction
{
    //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    
    public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");
    private readonly IMediator _mediator;

    public TripAction(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("StartTrip")]
    public async Task<string> StartTrip(StartTripModel model, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new StartTripCommand(model,UserId), cancellationToken);
    }

    [HttpGet("CheckTrip")]
    public async Task<TripModel> CheckTrip(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ShowUserTripCommand(UserId), cancellationToken);
    }
}