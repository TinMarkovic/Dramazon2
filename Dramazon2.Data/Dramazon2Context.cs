using Dramazon2.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data
{
    public class Dramazon2Context : DbContext
    {
        public Dramazon2Context() :
            base("dramazon2Connection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new Dramazon2DBInitializer());
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}

