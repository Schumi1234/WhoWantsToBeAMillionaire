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
		[Route("FiftyFifty")]
		public IEnumerable<AnswerModel> FiftyFiftyJoker([FromBody] int questionId)
		{
			return _jokerHandler.FiftyFiftyJoker(questionId);
		}
	}
}
