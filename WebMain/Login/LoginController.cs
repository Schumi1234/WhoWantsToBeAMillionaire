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
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				var viewModel = new LoginViewModel {ErrorMessage = "Alle Textboxen müssen ausgefüllt werden"};
				return View(viewModel);
			}
			var success = DoLogin(username, password);
			if (success)
			{
				return RedirectToAction("Home", "Game");
			}

			var model = new LoginViewModel
			{
				ErrorMessage = "Benutzer existiert nicht"
			};
			return View(model);
		}

		private bool DoLogin(string username, string password)
		{
			var requestModel = new LoginRequestModel
			{
				Username = username,
				Password = SecurePassword(password)
			};

			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Login.ToString(), "DoLogin", content);
			return success;
		}

		public IActionResult Return()
		{
			return RedirectToAction("Home", "Game");
		}

		// "Secure"
		private static string SecurePassword(string password)
		{
			var replace = password.ToLower().Replace('a', '&');
			var s = replace.Replace('b', '+');
			var replace1 = s.Replace('c', '*');
			var s1 = replace1.Replace('d', ')');
			var replace2 = s1.Replace('e', 'ç');
			var s2 = replace2.Replace('f', '?');
			var replace3 = s2.Replace('g', '%');
			var s3 = replace3.Replace('h', '#');
			var replace4 = s3.Replace('i', '§');
			var replace5 = replace4.Replace('j', '/');
			var s4 = replace5.Replace('k', '[');
			var s5 = s4.Replace('l', '$');
			var replace6 = s5.Replace('m', '=');
			var s6 = replace6.Replace('n', '(');
			var s7 = s6.Replace('o', ']');
			var replace7 = s7.Replace('p', '!');
			var replace8 = replace7.Replace('q', '£');
			var s8 = replace8.Replace('r', ';');
			var s9 = s8.Replace('s', '<');
			var s10 = s9.Replace('t', '>');
			var s11 = s10.Replace('u', '{');
			var s12 = s11.Replace('v', '}');
			var s13 = s12.Replace('w', '¢');
			var s14 = s13.Replace('x', 'a');
			var s15 = s14.Replace('y', 't');
			var s16 = s15.Replace('z', 'm');

			return s16;
		}
	}
}
