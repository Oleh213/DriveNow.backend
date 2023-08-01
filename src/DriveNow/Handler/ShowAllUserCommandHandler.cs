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
    public class ShowAllUserCommandHandler : IRequestHandler<ShowAllUserCommand, List<User>>
	{
		private ShopContext _context;
        private IMapper _mapper;

        public ShowAllUserCommandHandler(ShopContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<User>> Handle(ShowAllUserCommand command, CancellationToken cancellationToken)
        {
			var user_ckeack = await _context.users.FirstOrDefaultAsync(x => x.UserId == command._model.UserId);

			if (user_ckeack != null)
			{
				var user = _mapper.Map<User,UserModel>(user_ckeack);

				if(user.Role == Enums.Role.Admin)
				{
					List<User> user_list = await _context.users.ToListAsync() ;

					return user_list;
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

