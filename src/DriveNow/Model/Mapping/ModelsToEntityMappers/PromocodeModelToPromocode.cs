using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.ModelsToEntityMappers;

public class PromocodeModelToPromocode: Profile
{
    public PromocodeModelToPromocode()
    {
        CreateMap<PromocodeModel, Promocode>()
            .ForMember(dst => dst.PromocodeName, opt => opt.MapFrom(src => src.PromocodeName))
            .ForMember(dst => dst.Sum, opt => opt.MapFrom(src => src.Sum))
            .ForMember(dst => dst.PromocodeId, opt => opt.MapFrom(src => src.PromocodeId));
    }
}