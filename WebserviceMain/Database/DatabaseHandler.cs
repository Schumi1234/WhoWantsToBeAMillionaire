using System;
using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database.Helper;
using WebserviceMain.Database.Tables;
using WebserviceMain.Extensions;

namespace WebserviceMain.Database
{
	public class DatabaseHandler
	{
		private readonly DataContext _dataContext;

		public DatabaseHandler(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public Question GetRandomQuestion(int categoryId)
		{
			var questions = _dataContext.Question
				.Where(a => a.intCategoryId == categoryId)
				.ToList();
			questions.Shuffle();
			return questions.FirstOrDefault();
		}

		public IEnumerable<Answer> GetAnswers(int questionId)
		{
			return _dataContext.Answer
				.Where(a => a.intQuestionID == questionId)
				.ToList();
		}

		public IEnumerable<Category> GetCategories()
		{
			var categoriesWithQuestions = _dataContext.Question
				.Select(a => a.intCategoryId)
				.Distinct();
			// return categories with existing questions
			return _dataContext.Category
				.Where(a => categoriesWithQuestions
					.Any(b => b == a.intCategoryId))
				.ToList();
		}

		public IEnumerable<RankingModel> GetRanking()
		{
			var games = _dataContext.Game.ToList();
			var categories = _dataContext.Category.ToList();
			var games2Categories = _dataContext.Game2Category.ToList();
			var playedCategories = categories
				.Where(a => games2Categories
					.Any(b => b.intCategoryID == a.intCategoryId && games
								  .Any(x => x.intGameID == b.intGameID)));
			return games
				.Select(x => new RankingModel
				{
					GameBegin = x.datBegin,
					GameEnd = x.datEnd,
					PlayerName = x.strPlayerName,
					EvaluationPoints = x.intScore / (x.datEnd - x.datBegin).TotalSeconds,
					TimeOfGame = GetTimeDifference(x.datBegin, x.datEnd),
					Score = x.intScore,
					PlayedCategories = playedCategories
						.Select(b => new CategoryModel
						{
							Category = b.strName,
							CategoryId = b.intCategoryId
						})

				})
				.OrderBy(a => a.EvaluationPoints);
		}

		/// <summary>
		/// returns in format 00:00:00
		/// </summary>
		private static string GetTimeDifference(DateTime begin, DateTime end)
		{
			var seconds = (end - begin).TotalSeconds;
			var minutes = Math.Floor(seconds / 60);
			var hours = Math.Floor(minutes / 60);
			minutes = minutes - (minutes * 60);
			seconds = seconds - (minutes * 60);

			return $"{hours}:{minutes}:{seconds}";


		}

		public void DeleteEntries<T>(IEnumerable<T> tables)
		{
			//ToDo:Test delete
			switch (tables)
			{
				case IEnumerable<Category> categories:
					_dataContext.Category.RemoveRange(categories);
					break;
				case IEnumerable<Game> games:
					_dataContext.Game.RemoveRange(games);
					break;
				case IEnumerable<Question> questions:
					_dataContext.Question.RemoveRange(questions);
					break;
				case IEnumerable<Answer> answers:
					_dataContext.Answer.RemoveRange(answers);
					break;
				case IEnumerable<Game2Category> game2Categories:
					_dataContext.Game2Category.RemoveRange(game2Categories);
					break;
				default:
					throw new ArgumentException("Table unknown");
			}
		}

		public void InsertUpdate<T>(IEnumerable<T> tables)
		{
			switch (tables)
			{
				//ToDo: Test tables with foreign key
				case IEnumerable<Category> categories:
					_dataContext.Category.UpdateRange(categories);
					break;
				case IEnumerable<Game> games:
					_dataContext.Game.UpdateRange(games);
					break;
				case IEnumerable<Question> questions:
					_dataContext.Question.UpdateRange(questions);
					break;
				case IEnumerable<Answer> answers:
					_dataContext.Answer.UpdateRange(answers);
					break;
				case IEnumerable<Game2Category> game2Categories:
					_dataContext.Game2Category.UpdateRange(game2Categories);
					break;
				default:
					throw new ArgumentException("Table unknown");
			}
		}

		public Answer GetAnswer(int answerId)
		{
			return _dataContext.Answer.Single(a => a.intAnswerID == answerId);
		}
	}
}
