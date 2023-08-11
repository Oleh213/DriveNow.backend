using System;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler
{
	public class ShowAllCarsCommandHandler: IRequestHandler<ShowAllCarsCommand, List<Car>>
	{
        private ShopContext _context;

        public ShowAllCarsCommandHandler(ShopContext context)
		{
			_context = context;
		}

		public async Task<List<Car>> Handle(ShowAllCarsCommand command, CancellationToken cancellationToken)
		{
			var user_ckeck = await _context.users.FirstOrDefaultAsync(Userid => Userid.UserId == command.UserId);

			if (user_ckeck != null)
			{
				return _context.cars.ToList();
			}
			else
			{
				return null;
			}
		}
	}
}

