using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Work
{
    public int StaffId { get; set; }

    public int PartyId { get; set; }

    public decimal? Salary { get; set; }

    public virtual Party Party { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
