﻿using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string? BillingAddress { get; set; }

    public string? BillingCity { get; set; }

    public string? BillingState { get; set; }

    public string? BillingCountry { get; set; }

    public string? BillingPostalCode { get; set; }

    public decimal Total { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}
