using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data.Models
{
    public class Rating
    {
        public Rating()
        {
            Customer = new Customer();
            Product = new Product();
        }

        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        public Customer Customer { get; set; }

        public Product Product { get; set; }
    }
}
