namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models
{
    public class ErrorViewModel
    {
        public string? RequestId
        {
            get; set;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}