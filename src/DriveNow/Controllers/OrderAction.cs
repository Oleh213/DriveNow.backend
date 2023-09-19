using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DriveNow.Commands;
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

    private readonly string publicKey = "sandbox_i21688834201";

    private readonly string privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";
    
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
    public async Task<IActionResult> CreatePayment()
    {
        var publicKey = "sandbox_i21688834201";
        var privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";

        var invoiceRequest = new LiqPayRequest
        {
            Email = "email6@example.com",
            Amount = 2000,
            Currency = "UAH",
            OrderId = Id.ToString(),
            Action = LiqPayRequestAction.InvoiceSend,
            Language = LiqPayRequestLanguage.EN,
            Description = "Car Number 213",
            Version = 3,
            PublicKey = publicKey,
            ResultUrl = "http://localhost:4200/map-car",
            ServerUrl = "https://drive-now-backend.azurewebsites.net/api/payment/callback"
        };

        var liqPayClient = new LiqPayClient(publicKey, privateKey);

        var response = await liqPayClient.RequestAsync("request", invoiceRequest);

        return Ok(new { Response = response });
    }

    [HttpPost("callback")]

    public async Task<IActionResult> Callback([FromForm] string data, [FromForm] string signature,CancellationToken cancellationToken)
    {
        if (signature == CalculateSignature(data))
        {
            string api_url = "https://www.liqpay.ua/api/request";
            string json = $@"
        {{
            ""action"": ""status"",
            ""version"": 3,
            ""public_key"": ""{publicKey}"",
            ""order_id"": ""{Id.ToString()}""
        }}";


            // Encode the JSON data as base64
            byte[] dataBytes = Encoding.UTF8.GetBytes(json);
            string DATA = Convert.ToBase64String(dataBytes);

            // Calculate the signature
            string dataToSign = privateKey + DATA + privateKey;
            byte[] signatureBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            string SIGNATURE = Convert.ToBase64String(signatureBytes);
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("data", data),
                    new KeyValuePair<string, string>("signature", signature)
                });

                HttpResponseMessage response = await client.PostAsync(api_url, content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    
                    var rider = await _context.trips.Where(user => user.UserId == UserId).ToListAsync();

                    foreach (var trip in rider)
                    {
                        trip.Status = !trip.Status;
                    }


                    await _context.SaveChangesAsync();

                    return Ok("Okey");
                }
                else
                {
                    return BadRequest("Error");
                }
            }
        }

        return BadRequest("Error");
    } 
    private string CalculateSignature(string data)
        {
            using (var sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(privateKey)))
            {
                var dataBytes = Encoding.UTF8.GetBytes(privateKey + data + privateKey);
                var hashBytes = sha1.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }