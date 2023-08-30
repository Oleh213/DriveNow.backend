using System;
using MediatR;

namespace DriveNow.Model
{
	public class SingInInputModel
	{
		public string EmailOrNumber { get; set; }

		public string Password { get; set; }
	}
}

