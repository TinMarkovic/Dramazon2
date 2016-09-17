using Dramazon2.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data
{
    class Dramazon2DBInitializer : DropCreateDatabaseAlways<Dramazon2Context>
    {
        protected override void Seed(Dramazon2Context context)
        {
            Customer user1 = new Customer
            {
                Id = 1,
                Username = "TMarko",
                Password = "12345",
                Address = "Split",
                Fullname = "Tin Markovic",
                Email = "Dupemail@example.net"
            };
            Customer user2 = new Customer
            {
                Id = 2,
                Username = "SGolem",
                Password = "12345",
                Address = "Split",
                Fullname = "Stjepan Golemac",
                Email = "Dupemail2@example.net"
            };
            context.Customers.Add(user1);
            context.Customers.Add(user2);

            Tag tag1 = new Tag { Id = 1, Name = "Electronics" };
            Tag tag2 = new Tag { Id = 2, Name = "Houseware" };
            Tag tag3 = new Tag { Id = 3, Name = "Books" };
            Tag tag4 = new Tag { Id = 4, Name = "Clothing" };
            context.Tags.Add(tag1);
            context.Tags.Add(tag2);
            context.Tags.Add(tag3);
            context.Tags.Add(tag4);

            Product prod1 = new Product
            {
                Id = 1,
                Title = "Electric Oven",
                Description = "It's on fire, blazin' heat. Nice as a treat.",
                Price = 999,
                Creator = user2
            };
            prod1.Tags.Add(tag1);
            prod1.Tags.Add(tag2);
            Product prod2 = new Product
            {
                Id = 2,
                Title = "Game of Thrones",
                Description = "Author: George RR Martin \n The bestselling book that started it all.",
                Price = 19,
                Creator = user1
            };
            prod2.Tags.Add(tag3);
            context.Products.Add(prod1);
            context.Products.Add(prod2);

            context.SaveChanges();
        }
    }
}
