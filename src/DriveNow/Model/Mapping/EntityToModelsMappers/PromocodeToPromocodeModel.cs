using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.EntityToModelsMappers;

public class PromocodeToPromocodeModel: Profile
{
    public PromocodeToPromocodeModel()
    {
        CreateMap<Promocode, PromocodeModel>()
            .ForMember(dst => dst.PromocodeName, opt => opt.MapFrom(src => src.PromocodeName))
            .ForMember(dst => dst.Sum, opt => opt.MapFrom(src => src.Sum))
            .ForMember(dst => dst.PromocodeId, opt => opt.MapFrom(src => src.PromocodeId));
    }
}