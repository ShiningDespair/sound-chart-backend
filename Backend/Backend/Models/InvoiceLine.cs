using System;
using System.Collections.Generic;

namespace Backend.Models;

/// <summary>
/// Represents a line item in an invoice, linking a track to an invoice with quantity and price.
/// </summary>
public partial class InvoiceLine
{
    /// <summary>
    /// Gets or sets the unique identifier for the invoice line.
    /// </summary>
    public int InvoiceLineId { get; set; }

    /// <summary>
    /// Gets or sets the invoice ID to which this line belongs.
    /// </summary>
    public int InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the track ID for the item being invoiced.
    /// </summary>
    public int TrackId { get; set; }

    /// <summary>
    /// Gets or sets the unit price for the track.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the track being invoiced.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the invoice associated with this line.
    /// </summary>
    public virtual Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Gets or sets the track associated with this line.
    /// </summary>
    public virtual Track Track { get; set; } = null!;
}
