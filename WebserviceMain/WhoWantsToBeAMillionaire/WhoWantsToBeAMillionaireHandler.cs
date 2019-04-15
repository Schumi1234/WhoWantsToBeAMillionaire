using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database;

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
			return _databaseController
				.GetCategories()
				.Select(a => new CategoryModel
				{
					Category = a.strName,
					CategoryId = a.intCategoryId
				});
		}

		public QuestionModel GetRandomQuestion(int categoryId)
		{
			var question = _databaseController.GetRandomQuestion(categoryId);

			return new QuestionModel
			{
				Question = question.strName,
				QuestionId = question.intQuestionId
			};
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
				AnswerId = a.intAnswerID
			});
		}

		public bool CheckAnswer(int answerId)
		{
			return _databaseController.GetAnswer(answerId).blnCorrect;
		}
	}
}
