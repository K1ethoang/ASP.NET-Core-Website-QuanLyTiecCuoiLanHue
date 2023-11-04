using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Nhân viên")]
public partial class Staff
{
    public const string MALE = "Nam";
    public const string FEMALE = "Nữ";

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Mã nhân viên")]
    public int StaffId
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Tên nhân viên")]
    public string? StaffName
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Số điện thoại")]
    public string? PhoneNumber
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Giới tính")]
    public bool Sex
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Địa chỉ")]
    public string? Address
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Số CCCD")]
    public string? CitizenNumber
    {
        get; set;
    }

    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Loại nhân viên")]
    public int StaffTypeId
    {
        get; set;
    }

    public string? UsersId
    {
        get; set;
    }

    public virtual StaffType StaffType { get; set; } = null!;

    public virtual AspNetUser? Users
    {
        get; set;
    }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
