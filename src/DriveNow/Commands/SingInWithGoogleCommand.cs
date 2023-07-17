using System;
using DriveNow.Model;
using MediatR;

namespace DriveNow.Commands
{
	public class SingInWithGoogleCommand: IRequest<string>
	{
        public GoogleSingInModel SingInModel { get; set; }

        public SingInWithGoogleCommand(GoogleSingInModel googleSingInModel) {

            SingInModel = googleSingInModel;
        }
    }
}

