using System;
using System.Collections.Generic;

namespace ScaffoldDemo;

public partial class Invoice
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset InvoiceDate { get; set; }

    public DateTimeOffset DueDate { get; set; }

    public string Status { get; set; } = null!;
}
