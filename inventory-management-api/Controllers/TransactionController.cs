using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventory_management_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;
    public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactionsAsync()
    {
        var response = await _transactionService.GetAllTransactionsAsync();
        return Ok(response) ?? throw new Exception("Transactions not found");
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetTransactionByIdAsync(string id)
    {
        var response = await _transactionService.GetTransactionByIdAsync(id);
        return Ok(response) ?? throw new Exception("Transaction not found");
    }
    
    [HttpPost]
    public async Task<ActionResult<TransactionDto>> CreateTransactionAsync([FromBody] TransactionDto transactionDto)
    {
        var response = await _transactionService.CreateTransactionAsync(transactionDto);
        return Ok(response) ?? throw new Exception("Transaction not found");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionDto>> UpdateTransactionAsync(string id, [FromBody] TransactionDto transactionDto)
    {
        var response = await _transactionService.UpdateTransactionAsync(id, transactionDto);
        return Ok(response) ?? throw new Exception("Product not updated");
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteTransactionAsync(string id)
    {
        var response = await _transactionService.DeleteTransactionAsync(id);
        return Ok(response) ?? throw new Exception("Product not deleted");
    }
}