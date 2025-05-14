using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var query = from c in _context.Customers
                        join i in _context.Invoices on c.CustomerId equals i.CustomerId
                        join e in _context.Employees on c.SupportRepId equals e.EmployeeId
                        group i by new { e.FirstName, e.LastName, e.EmployeeId } into g
                        select new {
                            g.Key.EmployeeId,
                            FullName = g.Key.FirstName +" " + g.Key.LastName,
                            TotalSold = g.Sum(i => i.Total)
                        };

            var result = await query.OrderByDescending(x => x.TotalSold)
                                    .ToListAsync();

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

            var result = employees.Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Title,
                e.Country,
                e.City,
                e.Description
            });

            return Ok(result);
        }


        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
