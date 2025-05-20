using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RevenueRangesController : ControllerBase
{
    private readonly ChinookContext _context;

    public RevenueRangesController(ChinookContext context)
    {
        _context = context;
    }

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
