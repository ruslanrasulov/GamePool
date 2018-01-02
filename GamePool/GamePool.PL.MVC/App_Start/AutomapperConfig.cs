using AutoMapper;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Account;
using GamePool.PL.MVC.Models.Admin;
using GamePool.PL.MVC.Models.Product;
using GamePool.PL.MVC.Models.Search;

namespace GamePool.PL.MVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserLoginVm, UserEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Username));

                cfg.CreateMap<UserRegisterVm, UserEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.ResolveUsing(src => src.Username));

                cfg.CreateMap<CreateGameVm, GameEntity>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.AvatarId, opt => opt.Ignore())
                    .ForMember(dest => dest.MinimalSystemRequirements, opt => opt.Ignore())
                    .ForMember(dest => dest.RecommendedSystemRequirements, opt => opt.Ignore())
                    .ForMember(dest => dest.Rating, opt => opt.Ignore());

                cfg.CreateMap<CreateSystemRequirementsVm, SystemRequirements>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.GameId, opt => opt.Ignore());

                cfg.CreateMap<Genre, Select2GenreVm>()
                    .ForMember(dest => dest.Text, opt => opt.ResolveUsing(src => src.Name));

                cfg.CreateMap<GameEntity, GamePreviewVm>();

                cfg.CreateMap<SearchParametersVm, SearchParameters>()
                    .ForMember(dest => dest.PageSize, opt => opt.Ignore());

                cfg.CreateMap<SystemRequirements, DisplaySystemRequirementsVm>();

                cfg.CreateMap<GameEntity, DisplayGameVm>()
                    .ForMember(dest => dest.Genres, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                    {
                        dest.MinimalSystemRequirements = context.Mapper.Map<SystemRequirements, DisplaySystemRequirementsVm>(src.MinimalSystemRequirements);
                        dest.RecommendedSystemRequirements = context.Mapper.Map<SystemRequirements, DisplaySystemRequirementsVm>(src.RecommendedSystemRequirements);
                    });

                cfg.CreateMap<EditSystemRequirementsVm, SystemRequirements>()
                    .ForMember(dest => dest.GameId, opt => opt.Ignore());

                cfg.CreateMap<EditGameVm, GameEntity>()
                    .ForMember(dest => dest.AvatarId, opt => opt.Ignore())
                    .ForMember(dest => dest.Rating, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                     {
                         dest.MinimalSystemRequirements = context.Mapper.Map<EditSystemRequirementsVm, SystemRequirements>(src.MinimalSystemRequirements);
                         dest.RecommendedSystemRequirements = context.Mapper.Map<EditSystemRequirementsVm, SystemRequirements>(src.RecommendedSystemRequirements);
                     });

                cfg.CreateMap<SystemRequirements, EditSystemRequirementsVm>();

                cfg.CreateMap<GameEntity, EditGameVm>()
                    .ForMember(dest => dest.GenreIds, opt => opt.Ignore())
                    .AfterMap((src, dest, context) =>
                    {
                        dest.MinimalSystemRequirements = context.Mapper.Map<SystemRequirements, EditSystemRequirementsVm>(src.MinimalSystemRequirements);
                        dest.RecommendedSystemRequirements = context.Mapper.Map<SystemRequirements, EditSystemRequirementsVm>(src.RecommendedSystemRequirements);
                    });

                cfg.CreateMap<GameEntity, OrderedGameVm>()
                    .ForMember(dest => dest.Quantity, opt => opt.Ignore());

                cfg.CreateMap<Order, OrderListItemVm>();

                cfg.CreateMap<UserEntity, UserListItemVm>()
                    .ForMember(dest => dest.CurrentRoles, opt => opt.Ignore())
                    .ForMember(dest => dest.AvailableRoles, opt => opt.Ignore());
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}