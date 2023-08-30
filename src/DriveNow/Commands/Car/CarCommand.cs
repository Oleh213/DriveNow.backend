using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class CarCommand: IRequest<string>
	{
        public NewCarInputModel NewCarInputModel { get; set; }

        public Guid _userId;

        public CarCommand(NewCarInputModel newCarInputModel,Guid UserId)
		{
			NewCarInputModel = newCarInputModel;
			_userId = UserId;
		}
	}
}

