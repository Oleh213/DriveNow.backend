using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInWithGoogleCommand: IRequest<string>
	{
        public GoogleSingInInputModel SingInInputModel { get; set; }

        public SingInWithGoogleCommand(GoogleSingInInputModel googleSingInInputModel) {

            SingInInputModel = googleSingInInputModel;
        }
    }
}

