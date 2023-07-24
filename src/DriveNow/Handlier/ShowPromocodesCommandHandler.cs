using System;
using AutoMapper;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Handlier
{
	public class ShowPromocodesCommandHandler: IRequestHandler<ShowPromocodesCommand,List<Promocode>>
	{
        private ShopContext _context;
        private IMapper _mapper;

        public ShowPromocodesCommandHandler(ShopContext context, IMapper mapper)
		{
			_context = context;
			_mapper  = mapper;
		}

		public async Task<List<Promocode>> Handle(ShowPromocodesCommand command,CancellationToken cancellationToken)
		{
			var user_ckeck = await _context.users.FirstOrDefaultAsync(x => x.UserId == command._model.UserId);

			if(user_ckeck != null)
			{
				var user = _mapper.Map<User, UserModel>(user_ckeck);

				if(user.Role == Enums.Role.Admin)
				{
					List<Promocode> promocodes = await _context.promocodes.ToListAsync();

					return promocodes;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}
	}
}

