using Dpurp;
using Dpurp.Csv;
using Dpurp.EntityFramework;
using Dpurp.Json;
using Dpurp.Xml;
using Examples.Model;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Injection;

namespace Examples
{
    class Program
    {
        enum DataProviderType
        {
            SqlServer,
            XmlFileStore,
            JsonFileStore,
            CsvFileStore
        }

        class ApplicationSettings
        {
            public DataProviderType DataProvider;
            public string ConnectionString;
        }

        private static readonly string executingPath = 
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            var appSettings = new ApplicationSettings
            {
                DataProvider = DataProviderType.JsonFileStore,
                ConnectionString = executingPath
            };
            
            IRepository<Blog> blogRepository = CreateRepo<Blog>(appSettings);
            IRepository<Post> postRepository = CreateRepo<Post>(appSettings);

            // Create and save a new Blog 
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };
            blogRepository.Add(blog);
            blogRepository.SaveChanges();

            // Display all Blogs from the database 
            var query = blogRepository.GetAll().OrderBy(b => b.Name);

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
                Console.WriteLine(item.Name);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static IRepository<T> CreateRepo<T>(ApplicationSettings settings) where T : class
        {
            using (var container = new UnityContainer())
            {
                container.RegisterInstance(settings);

                switch (settings.DataProvider)
                {
                    case DataProviderType.SqlServer:
                        container.RegisterType<DbContext, EntityDataContext>();
                        container.RegisterType<EntityDataContext>(
                            new InjectionConstructor(settings.ConnectionString));
                        container.RegisterType(typeof(IRepository<>), typeof(EntityRepository<>));
                        break;

                    case DataProviderType.XmlFileStore:
                        container.RegisterType<FileContext, XmlFileContext>();
                        container.RegisterType<XmlFileContext>(
                            new InjectionConstructor(settings.ConnectionString));
                        container.RegisterType(typeof(IRepository<>), typeof(FileRepository<>));
                        break;

                    case DataProviderType.JsonFileStore:
                        container.RegisterType<FileContext, JsonFileContext>();
                        container.RegisterType<JsonFileContext>(
                            new InjectionConstructor(settings.ConnectionString));
                        container.RegisterType(typeof(IRepository<>), typeof(FileRepository<>));
                        break;
                    case DataProviderType.CsvFileStore:
                        container.RegisterType<FileContext, CsvFileContext>();
                        container.RegisterType<CsvFileContext>(
                            new InjectionConstructor(settings.ConnectionString));
                        container.RegisterType(typeof(IRepository<>), typeof(FileRepository<>));
                        break;
                    default:
                        throw new Exception($"The DataProvider Type {settings.DataProvider} is unknown.");
                }

                return container.Resolve<IRepository<T>>();
            }
        }
    }
}
