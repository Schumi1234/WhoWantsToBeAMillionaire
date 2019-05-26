using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.WhoWantsToBeAMillionaire
{
	public class WhoWantsToBeAMillionaireHandler
	{
		private readonly DatabaseController _databaseController;

		public WhoWantsToBeAMillionaireHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public IEnumerable<CategoryModel> GetCategories()
		{
			var categoriesWithQuestions = _databaseController
				.GetAllQuestions()
				.Select(a => a.intCategoryId);
			var categories = _databaseController.GetCategories();

			var playableCategories = categories
				.Where(a => categoriesWithQuestions
					.Any(b => b == a.intCategoryId));

			return playableCategories
				.Select(a => new CategoryModel
				{
					Category = a.strName,
					CategoryId = a.intCategoryId
				});
		}

		public IEnumerable<QuestionModel> GetQuestions(IEnumerable<int> categoryIds)
		{
			var questions = _databaseController.GetQuestionsByCategories(categoryIds);

			return questions.Select(question =>
			{
				var answeredCorrectlyPercentage = question.intAnsweredWrong + question.intAnsweredCorrectly > 0 ? question.intAnsweredCorrectly / (question.intAnsweredWrong + question.intAnsweredCorrectly + 0.0) * 100.0 : 0.0;
				return new QuestionModel
				{
					Question = question.strName,
					QuestionId = question.intQuestionId,
					AnsweredCorrectlyPercentage = answeredCorrectlyPercentage,
					NumberAnsweredCorrectly = question.intAnsweredCorrectly
				};
			});
		}

		public IEnumerable<RankingModel> GetRanking()
		{
			return _databaseController.GetRanking();
		}

		public IEnumerable<AnswerModel> GetAnswers(int questionId)
		{
			return _databaseController.GetAnswers(questionId).Select(a => new AnswerModel
			{
				Answer = a.strName,
				AnswerId = a.intAnswerID,
				Correct = a.blnCorrect
			});
		}

		public bool CheckAnswer(int answerId)
		{
			var answer = _databaseController.GetAnswer(answerId);
			var question = _databaseController.GetQuestionById(answer.intQuestionID);
			if (answer.blnCorrect)
			{
				question.intAnsweredCorrectly += 1;
				var questions = new List<Question>
				{
					question
				};
				_databaseController.InsertUpdate(questions);
			}
			else
			{
				question.intAnsweredWrong += 1;
				var questions = new List<Question>
				{
					question
				};
				_databaseController.InsertUpdate(questions);
			}

			return answer.blnCorrect;
		}

		public bool SaveGame(SaveGameRequestModel saveGameRequestModel)
		{
			var newGame = new Game
			{
				datBegin = saveGameRequestModel.GameBegin,
				intScore = saveGameRequestModel.Score,
				strPlayerName = saveGameRequestModel.PlayerName,
				datEnd = saveGameRequestModel.GameEnd
			};

			var newGameList = new List<Game>
			{
				newGame
			};
			_databaseController.InsertUpdate(newGameList);

			return true;
		}
	}
}
