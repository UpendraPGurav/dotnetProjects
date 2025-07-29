using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repo.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly EcommerceDbContext _context;
        public OrderItemRepository(EcommerceDbContext context)
        {
            this._context = context;
        }
        public async Task AddAsync(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            var item = _context.OrderItems.Find(orderId);
            if (item != null)
            {
                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Product) // Optional: Include product details
                .ToListAsync();
        }

        public async Task UpdateAsync(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
