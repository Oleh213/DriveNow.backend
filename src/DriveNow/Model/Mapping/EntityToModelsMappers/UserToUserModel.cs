using AutoMapper;
using DriveNow.Context;

namespace DriveNow.Model.Mapping.EntityToModelsMappers;

public class UserToUserModel: Profile
{
    public UserToUserModel()
    {
        CreateMap<User, UserModel>()
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dst => dst.SecondName, opt => opt.MapFrom(src => src.SecondName))
            .ForMember(dst => dst.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dst => dst.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dst => dst.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dst => dst.Language, opt => opt.MapFrom(src => src.Language))
            .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dst => dst.DocumentUrl, opt => opt.MapFrom(src => src.DocumentUrl))
            .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status));

    }
}