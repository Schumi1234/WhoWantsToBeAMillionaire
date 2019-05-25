using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.DataServiceProvider.Enums;
using WebMain.Models.Login;

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
			return View(new LoginViewModel());
		}
		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			var success = DoLogin(username, password);
			return RedirectToAction("Home", "Game");
		}

		private bool DoLogin(string username, string password)
		{
			var requestModel = new LoginRequestModel
			{
				Password = "TestPassword",
				Username = username
			};

			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Login.ToString(), "DoLogin", content);
			return success;
		}

		public IActionResult Return()
		{
			return RedirectToAction("Home", "Game");
		}
	}
}
