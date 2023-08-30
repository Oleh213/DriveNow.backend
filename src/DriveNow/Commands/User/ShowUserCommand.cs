using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowUserCommand: IRequest<UserModel>
	{
        public ShowForAdminModel _model;

        public ShowUserCommand(ShowForAdminModel model)
		{
            _model = model;
		}
	}
}

