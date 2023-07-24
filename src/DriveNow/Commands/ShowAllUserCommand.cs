using System;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowAllUserCommand: IRequest<List<User>>
	{
        public ShowForAdminModel _model;

        public ShowAllUserCommand(ShowForAdminModel model)
		{
			_model = model;
		}
	}
}

