using ProductInventory.Models.Entities;
using ProductInventory.Models.ViewModel;

namespace ProductInventory.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();
    void Save(Product product);
    void Delete(int id);
    ProductEditViewModel GetProductEditViewModel();
    ProductEditViewModel GetProductEditViewModel(int id);
}