using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);

        // 🔍 Custom
        //GetOrdersByCustomerIdAsync → shows all orders placed by one customer.
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
        //GetOrderWithItemsAsync → returns order with its items and product info.
        Task<Order> GetOrderWithItemsAsync(int orderId);

    }
}
