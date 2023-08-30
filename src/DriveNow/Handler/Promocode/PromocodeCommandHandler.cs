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
	public class PromocodeCommandHandler: IRequestHandler<PromocodeCommand, string>
	{
        private ShopContext _context;
        private IMapper _mapper;

        public PromocodeCommandHandler(ShopContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<string> Handle(PromocodeCommand command, CancellationToken cancellationToken)
		{
			var user_check = await _context.users.FirstOrDefaultAsync(x => x.UserId == command.UserId);

			if(user_check != null)
			{
				var user = _mapper.Map<User, UserModel>(user_check);

				if(user.Role == Enums.Role.Admin)
				{
					var promodome_name_check = await _context.promocodes.FirstOrDefaultAsync(x => x.PromocodeName == command.InputModel.PromocodeName);

					if(promodome_name_check == null)
					{
						_context.promocodes.Add(new Promocode
						{
							PromocodeName = command.InputModel.PromocodeName,
							PromocodeId = Guid.NewGuid(),
							Sum = command.InputModel.Sum
						});

						await _context.SaveChangesAsync();

						return ("Successful!");
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
			else
			{
				return null;
			}
		}
	}
}

