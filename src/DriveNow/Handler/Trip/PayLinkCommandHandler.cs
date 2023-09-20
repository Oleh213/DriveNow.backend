using DriveNow.Commands.Trip;
using DriveNow.Context;
using DriveNow.DBContext;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler.Trip;

public class PayLinkCommandHandler: IRequestHandler<PayLinkCommand, string>
{
    public ShopContext _context { get; set; }
    
    private readonly string publicKey = "sandbox_i21688834201";

    private readonly string privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";

    public PayLinkCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(PayLinkCommand command, CancellationToken cancellationToken)
    {
        Context.Trip trip_user = await _context.trips.FirstOrDefaultAsync(user => user.UserId == command._userId);

        User user_check = await _context.users.FirstOrDefaultAsync(user => user.UserId == command._userId);

        if (trip_user != null)
        {
            Car car_info = await _context.cars.FirstOrDefaultAsync(car => car.CarId == command._model.CarId);
            
            var invoiceRequest = new LiqPayRequest
        {
            Email = user_check.Email ?? user_check.Number,
            Amount = car_info.Price,
            Currency = "UAH",
            OrderId = trip_user.TripId.ToString(),
            Action = LiqPayRequestAction.InvoiceSend,
            Language = LiqPayRequestLanguage.EN,
            Description = car_info.About,
            Version = 3,
            PublicKey = publicKey,
            ResultUrl = "http://localhost:4200/map-car",
            ServerUrl = "https://drive-now-backend.azurewebsites.net/api/payment/callback"
        };

        var liqPayClient = new LiqPayClient(publicKey, privateKey);

        var response = await liqPayClient.RequestAsync("request", invoiceRequest);

        return (response.Href);
        }
        else
        {
            return null;
        }
    }
}