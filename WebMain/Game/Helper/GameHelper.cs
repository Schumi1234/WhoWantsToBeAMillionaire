using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.DataServiceProvider.Enums;
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
			return _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.WhoWantsToBeAMillionaire.ToString(), "Categories");
		}

		public IEnumerable<QuestionModel> GetQuestions(IEnumerable<int> categoryIds)
		{
			var jsonObject = JsonConvert.SerializeObject(categoryIds);
			return _webserviceProvider.PostDataFromWebService<IEnumerable<QuestionModel>>(Controllers.WhoWantsToBeAMillionaire.ToString(), "Question", jsonObject);
		}

		public IEnumerable<AnswerModel> GetAnswers(int questionId)
		{
			var jsonObject = JsonConvert.SerializeObject(questionId);
			return _webserviceProvider.PostDataFromWebService<IEnumerable<AnswerModel>>(Controllers.WhoWantsToBeAMillionaire.ToString(), "Answers", jsonObject);
		}

		public void SetNextQuestion(GameRoundViewModel gameRound)
		{
			gameRound.QuestionForTheRound = gameRound.QuestionsToPlay.First();
			gameRound.QuestionsToPlay.Remove(gameRound.QuestionForTheRound);
		}

		public bool SaveGame(GameRoundViewModel  viewModel)
		{
			var requestModel = new SaveGameRequestModel
			{
				Score = viewModel.Score,
				GameEnd = viewModel.GameEnd,
				PlayerName = viewModel.PlayerName,
				GameBegin = viewModel.GameBegin,
				Categories = viewModel.Categories.Select(a => new CategoryModel
				{
					CategoryId = a.CategoryId
				}).ToList()
			};
			var content = JsonConvert.SerializeObject(requestModel);
			return _webserviceProvider.PostDataFromWebService<bool>(Controllers.WhoWantsToBeAMillionaire.ToString(),"SaveGame", content);
		}

		public IEnumerable<AnswerModel> Joker(int questionId)
		{
			var content = JsonConvert.SerializeObject(questionId);
			return _webserviceProvider.PostDataFromWebService<IEnumerable<AnswerModel>>(Controllers.Joker.ToString(), "FiftyFifty", content);
		}

		public bool SaveQuestionAnswer(bool correct, int questionId)
		{
			var requestModel = new SaveQuestionAnswerRequestModel
			{
				Correct = correct,
				QuestionId = questionId
			};
			var content = JsonConvert.SerializeObject(requestModel);
			return _webserviceProvider.PostDataFromWebService<bool>(Controllers.WhoWantsToBeAMillionaire.ToString(), "SaveQuestionAnswer", content);
		}
	}
}
