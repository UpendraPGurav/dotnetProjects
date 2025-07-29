using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        //Foreign key to  customer
        [Required]
        public int CustomerId { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        public Customer Customer { get; set; }

        //order->many order items
        public List<OrderItem> OrderItems { get; set; }
    }
}
