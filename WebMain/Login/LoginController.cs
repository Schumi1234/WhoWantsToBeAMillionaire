using System;
using Microsoft.AspNetCore.Mvc;
using WebMain.DataServiceProvider;

namespace WebMain.Login
{
	public class LoginController : Controller
	{
		private readonly WebserviceProvider _webserviceProvider;

		public LoginController(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public void DoLogin()
		{
			throw new NotImplementedException();
		}

		public IActionResult Return()
		{
			return RedirectToAction("Home", "Game");
		}
	}
}
