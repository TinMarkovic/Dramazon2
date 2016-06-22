using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data.Models
{
    public class Purchase
    {
        public Purchase()
        {
            Customer = new Customer();
            Products = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public Customer Customer { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
