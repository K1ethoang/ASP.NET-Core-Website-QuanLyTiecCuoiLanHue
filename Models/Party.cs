using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Tiệc")]
public partial class Party
{
    public const string HAS_MENU = "Đã chọn";
    public const string NO_MENU = "Chưa chọn";

    public const string COMING_SOON = "Sắp diễn ra";
    public const string GOING_ON = "Đang diễn ra";
    public const string DONE = "Đã xong";

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }

    }

    [DisplayName("Mã tiệc")]
    public int PartyId
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [StringLength(maximumLength: 255, MinimumLength = 0, ErrorMessage = "Độ dài tối đa 255 ký tự")]
    [DisplayName("Tên tiệc")]
    public string PartyName { get; set; } = null!;

    [Required(ErrorMessage = "{0} không được trống")]
    [Range(0, 100, ErrorMessage = "{0} trong khoảng [{1} và {2}]")]
    [DisplayName("Số bàn")]
    public int? Quantity
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DataType(DataType.Date, ErrorMessage = "{0} Không hợp lệ")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [CheckDateRange(ErrorMessage = "Phải lớn hơn ngày hôm nay")]
    [DisplayName("Ngày")]
    public DateTime? Date
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DataType(DataType.Time, ErrorMessage = "{0} Không hợp lệ")]
    [DisplayName("Giờ")]
    public TimeSpan? Time
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Độ dài tối đa 400 ký tự")]
    [DisplayName("Nơi tổ chức")]
    public string? Location
    {
        get; set;
    }

    [DisplayName("Ghi chú")]
    public string? Note
    {
        get; set;
    }

    [DisplayName("Trạng thái tiệc")]
    public string? Status
    {
        get; set;
    }

    [DisplayName("Chọn món ăn")]
    public bool HasMenu
    {
        get; set;
    }

    [DisplayName("Loại tiệc")]
    public int PartyTypeId
    {
        get; set;
    }

    [DisplayName("Khách hàng")]
    public int CustomerId
    {
        get; set;
    }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual PartyType PartyType { get; set; } = null!;

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
