using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using K94Warriors.Controllers;
using K94Warriors.Data;
using K94Warriors.Data.Contracts;
using K94Warriors.ScheduledTaskServices;
using K94Warriors.ScheduledTaskServices.Tasks;

[assembly: WebActivator.PreApplicationStartMethod(typeof(K94Warriors.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(K94Warriors.App_Start.NinjectWebCommon), "Stop")]

namespace K94Warriors.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Bind Entity Framework repository and DbContext
            kernel.Bind(typeof(IRepository<>)).To(typeof(EFRepository<>)).InRequestScope();
            kernel.Bind<DbContext>().To<K9DbContext>().InRequestScope()
                  .WithConstructorArgument("nameOrConnectionString", "K9");

            // Bind to the Images blob container for DogController
            kernel.Bind<IBlobRepository>().To<K9BlobRepository>()
                  .WhenInjectedInto<DogController>()
                  .WithConstructorArgument("connectionString",
                                           ConfigurationManager.AppSettings["StorageAccountConnectionString"])
                  .WithConstructorArgument("imageContainer",
                                           ConfigurationManager.AppSettings["ImageBlobContainerName"]);

            // Bind to the Medical Records blob container for MedicalRecordsController
            kernel.Bind<IBlobRepository>().To<K9BlobRepository>()
                  .WhenInjectedInto<MedicalRecordsController>()
                  .WithConstructorArgument("connectionString",
                                           ConfigurationManager.AppSettings["StorageAccountConnectionString"])
                  .WithConstructorArgument("imageContainer",
                                           ConfigurationManager.AppSettings["MedicalRecordBlobContainerName"]);

            // Bind to the Notes blob container for MedicalRecordsController
            kernel.Bind<IBlobRepository>().To<K9BlobRepository>()
                  .WhenInjectedInto<NotesController>()
                  .WithConstructorArgument("connectionString",
                                           ConfigurationManager.AppSettings["StorageAccountConnectionString"])
                  .WithConstructorArgument("imageContainer",
                                           ConfigurationManager.AppSettings["NotesBlobContainerName"]);

            // Scheduled tasks with Aditi cloud scheduler
            kernel.Bind<IScheduledTaskService>().To<ScheduledTaskService>()
                  .WhenInjectedInto<SchedulerApiController>();
            kernel.Bind<IScheduledTaskFactory>().To<ScheduledTaskFactory>()
                  .WhenInjectedInto<IScheduledTaskService>()
                  .WithConstructorArgument("taskDictionary", new Dictionary<string, Type>
                      {
                          {"email", typeof(MorningEmailTask)},
                      });
            kernel.Bind<ScheduledTaskFactory>().ToSelf().InSingletonScope();
        }
    }
}
