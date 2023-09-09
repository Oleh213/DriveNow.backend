using System.Reflection.Metadata;
using DriveNow.DTO;
using DriveNow.Enums;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands;

public class CheckDocumentCommand : IRequest<string>
{
    public CheckDocumentInputModel DocumentInputModel { get; set; }

    public CheckDocumentCommand(CheckDocumentInputModel documentInputModel)
    {
        DocumentInputModel = documentInputModel;
    }
}