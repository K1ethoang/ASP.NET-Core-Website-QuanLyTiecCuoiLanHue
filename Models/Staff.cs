using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? Sex { get; set; }

    public string? Address { get; set; }

    public string? CitizenNumber { get; set; }

    public int StaffTypeId { get; set; }

    public string? UsersId { get; set; }

    public virtual StaffType StaffType { get; set; } = null!;

    public virtual AspNetUser? Users { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
