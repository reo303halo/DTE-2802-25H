using Microsoft.AspNetCore.Mvc;
using ProductInventory.Repositories;

namespace ProductInventory.Controllers;

public class ProductController(IProductRepository repository) : Controller
{
    // GET: Product
    public IActionResult Index()
    {
        var products = repository.GetProducts();
        return View(products);
    }

}