using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class PromocodeCommand: IRequest<string>
	{
        public PromocodeModel _model;

		public Guid UserId { get; set; }

		public PromocodeCommand(PromocodeModel model, Guid Id)
		{
			_model = model;
			UserId = Id;
		}
	}
}

