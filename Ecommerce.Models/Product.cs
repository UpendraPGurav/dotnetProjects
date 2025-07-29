using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [Range(0,int.MaxValue)]
        public int Stock {  get; set; }

        [Url]
        public string ImageUrl { get; set; }

        //Product -> Many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
