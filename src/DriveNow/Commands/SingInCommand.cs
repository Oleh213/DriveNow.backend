using System;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInCommand : IRequest<ResultUserSingInCommand>
	{
        public string Number { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

