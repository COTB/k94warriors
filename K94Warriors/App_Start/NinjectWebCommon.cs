using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using K94Warriors;
using K94Warriors.Controllers;
using K94Warriors.Data;
using K94Warriors.Data.Contracts;
using K94Warriors.Email;
using K94Warriors.ScheduledTaskServices;
using K94Warriors.ScheduledTaskServices.Tasks;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace K94Warriors
{
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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            BindAzureBlobServices(kernel);

            BindScheduledTaskServices(kernel);

            // Bind Entity Framework repository and DbContext
            kernel.Bind(typeof(IRepository<>)).To(typeof(EFRepository<>)).InRequestScope();
            kernel.Bind<DbContext>().To<K9DbContext>().InRequestScope()
                  .WithConstructorArgument("nameOrConnectionString", "K9");


            // Bind IMailer to the SmtpMailer (could also use SendGrid, etc)
            kernel.Bind<IMailer>().To<SmtpMailer>();
        }

        private static void BindScheduledTaskServices(IKernel kernel)
        {
            // Scheduled tasks with Aditi cloud scheduler
            kernel.Bind<IScheduledTask>().To<MorningEmailTask>().Named("morningTaskEmail")
                .WithConstructorArgument("from", ConfigurationManager.AppSettings["MorningTasksFromEmailAddress"])
                .WithConstructorArgument("subject", ConfigurationManager.AppSettings["MorningTasksEmailSubject"]);

            kernel.Bind<IScheduledTaskService>().To<ScheduledTaskService>()
                  .WhenInjectedInto<SchedulerApiController>();

            kernel.Bind<IScheduledTaskProvider>().To<ScheduledTaskProvider>()
                  .WhenInjectedInto<IScheduledTaskService>()
                  .WithConstructorArgument("factories", new Dictionary<string, Func<IScheduledTask>>
                      {
                          {"morningTaskEmail", () => kernel.Get<IScheduledTask>("morningTaskEmail")},
                      });

            kernel.Bind<ScheduledTaskProvider>().ToSelf().InSingletonScope();
        }

        private static void BindAzureBlobServices(IBindingRoot kernel)
        {
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
        }
    }
}
