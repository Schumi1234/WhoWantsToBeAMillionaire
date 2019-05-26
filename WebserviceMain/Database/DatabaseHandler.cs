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

		public IEnumerable<Question> GetAllQuestions()
		{
			return _dataContext.Question;
		}

		public IEnumerable<Question> GetQuestionsByCategories(IEnumerable<int> categoryIds)
		{
			var questions = _dataContext.Question
				.Where(a => categoryIds.Any(b => b == a.intCategoryId))
				.ToList();
			questions.Shuffle();
			return questions;
		}

		public IEnumerable<Answer> GetAnswers(int questionId)
		{
			return _dataContext.Answer
				.Where(a => a.intQuestionID == questionId)
				.ToList();
		}

		public IEnumerable<Category> GetCategories()
		{
			return _dataContext.Category;
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
					GameId = x.intGameID,
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

		public bool DeleteEntries<T>(IEnumerable<T> tables)
		{
			using (var transaction = _dataContext.Database.BeginTransaction())
			{
				try
				{
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
						case IEnumerable<User> users:
							_dataContext.User.RemoveRange(users);
							break;
						default:
							throw new ArgumentException("Table unknown");
					}
					_dataContext.SaveChanges();
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					return false;
				}
			}

			return true;
		}
		
		public bool InsertUpdate<T>(IEnumerable<T> tables)
		{
			using (var transaction = _dataContext.Database.BeginTransaction())
			{
				try
				{
					switch (tables)
					{
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
						case IEnumerable<User> users:
							_dataContext.User.UpdateRange(users);
							break;
						default:
							throw new ArgumentException("Table unknown");
					}

					_dataContext.SaveChanges();
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					return false;
				}
			}

			return true;
		}

		public Answer GetAnswer(int answerId)
		{
			return _dataContext.Answer.Single(a => a.intAnswerID == answerId);
		}

		public bool AddUser(string username, string password)
		{
			
			var user = new User
			{
				strPassword = password,
				strUsername = username
			};
			var test = new List<User>
			{
				user
			};
			InsertUpdate(test);
			return false;
		}

		public Question GetQuestionById(int questionId)
		{
			return _dataContext.Question.Single(a => a.intQuestionId == questionId);
		}

		public IEnumerable<Answer> GetAllAnswers()
		{
			return _dataContext.Answer;
		}

		public IEnumerable<Game2Category> GetGame2Categories(IEnumerable<int> categoryIds)
		{
			return _dataContext.Game2Category.Where(a => categoryIds.Any(b => b == a.intCategoryID));
		}

		public IEnumerable<Game> GetGames()
		{
			return _dataContext.Game;
		}

		public IEnumerable<Game2Category> GetGame2CategoriesByGame(IEnumerable<int> gameIds)
		{
			return _dataContext.Game2Category.Where(a => gameIds.Any(b => b == a.intGameID));
		}

		public bool CheckLogin(string modelUsername, string password)
		{
			var user = _dataContext.User.SingleOrDefault(a => a.strUsername.Equals(modelUsername) && a.strPassword.Equals(password));
			if (user == null) return false;
			user.blnLoggedIn = true;
			user.datLastSignedIn = DateTime.Now;
			var userList = new List<User> {user};
			InsertUpdate(userList);
			return true;
		}

		public bool LoggedIn()
		{
			var user = _dataContext.User.Single(a => a.strUsername.Equals("Administrator"));
			var span = DateTime.Now.Subtract(user.datLastSignedIn);
			if ((span.TotalMinutes < 30)) return user.blnLoggedIn;
			user.blnLoggedIn = false;
			var userList = new List<User> { user };
			InsertUpdate(userList);
			return user.blnLoggedIn;
		}
	}
}
