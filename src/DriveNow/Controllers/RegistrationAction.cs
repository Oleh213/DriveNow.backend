using System;
using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.Controllers
{
	[ApiController]
	[Route("RegistrationAction")]
	public class RegistrationAction : ControllerBase
	{

		public ShopContext _context;

        private IMediator _mediator;

        public RegistrationAction(ShopContext context, IMediator mediator)
		{
			_context = context;
			_mediator = mediator;
		}

		[HttpPost("Registration")]
		public async Task<string> Registration(RegistrationModel registrationModel,CancellationToken cancellationToken) {

			return await _mediator.Send(new RegistrationCommand(registrationModel), cancellationToken);
        }
	}			
}