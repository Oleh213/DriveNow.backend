using System;
using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserModel>()
				.ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName));
		}
	}
}

