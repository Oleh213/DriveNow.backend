using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class RegistrationCommand: IRequest<string>
    {
		public NewUserInputModel NewUserInputModel { get; set; }

		public RegistrationCommand(NewUserInputModel newUserInputModel)
		{
            NewUserInputModel = newUserInputModel;
		}
	}
}

