using DataValidator.Model;
using Microsoft.AspNetCore.Mvc;

namespace DataValidator.Interface
{
    public interface IProductsController
    {
        ActionResult<Product> CreateItem(Product newItem);
        IActionResult DeleteItem(int id);
        ActionResult<IEnumerable<Product>> GetItems();
        IActionResult UpdateItem(int id, Product updatedItem);
    }
}