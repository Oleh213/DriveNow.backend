using System;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handler
{
	public class ShowAllCarsCommandHandler: IRequestHandler<ShowAllCarsCommand, List<CarModel>>
	{
        private ShopContext _context;
        private IMapper _mapper;

        public ShowAllCarsCommandHandler(ShopContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<CarModel>> Handle(ShowAllCarsCommand command, CancellationToken cancellationToken)
		{
			var user_ckeck = await _context.users.FirstOrDefaultAsync(Userid => Userid.UserId == command.UserId);

			if (user_ckeck != null)
			{
				var cars = await _context.cars.ToListAsync();
				
				var result = cars.Select(car => _mapper.Map<CarModel>(car)).ToList();

				return result;
			}
			else
			{
				return null;
			}
		}
	}
}

