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
    public class ShowUserCommandHandler : IRequestHandler<ShowUserCommand, UserModel>
	{
        private ShopContext _context;

        private IMapper _mapper;

        public ShowUserCommandHandler(ShopContext context, IMapper mapper)
		{
			_context = context;

			_mapper = mapper;
		}

		public async Task<UserModel> Handle(ShowUserCommand command, CancellationToken cancellationToken)
		{
			var user_check = await _context.users.FirstOrDefaultAsync(x => x.UserId == command._model.UserId);

			if(user_check != null)
			{
				UserModel user = _mapper.Map<User, UserModel>(user_check);

				return (user);
			}
			else
			{
				return null;
			}
		}
	}
}

