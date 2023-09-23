using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.EntityToModelsMappers;

public class CarToCarModel: Profile
{
    public CarToCarModel()
    {
        CreateMap<Car, CarModel>()
            .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dst => dst.NameCar, opt => opt.MapFrom(src => src.NameCar))
            .ForMember(dst => dst.Power, opt => opt.MapFrom(src => src.Power))
            .ForMember(dst => dst.FromOneToHundred, opt => opt.MapFrom(src => src.FromOneToHundred))
            .ForMember(dst => dst.MaxSpeed, opt => opt.MapFrom(src => src.MaxSpeed))
            .ForMember(dst => dst.Passengers, opt => opt.MapFrom(src => src.Passengers))
            .ForMember(dst => dst.Expenditure, opt => opt.MapFrom(src => src.Expenditure))
            .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dst => dst.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.About, opt => opt.MapFrom(src => src.About))
            .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dst=>dst.Longitude, opt=>opt.MapFrom(src=>src.Longitude))
            .ForMember(dst => dst.PowerReserve, opt => opt.MapFrom(src => src.AccualFileUrl))
            .ForMember(dst => dst.CategoryForId, opt => opt.MapFrom(src => src.CategoryForId))
            .ForMember(dst => dst.Free, opt => opt.MapFrom(src => src.Free))
            .ForMember(dst => dst.Catogories, opt => opt.MapFrom(src => src.Catogories));
    }
}