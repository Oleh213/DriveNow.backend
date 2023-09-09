using System;
using DriveNow.Enums;

namespace DriveNow.Model
{
	public class UserModel
	{
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string? Number { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public DateTime? Birthday { get; set; }

        public SexEnum? Sex { get; set; }

        public LanguageEnum? Language { get; set; }

        public Role Role { get; set; }
        
        public string? DocumentUrl { get; set; }

        public DocumentStatus? Status { get; set; }
    }
}

