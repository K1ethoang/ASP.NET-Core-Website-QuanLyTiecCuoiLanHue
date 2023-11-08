using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Helpers
{
	public static class NavigationIndicatorHelper
	{
		public static string MakeActiveClass(this IUrlHelper urlHelper, string controller, string action)
		{
			try
			{
				string result = "active";
				string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
				string methodName = urlHelper.ActionContext.RouteData.Values["action"].ToString();
				if (string.IsNullOrEmpty(controllerName))
					return null;
				if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
				{
					if (action == "" || methodName.Equals(action, StringComparison.OrdinalIgnoreCase))
					{
						return result;
					}
				}
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static string MakeMenuOpenClass(this IUrlHelper urlHelper, string controller)
		{
			try
			{
				string result = "menu-open";
				string controllerName = urlHelper.ActionContext.RouteData.Values["controller"].ToString();


				if (string.IsNullOrEmpty(controllerName))
					return null;

				if (controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
				{
					return result;
				}
				return null;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
