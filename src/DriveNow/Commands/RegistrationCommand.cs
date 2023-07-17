using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class RegistrationCommand: IRequest<string>
    {
		public RegistrationModel _registrationModel { get; set; }

		public RegistrationCommand(RegistrationModel registrationModel)
		{
            _registrationModel = registrationModel;
		}
	}
}

