using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents a country in the Chinook database.
/// </summary>
public partial class Country
{
    /// <summary>
    /// Gets or sets the unique identifier for the country.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string? CountryName { get; set; }

    /// <summary>
    /// Gets or sets the ISO code for the country.
    /// </summary>
    public int? CountryIsoCode { get; set; }

    /// <summary>
    /// Gets or sets the country code for the country.
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the collection of customers associated with the country.
    /// </summary>
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
