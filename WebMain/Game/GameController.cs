using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
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
				QuestionId = a.QuestionId,
				Question = a.Question,
				Answers = _gameHelper.GetAnswers(a.QuestionId).ToList(),
				PercentageOfCorrectlyAnswered = a.AnsweredCorrectlyPercentage
			});

			var gameRound = new GameRoundViewModel
			{
				Categories = categories.AllCategories.Where(a => a.Chosen).ToList(),
				GameBegin = DateTime.Now,
				Score = 0,
				QuestionsToPlay = questionsViewModel.ToList()
			};

			_gameHelper.SetNextQuestion(gameRound);

			return View("GameRound", gameRound);
		}


		public IActionResult GameRound(GameRoundViewModel gameRoundViewModel)
		{
			ModelState.Clear();
			var index = gameRoundViewModel.IndexOfSelectedAnswer;
			if (!gameRoundViewModel.QuestionForTheRound.Answers[index].Correct)
			{
				return RedirectToAction("Home");
			}

			if (gameRoundViewModel.QuestionsToPlay.Count == 0 || !string.IsNullOrEmpty(Request.Form["End"]))
			{
				gameRoundViewModel.GameEnd = DateTime.Now;
				return View("Winner", gameRoundViewModel);
			}
			_gameHelper.SetNextQuestion(gameRoundViewModel);

			return View(gameRoundViewModel);
		}

		[HttpPost]
		public IActionResult ShowResults(GameRoundViewModel viewModel)
		{
			ModelState.Clear();
			if (!string.IsNullOrEmpty(Request.Form["Joker"]))
			{
				viewModel = FiftyFiftyJoker(viewModel);
				return View("GameRound", viewModel);
			}

			var index = viewModel.IndexOfSelectedAnswer;

			var correct = viewModel.QuestionForTheRound.Answers[index].Correct;
			_gameHelper.SaveQuestionAnswer(correct, viewModel.QuestionForTheRound.QuestionId);
			if (correct)
			{
				viewModel.Score += 30;
			}

			return View(viewModel);
		}

		public IActionResult SaveGame(GameRoundViewModel viewModel)
		{
			_gameHelper.SaveGame(viewModel);
			return RedirectToAction("Home");
		}

		public GameRoundViewModel FiftyFiftyJoker(GameRoundViewModel viewModel)
		{
			var answers = _gameHelper.Joker(viewModel.QuestionForTheRound.QuestionId);
			viewModel.JokerUsed = true;

			var newAnswers = viewModel.QuestionForTheRound.Answers.Select(a => new AnswerModel
			{
				AnswerId = a.AnswerId,
				Answer = a.Answer,
				Correct = a.Correct,
				QuestionId = a.QuestionId,
				DisableAnswer = answers.Any(b => b.AnswerId == a.AnswerId)
			});
			viewModel.QuestionForTheRound.Answers = newAnswers.ToList();

			return viewModel;
		}
	}
}
