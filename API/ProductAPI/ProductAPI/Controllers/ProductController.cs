using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.DTO;
using ProductAPI.Models.Entities;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[Route("api/[controller]")] // api/Product
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    
    [HttpGet]
    public async Task<List<Product>> GetAll() => await productService.GetAll();

    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        var product = _productService.Get(id);
        if (product == null)
            return NotFound($"No product with the id {id}.");
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price
        };

        await _productService.Save(newProduct);
        return CreatedAtAction(
            nameof(Get),
            new { id = newProduct.Id },
            newProduct
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product product)
    {
        if (id != product.Id)
            return BadRequest("Id from Route does not match product id.");
        
        var existingProduct = _productService.Get(id);
        if (existingProduct == null)
            return NotFound($"No product with the id {id}.");

        var newProduct = new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
        
        await _productService.Save(newProduct);
        return Ok("Product successfully updated.");
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var product = _productService.Get(id);
        if (product == null)
            return NotFound($"No product with the id {id}.");

        _productService.Delete(id);
        return Ok("Product successfully deleted.");
    }
}