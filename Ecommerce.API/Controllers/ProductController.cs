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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductController(IProductService service, IMapper mapper, IImageService imageService)
        {
            _service = service;
            _mapper = mapper;
            _imageService = imageService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto dto)
        {
            if (id != dto.Id) 
                return BadRequest("Mismatched ID");
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) 
                return NotFound();

            var updatedProduct = _mapper.Map(dto, existing);
            await _service.UpdateAsync(updatedProduct);
            return NoContent();
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            Console.WriteLine("✅ Reached ProductController.Create");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                //string imageUrl = "https://via.placeholder.com/150";
                string imageUrl = await _imageService.UploadImageAsync(dto.ImageFile);

                var product = _mapper.Map<Product>(dto);
                product.ImageUrl = imageUrl;

                await _service.AddAsync(product);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, _mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Image upload failed: {ex.Message}");
            }
        }
    }
}
