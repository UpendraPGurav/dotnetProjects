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
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;
        public OrderRepository(EcommerceDbContext context)
        {
            this._context = context;
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderItems)             // optional: include order items
            .OrderByDescending(o => o.OrderDate)    // optional: recent orders first
            .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)       // eager load product for each order item
                                 .Include(o => o.Customer)                // optional: include customer details
                                 .FirstOrDefaultAsync(o => o.Id == orderId);

        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
