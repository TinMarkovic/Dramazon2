using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramazon2.Data.Models
{
    public class Product
    {
        public Product()
        {
            Tags = new List<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(4)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public float Price { get; set; }

        public Customer Creator { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
