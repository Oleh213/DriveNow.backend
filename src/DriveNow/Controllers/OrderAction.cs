using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DriveNow.Commands;
using DriveNow.Commands.Trip;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.DTO;
using DriveNow.Model;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Controllers;

[ApiController]
[Route("api/payment")]
public class OrderAction : ControllerBase
{
    private readonly IMediator _mediator;
    
    public Guid Id = Guid.NewGuid();
    public OrderAction(IMediator mediator, ShopContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    //private Guid UserId => Guid.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

    public Guid UserId = Guid.Parse("9500269c-df16-48f7-a7cf-003cb79fd0ed");
    
    private ShopContext _context;

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

    [HttpPost("create-payment")]
    public async Task<string> CreatePayment(PayLinkInputModel model,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PayLinkCommand(model, UserId), cancellationToken);
    }
    
    [HttpPost("callback")]

    public async Task<IActionResult> Callback([FromForm] string data, [FromForm] string signature,CancellationToken cancellationToken)
    {
        return await _mediator.Send(new LiqPayCallbackCommand(data, signature, UserId), cancellationToken);
    }
}