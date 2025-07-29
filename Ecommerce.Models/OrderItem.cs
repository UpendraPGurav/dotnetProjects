using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{

    public class OrderItem
    {
        //[Key] // automatically recognized as primary key 
        public int Id { get; set; }

        //Foreign key to order. this automatically recognized as the foreign to order
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        //Foreign key to Product
        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        //not mapped to db just for calculation
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
