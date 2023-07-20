using System;
using System.Security.Cryptography;
using System.Text;
using DriveNow.Commands;
using DriveNow.Context;
using DriveNow.DBContext;
using DriveNow.Model;
using DriveNow.Services;
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
        private IConfiguration _config;
        private IStorageService _storageService;

        public RegistrationAction(ShopContext context, IMediator mediator, IConfiguration config, IStorageService storageService)
		{
			_context = context;
			_mediator = mediator;
            _config = config;
            _storageService = storageService;

        }

		[HttpPost("Registration")]
		public async Task<string> Registration(RegistrationModel registrationModel,CancellationToken cancellationToken) {

			return await _mediator.Send(new RegistrationCommand(registrationModel), cancellationToken);
        }
    }			
}