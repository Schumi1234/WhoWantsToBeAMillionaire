using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.DataServiceProvider.Enums;
using WebMain.Models.Edit;

namespace WebMain.Edit
{
	public class EditController : Controller
	{
		private readonly WebserviceProvider _webserviceProvider;

		public EditController(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}

		private bool CheckLoggedIn()
		{
			return _webserviceProvider.GetDataFromWebService<bool>(Controllers.Login.ToString(), "CheckLoggedIn");
		}

		[HttpGet]
		public IActionResult EditCategories()
		{
			if (!CheckLoggedIn())
			{
				return RedirectToAction("Login", "Login");
			}
			var viewModel = GetAllCategories();
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult EditCategories(EditCategoriesViewModel requestModel)
		{
			if (!string.IsNullOrEmpty(Request.Form["newCategory"]))
			{
				requestModel.Categories.Add(new EditCategoryViewModel());
				return View(requestModel);
			}

			if (requestModel.Categories.Any(a => string.IsNullOrEmpty(a.Name)))
			{
				requestModel.ErrorMessage = "Alle Textboxen müssen ausgefüllt sein";
				return View(requestModel);
			}

			var success = SaveCategories(requestModel);
			var viewModel = GetAllCategories();
			if (!success)
			{
				viewModel.ErrorMessage = "Ein Fehler ist aufgetreten";
			}
			return View(viewModel);
		}

		private bool SaveCategories(EditCategoriesViewModel viewModel)
		{
			var requestModel = new SaveCategoriesRequestModel
			{
				EditedCategories = viewModel.Categories.Select(a => new EditCategoryModel
				{
					Name = a.Name,
					Delete = a.Delete,
					Id = a.Id
				})
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "SaveCategories", content);
			return success;
		}

		private EditCategoriesViewModel GetAllCategories()
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			return new EditCategoriesViewModel
			{
				Categories = categories
					.Select(a =>
						new EditCategoryViewModel
						{
							Name = a.Category,
							Id = a.CategoryId
						}).ToList()
			};
		}

		public IActionResult EditQuestions()
		{
			if (!CheckLoggedIn())
			{
				return RedirectToAction("Login", "Login");
			}

			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			var questions = _webserviceProvider.GetDataFromWebService<IEnumerable<QuestionModel>>(Controllers.Edit.ToString(), "AllQuestions");
			var answers = _webserviceProvider.GetDataFromWebService<IEnumerable<AnswerModel>>(Controllers.Edit.ToString(), "AllAnswers");
			var viewModel = new EditQuestionAndAnswersViewModel
			{
				Categories = categories.ToList(),
				QuestionsAndAnswers = questions.Select(a => new EditQuestionAndAnswerViewModel
				{
					Name = a.Question,
					SelectedCategoryId = a.CategoryId,
					Answers = answers.Where(b => b.QuestionId == a.QuestionId).ToList(),
					Id = a.QuestionId
				}).ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult AddCategory()
		{
			var viewModel = GetAllCategories();
			viewModel.Categories.Add(new EditCategoryViewModel());
			return View("EditCategories", viewModel);
		}

		[HttpPost]
		public IActionResult QuestionDetail()
		{
			var viewModel = new EditQuestionAndAnswerViewModel
			{
				Answers = new List<AnswerModel>
				{
					new AnswerModel(),
					new AnswerModel(),
					new AnswerModel(),
					new AnswerModel()
				},
				Categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories").ToList()
		};
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult QuestionDetail(int questionId)
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			var content = JsonConvert.SerializeObject(questionId);
			var question = _webserviceProvider.PostDataFromWebService<QuestionModel>(Controllers.Edit.ToString(), "QuestionById", content);
			var answers = _webserviceProvider.PostDataFromWebService<IEnumerable<AnswerModel>>(Controllers.Edit.ToString(), "AnswersOfQuestion", content);
			var viewModel = new EditQuestionAndAnswerViewModel
			{
				Answers = answers.ToList(),
				Categories = categories.ToList(),
				Name = question.Question,
				Id = questionId,
				SelectedCategoryId = question.CategoryId
			};
			return View(viewModel);
		}

		public IActionResult SaveQuestion(EditQuestionAndAnswerViewModel viewModel)
		{
			if (viewModel.Answers.Any(a => string.IsNullOrEmpty(a.Answer)) || string.IsNullOrEmpty(viewModel.Name))
			{
				viewModel.ErrorMessage = "Alle Textboxen müssen ausgefüllt werden.";
				return View("QuestionDetail", viewModel);
			}
			viewModel.Answers[viewModel.CorrectAnswerIndex].Correct = true;
			var requestModel = new SaveQuestionAndAnswersRequestModel
			{
				Answers = viewModel.Answers,
				Question = new QuestionModel
				{
					Question = viewModel.Name,
					QuestionId = viewModel.Id,
					CategoryId = viewModel.SelectedCategoryId
				}
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "SaveQuestionWithAnswer", content);
			return RedirectToAction("EditQuestions");
		}

		public IActionResult DeleteQuestionAndAnswers(EditQuestionAndAnswersViewModel viewModel)
		{
			var answerIds = new List<int>();
			foreach (var question in viewModel.QuestionsAndAnswers.Where(a => a.Delete))
			{
					answerIds.AddRange(question.Answers.Select(a => a.AnswerId));
			}
			
			var requestModel = new DeleteQuestionAndAnswersRequestModel
			{
				QuestionIds = viewModel.QuestionsAndAnswers.Where(a => a.Delete).Select(a => a.Id),
				AnswerIds = answerIds
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "DeleteQuestionWithAnswer", content);
			
			return RedirectToAction("EditQuestions");
		}

		[HttpGet]
		public IActionResult EditHighscoreList()
		{
			var ranking = _webserviceProvider.GetDataFromWebService<IEnumerable<RankingModel>>(Controllers.Edit.ToString(), "Ranking");
			var viewModel = new RankingViewModel
			{
				LoggedIn = CheckLoggedIn(),
				Rankings = ranking.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult DeleteHighScoreEntries(RankingViewModel viewModel)
		{
			var content =  JsonConvert.SerializeObject(viewModel.Rankings.Select(a => a.GameId));
			_webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "DeleteHighScore", content);
			return RedirectToAction("EditHighScoreList");
		}
	}
}
