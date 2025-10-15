using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventory_management_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
    {
        var response = await _productService.GetAllProductsAsync();
        return Ok(response) ?? throw new Exception("Products not found");
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductByIdAsync(string id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return Ok(response) ?? throw new Exception("Product not found");
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] ProductDto productDto)
    {
        var response = await _productService.CreateProductAsync(productDto);
        return Ok(response) ?? throw new Exception("Product not created");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProductAsync(string id, [FromBody] ProductDto productDto)
    {
        var response = await _productService.UpdateProductAsync(id, productDto);
        return Ok(response) ?? throw new Exception("Product not updated");
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProductAsync(string id)
    {
        var response = await _productService.DeleteProductAsync(id);
        return Ok(response) ?? throw new Exception("Product not deleted");
    }
}