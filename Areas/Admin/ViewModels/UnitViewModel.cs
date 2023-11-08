using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    [Keyless]
    public class UnitViewModel
    {
        [Required(ErrorMessage = "{0} không được trống")]
        [DisplayName("Mã đơn vị tính")]
        public int UnitId
        {
            get; set;
        }
        [Required(ErrorMessage = "{0} không được trống")]
        [StringLength(20, MinimumLength = 0, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        [Display(Name = "Tên đơn vị tính", Prompt = "Vd: Tô")]
        public string UnitName { get; set; } = null!;


        [StringLength(50, MinimumLength = 0, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        [Display(Name = "Mô tả", Prompt = "Vd: 1 đĩa")]
        public string? Description
        {
            get; set;
        }

        public UnitViewModel()
        {
        }

        public UnitViewModel(Unit unit) : this()
        {
            UnitId = unit.UnitId;
            UnitName = unit.UnitName;
            Description = unit.Description;
        }

        public Unit ToUnit(QlDichVuNauTiecLanHueContext context)
        {
            return new Unit()
            {
                UnitId = UnitId,
                UnitName = UnitName,
                Description = Description,
            };
        }
    }
}
