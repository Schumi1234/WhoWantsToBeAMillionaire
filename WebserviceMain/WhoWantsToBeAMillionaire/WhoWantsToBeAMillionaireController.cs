using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.WhoWantsToBeAMillionaire
{
	[Route("[controller]")]
	[ApiController]
	public class WhoWantsToBeAMillionaireController : Controller
	{
		private WhoWantsToBeAMillionaireHandler _gameHandler;

		public WhoWantsToBeAMillionaireController(WhoWantsToBeAMillionaireHandler gameHandler)
		{
			_gameHandler = gameHandler;
		}

		[Route("Categories")]
		[HttpGet]
		public IEnumerable<Category> GetCategories()
		{
			return _gameHandler.GetCategories();
		}
	}
}
