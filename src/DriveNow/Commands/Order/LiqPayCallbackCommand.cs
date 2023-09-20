using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Commands;

public class LiqPayCallbackCommand: IRequest<IActionResult>
{
    
    public LiqPayModel _model { get; set; }
    public Guid _UserId { get; set; }

    public LiqPayCallbackCommand(string data, string signature, Guid UserId)
    {
        _model = new LiqPayModel();
        _model.Data = data;
        _model.Signature = signature;
        _UserId = UserId;
    }
}