using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class CarCommand: IRequest<string>
	{
        public CarModel _carModel { get; set; }

        public Guid _userId;

        public CarCommand(CarModel carModel,Guid UserId)
		{
			_carModel = carModel;
			_userId = UserId;
		}
	}
}

