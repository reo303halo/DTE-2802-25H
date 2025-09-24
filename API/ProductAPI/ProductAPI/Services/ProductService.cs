using ProductAPI.Models.Entities;

namespace ProductAPI.Services;

public class ProductService : IProductService
{
    private static List<Product> Products { get; }
    private static int _nextId = 4;

    static ProductService()
    {
        Products =
        [
            new Product { Id = 1, Name = "Hammer", Price = 76.0m },
            new Product { Id = 2, Name = "Water Bottle", Price = 30.0m },
            new Product { Id = 3, Name = "Keyboard", Price = 1299.90m }
        ];
    }

    public async Task<List<Product>> GetAll() => await Task.FromResult(Products);

    public Product? Get(int id) => Products.FirstOrDefault(p => p.Id == id);

    public Task Save(Product product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }
        else
        {
            product.Id = _nextId++;
            Products.Add(product);
        }

        return Task.CompletedTask;
    }

    public async Task Delete(int id)
    {
        var product = Get(id);
        if (product is null)
            return;
        
        Products.Remove(product);
        await Task.Yield();
    }
}