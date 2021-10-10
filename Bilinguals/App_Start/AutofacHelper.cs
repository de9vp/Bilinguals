using Autofac;
using Autofac.Integration.Mvc;
using Bilinguals.Data;
using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.App_Start
{
    public class AutofacHelper
    {
        public static void RegisterDependencies(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Configuring Autofac to work with the ASP.NET Identity: https://m.51dev.com/show.php?id=12917
            //builder.RegisterType<BilingualDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<BilingualDbContext>().As<IBilingualDbContext>().InstancePerRequest();

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();

            //builder.RegisterType<ApplicationRoleStore>().As<IRoleStore<ApplicationRole>>().InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();

            //builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<DialogService>().As<IDialogService>().InstancePerRequest();
            builder.RegisterType<SentenceService>().As<ISentenceService>().InstancePerRequest();
            builder.RegisterType<UserDialogService>().As<IUserDialogService>().InstancePerRequest();
            builder.RegisterType<UserSentenceService>().As<IUserSentenceService>().InstancePerRequest();
            builder.RegisterType<GroupService>().As<IGroupService>().InstancePerRequest();
            builder.RegisterType<ImageService>().As<IImageService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        // From PGB API
        //private static void SetAutofacContainer()
        //{
        //    var builder = new ContainerBuilder();
        //    //builder.RegisterControllers(Assembly.GetExecutingAssembly());

        //    //Register Controller
        //    builder.RegisterControllers(Assembly.GetExecutingAssembly());

        //    //Register WebApi Controller
        //    builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        //    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        //    builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

        //    // Repositories
        //    builder.RegisterAssemblyTypes(Assembly.Load("PGB.OMR.Repository"))
        //        .Where(t => t.Name.EndsWith("Repository"))
        //        .AsImplementedInterfaces().InstancePerRequest();
        //    // Services
        //    builder.RegisterAssemblyTypes(Assembly.Load("PGB.OMR.Service"))
        //       .Where(t => t.Name.EndsWith("Service"))
        //       .AsImplementedInterfaces().InstancePerRequest();

        //    // Logger
        //    builder.RegisterType<Log4NetAdapter>().As<ILogger>().SingleInstance();

        //    var container = builder.Build();
        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //    GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        //    LoggingFactory.InitializeLogFactory(container.Resolve<ILogger>());
        //}
    }
}
