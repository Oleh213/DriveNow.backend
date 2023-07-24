using System;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowAllCarsCommand: IRequest<List<Car>>
	{
        public ShowForAdminModel _model;

        public ShowAllCarsCommand(ShowForAdminModel model)
		{
			_model = model;
		}
	}
}

