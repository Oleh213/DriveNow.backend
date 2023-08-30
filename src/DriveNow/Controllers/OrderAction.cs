using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DTO;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers;
[ApiController]
[Route("OrderController")]
public class OrderAction: ControllerBase
{
    private readonly IMediator _mediator;

    public OrderAction(IMediator mediator)
    {
        _mediator = mediator;
    }
    //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    
    public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");

    [HttpPost("CreateOrder")]

    public async Task<ResultModel> CreateOrder(NewOrderInputModel inputModel, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateOrderCommand(UserId, inputModel), cancellationToken);
    }

    [HttpGet("ShowUserOrders")]

    public async Task<List<OrderModel>> ShowUserOrders(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new ShowOrderCommand(UserId), cancellationToken);
    }
}