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
    public class OrderItemService: IOrderItemService
    {
        private readonly IOrderItemRepository _repo;

        public OrderItemService(IOrderItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId) => await _repo.GetByOrderIdAsync(orderId);
        public async Task AddAsync(OrderItem item) => await _repo.AddAsync(item);
        public async Task UpdateAsync(OrderItem item) => await _repo.UpdateAsync(item);
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
