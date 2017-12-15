using AutoMapper;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Account;
using GamePool.PL.MVC.Models.Admin;
using GamePool.PL.MVC.Models.Product;
using GamePool.PL.MVC.Models.Search;
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
                    .ForMember(dest => dest.RecommendedSystemRequirements, opt => opt.Ignore())
                    .ForMember(dest => dest.Rating, opt => opt.Ignore());

                cfg.CreateMap<CreateSystemRequirementsVM, SystemRequirements>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.GameId, opt => opt.Ignore());

                cfg.CreateMap<Genre, Select2GenreVM>()
                    .ForMember(dest => dest.Text, opt => opt.ResolveUsing(src => src.Name));

                cfg.CreateMap<GameEntity, GamePreviewVM>();

                cfg.CreateMap<SearchParametersVM, SearchParameters>()
                    .ForMember(dest => dest.PageSize, opt => opt.Ignore());

                cfg.CreateMap<SystemRequirements, DisplaySystemRequirementsVM>();

                cfg.CreateMap<GameEntity, DisplayGameVM>()
                    .ForMember(dest => dest.Genres, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                    {
                        dest.MinimalSystemRequirements = context.Mapper.Map<SystemRequirements, DisplaySystemRequirementsVM>(src.MinimalSystemRequirements);
                        dest.RecommendedSystemRequirements = context.Mapper.Map<SystemRequirements, DisplaySystemRequirementsVM>(src.RecommendedSystemRequirements);
                    });

                cfg.CreateMap<EditSystemRequirementsVM, SystemRequirements>()
                    .ForMember(dest => dest.GameId, opt => opt.Ignore());

                cfg.CreateMap<EditGameVM, GameEntity>()
                    .ForMember(dest => dest.AvatarId, opt => opt.Ignore())
                    .ForMember(dest => dest.Rating, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                     {
                         dest.MinimalSystemRequirements = context.Mapper.Map<EditSystemRequirementsVM, SystemRequirements>(src.MinimalSystemRequirements);
                         dest.RecommendedSystemRequirements = context.Mapper.Map<EditSystemRequirementsVM, SystemRequirements>(src.RecommendedSystemRequirements);
                     });

                cfg.CreateMap<SystemRequirements, EditSystemRequirementsVM>();

                cfg.CreateMap<GameEntity, EditGameVM>()
                    .ForMember(dest => dest.GenreIds, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                    {
                        dest.MinimalSystemRequirements = context.Mapper.Map<SystemRequirements, EditSystemRequirementsVM>(src.MinimalSystemRequirements);
                        dest.RecommendedSystemRequirements = context.Mapper.Map<SystemRequirements, EditSystemRequirementsVM>(src.RecommendedSystemRequirements);
                    });

                cfg.CreateMap<GameEntity, OrderedGameVM>()
                    .ForMember(dest => dest.Quantity, opt => opt.Ignore());

                cfg.CreateMap<Order, OrderListItemVM>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}