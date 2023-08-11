using System;
using MediatR;

namespace DriveNow.Model
{
	public class SingInModel
	{
		public string EmailOrNumber { get; set; }

		public string Password { get; set; }
	}
}

