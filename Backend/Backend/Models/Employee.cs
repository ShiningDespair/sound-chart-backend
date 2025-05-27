using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents an employee in the Chinook database.
/// </summary>
public partial class Employee
{
    /// <summary>
    /// Gets or sets the unique identifier for the employee.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the last name of the employee.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the first name of the employee.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the job title of the employee.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the employee ID to whom this employee reports.
    /// </summary>
    public int? ReportsTo { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the employee.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the hire date of the employee.
    /// </summary>
    public DateTime? HireDate { get; set; }

    /// <summary>
    /// Gets or sets the address of the employee.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the city of the employee.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state of the employee.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the country of the employee.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the employee.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the employee.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the fax number of the employee.
    /// </summary>
    public string? Fax { get; set; }

    /// <summary>
    /// Gets or sets the email address of the employee.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the description or notes for the employee.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of customers associated with the employee.
    /// </summary>
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    /// <summary>
    /// Gets or sets the collection of employees who report to this employee.
    /// </summary>
    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    /// <summary>
    /// Gets or sets the employee to whom this employee reports.
    /// </summary>
    public virtual Employee? ReportsToNavigation { get; set; }
}
