using System;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInWithGoogleCommand: IRequest<ResultUserSingInCommand>
	{
        public string Credential { get; set; }
    }
}

