using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace WebserviceMain.Joker
{
	[Route("[controller]")]
	[ApiController]
	public class JokerController : Controller
	{
		private readonly JokerHandler _jokerHandler;

		public JokerController(JokerHandler jokerHandler)
		{
			_jokerHandler = jokerHandler;
		}

		[HttpPost]
		[Route("fiftyFifty")]
		public IEnumerable<AnswerModel> FiftyFiftyJoker(int questionId)
		{
			return _jokerHandler.FiftyFiftyJoker(questionId);
		}
	}
}
