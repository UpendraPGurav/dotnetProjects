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
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;
        public ProductRepository(EcommerceDbContext context)
        {
            this._context = context;
        }


        //Add the product
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        //Delete the product
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }


        //get all the products
        public async Task<IEnumerable<Product>> GetAllSync()
        {
            return await _context.Products.ToListAsync();
        }


        //get product by id
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);

        }


        //update the product
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
