using System;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowPromocodesCommand: IRequest<List<Promocode>>
	{
        public ShowForAdminModel _model;

        public ShowPromocodesCommand(ShowForAdminModel model)
		{
            _model = model;
		}
	}
}

