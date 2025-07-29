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
    public class OrderController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IProductService productService, IMapper mapper)
        {
            _orderService = orderService;
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(result);
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderWithItemsAsync(id);
            if (order == null) return NotFound();

            var result = _mapper.Map<OrderDto>(order);
            return Ok(result);
        }

        // GET: api/order/customer/3
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByCustomerId(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(result);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Map to Order model
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                OrderDate = DateTime.UtcNow,
                OrderItems = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = 0  // We’ll update this below
                }).ToList()
            };

            // Calculate total and unit prices
            decimal total = 0;
            foreach (var item in order.OrderItems)
            {
                // Ideally, call ProductService to get price
                var product = await _productService.GetByIdAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product with ID {item.ProductId} not found");

                item.UnitPrice = product.Price;
                total += item.Quantity * item.UnitPrice;
            }

            order.TotalAmount = total;

            await _orderService.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order.Id);
        }

        // DELETE: api/order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
