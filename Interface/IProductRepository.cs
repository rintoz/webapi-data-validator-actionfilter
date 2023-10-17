using DataValidator.Model;

namespace DataValidator.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllItems();

        Product GetItemById(int id);

        void AddItem(Product newItem);

        void UpdateItem(int id, Product updatedItem);

        void DeleteItem(int id);
    }
}