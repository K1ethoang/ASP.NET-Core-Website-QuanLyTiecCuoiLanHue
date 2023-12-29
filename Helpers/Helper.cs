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
    public static class PrivateLogger
    {
        public static void Log(string message) { Console.WriteLine(message); }
        public static void LogError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red; 
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
