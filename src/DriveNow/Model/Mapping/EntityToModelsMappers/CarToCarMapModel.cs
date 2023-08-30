using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.EntityToModelsMappers;

public class CarToCarMapModel: Profile
{
    public CarToCarMapModel()
    {
        CreateMap<Car, CarMapModel>()
            .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dst => dst.NameCar, opt => opt.MapFrom(src => src.NameCar))
            .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dst => dst.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dst => dst.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Catogories))
            .ForMember(dst => dst.Free, opt => opt.MapFrom(src => src.Free))
            .ForMember(dst => dst.PhotoUrl, opt => opt.MapFrom(src => src.AccualFileUrl));
    }
}