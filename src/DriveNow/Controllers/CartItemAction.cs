using System.Security.Claims;
using DriveNow.Commands;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers;
[ApiController]
[Route("CartController")]
public class CartItemAction: ControllerBase
{
    private readonly IMediator _mediator;
    //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
    
    public Guid UserId = Guid.Parse("41122ab2-ddcd-4dc1-8860-0492520a58e4");

    public CartItemAction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddCartItem")]
    public async Task<ResultModel> AddCartItem([FromBody]AddCartItemModel model, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AddCartItemCommand(UserId, model), cancellationToken);
    }
}