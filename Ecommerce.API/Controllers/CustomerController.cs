using AutoMapper;
using Ecommerce.Models;
using Ecommerce.Models.DTOs;
using Ecommerce.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _service.GetAllAsync();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // POST: api/customer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(dto);
            await _service.AddAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, _mapper.Map<CustomerDto>(customer));
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest("Mismatched ID");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _service.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/customer/5/orders
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetCustomerWithOrders(int id)
        {
            var customer = await _service.GetCustomerWithOrdersAsync(id);
            if (customer == null) return NotFound();

            return Ok(customer); // You can map to a custom DTO if needed
        }
    }
}
