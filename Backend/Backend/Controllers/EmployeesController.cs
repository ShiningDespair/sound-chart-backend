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
    /// <summary>
    /// Controller for managing employees in the Chinook database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ChinookContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public EmployeesController(ChinookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of employees with their total sales amount, grouped by employee name for D3.js PieChart.
        /// </summary>
        /// <returns>A list of <see cref="Employee"/> objects.</returns>
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



        /// <summary>
        /// Retrieves detailed information about a specific employee by their ID.
        /// </summary>
        /// <param name="id">The id of employee to retrieve</param>
        /// <returns>An object of <see cref="Employee"/>.</returns>
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

        /// <summary>
        /// Retrieves a list of comments associated with a specific employee by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of <see cref="CommentDto"/> objects. </returns>
        [HttpGet("{id}/comments")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsOfEmployee(int id)
        {
            var comments = await _context.Comments
                .Where(co => co.EmployeeId == id)
                .Select(co => new CommentDto
                {
                    id = id,
                    text = co.Comment1
                })
                .ToListAsync();

            return Ok(comments);
        }

        /// <summary>
        /// Creates a new comment for an employee.
        /// </summary>
        /// <param name="commentDto"></param>
        /// <returns></returns>
        [HttpPost("{id}/comments")]
        public async Task<ActionResult<CommentDto>> PostComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null || string.IsNullOrWhiteSpace(commentDto.text))
            {
                return BadRequest("Comment text cannot be empty.");
            }

            var comment = new Comment
            {
                EmployeeId = commentDto.id,
                Comment1 = commentDto.text
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("comments/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            _context.Entry(comment).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok("Successfuly Deleted");
        }



    }
}
