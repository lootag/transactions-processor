using Lootag.TransactionsProcessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lootag.TransactionsProcessor.WebApi.Controllers;

[ApiController]
[Route("api/v1/security-transactions")]
public class SecurityTransactionsController : ControllerBase
{
    public SecurityTransactionsController(
        ISecurityTransactionService securityTransactionService
    )
    {
        _securityTransactionsService = securityTransactionService;
    }

    private readonly ISecurityTransactionService _securityTransactionsService;

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        try
        {
            var transactions = await _securityTransactionsService.RetrieveSecurityTransactions();
            var response = GetSecurityTransactionsResponse.FromSecurityTransactions(transactions);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
