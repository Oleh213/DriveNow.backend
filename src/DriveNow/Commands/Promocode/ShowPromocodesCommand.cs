using System;
using DriveNow.Context;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class ShowPromocodesCommand: IRequest<List<PromocodeModel>>
	{
        public Guid _userId;

        public ShowPromocodesCommand(Guid userId)
		{
            _userId = userId;
		}
	}
}

