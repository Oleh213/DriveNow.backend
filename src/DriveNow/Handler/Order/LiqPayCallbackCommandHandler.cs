using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class LiqPayCallbackCommandHandler : ControllerBase, IRequestHandler<LiqPayCallbackCommand, IActionResult>
{
    public ShopContext _context { get; set; }
    
    public string publicKey = "sandbox_i21688834201";
                                                 
    public string privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";

    public LiqPayCallbackCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Handle(LiqPayCallbackCommand command, CancellationToken cancellationToken)
    {
        if (command._model.Signature == CalculateSignature(command._model.Data))
        {
            string api_url = "https://www.liqpay.ua/api/request";

            Context.Trip user_trip = await _context.trips.FirstOrDefaultAsync(trip => (trip.UserId == command._UserId)&&(trip.Status));
            
            string json = $@"
        {{
            ""action"": ""status"",
            ""version"": 3,
            ""public_key"": ""{publicKey}"",
            ""order_id"": ""{user_trip.TripId.ToString()}""
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
                    new KeyValuePair<string, string>("data", DATA),
                    new KeyValuePair<string, string>("signature", SIGNATURE)
                });

                HttpResponseMessage response = await client.PostAsync(api_url, content);

                if (response.IsSuccessStatusCode)
                {

                    Car car = await _context.cars.FirstOrDefaultAsync(car => car.CarId == user_trip.CarId);

                    user_trip.Status = !user_trip.Status;

                    if (car != null)
                    {
                        _context.orders.Add(new Order
                        {
                            UserId = command._UserId,
                            OrderId = user_trip.TripId,
                            TotalPrice = car.Price,
                            OrderTime = DateTimeOffset.Now,
                            Promocode = "",
                            CarId = car.CarId
                        });
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
    string CalculateSignature(string data)
    {
        // Concatenate private_key, data, and private_key again
        string concatenatedString = privateKey + data + privateKey;

        // Calculate SHA-1 hash
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(concatenatedString));

            // Convert the hash to base64
            string signature = Convert.ToBase64String(hashBytes);

            return signature;
        }
    }
}