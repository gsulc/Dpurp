using Dpurp;
using Dpurp.Json;
using Examples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Persistence
{
    public class PlutoContext : JsonFileContext
    {
        public PlutoContext()
            : base(@"\\PlutoContext")
        {
        }

        public virtual ISet<Author> Authors { get; set; }
        public virtual ISet<Course> Courses { get; set; }
        public virtual ISet<Tag> Tags { get; set; }
    }
}
