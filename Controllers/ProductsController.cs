using DataValidator.Interface;
using DataValidator.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataValidator.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetItems()
        {
            var items = productRepository.GetAllItems();
            return Ok(items);
        }

        [HttpPost]
        public ActionResult<Product> CreateItem(Product newItem)
        {
            if (newItem == null)
            {
                return BadRequest();
            }

            productRepository.AddItem(newItem);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Product updatedItem)
        {
            var existingItem = productRepository.GetItemById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            productRepository.UpdateItem(id, updatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var existingItem = productRepository.GetItemById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            productRepository.DeleteItem(id);
            return NoContent();
        }
    }
}
