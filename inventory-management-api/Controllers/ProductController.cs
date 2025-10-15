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
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")] 
    public async Task<ActionResult<ProductDto>> GetProductByIdAsync(string id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        return Ok(response)  ;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] ProductDto productDto)
    {
        var product = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProductByIdAsync), new { id = product.Id }, product);
    }
}