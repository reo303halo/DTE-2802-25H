using Microsoft.EntityFrameworkCore;
using ProductInventory.Data;
using ProductInventory.Models.Entities;
using ProductInventory.Models.ViewModel;

namespace ProductInventory.Repositories;

public class ProductRepository(ProductDbContext db) : IProductRepository
{
    private ProductEditViewModel _viewModel;

    public IEnumerable<Product> GetProducts()
    {
        var products = db.Products
            .Include(p => p.Category)
            .Include(p => p.Manufacturer)
            .ToList();
        return products;
    }

    public void Save(Product product)
    {
        db.Products.Update(product);
        db.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var product = db.Products.Find(id);

        if (product != null) db.Products.Remove(product);
        db.SaveChanges();
    }

    public ProductEditViewModel GetProductEditViewModel()
    {
        _viewModel = new ProductEditViewModel
        {
            Categories = db.Categories.ToList(),
            Manufacturers = db.Manufacturers.ToList()
        };
        return _viewModel;
    }
    
    public ProductEditViewModel GetProductEditViewModel(int id)
    {
        var product = db.Products.Find(id);
        if (product == null) return null;
        
        _viewModel = new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Categories = db.Categories.ToList(),
            ManufacturerId = product.ManufacturerId,
            Manufacturers = db.Manufacturers.ToList()
        };
        return _viewModel;
    }
}