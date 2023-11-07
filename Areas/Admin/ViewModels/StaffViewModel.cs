using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    [Keyless]
    public class StaffViewModel
    {
        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Mã nhân viên")]
        public int StaffId
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [Display(Name = "Tên nhân viên", Prompt = "Vd: Nguyễn Văn An")]
        public string? StaffName
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [RegularExpression(@"0(\d{9})", ErrorMessage = "{0} không hợp lệ")]
        [Display(Name = "Số điện thoại", Prompt = "Vd: 0123456789")]
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
        [Display(Name = "Địa chỉ", Prompt = "Vd: 23/10, Tổ 23, Kp23, P. Bình Đa, Tp. Biên Hoà")]
        public string? Address
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [RegularExpression(@"\d{12}", ErrorMessage = "{0} không hợp lệ")]
        [Display(Name = "Số CCCD", Prompt = "Vd: 012345678910")]
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

        //public string? UsersId
        //{
        //    get; set;
        //}

        public StaffViewModel()
        {
        }
        public StaffViewModel(Staff staff) : this()
        {
            StaffId = staff.StaffId;
            StaffName = staff.StaffName;
            StaffTypeId = staff.StaffTypeId;
            CitizenNumber = staff.CitizenNumber;
            PhoneNumber = staff.PhoneNumber;
            Address = staff.Address;
        }

        public Staff ToStaff(QlDichVuNauTiecLanHueContext context)
        {
            return new Staff()
            {
                StaffName = StaffName,
                PhoneNumber = PhoneNumber,
                Sex = Sex,
                Address = Address,
                CitizenNumber = CitizenNumber,

                // Foreign Object
                StaffType = context.StaffTypes.Find(StaffTypeId) ?? new StaffType(),
                //Users = context.AspNetUsers.Find(UsersId) ?? new AspNetUser(),
            };
        }
    }
}
