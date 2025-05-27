using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents an invoice in the Chinook database.
/// </summary>
public partial class Invoice
{
    /// <summary>
    /// Gets or sets the unique identifier for the invoice.
    /// </summary>
    public int InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the customer ID associated with the invoice.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the date the invoice was created.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Gets or sets the billing address for the invoice.
    /// </summary>
    public string? BillingAddress { get; set; }

    /// <summary>
    /// Gets or sets the billing city for the invoice.
    /// </summary>
    public string? BillingCity { get; set; }

    /// <summary>
    /// Gets or sets the billing state for the invoice.
    /// </summary>
    public string? BillingState { get; set; }

    /// <summary>
    /// Gets or sets the billing country for the invoice.
    /// </summary>
    public string? BillingCountry { get; set; }

    /// <summary>
    /// Gets or sets the billing postal code for the invoice.
    /// </summary>
    public string? BillingPostalCode { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the invoice.
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the invoice.
    /// </summary>
    public virtual Customer Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of invoice lines for the invoice.
    /// </summary>
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}
