using GamePool.BLL.Core;
using GamePool.BLL.LogicContracts;
using GamePool.DAL.DALContracts;
using GamePool.DAL.SqlDAL;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePool.Common.NinjectApp
{
    public sealed class NinjectConfig
    {
        private static readonly string connectionString;
        private static readonly string providerName;

        static NinjectConfig()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
        }

        public static void RegisterServices(IKernel kernel)
        {
            #region DALContracts
            kernel
                .Bind<IAvatarDAO>()
                .To<AvatarDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<IGameDAO>()
                .To<GameDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<IImageDAO>()
                .To<ImageDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<IScreenshotDAO>()
                .To<ScreenshotDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<ISystemRequirementsDAO>()
                .To<SystemRequirementsDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<IUserDAO>()
                .To<UserDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            kernel
                .Bind<IUserRoleDAO>()
                .To<UserRoleDAO>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", connectionString)
                .WithConstructorArgument("providerName", providerName);
            #endregion

            #region LogicContracts
            kernel
                .Bind<IAvatarLogic>()
                .To<AvatarLogic>()
                .InSingletonScope();
            kernel
                .Bind<IGameLogic>()
                .To<GameLogic>()
                .InSingletonScope();
            kernel
                .Bind<IImageLogic>()
                .To<ImageLogic>()
                .InSingletonScope();
            kernel
                .Bind<IScreenshotLogic>()
                .To<ScreenshotLogic>()
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
            #endregion
        }
    }
}