using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; }
    }

}
