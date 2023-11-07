using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Vị trí")]
public partial class StaffType
{
    public int StaffTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
