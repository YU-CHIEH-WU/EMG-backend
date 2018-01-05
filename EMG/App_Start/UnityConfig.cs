using EMG.Interface;
using EMG.Model;
using EMG.Services;
using Microsoft.Practices.Unity;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;

namespace EMG
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            string connectionString =
                WebConfigurationManager.ConnectionStrings["EMGDataBaseEntities"].ConnectionString;
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDbContextFactory, DbContextFactory>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(connectionString))
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType(
                    typeof(IRepository<>),
                    typeof(GenericRepository<>),
                    new PerRequestLifetimeManager())
                .RegisterType<IUserServices, UserServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(connectionString))
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType(
                    typeof(IRepository<>),
                    typeof(GenericRepository<>),
                    new PerRequestLifetimeManager())
                .RegisterType<IUsertestServices, UsertestServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(connectionString))
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType(
                    typeof(IRepository<>),
                    typeof(GenericRepository<>),
                    new PerRequestLifetimeManager())
                .RegisterType<IMessageServices, MessageServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(connectionString))
                .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType(
                    typeof(IRepository<>),
                    typeof(GenericRepository<>),
                    new PerRequestLifetimeManager())
                .RegisterType<IReplyServices, ReplyServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<ITextServices, TextServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<IFileServices, FileServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<IEMGServices, EMGServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<ICourseServices, CourseServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<IChartServices, ChartServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<IAlbumServices, AlbumServices>(new PerRequestLifetimeManager());

            container.RegisterType<IDbContextFactory, DbContextFactory>(
                   new PerRequestLifetimeManager(),
                   new InjectionConstructor(connectionString))
               .RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager())
               .RegisterType(
                   typeof(IRepository<>),
                   typeof(GenericRepository<>),
                   new PerRequestLifetimeManager())
               .RegisterType<INewsServices, NewsServices>(new PerRequestLifetimeManager());
           
            //Unity MVC5
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            //Unity Api
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}