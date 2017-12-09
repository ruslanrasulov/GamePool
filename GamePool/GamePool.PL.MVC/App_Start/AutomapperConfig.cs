using AutoMapper;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Account;
using GamePool.PL.MVC.Models.Admin;
using GamePool.PL.MVC.Models.Product;
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

                cfg.CreateMap<CreateGameVM, GameEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.AvatarId, opt => opt.Ignore())
                    .ForMember(dest => dest.MinimalSystemRequirements, opt => opt.Ignore())
                    .ForMember(dest => dest.ReccomendedSystemRequirements, opt => opt.Ignore())
                    .ForMember(dest => dest.Rating, opt => opt.Ignore());

                cfg.CreateMap<CreateSystemRequirementsVM, SystemRequirements>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.GameId, opt => opt.Ignore());

                cfg.CreateMap<Genre, Select2GenreVM>()
                    .ForMember(dest => dest.Text, opt => opt.ResolveUsing(src => src.Name));

                cfg.CreateMap<GameEntity, GamePreviewVM>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}