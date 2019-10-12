
using System;
using System.Threading.Tasks;
using ImportExport.DB;
using ImportExport.DB.Entities;
using ImportExport.FileProcessor;
using Microsoft.Extensions.DependencyInjection;

namespace ImportExport.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<Executor>().Execute();
        }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ImportExportContext>();
            //DI
            // a new instance is provided to every controller and every service.
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IFileProcessor, FileProcessor.FileProcessor>();
            services.AddTransient<Executor>();
            return services;
        }
    }

    public class Executor
    {
        private IFileProcessor fileProcessor;
        public Executor(IFileProcessor fileProcessor)
        {
            this.fileProcessor = fileProcessor;
        }

        public void Execute()
        {
            fileProcessor.LoadInDB(@"C:\Users\deepi\Desktop\test.xlsx");
        }
    }
}
