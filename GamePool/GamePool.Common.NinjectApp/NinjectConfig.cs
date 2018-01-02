using System.Configuration;
using GamePool.BLL.Core;
using GamePool.BLL.LogicContracts;
using GamePool.DAL.DALContracts;
using GamePool.DAL.SqlDAL;
using Ninject;

namespace GamePool.Common.NinjectApp
{
    public sealed class NinjectConfig
    {
        private static readonly string ConnectionString;
        private static readonly string ProviderName;

        static NinjectConfig()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        }

        public static void RegisterServices(IKernel kernel)
        {
            #region DALContracts
            kernel
                .Bind<IGameDao>()
                .To<GameDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<IImageDao>()
                .To<ImageDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<ISystemRequirementsDao>()
                .To<SystemRequirementsDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<IUserDao>()
                .To<UserDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<IUserRoleDao>()
                .To<UserRoleDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<IOrderDao>()
                .To<OrderDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);

            kernel
                .Bind<IGenreDao>()
                .To<GenreDao>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", ConnectionString)
                .WithConstructorArgument("providerName", ProviderName);
            #endregion

            #region LogicContracts
            kernel
                .Bind<IGameLogic>()
                .To<GameLogic>()
                .InSingletonScope();

            kernel
                .Bind<IImageLogic>()
                .To<ImageLogic>()
                .InSingletonScope();

            kernel
                .Bind<ISystemRequirementsLogic>()
                .To<SystemRequirementsLogic>()
                .InSingletonScope();

            kernel
                .Bind<IUserLogic>()
                .To<UserLogic>()
                .InSingletonScope();

            kernel
                .Bind<IUserRoleLogic>()
                .To<UserRoleLogic>()
                .InSingletonScope();

            kernel
                .Bind<IOrderLogic>()
                .To<OrderLogic>()
                .InSingletonScope();

            kernel
                .Bind<IGenreLogic>()
                .To<GenreLogic>()
                .InSingletonScope();
            #endregion
        }
    }
}