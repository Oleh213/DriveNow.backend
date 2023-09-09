using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInCommand : IRequest<string>
	{
        public SingInInputModel SingInInputModel { get; set; }

        public SingInCommand(SingInInputModel singnInputModel)
        {

            SingInInputModel = singnInputModel;

        }
    }
}

