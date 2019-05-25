using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.Models.Game;

namespace WebMain.Game.Helper
{
	public class GameHelper
	{
		private readonly WebserviceProvider _webserviceProvider;

		public GameHelper(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}

		public IEnumerable<CategoryModel> GetCategories()
		{
			return _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(@"https://localhost:44339/WhoWantsToBeAMillionaire/Categories");
		}

		public IEnumerable<QuestionModel> GetQuestions(IEnumerable<int> categoryIds)
		{
			var jsonObject = JsonConvert.SerializeObject(categoryIds);
			return _webserviceProvider.PostDataFromWebService<IEnumerable<QuestionModel>>(@"https://localhost:44339/WhoWantsToBeAMillionaire/Question", jsonObject);
		}

		public IEnumerable<AnswerModel> GetAnswers(int questionId)
		{
			var jsonObject = JsonConvert.SerializeObject(questionId);
			return _webserviceProvider.PostDataFromWebService<IEnumerable<AnswerModel>>(@"https://localhost:44339/WhoWantsToBeAMillionaire/Answers", jsonObject);
		}

		public void SetNextQuestion(GameRoundViewModel gameRound)
		{
			gameRound.QuestionForTheRound = gameRound.QuestionsToPlay.First();
			gameRound.QuestionsToPlay.Remove(gameRound.QuestionForTheRound);
		}

		public bool SaveGame()
		{
			return _webserviceProvider.GetDataFromWebService<bool>(@"https://localhost:44339/WhoWantsToBeAMillionaire/SaveGame");
		}
	}
}
