using System;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowAllCarsCommand: IRequest<List<CarModel>>
	{
        public Guid UserId;

        public ShowAllCarsCommand(Guid userId)
		{
			UserId = userId;
		}
	}
}

