using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Party
{
    public const string HAS_MENU = "Đã chọn";
    public const string NO_MENU = "Chưa chọn";

    public const string COMING_SOON = "Sắp diễn ra";
    public const string GOING_ON = "Đang diễn ra";
    public const string DONE = "Đã xong";

    [DisplayName("Mã tiệc")]
    public int PartyId { get; set; }

    [DisplayName("Tên tiệc")]
    public string PartyName { get; set; } = null!;

    [DisplayName("Số bàn")]
    public int? Quantity { get; set; }

    [DisplayName("Ngày")]
    public DateTime Date { get; set; }

    [DisplayName("Giờ")]
    public TimeSpan? Time { get; set; }

    [DisplayName("Nơi tổ chức")]
    public string? Location { get; set; }

    [DisplayName("Ghi chú")]
    public string? Note { get; set; }

    [DisplayName("Trạng thái tiệc")]
    public string? Status { get; set; }

    [DisplayName("Chọn món ăn")]
    public bool HasMenu { get; set; }

    public int PartyTypeId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual PartyType PartyType { get; set; } = null!;

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
