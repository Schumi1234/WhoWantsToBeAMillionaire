using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Newtonsoft.Json;
using SharedModels;

namespace WebMain.Game
{
	public class GameController : Controller
	{
		public IActionResult GetCategories()
		{
			var client = new HttpClient();
			var response = client.GetAsync(new Uri(@"https://localhost:44339/WhoWantsToBeAMillionaire/Categories")).Result;
			if (response.IsSuccessStatusCode)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				var test = JsonConvert.DeserializeObject<IEnumerable<CategoryModel>>(responseString);
				return View();
			}

		}
	}
}
