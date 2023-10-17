using DataValidator.Interface;
using DataValidator.Model;

namespace DataValidator.Repository
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> products = new()
        {

        };

        public ProductRepository()
        {

        }

        public void AddItem(Product newItem)
        {
            products.Add(newItem);
        }

        public void DeleteItem(int id)
        {
            products.Remove(products.FirstOrDefault(p => p.Id == id));
        }

        public IEnumerable<Product> GetAllItems()
        {
            return products;
        }

        public Product GetItemById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateItem(int id, Product updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}