namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Helpers
{
    public static class Helper
    {
        public static string FormatMoney(decimal money, bool hasUnit)
        {
            if (hasUnit)
            {
                return money.ToString("#,##").Replace(",", ".") + 'đ';
            }
            return money.ToString("#,##").Replace(",", ".");
        }
    }
}
