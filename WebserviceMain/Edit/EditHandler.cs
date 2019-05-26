using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Edit
{
	public class EditHandler
	{
		private readonly DatabaseController _databaseController;

		public EditHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public IEnumerable<CategoryModel> GetAllCategories()
		{
			return _databaseController
				.GetCategories()
				.Select(a => new CategoryModel
				{
					Category = a.strName,
					CategoryId = a.intCategoryId
				});

		}

		public bool SaveEditedCategories(SaveCategoriesRequestModel requestModel)
		{
			bool success;
			IEnumerable<Category> categoryTable;
			if (requestModel.EditedCategories.Any(a => a.Delete && a.Id != 0))
			{

				var game2CategoryTable = _databaseController.GetGame2Categories(requestModel.EditedCategories.Where(a => a.Delete).Select(a => a.Id));
				categoryTable = requestModel.EditedCategories.Where(a => a.Delete).Select(a => new Category
				{
					intCategoryId = a.Id,
					strName = a.Name
				});

				success = _databaseController.Delete(categoryTable);
				if (!success)
				{
					return false;
				}
			}

			categoryTable = requestModel.EditedCategories.Where(a => !a.Delete).Select(a => new Category
			{
				intCategoryId = a.Id,
				strName = a.Name
			});

			success = _databaseController.InsertUpdate(categoryTable);

			return success;
		}

		public IEnumerable<QuestionModel> GetAllQuestions()
		{
			return _databaseController
				.GetAllQuestions()
				.Select(a => new QuestionModel
				{
					Question = a.strName,
					QuestionId = a.intQuestionId,
					CategoryId = a.intCategoryId
				});
		}

		public IEnumerable<AnswerModel> GetAllAnswers()
		{
			return _databaseController.GetAllAnswers().Select(a => new AnswerModel
			{
				Answer = a.strName,
				AnswerId = a.intAnswerID,
				QuestionId = a.intQuestionID,
				Correct = a.blnCorrect
			});
		}

		public QuestionModel GetQuestionById(int questionId)
		{
			var question = _databaseController.GetQuestionById(questionId);
			return new QuestionModel
			{
				Question = question.strName,
				QuestionId = question.intQuestionId,
				CategoryId = question.intCategoryId
			};
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

		public bool SaveQuestionAndAnswers(SaveQuestionAndAnswersRequestModel requestModel)
		{
			IEnumerable<Question> questionTable = new List<Question>
			{
				new Question
				{
					intCategoryId = requestModel.Question.CategoryId,
					intQuestionId = requestModel.Question.QuestionId,
					strName = requestModel.Question.Question
				}
			};

			var success = _databaseController.InsertUpdate(questionTable);
			if (!success)
			{
				return false;
			}

			if (requestModel.Question.QuestionId == 0)
			{
				requestModel.Question.QuestionId = _databaseController
					.GetAllQuestions()
					.OrderByDescending(a => a.intQuestionId)
					.First()
					.intQuestionId;
			}

			var answerTable = requestModel
				.Answers
				.Select(a => new Answer
				{
					blnCorrect = a.Correct,
					intAnswerID = a.AnswerId,
					intQuestionID = requestModel.Question.QuestionId,
					strName = a.Answer
				});
			success = _databaseController.InsertUpdate(answerTable);
			return success;
		}

		public bool DeleteQuestionAndAnswers(DeleteQuestionAndAnswersRequestModel requestModel)
		{
			var answerTable = requestModel
				.AnswerIds
				.Select(a => new Answer
				{
					intAnswerID = a
				});

			var success = _databaseController.Delete(answerTable);

			if (!success)
			{
				return false;
			}
			var questionTable = requestModel
				.QuestionIds
				.Select(a => new Question
				{
					intQuestionId = a
				});

			success = _databaseController.Delete(questionTable);

			return success;
		}

		public IEnumerable<RankingModel> GetRanking()
		{
			return _databaseController.GetRanking();
		}

		public bool DeleteHighScoreEntries(IEnumerable<int> gameIds)
		{
			var idList = gameIds.ToList();
			var game2Categories = _databaseController.GetGame2CategoriesByGame(idList);
			_databaseController.Delete(game2Categories);
			var games = _databaseController.GetGames().Where(a => idList.Any(b => b == a.intGameID));
			_databaseController.Delete(games);
			return true;
		}
	}
}
