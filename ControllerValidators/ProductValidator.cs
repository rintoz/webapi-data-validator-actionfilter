using DataValidator.Controllers;
using DataValidator.Interface;
using DataValidator.Model;
using DataValidator.Validator;
using Microsoft.AspNetCore.Mvc;

namespace DataValidator.ControllerValidators
{
    [DataValidator(typeof(ProductsController))]
    public class ProductValidator : BaseDataValidator, IProductsController
    {
        public ActionResult<Product> CreateItem(Product newItem)
        {
            if (string.IsNullOrWhiteSpace(newItem.Name))
            {
                AddErrorMessage("Product name cannot be empty!");
            }

            return null;
        }

        public IActionResult DeleteItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id cannot be 0 or negative!");
            }

            return null;
        }

        public ActionResult<IEnumerable<Product>> GetItems()
        {
            return null;
        }

        public IActionResult UpdateItem(int id, Product updatedItem)
        {
            return null;
        }
    }
}
