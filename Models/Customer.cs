using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CusName { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? Sex { get; set; }

    public string? Address { get; set; }

    public string? CitizenNumber { get; set; }

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}
