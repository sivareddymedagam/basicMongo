using BasicMongo.Models;
using BasicMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll() =>
            Ok(await _productService.GetAllAsync());

        // GET api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product updated)
        {
            var existing = await _productService.GetByIdAsync(id);
            if (existing is null) return NotFound();

            updated.Id = id;
            await _productService.UpdateAsync(id, updated);
            return NoContent();
        }

        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _productService.GetByIdAsync(id);
            if (existing is null) return NotFound();

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
