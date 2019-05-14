using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.Models.Game;

namespace WebMain.Game
{
	public class GameController : Controller
	{
		private readonly WebserviceProvider _webserviceProvider;

		public GameController(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}
		public IActionResult DetailCategories()
		{
			var categories = GetCategories();
			var gameViewModel = new GameCategoriesViewModel()
			{
				Categories = categories
			};

			return View();
		}

		public IActionResult Home()
		{
			return View();
		}

		private IEnumerable<CategoryModel> GetCategories()
		{
			return _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(@"https://localhost:44339/WhoWantsToBeAMillionaire/Categories");
		}
	}
}
