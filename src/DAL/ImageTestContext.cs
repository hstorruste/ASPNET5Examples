using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".

    public class ImageTestContext : DbContext
    {
        public ImageTestContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Pictures> Pictures { get; set;}

    }

    public class Pictures
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [ForeignKey("Page")]
        public int PagesId { get; set; }

        public virtual Pages Page { get; set; }
    }

    public class Pages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Pictures> Pictures { get; set; }
    }
}
