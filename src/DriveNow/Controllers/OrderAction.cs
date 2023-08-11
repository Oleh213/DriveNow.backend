using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers;
[ApiController]
[Route("OrderAction")]
public class OrderAction: ControllerBase
{
    private readonly IMediator _mediator;

    public OrderAction(IMediator mediator)
    {
        _mediator = mediator;
    }
    //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    
    public Guid UserId = Guid.Parse("41122ab2-ddcd-4dc1-8860-0492520a58e4");

    [HttpPost("CreateOrder")]

    public async Task<ResultModel> CreateOrder(CreateOrderModel model, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateOrderCommand(UserId, model), cancellationToken);
    }
}