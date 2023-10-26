using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("khách hàng")]
public partial class Customer
{
    [DisplayName("Mã khách hàng")]
    public int CustomerId { get; set; }

    [DisplayName("Tên khách hàng")]
    public string? CusName { get; set; }

    [DisplayName("SĐT khách hàng")]
    public string? PhoneNumber { get; set; }

    [DisplayName("Giới tính")]
    public bool? Sex { get; set; }

    [DisplayName("Địa chỉ")]
    public string? Address { get; set; }

    [DisplayName("Số CCCD")]
    public string? CitizenNumber { get; set; }

    public virtual ICollection<Party> Parties { get; set; } = new List<Party>();
}
