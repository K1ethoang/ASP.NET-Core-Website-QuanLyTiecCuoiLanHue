﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models.Party;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    [Keyless]
    public class PartyViewModel
    {
        //[DisplayName("Mã tiệc")]
        //public int PartyId { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"
            //,	ApplyFormatInEditMode = true
            )]
        [CheckDateRange(ErrorMessage = "Phải lớn hơn ngày hôm nay")]
        [DisplayName("Ngày")]
        public DateTime? Date
        {
            get; set;
        }
        // Time
        [Required(ErrorMessage = "{0} không được trống")]
        [DataType(DataType.Time, ErrorMessage = "{0} Không hợp lệ")]
        [DisplayName("Giờ")]
        public TimeSpan? Time { get; set; }
        [DisplayName("Ghi chú")]
        public string? Note { get; set; }
        [Required(ErrorMessage = "{0} không được trống")]
        [StringLength(maximumLength: 400, MinimumLength = 0, ErrorMessage = "Độ dài tối đa 400 ký tự")]
        [DisplayName("Nơi tổ chức")]
        public string? Location { get; set; }
        //CustomerId
        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Khách hàng")]
        public int CustomerId { get; set; }
        //PartyTypeId
        [DisplayName("Loại tiệc")]
        public int PartyTypeId { get; set; }
		// HASMENU
		[DisplayName("Thực đơn")]
		public bool HasMenu
        {
            get; set;
        } = false;

        public PartyViewModel() { }
        public PartyViewModel(Party party) : this()
        {
            PartyName = party.PartyName;
            Quantity = party.Quantity;
            Date = party.Date!;
            Time = party.Time;
            Location = party.Location;
            CustomerId = party.CustomerId;
            PartyTypeId = party.PartyTypeId;
            Note = party.Note;
            HasMenu = party.HasMenu;
        }
        public class CheckDateRangeAttribute : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                if (value == null) return new ValidationResult(ErrorMessage);

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
            return new Party()
            {
                PartyName = PartyName,
                Status = Party.COMING_SOON,
                HasMenu = HasMenu,
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
    [Keyless]
    public class PartyDetailsViewModel : PartyViewModel
    {
        [Display(Name = "")]
        public string CustomerName { get; set; }
        [Display(Name = "")]
        public string PartyTypeName { get; set; }
        [DisplayName("Trạng thái tiệc")]
        public string Status { get; set; }
        [DisplayName("Mã tiệc")]
        public int PartyId { get; set; }
        [DisplayName("Trạng thái thanh toán")]
        public bool HasCharges { get; set; } // paymenttime!=null or deposit !=null

        public ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();
    }
    [Keyless]
    public class InvoiceDetailsViewModel
    {
		[DisplayName("Mã tiệc")]
		public int PartyId { get; set; }
        [DisplayName("")]
		public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; } = null;

		public ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

        public Party Party { get; set; } = null;

        public Customer Customer { get; set; } = null;

        
	}
}
