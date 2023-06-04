using AutoMapper;
using WWW.Domain.Entity;
using WWW.Domain.ViewModels.Account;

namespace WWW.Mapping
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, RegisterViewModel>()
            //    .ForMember(dest => dest.Avatar , opt => opt.Ignore());

            CreateMap<User, RegisterViewModel>()
                .ForPath(dest => dest.Introdaction, opt => opt.MapFrom(src => src.Details.Introdaction))
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            CreateMap<RegisterViewModel, User>()
                .ForPath(dest => dest.Details.Introdaction, opt => opt.MapFrom(src => src.Introdaction))
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

        }

    }
}

