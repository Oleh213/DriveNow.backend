using System;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handlier
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
			List<Car> cars = await _context.cars.ToListAsync();

			return cars;
		}
	}
}

