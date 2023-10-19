using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class PartyType
{
    [DisplayName("Mã loại tiệc")]
    public int PartyTypeId { get; set; }

    [DisplayName("Loại tiệc")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}
