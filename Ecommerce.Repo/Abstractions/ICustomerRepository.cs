using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repo.Abstractions
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync (Customer customer);
        Task UpdateAsync (Customer customer);
        Task DeleteAsync (int id);

        //custom
        Task<Customer> GetCustomerWithOrderAsync(int id);
    }
}
