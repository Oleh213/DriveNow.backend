using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class PromocodeCommand: IRequest<string>
	{
        public NewPromocodeInputModel InputModel;

		public Guid UserId { get; set; }

		public PromocodeCommand(NewPromocodeInputModel inputModel, Guid Id)
		{
			InputModel = inputModel;
			UserId = Id;
		}
	}
}

