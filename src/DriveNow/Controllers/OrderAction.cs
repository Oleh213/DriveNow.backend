using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DTO;
using DriveNow.Model;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Controllers;

[ApiController]
[Route("api/payment")]
public class OrderAction : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly Dictionary<string, string> LiqPayCredentials = new Dictionary<string, string>
    {
        { "public_key", "sandbox_i21688834201" },
        { "private_key", "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7" }
    };

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

    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment()
    {
        var publicKey = "sandbox_i21688834201";
        var privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";

        var invoiceRequest = new LiqPayRequest
        {
            Email = "email6@example.com",
            Amount = 2000,
            Currency = "UAH",
            OrderId = Guid.NewGuid().ToString(),
            Action = LiqPayRequestAction.InvoiceSend,
            Language = LiqPayRequestLanguage.EN,
            Description = "Car Number 213",
            Version = 3,
            PublicKey = publicKey,
            ResultUrl = "http://localhost:4200/map-car",
            ServerUrl = "https://www.liqpay.ua/api/request"
        };

        var liqPayClient = new LiqPayClient(publicKey, privateKey);

        var response = await liqPayClient.RequestAsync("request", invoiceRequest);

        return Ok(new { Response = response });
    }

    [HttpPost("callback")]

    public async Task<string> Callback([FromBody]LiqPayModel model, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new LiqPayCallbackCommand(model,UserId), cancellationToken);
    }
}