using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler;

public class LiqPayCallbackCommandHandler: IRequestHandler<LiqPayCallbackCommand, string>
{
    public ShopContext _context { get; set; }
    
    public string publicKey = "sandbox_i21688834201";
                                                 
    public string privateKey = "sandbox_SQ8Wu9QY1XfXmaqmy4wu1TpL1qC4WTu0KQ83DhD7";

    public LiqPayCallbackCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(LiqPayCallbackCommand command, CancellationToken cancellationToken)
    {
        if (IsValidSignature(command._Model.Data, command._Model.Signature))
        {
            var trip_check = await _context.trips.FirstOrDefaultAsync(user => user.UserId == command._UserId);

            trip_check.Status = !trip_check.Status;

            await _context.SaveChangesAsync();

            return "Okey";
        }
        else
        {
            return null;
        }
    }
    private bool IsValidSignature(string data, string signature)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            var computedSignatureBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(publicKey + data + privateKey));
            var computedSignature = Convert.ToBase64String(computedSignatureBytes);
            return computedSignature == signature;
        }
    }
    static string GenerateLiqPaySignature(string privateKey, string data)
    {
        // Concatenate the private key, data, and private key again
        string concatenatedData = $"{privateKey}{data}{privateKey}";

        // Compute the SHA1 hash of the concatenated data
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(concatenatedData));

            // Convert the hash to lowercase hexadecimal
            StringBuilder signatureBuilder = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                signatureBuilder.Append(b.ToString("x2"));
            }

            return signatureBuilder.ToString();
        }
    }
}