using Microsoft.EntityFrameworkCore;
using ProductInventory.Data;
using ProductInventory.Models.Entities;

namespace ProductInventory.Repositories;

public class ProductRepository(ProductDbContext db) : IProductRepository
{
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
}