using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("Loại nhân viên")]
        public int StaffTypeId
        {
            get; set;
        }

        //public string? UsersId
        //{
        //    get; set;
        //}

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
