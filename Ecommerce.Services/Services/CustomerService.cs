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
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
            
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Customer> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        public async Task AddAsync(Customer customer) => await _repo.AddAsync(customer);
        public async Task UpdateAsync(Customer customer) => await _repo.UpdateAsync(customer);
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);


        public async Task<Customer> GetCustomerWithOrdersAsync(int id) => await _repo.GetCustomerWithOrderAsync(id);
    }
}
