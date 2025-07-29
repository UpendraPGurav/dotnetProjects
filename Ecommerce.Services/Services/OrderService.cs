using Ecommerce.Models;
using Ecommerce.Repo.Abstractions;
using Ecommerce.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Order> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task AddAsync(Order order) => await _repo.AddAsync(order);
        public async Task UpdateAsync(Order order) => await _repo.UpdateAsync(order);
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId) => await _repo.GetOrdersByCustomerIdAsync(customerId);
        public async Task<Order> GetOrderWithItemsAsync(int orderId) => await _repo.GetOrderWithItemsAsync(orderId);
    }
}
