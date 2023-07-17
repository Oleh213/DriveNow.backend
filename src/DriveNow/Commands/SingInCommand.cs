using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInCommand : IRequest<string>
	{
        public SingInModel SingInModel { get; set; }

        public SingInCommand(SingInModel singnModel)
        {

            SingInModel = singnModel;

        }
    }
}

