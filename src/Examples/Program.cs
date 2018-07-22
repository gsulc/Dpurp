using Dpurp.Abstractions;
using Dpurp.Json;
using Examples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void TestBlogWithJson()
        {
            var jsonContext = new JsonFileContext(@"\");
            IRepository<Blog> blogRepository = new JsonFileRepository<Blog>(jsonContext);
            IRepository<Post> postRepository = new JsonFileRepository<Post>(jsonContext);

            // Create and save a new Blog 
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();

            var blog = new Blog { Name = name };
            blogRepository.Add(blog);
            //blogRepository.SaveChanges();

            // Display all Blogs from the database 
            var query = blogRepository.GetAll().OrderBy(b => b.Name);

            Console.WriteLine("All blogs in the database:");
            foreach (var item in query)
                Console.WriteLine(item.Name);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
