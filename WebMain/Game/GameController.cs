using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using WebMain.Game.Helper;
using WebMain.Models.Game;

namespace WebMain.Game
{
	public class GameController : Controller
	{
		private readonly GameHelper _gameHelper;

		public GameController(GameHelper gameHelper)
		{
			_gameHelper = gameHelper;
		}

		public IActionResult Home()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ChooseCategories()
		{
			var categories = _gameHelper.GetCategories().ToList();

			var categoriesViewModel = new CategoriesViewModel()
			{
				AllCategories = categories.Select(a => new CategoryViewModel
				{
					CategoryId = a.CategoryId,
					CategoryName = a.Category,
					Chosen = false
				}).ToList()
			};

			return View(categoriesViewModel);
		}

		[HttpPost]
		public IActionResult StartGameRound(CategoriesViewModel categories)
		{
			if (categories.AllCategories.TrueForAll(a => !a.Chosen))
			{
				categories.ErrorMessage = "Wählen Sie mindestens eine Kategorie aus.";
				return View("ChooseCategories", categories);
			}

			var categoryIds = categories.AllCategories.Where(a => a.Chosen).Select(a => a.CategoryId);
			var questions = _gameHelper.GetQuestions(categoryIds);
			var questionsViewModel = questions.Select(a => new QuestionViewModel
			{
				Question = a.Question,
				Answers = _gameHelper.GetAnswers(a.QuestionId).ToList(),
				PercentageOfCorrectlyAnswered = a.AnsweredCorrectlyPercentage
			});

			var gameRound = new GameRoundViewModel
			{
				Score = 0,
				QuestionsToPlay = questionsViewModel.ToList()
			};

			_gameHelper.SetNextQuestion(gameRound);

			return View("GameRound", gameRound);
		}


		public IActionResult GameRound(GameRoundViewModel gameRoundViewModel)
		{
			if (gameRoundViewModel.QuestionsToPlay.Count > 0)
			{
				_gameHelper.SetNextQuestion(gameRoundViewModel);
			}
			/*else
			{
				RedirectToAction();
			}*/

			return View();
		}
	}
}
