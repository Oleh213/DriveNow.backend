using System;
using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, ShowUserOrderOutputModel>()
				.ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderId))
				.ForMember(dst => dst.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
				.ForMember(dst => dst.OrderTime, opt => opt.MapFrom(src => src.OrderTime))
				.ForMember(dst => dst.Promocode, opt => opt.MapFrom(src => src.Promocode))
				.ForMember(dst => dst.Car, opt => opt.MapFrom(src => src.Car));

		}
	}
}
