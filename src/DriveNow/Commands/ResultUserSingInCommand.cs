using System;
namespace DriveNow.Commands
{
	public class ResultUserSingInCommand
	{
        public bool Success { get; set; }

        public string? Token { get; set; }

        public string Message { get; set; }
    }
}

