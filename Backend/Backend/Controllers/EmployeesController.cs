using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ChinookContext _context;

        public EmployeesController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/Employees for PieChart
        [HttpGet]
        public async Task<ActionResult<List<EmployeeSalesDto>>> GetEmployees()
        {
            var query = from c in _context.Customers
                        join i in _context.Invoices on c.CustomerId equals i.CustomerId
                        join e in _context.Employees on c.SupportRepId equals e.EmployeeId
                        group i by new { e.FirstName, e.LastName, e.EmployeeId } into g
                        select new EmployeeSalesDto
                        {
                            EmployeeId = g.Key.EmployeeId,
                            FullName = g.Key.FirstName + " " + g.Key.LastName,
                            TotalSold = g.Sum(i => i.Total)
                        };

            var result = await query.OrderByDescending(x => x.TotalSold).ToListAsync();

            return Ok(result);
        }




        [HttpGet("detail/{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            var employees = await _context.Employees
                .Where(e => e.EmployeeId == id)
                .ToListAsync(); 

            if (!employees.Any())
                return NotFound();

            var result = employees.Select(e => new EmployeeDetailDto
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Title = e.Title!,
                Country = e.Country!,
                City = e.City!,
                Description = e.Description!
            });

            return Ok(result);
        }
    }
}
