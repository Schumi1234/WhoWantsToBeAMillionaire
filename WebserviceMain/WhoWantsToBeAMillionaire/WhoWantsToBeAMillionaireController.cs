using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace WebserviceMain.WhoWantsToBeAMillionaire
{
	[Route("[controller]")]
	[ApiController]
	public class WhoWantsToBeAMillionaireController : Controller
	{
		private readonly WhoWantsToBeAMillionaireHandler _gameHandler;

		public WhoWantsToBeAMillionaireController(WhoWantsToBeAMillionaireHandler gameHandler)
		{
			_gameHandler = gameHandler;
		}

		[Route("Categories")]
		[HttpGet]
		public IEnumerable<CategoryModel> GetCategories()
		{
			return _gameHandler.GetCategories();
		}

		[Route("Question")]
		[HttpPost]
		public IEnumerable<QuestionModel> GetRandomQuestion(IEnumerable<int> categoryIds)
		{
			return _gameHandler.GetQuestions(categoryIds);
		}

		[Route("Ranking")]
		[HttpGet]
		public IEnumerable<RankingModel> GetRanking()
		{
			return _gameHandler.GetRanking();
		}

		[Route("Answers")]
		[HttpPost]
		public IEnumerable<AnswerModel> GetAnswers(int questionId)
		{
			return _gameHandler.GetAnswers(questionId);
		}

		[Route("ValidateAnswer")]
		[HttpPost]
		public bool CheckAnswer(int answerId)
		{
			return _gameHandler.CheckAnswer(answerId);
		}

		[Route("SaveGame")]
		[HttpGet]
		public bool SaveGame(SaveGameRequestModel playerName)
		{
			return _gameHandler.SaveGame(playerName);
		}
	}
}
