using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents a customer in the Chinook database.
/// </summary>
public partial class Customer
{
    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the customer.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the company name associated with the customer.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Gets or sets the address of the customer.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the city of the customer.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the state of the customer.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the customer.
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the fax number of the customer.
    /// </summary>
    public string? Fax { get; set; }

    /// <summary>
    /// Gets or sets the email address of the customer.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the support representative's employee ID.
    /// </summary>
    public int? SupportRepId { get; set; }

    /// <summary>
    /// Gets or sets the country ID associated with the customer.
    /// </summary>
    public int? CountryId { get; set; }

    /// <summary>
    /// Gets or sets the country associated with the customer.
    /// </summary>
    public virtual Country? Country { get; set; }

    /// <summary>
    /// Gets or sets the collection of invoices for the customer.
    /// </summary>
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    /// <summary>
    /// Gets or sets the support representative associated with the customer.
    /// </summary>
    public virtual Employee? SupportRep { get; set; }
}
