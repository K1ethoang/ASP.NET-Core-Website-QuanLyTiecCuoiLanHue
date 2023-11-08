using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    [Keyless]
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Mã khách hàng")]
        public int CustomerId
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [Display(Name = "Tên nhân viên", Prompt = "Vd: Nguyễn Văn An")]
        public string? CusName
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [RegularExpression(@"(0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "{0} không hợp lệ")]
        [Display(Name = "Số điện thoại", Prompt = "Vd: 0123456789")]
        public string? PhoneNumber
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Giới tính")]
        public bool? Sex
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

        public CustomerViewModel()
        {
        }
        public CustomerViewModel(Customer customer) : this()
        {
            CustomerId = customer.CustomerId;
            CusName = customer.CusName;
            PhoneNumber = customer.PhoneNumber;
            Sex = customer.Sex;
            Address = customer.Address;
            CitizenNumber = customer.CitizenNumber;
        }

        public Customer ToCustomer(QlDichVuNauTiecLanHueContext context)
        {
            return new Customer()
            {
                CustomerId = CustomerId,
                CusName = CusName,
                PhoneNumber = PhoneNumber,
                Sex = Sex,
                Address = Address,
                CitizenNumber = CitizenNumber,
            };
        }
    }
}
