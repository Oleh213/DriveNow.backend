using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.ModelsToEntityMappers;

public class TripModelToTrip: Profile
{
    public TripModelToTrip()
    {
        CreateMap<TripModel, Trip>()
            .ForMember(dst => dst.TripId, opt => opt.MapFrom(src => src.TripId))
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dst => dst.CarId, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dst => dst.StartTrip, opt => opt.MapFrom(src => src.StartTrip))
            .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status));
    }
}