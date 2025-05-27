using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Controller for managing revenue ranges in the Chinook database.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RevenueRangesController : ControllerBase
{
    private readonly ChinookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RevenueRangesController"/> class with the specified database context.
    /// </summary>
    /// <param name="context"></param>
    public RevenueRangesController(ChinookContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves the minimum and maximum revenue ranges from the invoice lines.
    /// </summary>
    /// <returns> A list of <see cref="RevenueRangesDto"/> objects.</returns>
    [HttpGet]
    public async Task<IActionResult> GetRevenueRanges()
    {
        var invoices = await _context.InvoiceLines.ToListAsync();
        var grouped = invoices
            .GroupBy(il => il.InvoiceId)
            .Select(g => g.Sum(il => il.UnitPrice * il.Quantity))
            .ToList();

        var min = grouped.Min();
        var max = grouped.Max();

        var revenueRangesDto = new RevenueRangesDto(min, max);

        return Ok(revenueRangesDto);
    }
}
