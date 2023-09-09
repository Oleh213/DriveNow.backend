using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.EntityToModelsMappers;

public class OrderToOrderModel: Profile
{
    public OrderToOrderModel()
    {
        CreateMap<Order, OrderModel>()
            .ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dst => dst.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dst => dst.OrderTime, opt => opt.MapFrom(src => src.OrderTime))
            .ForMember(dst => dst.Promocode, opt => opt.MapFrom(src => src.Promocode))
            .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dst => dst.Car, opt => opt.MapFrom(src => src.Car));

    }

}