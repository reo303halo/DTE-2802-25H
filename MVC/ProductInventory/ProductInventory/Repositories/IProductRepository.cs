using ProductInventory.Models.Entities;

namespace ProductInventory.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    void Save(Product product);
    void Delete(int id);
}