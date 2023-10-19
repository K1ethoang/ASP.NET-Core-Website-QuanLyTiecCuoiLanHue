using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public DateTime? PaymentTime { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? Deposit { get; set; }

    public decimal? Total { get; set; }

    public int PartyId { get; set; }

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

    public virtual Party Party { get; set; } = null!;
}
