using AutoMapper;
using GamePool.PL.MVC.Models.Account;
using UserEntity = GamePool.Common.Entities.User;

namespace GamePool.PL.MVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserLoginVM, UserEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Username))
                    .ForMember(dest => dest.Roles, opt => opt.Ignore());

                cfg.CreateMap<UserRegisterVM, UserEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Username))
                    .ForMember(dest => dest.Roles, opt => opt.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}