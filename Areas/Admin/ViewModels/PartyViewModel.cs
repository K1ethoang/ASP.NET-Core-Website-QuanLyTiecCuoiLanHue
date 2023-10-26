using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models.Party;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    public class PartyViewModel
    {

        // PartyName
        [Required(ErrorMessage = "{0} không được trống")]
        [StringLength(maximumLength: 255, MinimumLength = 0, ErrorMessage = "Độ dài tối đa 255 ký tự")]
        [DisplayName("Tên tiệc")]
        public string PartyName { get; set; } = null!;
        // Quantity
        [Required(ErrorMessage = "{0} không được trống")]
        [Range(0, 100, ErrorMessage = "{0} trong khoảng [{1} và {2}]")]
        [DisplayName("Số bàn")]
        public int? Quantity { get; set; }
        // Date
        [Required(ErrorMessage = "{0} không được trống")]
        [DataType(DataType.Date, ErrorMessage = "{0} Không hợp lệ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CheckDateRange(ErrorMessage = "Phải lớn hơn ngày hôm nay")]
        [DisplayName("Ngày")]
        public DateTime? Date { get; set; }
        // Time
        [Required(ErrorMessage = "{0} không được trống")]
        [DataType(DataType.Time, ErrorMessage = "{0} Không hợp lệ")]
        [DisplayName("Giờ")]
        public TimeSpan? Time { get; set; }
        [DisplayName("Ghi chú")]
        public string? Note
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
        //CustomerId
        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Khách hàng")]
        public int CustomerId { get; set; }
        //PartyTypeId
        [DisplayName("Loại tiệc")]
        public int PartyTypeId { get; set; }
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

        public Party ToParty(QlDichVuNauTiecLanHueContext context)
        {
            return new Party() {
            PartyName = PartyName,
            Status = Party.COMING_SOON,
            HasMenu = false,
            Date = Date,
            Time = Time,
            Quantity = Quantity,
            Location = Location,
            Note = Note,
            PartyTypeId = PartyTypeId,
            CustomerId = CustomerId,
            // Foreign Objects
            PartyType = context.PartyTypes.Find(PartyTypeId) ?? new PartyType(),
            Customer = context.Customers.Find(CustomerId) ?? new Customer(),


            };
        }
    }
}
